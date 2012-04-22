using System;
using System.Threading;
using System.Net.Sockets;
using Shared.Delegates;
using Shared.Communication;
using Shared.Logging;
using Shared.Datastructures;
using System.Collections.Generic;
using Shared.Communication.Messages.Requests;

namespace Server.Communication
{
	/// <summary>
	///	De Communication class handelt alle communicatie tussen server en client af.
	///	Hier zit ook het multithread gebeuren en alle socket afhandeling
	/// </summary>
	internal class Communication
	{
		public static event Delegates.ServerStoppedDelegate ServerStopped;
		public static event Delegates.ServerStartedDelegate ServerStarted;
		public static event Delegates.ServerConnectionDelegate ClientConnected;
		public static event Delegates.ServerConnectionDelegate ClientDisconnected;

		private static TcpListener connectionListener;

		private static ReaderWriterLock clientsListLock = new ReaderWriterLock();

		private static Dictionary<long,Connection> clients = new Dictionary<long,Connection>();
		private static MessagePool MessagePool = new MessagePool();

		private static AtomicCounter currentClientID = new AtomicCounter(0);

		private static Thread connectionloop;
		private static Thread messageloop;
		private static bool stopping = false;

		/// <summary>
		/// Maak een nieuwe communicatie klasse aan zodat we kunnen communiceren
		/// met clients. We willen echter een statische benadering van deze class
		/// dus de constructor moet private zijn.
		/// </summary>
		static Communication()
		{
			//De MessageLoop is een loop die altijd aan mag blijven.
			messageloop = new Thread(MessageLoop);
			messageloop.IsBackground = true;
			messageloop.Name = "ServerMessageLoop";
			messageloop.Start();
		}

		/// <summary>
		/// Laat de server starten met luisteren naar verbindingen
		/// </summary>
		/// <returns>True als het listenen is gelukt, in de andere gevallen false</returns>
		public static bool Start(int ServerPort)
		{
			//We moeten eerste kijken of we al een verbinding hebben met een Server
			if (!(connectionListener == null))
			{
				Stop();
			}

			//Als eerste gaan we proberen een nieuwe socket verbinding te openen naar de server
			connectionListener = new TcpListener(System.Net.IPAddress.Any, ServerPort);

			try
			{
				connectionListener.Start();
			}
			catch (Exception e)
			{
				//Als we een fout krijgen dan mogen we niet door gaan
				Logger.Write("Communication", "socket connection error " + e.Message);
				return false;
			}

			//Zorg dat threads kunnen draaien
			stopping = false;

			connectionloop = new Thread(ConnectionLoop);
			connectionloop.Name = "ServerConnectionLoop";

			try
			{
				connectionloop.Start();
			}
			catch
			{
				return false;
			}

			if (!(ServerStarted == null)) ServerStarted();


			return true;
		}

		/// <summary>
		/// Stop met het luisteren naar nieuwe verbindingen
		/// </summary>
		public static void Stop()
		{
			//als de socket al gesloten is hoeven we niets meer af te handelen
			if (connectionListener == null) return;

			//Ga nu alle clients disconnecten
			DisconnectAll();

			stopping = true;

			//Leeg de berichtenpomppoolding
			MessagePool.Clear();

			connectionListener.Stop();

			//dispose om finalization te voorkomen
			connectionListener = null;

			if (ServerStopped != null) ServerStopped();
		}

		/// <summary>
		/// Deze methode kijkt of er een nieuwe inkomende verbinding is, en accepteerd deze.
		/// vervolgens worden nieuwe verbindingen in de clients lijst toegevoegd
		/// </summary>
		private static void ConnectionLoop()
		{
			//Object o is de placeholder van het bericht dat we binnenhalen
			TcpClient client;
			bool hasError = false;

			try
			{
				//Zo lang we nog berichten moeten ophalen doen we dat
				while (!stopping)
				{
					//Vraag de volgende verbinding
					try
					{
						client = connectionListener.AcceptTcpClient();

						//Nu gaan we de client toevoegen waardoor we schrijfaccess op de lijst moeten
						//hebben
						clientsListLock.AcquireWriterLock(-1);

						long nextid = currentClientID.IncreaseAndGetValue();
						clients.Add(nextid, new Connection(nextid, client));

						//Geef toegang tot de lijst weer vrij
						clientsListLock.ReleaseWriterLock();

						if (!(ClientConnected == null)) ClientConnected(nextid);
					}
					catch (Exception e)
					{
						//Als er fouten ontstaan in bijvoorbeeld de verbinding of
						//er komt een illegaal object aan
						Logger.Write("Communication", "Deserializing error in MessageLoop " + e.Message);
						hasError = true;
					}

					//Nu kijken we of er fouten zijn ontstaan
					if (hasError)
					{
						stopping = true;
						continue;
					}

				}

			}

			catch (Exception e)
			{
				Logger.Write("Communication", "Error in MessageLoop " + e.Message);
			}
		}

