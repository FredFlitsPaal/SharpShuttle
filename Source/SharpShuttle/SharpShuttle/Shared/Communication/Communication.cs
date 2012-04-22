using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Shared.Communication.Messages;
using Shared.Communication.Serials;
using Shared.Logging;


namespace Shared.Communication
{
	/// <summary>
	///	De Communication class handelt alle communicatie tussen server en client af.
	///	Hier zit ook het multithread gebeuren en alle socket afhandeling
	/// </summary>
	public class Communication
	{
		#region Fields

		/// <summary>
		/// De singelton instance van deze class
		/// </summary>
		private static Communication instance = new Communication();
		/// <summary>
		/// De verbinding met de server
		/// </summary>
		private TcpClient connectionSocket;
		/// <summary>
		/// Mutex om het schrijven naar de TcpClient te synchronizeren
		/// </summary>
		private Mutex socketWriteMutex = new Mutex(false);
		/// <summary>
		/// ManualResetEvent om het disconnecten te synchronizeren
		/// </summary>
		private ManualResetEvent disconnectEvent = new ManualResetEvent(true);
		/// <summary>
		/// BinaryFormatter voor serializatie
		/// </summary>
		private BinaryFormatter binaryformatter = new BinaryFormatter();
		/// <summary>
		/// De donnectionStream
		/// </summary>
		private Stream connectionStream;

		/// <summary>
		/// De requestpool
		/// </summary>
		private RequestPool requestpool = new RequestPool();

		/// <summary>
		/// De thread waarin de message loop draait
		/// </summary>
		private Thread messageloop;
		/// <summary>
		/// Geeft aan of de messageloop moet stoppen
		/// </summary>
		private bool stopping = false;
		/// <summary>
		/// Geeft aan of we disconnected zijn
		/// </summary>
		private bool disconnected = true;

		/// <summary>
		/// Een mutex voor de verbinding
		/// </summary>
		private Mutex connectMutex = new Mutex();

		#endregion

		#region Properties

		/// <summary>
		/// De Singelton instance van deze class
		/// </summary>
		public static Communication Instance { get { return instance; } }

		/// <summary>
		/// Geeft aan of de connectie in stand is
		/// </summary>
		public bool Connected
		{
			get
			{
				return connectionSocket != null && connectionSocket.Connected;
			}
		}

		#endregion

		#region Events

		/// <summary>
		/// Wordt geinvoked als de connectie verbroken wordt <para/>
		/// PAS OP!! wordt op een ander thread uitgevoerd
		/// </summary>
		public static event Delegates.Delegates.ServerDisconnectedDelegate ServerDisconnected;
		/// <summary>
		/// Wordt geinvoked als een connectie gemaakt wordt
		/// </summary>
		public static event Delegates.Delegates.ServerConnectedDelegate ServerConnected;

		#endregion

		#region Constructors

		// DO NOT REMOVE!!
		// google BeforeFieldInit+singleton
		/// <summary>
		/// Maak een nieuwe communicatie klasse aan zodat we kunnen communiceren
		/// met de server. We willen echter een statische benadering van deze class
		/// dus de constructor moet private zijn.
		/// </summary>
		static Communication() { }

		#endregion

		#region Methods

		/// <summary>
		/// Probeer verbinding te maken met een server
		/// </summary>
		/// <param name="ServerAddress">Het Adres van de server waarnaar we willen connecten</param>
		/// <returns>True als het connecten is gelukt, in de andere gevallen false</returns>
		public bool Connect(string ServerAddress)
		{
			//We willen maximaal 1 thread die het connecten afhandelt
			connectMutex.WaitOne();

			//We moeten eerste kijken of we al een verbinding hebben met een Server
			if (Connected)
			{
				Disconnect();
			}

			//Zorg ervoor dat de Cache van serial weer geleegd wordt.
			//hierdoor wordt effectief de oude data in de datacache niet meet gebruikt.
			SerialCache.Instance.Clear();

			//Als eerste gaan we proberen een nieuwe socket verbinding te openen naar de server
			connectionSocket = new TcpClient();
			try
			{
				connectionSocket.Connect(ServerAddress, 7015);
			}
			catch (Exception e)
			{
				//Als we een fout krijgen dan mogen we niet door gaan
				Logger.Write("Communication", "socket connection error " + e.Message);

				//Geef toegang tot Connect weer vrij
				connectMutex.ReleaseMutex();
				return false;
			}

			disconnected = false;
			stopping = false;

			connectionStream = connectionSocket.GetStream();

			//Deactiveer de disconnect event
			disconnectEvent.Reset();

			try
			{
				messageloop = new Thread(MessageLoop);
				messageloop.Name = "ClientMessageLoop";
				messageloop.Start();
			}
			catch (Exception e)
			{
				//Als we een fout krijgen dan mogen we niet door gaan
				Logger.Write("Communication", "socket connection error " + e.Message);

				//Geef toegang tot Connect weer vrij
				connectMutex.ReleaseMutex();
				return false;
			}

			//Als het gelukt is moeten we de event afvuren
			if (ServerConnected != null) ServerConnected();

			//Geef toegang tot Connect weer vrij
			connectMutex.ReleaseMutex();
			return true;
		}

