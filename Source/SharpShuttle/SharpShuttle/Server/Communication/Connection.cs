using System;
using System.Threading;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using Shared.Logging;
using System.IO;
using Shared.Communication;

namespace Server.Communication
{
	/// <summary>
	/// Representeert een connectie met een client
	/// </summary>
	public class Connection
	{
		/// <summary>
		/// De TCP client
		/// </summary>
		private TcpClient Client;

		/// <summary>
		/// Een mutex voor het schrijven naar de socket
		/// </summary>
		private Mutex socketWriteMutex = new Mutex(false);
		/// <summary>
		/// Serializeert alle berichten
		/// </summary>
		private BinaryFormatter binaryformatter = new BinaryFormatter();
		/// <summary>
		/// Stream van de verbinding
		/// </summary>
		private Stream connectionStream;

		/// <summary>
		/// De thread die in een loop berichten van de client verwerkt
		/// </summary>
		private Thread connectionLoop;
		/// <summary>
		/// Stop met het ontvangen van berichten
		/// </summary>
		private bool stopping = false;

		/// <summary>
		/// De verbinding met de client is al verbroken
		/// </summary>
		private bool disconnected = false;

		/// <summary>
		/// De client is verwijderd
		/// </summary>
		internal bool IsRemoved = false;

		/// <summary>
		/// Start een verbinding op
		/// </summary>
		/// <param name="ClientID"> Het ID van de Client</param>
		/// <param name="Client"> De TCP Client </param>
		public Connection(long ClientID, TcpClient Client)
		{
			this.Client = Client;
			this.ClientID = ClientID;

			try
			{
				connectionStream = Client.GetStream();
			}
			catch
			{
				Disconnect();
				return;
			}

			//Start nu de connectionloop
			connectionLoop = new Thread(MessageLoop);
			connectionLoop.Name = "ClientConnectionLoop" + ClientID;
			connectionLoop.Start();
		}

		/// <summary>
		/// De client ID
		/// </summary>
		public long ClientID
		{
			private set;
			get;
		}

		/// <summary>
		/// MessageLoop is de berichtenpomp van een client connection.
		/// Deze wordt in zijn eigen thread uitgevoerd en blijft lopen totdat er gestopt moet worden
		/// </summary>
		private void MessageLoop()
		{
			//Object o is de placeholder van het bericht dat we binnenhalen
			Object o;
			bool hasError = false;

			try
			{
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
					}
					catch(Exception e)
					{
						//Als er fouten ontstaan in bijvoorbeeld de verbinding of
						//er komt een illegaal object aan
						Logger.Write("ClientMessageLoop", "Error in deserializing: " + e.Message);
						hasError = true;
					}

					//Nu kijken we moeten stoppen of fouten zijn ontstaan 
					if (hasError || stopping)
					{
						stopping = true;
						continue;
					}

					//Nu moeten we de objecten afhandelen
					Communication.AddMessage(ClientID, o);
				}

			}
			//TODO iets zinnigs in de catch clause zetten
			catch (Exception e)
			{
				Logger.Write("Communication", "Error in MessageLoop " + e.Message);
			}

			//Zorg er voor dat we alles nog wel goed afhandelen
			Disconnect();
		}

		/// <summary>
		/// Stuur een bericht naar de client
		/// </summary>
		/// <param name="message">Het te versturen bericht</param>
		public void SendMessage(Object message)
		{
			socketWriteMutex.WaitOne();

			if (Client == null || !Client.Connected)
			{
				return;
			}

			try
			{
				binaryformatter.Serialize(connectionStream, message);
				
				((MainForm)System.Windows.Forms.Application.OpenForms[0]).AddConnectionLog(message.ToString());

				connectionStream.Flush();
			}
			catch (Exception e)
			{
				Logger.Write("Communication", "Serializing error in SendMessage " + e.Message);
				return;
			}
			finally
			{
				socketWriteMutex.ReleaseMutex();
			}

			
			return;
		}

		/// <summary>
		/// Disconnect stopt de verbinding van de client
		/// </summary>
		public void Disconnect()
		{
			//Als we al door deze procedure zijn gelopen dan niet nog een keer doen
			if (disconnected) return;

			disconnected = true;

			//Verstuur voor het laatste een shutdownbericht
			SendMessage(new Message(MessageTypes.GOODBYE));

			//Zorg dat de MessageLoop gaat stoppen
			stopping = true;

			//Stop alle Netwerk Communicatie
			try
			{
				connectionStream.Close();
				connectionStream.Dispose();
			}
			catch { }
			finally
			{
				connectionStream = null;
			}
			
			//Stop de TcpClient
			try
			{
				Client.Close();
			}
			catch { }
			finally
			{
				Client = null;
			}
			

			//Als we al zijn removed hoeven we niets meer te doen en zijn we klaar.
			if (!IsRemoved)
				Communication.RemoveClient(ClientID);

			IsRemoved = true;
		}
	}
}