		private static void MessageLoop()
		{
			QueuedMessage next;
				try
				{
					//Zo lang we nog berichten moeten ophalen doen we dat
					while (true)
					{
						next = null;

						//Volgende bericht ophalen
						try
						{
							//Dit is een blokkerende operatie:
							next = MessagePool.Dequeue();

							//Nu moeten we het bericht gaan afhandelen:
							HandleMessage(next.ClientID, next.Message);
						}
						catch (Exception e)
						{
							//Als er fouten ontstaan in bijvoorbeeld de verbinding of
							//er komt een illegaal object aan
							Logger.Write("Communication", "MessageDequeue error in MessageLoop " + e.Message);
						}
					}

				}
				catch (Exception e)
				{
					Logger.Write("Communication", "Error in MessageLoop " + e.Message);
				}
		}

		/// <summary>
		/// RemoveClient zorgt ervoor dat de client wordt verwijderd uit de lijst met verbindingen
		/// </summary>
		/// <param name="ClientID">De id van de client die moet worder verwijderd</param>
		public static void RemoveClient(long ClientID){
			//Omdat we weer de clientlist gaan veranderen moeten we schrijfaccess
			clientsListLock.AcquireWriterLock(-1);

			Connection client;
			if (clients.TryGetValue(ClientID, out client))
			{
				client.IsRemoved = true;
				client.Disconnect();
				if (!(ClientDisconnected == null)) ClientDisconnected(ClientID);
			}

			clients.Remove(ClientID);

			//Geef de lijst weer terug
			clientsListLock.ReleaseWriterLock();
		}

		/// <summary>
		/// DisconnectAll gaat elke verbinding afzonderlijk verbreken zodat er geen verbindingen meer overblijven
		/// </summary>
		public static void DisconnectAll()
		{
			//Omdat we weer de clientlist gaan veranderen moeten we schrijfaccess
			clientsListLock.AcquireWriterLock(-1);

			foreach (Connection c in clients.Values)
			{
				c.IsRemoved = true;
				c.Disconnect();
				if (!(ClientDisconnected == null)) ClientDisconnected(c.ClientID);
			}

			clients.Clear();

			//Geef de lijst weer terug
			clientsListLock.ReleaseWriterLock();
		}
		
		/// <summary>
		/// Add message stopt een bericht in de MessagePool
		/// </summary>
		/// <param name="ClientID"> Het ID van de Client</param>
		/// <param name="o">Het object dat binnengekomen is</param>
		internal static void AddMessage(long ClientID, Object o)
		{
			//Nu moeten we controleren wat voor een bericht er is binnengekomen
			if (o is Message)
			{
				Message m = o as Message;

				//Als het GoodBye is, dan moeten we de client verwijderen
				if (m.Type == MessageTypes.GOODBYE)
				{
					RemoveClient(ClientID);
					return;
				}
				
				//Anders komt het bericht in de MessagePool
				MessagePool.Enqueue(ClientID, m);
			}

			//Als het bericht dus geen Message is dan negeren wij het
		}