		/// <summary>
		/// Verbreek de eventuele verbinding tussen de server en client
		/// </summary>
		public void Disconnect()
		{
			if (disconnected) return;

			//stuur nu een shutdown bericht
			// of niet, want dat kan niet als de server dood is gegaan
			//SendMessage(new Message(MessageTypes.GOODBYE));

			//Zorg dat we gaan disconnecten
			stopping = true;

			////als de socket al gesloten is hoeven we niets meer af te handelen
			//if (connectionSocket == null || connectionSocket.Connected == false) return;

			//sluit alle verbindingen
			try
			{
				connectionStream.Close();
			}
			catch (Exception e)
			{
				Logger.Write("Communication.Disconnect", e.ToString());
			}
			try
			{
				connectionSocket.Close();
			}
			catch (Exception e)
			{
				Logger.Write("Communication.Disconnect", e.ToString());
			}

			//dispose om finalization te voorkomen
			try
			{
				connectionStream.Dispose();
			}
			catch (Exception e)
			{
				Logger.Write("Communication.Disconnect", e.ToString());
			}

			connectionSocket = null;
			connectionStream = null;

			//We willen wel echt wachten tot alles daadwerkelijk is gedisconnect
			// nee willen we niet, want dan kun je niet uitloggen als de server dood gaat
			//disconnectEvent.WaitOne();

			disconnected = true;

			//Nu zijn we echt disconnected, dus de event raisen
			if (ServerDisconnected != null) ServerDisconnected();
		}

		/// <summary>
		/// Deze methode is het hart van de Communication object, deze methode die in een aparte thread draait,
		/// haalt alle berichten van de Socket op.
		/// </summary>
		private void MessageLoop()
		{

			bool hasError = false;

			try
			{
				//Object o is de placeholder van het bericht dat we binnenhalen
				Object o;
				//Zo lang we nog berichten moeten ophalen doen we dat
				while (!stopping)
				{
					//Reset het bericht
					o = null;

					//Probeer nu een object van de stream te halen
					try
					{
						//Deserialiseer een object van de stream
						o = binaryformatter.Deserialize(connectionStream);
						Console.WriteLine(o);
					}
					catch (Exception e)
					{
						//Als er fouten ontstaan in bijvoorbeeld de verbinding of
						//er komt een illegaal object aan
						Logger.Write("Communication", "Deserializing error in MessageLoop " + e.Message);
						hasError = true;
					}

					//Nu kijken we of er fouten zijn ontstaan
					if (stopping || hasError)
					{
						stopping = true;
						continue;
					}

					//Nu moeten we de objecten afhandelen
					handleMessage(o);
				}

			}
			//TODO iets zinnigs in de catch clause zetten
			catch (Exception e)
			{
			    Logger.Write("Communication", "Error in MessageLoop " + e.Message);
			}

			//We zijn klaar met de thread, zet de disconnected Event op signaled
			//disconnectEvent.Set();

			//Zorg ervoor dat we altijd de verbinding verbreken
			Disconnect();
		}

		/// <summary>
		/// De handleMessage methode handelt alle berichten af die van de server zijn binnengekomen 
		/// </summary>
		/// <param name="o">Het object dat binnengekomen is</param>
		private static void handleMessage(Object o)
		{
			//Eerst kijken of het een message is
			if (!(o is Message))
				return;

			Message message = (Message)o;

			//nu moeten we kijken welke type bericht het is.
			switch (message.Type)
			{
				case MessageTypes.GOODBYE:
					//De server stopt er mee, dus disconnecten
					instance.Disconnect();
					break;

				case MessageTypes.RESPONSE:
					//Het is een response, dus bericht aan de RequestPool doorgeven
					instance.requestpool.HandleRequest((ResponseMessage)o);
					break;

				case MessageTypes.SERIALUPDATE:
					//Als het een serialupdate is moeten we de SerialTracker waarschuwen
					SerialTracker.Instance.HandleSerialUpdateMessage((SerialUpdateMessage)o);
					break;
			}
		}

		/// <summary>
		/// Stuur een message
		/// </summary>
		/// <param name="message">De message om te sturen</param>
		/// <returns>True als het gelukt is</returns>
		public bool SendMessage(Message message)
		{
			socketWriteMutex.WaitOne();

			if (connectionSocket == null || !connectionSocket.Connected)
			{
				socketWriteMutex.ReleaseMutex();
				return false;
			}

			try
			{
				binaryformatter.Serialize(connectionStream, message);
				Console.WriteLine(message);
				connectionStream.Flush();
			}
			catch (Exception e)
			{
				socketWriteMutex.ReleaseMutex();
				Logger.Write("Communication", "Serializing error in SendMessage " + e.Message);
				return false;
			}

			socketWriteMutex.ReleaseMutex();
			return true;
		}

		/// <summary>
		/// Stuur een RequestMessage
		/// </summary>
		/// <param name="Message">De Request om te sturen</param>
		/// <returns>Een RequestContainer voor de gestuurde message</returns>
		public RequestContainer SendRequest(RequestMessage Message)
		{
			//Voeg de request toe aan de poule
			RequestContainer request = requestpool.AddRequest(Message);

			//Verstuur het bericht
			SendMessage(request.request);

			//Geef de container terug
			return request;
		}

		/// <summary>
		/// Verwijder een Request
		/// </summary>
		/// <param name="MessageID">ID van de Request om te verwijderen</param>
		public void RemoveRequest(long MessageID)
		{
			instance.requestpool.RemoveRequest(MessageID);
		}

		#endregion
	}
}