		/// <summary>
		/// HandleMessage handelt een bericht af van een client,
		/// omdat dit door de MessageLoop wordt aangeroepen betekend het dat 1 client,
		/// tegelijkertijd wordt behandelt.
		/// </summary>
		/// <param name="ClientID">De ID van de client</param>
		/// <param name="message">Het bericht van de client</param>
		internal static void HandleMessage(long ClientID, Message message)
		{
			//Als we momenteel toch aan het stoppen zijn mogen we het bericht niet meer afhandelen
			if (stopping) return;

			//Nu gaan we kijken wat voor een bericht er binnen is gekomen
			switch (message.Type)
			{
				case MessageTypes.placeholder:
				case MessageTypes.PING:
				case MessageTypes.PONG:
				case MessageTypes.RESPONSE:
					break;
				case MessageTypes.REQUEST:
					//We moeten een request afhandelen
					HandleRequestMessage(ClientID, message as RequestMessage);
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// RequestMessage handelt alle RequestMessages af
		/// </summary>
		/// <param name="ClientID">De client id waar het bericht vandaan kwam</param>
		/// <param name="request">Het binnengekomen RequestMessage</param>
		private static void HandleRequestMessage(long ClientID, RequestMessage request)
		{
			//Nu moeten we gaan kijken wat voor een soort Request het is:
			switch (request.RequestType)
			{
				case RequestTypes.GetAllUsers:
					RequestHandler.GetAllUsers(ClientID, request as RequestGetAllUsers);
					break;
				case RequestTypes.GetAllPoules:
					RequestHandler.GetAllPoules(ClientID, request as RequestGetAllPoules);
					break;
				case RequestTypes.SetAllPoules:
					RequestHandler.SetAllPoules(ClientID, request as RequestSetAllPoules);
					break;
				case RequestTypes.GetAllPlayers:
					RequestHandler.GetAllPlayers(ClientID, request as RequestGetAllPlayers);
					break;
				case RequestTypes.SetAllPlayers:
					RequestHandler.SetAllPlayers(ClientID, request as RequestSetAllPlayers);
					break;
				case RequestTypes.GetPoule:
					RequestHandler.GetPoule(ClientID, request as RequestGetPoule);
					break;
				case RequestTypes.SetPoule:
					RequestHandler.SetPoule(ClientID, request as RequestSetPoule);
					break;
				case RequestTypes.GetPoulePlanning:
					RequestHandler.GetPoulePlanning(ClientID, request as RequestGetPoulePlanning);
					break;
				case RequestTypes.SetPoulePlanning:
					RequestHandler.SetPoulePlanning(ClientID, request as RequestSetPoulePlanning);
					break;
				case RequestTypes.GetPouleTeams:
					RequestHandler.GetPouleTeams(ClientID, request as RequestGetPouleTeams);
					break;
				case RequestTypes.SetPouleTeams:
					RequestHandler.SetPouleTeams(ClientID, request as RequestSetPouleTeams);
					break;
				case RequestTypes.GetPouleLadder:
					RequestHandler.GetPouleLadder(ClientID, request as RequestGetPouleLadder);
					break;
				case RequestTypes.GetPouleMatches:
					RequestHandler.GetPouleMatches(ClientID, request as RequestGetPouleMatches);
					break;
				case RequestTypes.SetPouleNextRound:
					RequestHandler.SetPouleNextRound(ClientID, request as RequestSetPouleNextRound);
					break;
				case RequestTypes.GetAllMatches:
					RequestHandler.GetAllMatches(ClientID, request as RequestGetAllMatches);
					break;
				case RequestTypes.GetAllHistoryMatches:
					RequestHandler.GetAllHistoryMatches(ClientID, request as RequestGetAllHistoryMatches);
					break;
				case RequestTypes.GetPouleHistoryMatches:
					RequestHandler.GetPouleHistoryMatches(ClientID, request as RequestGetPouleHistoryMatches);
					break;
				case RequestTypes.GetMatch:
					RequestHandler.GetMatch(ClientID, request as RequestGetMatch);
					break;
				case RequestTypes.SetMatch:
					RequestHandler.SetMatch(ClientID, request as RequestSetMatch);
					break;
				case RequestTypes.PouleFinishRound:
					RequestHandler.PouleFinishRound(ClientID, request as RequestPouleFinishRound);
					break;
				case RequestTypes.PouleRemoveRound:
					RequestHandler.PouleRemoveRound(ClientID, request as RequestPouleRemoveRound);
					break;
				case RequestTypes.GetSettings:
					RequestHandler.GetSettings(ClientID, request as RequestGetSettings);
					break;
				case RequestTypes.SetSettings:
					RequestHandler.SetSettings(ClientID, request as RequestSetSettings);
					break;
				default:
					throw new NotImplementedException("Unknown message type");
			}
		}

		/// <summary>
		/// Stuur een bericht naar alle verbindingen
		/// </summary>
		/// <param name="message">het bericht dat moet worden verstuurd</param>
		public static void SendAll(Object message)
		{
			//We gaan in de berichtenlijst kijken wie er allemaal verbinding heeft, dus een readlock
			clientsListLock.AcquireReaderLock(-1);

			foreach (Connection c in clients.Values)
			{
				//Stuur het bericht aan de klant
				c.SendMessage(message);
			}

			//We zijn weer klaar
			clientsListLock.ReleaseReaderLock();
		}

		/// <summary>
		/// Stuur een bericht aan alle verbonden connecties met uit zondering van 1.
		/// </summary>
		/// <param name="message">Het te versturen bericht</param>
		/// <param name="IgnoreClientID">De client die het bericht niet mag krijgen</param>
		public static void SendAllExcept(long IgnoreClientID, Object message)
		{
			//We gaan in de berichtenlijst kijken wie er allemaal verbinding heeft, dus een readlock
			clientsListLock.AcquireReaderLock(-1);

			foreach (Connection c in clients.Values)
			{
				//Als het IgnoreCLientID is dan niet versturen
				if (c.ClientID == IgnoreClientID)
					continue;

				//Stuur het bericht aan de klant
				c.SendMessage(message);
			}

			//We zijn weer klaar
			clientsListLock.ReleaseReaderLock();
		}

		public static void Send(long ClientID, Object message)
		{
			//We gaan in de berichtenlijst kijken wie er allemaal verbinding heeft, dus een readlock
			clientsListLock.AcquireReaderLock(-1);

			Connection conn = null;

			clients.TryGetValue(ClientID, out conn);

			//We zijn weer klaar
			clientsListLock.ReleaseReaderLock();

			if (conn != null) conn.SendMessage(message);
		}
	}
}
