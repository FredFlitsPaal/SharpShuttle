using System;
using System.Collections.Generic;
using Shared.Communication.Messages;

namespace Shared.Communication.Serials
{
	/// <summary>
	/// Type van een update
	/// </summary>
	[Flags]
	public enum SerialEventTypes
	{
		/// <summary>
		/// Niets gebeurd
		/// </summary>
		None = 0,
		/// <summary>
		/// Geupdate
		/// </summary>
		Updated = 1,
		/// <summary>
		/// Veranderd
		/// </summary>
		Changed = 2,
		/// <summary>
		/// Verwijderd
		/// </summary>
		Removed = 4,
		/// <summary>
		/// Toegevoegd
		/// </summary>
		Added = 8,
		/// <summary>
		/// Allemaal (behalve None)
		/// </summary>
		All = int.MaxValue
	}

	/// <summary>
	/// De Serial Tracker, invoked de juiste events als ze nodig zijn, Singleton<para/>
	/// !!DEADLOCK WARNING!! deze class lockt op this
	/// </summary>
	public class SerialTracker
	{
		/// <summary>
		/// De singleton instance van deze class
		/// </summary>
		private readonly static SerialTracker singleton = new SerialTracker();

		// DO NOT REMOVE!!
		// google BeforeFieldInit+singleton
		static SerialTracker() { }

		/// <summary>
		/// Signature voor Serial Update event handlers zonder extra informatie
		/// </summary>
		/// <param name="SerialEvent">Type update</param>
		public delegate void AllUpdatedDelegate(SerialEventTypes SerialEvent);
		/// <summary>
		/// Signature voor Serial Update event handlers met PouleID
		/// </summary>
		/// <param name="PouleID">ID van de poule</param>
		/// <param name="SerialEvent">Type update</param>
		public delegate void PouleUpdatedDelegate(int PouleID, SerialEventTypes SerialEvent);
		/// <summary>
		/// Signature voor Serial Update event handlers met MatchID
		/// </summary>
		/// <param name="MatchID">ID van de match</param>
		/// <param name="SerialEvent">Type update</param>
		public delegate void MatchUpdatedDelegate(int MatchID, SerialEventTypes SerialEvent);

		/// <summary>
		/// Private event voor AllPoulesUpdated
		/// </summary>
		private event AllUpdatedDelegate _allPoulesUpdated;
		/// <summary>
		/// Private event voor AllUsersUpdated
		/// </summary>
		private event AllUpdatedDelegate _allUsersUpdated;

		/// <summary>
		/// Event die geinvoked wordt als er iets verandert aan de lijst met poules
		/// </summary>
		public event AllUpdatedDelegate AllPoulesUpdated
		{
			add
			{
				lock (this)
					_allPoulesUpdated += value;
			}
			remove
			{
				lock (this)
					_allPoulesUpdated -= value;
			}
		}

		/// <summary>
		/// Event die geinvoked wordt als er iets verandert aan de lijst met users
		/// </summary>
		public event AllUpdatedDelegate AllUsersUpdated
		{
			add
			{
				lock (this)
					_allUsersUpdated += value;
			}
			remove
			{
				lock (this)
					_allUsersUpdated -= value;
			}
		}

		/// <summary>
		/// Private event voor het updaten van de settings
		/// </summary>
		private event AllUpdatedDelegate _settingsUpdated;
		/// <summary>
		/// Private event voor het updaten van alle spelers
		/// </summary>
		private event AllUpdatedDelegate _allPlayersUpdated;
		/// <summary>
		/// private event voor het updaten van alle matches
		/// </summary>
		private event AllUpdatedDelegate _allMatchesUpdated;
		/// <summary>
		/// Private event voor het updaten van alle history matches
		/// </summary>
		private event AllUpdatedDelegate _allHistoryMatchesUpdated;

		/// <summary>
		/// De event die geinvoked wordt als de settings worden veranderd
		/// </summary>
		public event AllUpdatedDelegate SettingsUpdated
		{
			add
			{
				lock (this)
					_settingsUpdated += value;
			}
			remove
			{
				lock (this)
					_settingsUpdated -= value;
			}
		}


		/// <summary>
		/// De event die geinvoked wordt als AllPlayers veranderd
		/// </summary>
		public event AllUpdatedDelegate AllPlayersUpdated
		{
			add
			{
				lock (this)
					_allPlayersUpdated += value;
			}
			remove
			{
				lock (this)
					_allPlayersUpdated -= value;
			}
		}

		/// <summary>
		/// De event die geinvoked wordt als AllMatches veranderd
		/// </summary>
		public event AllUpdatedDelegate AllMatchesUpdated
		{
			add
			{
				lock (this)
					_allMatchesUpdated += value;
			}
			remove
			{
				lock (this)
					_allMatchesUpdated -= value;
			}
		}

		/// <summary>
		/// De event die geinvoked wordt als AllHistoryMatches veranderd
		/// </summary>
		public event AllUpdatedDelegate AllHistoryMatchesUpdated
		{
			add
			{
				lock (this)
					_allHistoryMatchesUpdated += value;
			}
			remove
			{
				lock (this)
					_allHistoryMatchesUpdated -= value;
			}
		}


		/// <summary>
		/// Private event voor PouleUpdated
		/// </summary>
		private event PouleUpdatedDelegate _pouleUpdated;
		/// <summary>
		/// Private event voor PoulePlanningUpdated
		/// </summary>
		private event PouleUpdatedDelegate _poulePlanningUpdated;
		/// <summary>
		/// Private event voor PouleTeamsUpdated
		/// </summary>
		private event PouleUpdatedDelegate _pouleTeamsUpdated;
		/// <summary>
		/// Private event voor PouleLadderUpdated
		/// </summary>
		private event PouleUpdatedDelegate _pouleLadderUpdated;
		/// <summary>
		/// Private event voor PouleMatchesUpdated
		/// </summary>
		private event PouleUpdatedDelegate _pouleMatchesUpdated;

		/// <summary>
		/// De event die geinvoked wordt als een poule veranderd
		/// </summary>
		public event PouleUpdatedDelegate PouleUpdated
		{
			add
			{
				lock (this)
					_pouleUpdated += value;
			}
			remove
			{
				lock (this)
					_pouleUpdated -= value;
			}
		}

		/// <summary>
		///	De event die geinvoked wordt als een pouleplanning veranderd
		/// </summary>
		public event PouleUpdatedDelegate PoulePlanningUpdated
		{
			add
			{
				lock (this)
					_poulePlanningUpdated += value;
			}
			remove
			{
				lock (this)
					_poulePlanningUpdated -= value;
			}
		}

		/// <summary>
		/// De event die geinvoked wordt als pouleteams veranderd
		/// </summary>
		public event PouleUpdatedDelegate PouleTeamsUpdated
		{
			add
			{
				lock (this)
					_pouleTeamsUpdated += value;
			}
			remove
			{
				lock (this)
					_pouleTeamsUpdated -= value;
			}
		}

		/// <summary>
		/// De event die geinvoked wordt als poule ladder veranderd
		/// </summary>
		public event PouleUpdatedDelegate PouleLadderUpdated
		{
			add
			{
				lock (this)
					_pouleLadderUpdated += value;
			}
			remove
			{
				lock (this)
					_pouleLadderUpdated -= value;
			}
		}

		/// <summary>
		/// De event die geinvoked wordt als poulematches veranderd
		/// </summary>
		public event PouleUpdatedDelegate PouleMatchesUpdated
		{
			add
			{
				lock (this)
					_pouleMatchesUpdated += value;
			}
			remove
			{
				lock (this)
					_pouleMatchesUpdated -= value;
			}
		}

		/// <summary>
		/// Private event voor MatchUpdated
		/// </summary>
		private event MatchUpdatedDelegate _matchUpdated;
		/// <summary>
		/// De event die geinvoked wordt als een match veranderd
		/// </summary>
		public event MatchUpdatedDelegate MatchUpdated
		{
			add
			{
				lock (this)
					_matchUpdated += value;
			}
			remove
			{
				lock (this)
					_matchUpdated -= value;
			}
		}

		/// <summary>
		/// De Singleton instance van deze class
		/// </summary>
		public static SerialTracker Instance
		{
			get
			{
				return singleton;
			}
		}

		/// <summary>
		/// Invoke UpdateAllUsersEvent
		/// </summary>
		public void UpdateAllUsersEvent(SerialEventTypes SerialEvent)
		{
			AllUpdatedDelegate handler = _allUsersUpdated;
			if (handler != null)
				foreach (AllUpdatedDelegate d in handler.GetInvocationList())
					d.BeginInvoke(SerialEvent, null, null);
		}

		/// <summary>
		/// Invoke UpdateAllPoulesEvent
		/// </summary>
		public void UpdateAllPoulesEvent(SerialEventTypes SerialEvent)
		{
			AllUpdatedDelegate handler = _allPoulesUpdated;
			if (handler != null)
				foreach (AllUpdatedDelegate d in handler.GetInvocationList())
					d.BeginInvoke(SerialEvent, null, null);
		}

		/// <summary>
		/// Invoke UpdateAllPlayersEvent
		/// </summary>
		public void UpdateAllPlayersEvent(SerialEventTypes SerialEvent)
		{
			AllUpdatedDelegate handler = _allPlayersUpdated;
			if (handler != null)
				foreach (AllUpdatedDelegate d in handler.GetInvocationList())
					d.BeginInvoke(SerialEvent, null, null);
		}

		/// <summary>
		/// Invoke UpdateAllMatchesEvent
		/// </summary>
		public void UpdateAllMatchesEvent(SerialEventTypes SerialEvent)
		{
			AllUpdatedDelegate handler = _allMatchesUpdated;
			if (handler != null)
				foreach (AllUpdatedDelegate d in handler.GetInvocationList())
					d.BeginInvoke(SerialEvent, null, null);
		}

		/// <summary>
		/// Invoke UpdateAllHistoryMatchesEvent
		/// </summary>
		public void UpdateAllHistoryMatchesEvent(SerialEventTypes SerialEvent)
		{
			AllUpdatedDelegate handler = _allHistoryMatchesUpdated;
			if (handler != null)
				foreach(AllUpdatedDelegate d in handler.GetInvocationList())
					d.BeginInvoke(SerialEvent, null, null);
		}

		/// <summary>
		/// Invoke UpdatePouleEvent
		/// </summary>
		public void UpdatePouleEvent(int PouleID, SerialEventTypes SerialEvent)
		{
			PouleUpdatedDelegate handler = _pouleUpdated;
			if (handler != null)
				foreach (PouleUpdatedDelegate d in handler.GetInvocationList())
					d.BeginInvoke(PouleID, SerialEvent, null, null);
		}

		/// <summary>
		/// Invoke UpdatePoulePlanningEvent
		/// </summary>
		public void UpdatePoulePlanningEvent(int PouleID, SerialEventTypes SerialEvent)
		{
			PouleUpdatedDelegate handler = _poulePlanningUpdated;
			if (handler != null)
				foreach (PouleUpdatedDelegate d in handler.GetInvocationList())
					d.BeginInvoke(PouleID, SerialEvent, null, null);
		}

		/// <summary>
		/// Invoke UpdatePouleTeamsEvent
		/// </summary>
		public void UpdatePouleTeamsEvent(int PouleID, SerialEventTypes SerialEvent)
		{
			PouleUpdatedDelegate handler = _pouleTeamsUpdated;
			if (handler != null)
				foreach (PouleUpdatedDelegate d in handler.GetInvocationList())
					d.BeginInvoke(PouleID, SerialEvent, null, null);
		}

		/// <summary>
		/// Invoke UpdatePouleLadderEvent
		/// </summary>
		public void UpdatePouleLadderEvent(int PouleID, SerialEventTypes SerialEvent)
		{
			PouleUpdatedDelegate handler = _pouleLadderUpdated;
			if (handler != null)
				foreach(PouleUpdatedDelegate d in handler.GetInvocationList())
					d.BeginInvoke(PouleID, SerialEvent, null, null);
		}

		/// <summary>
		/// Invoke UpdatePouleMatchesEvent
		/// </summary>
		public void UpdatePouleMatchesEvent(int PouleID, SerialEventTypes SerialEvent)
		{
			PouleUpdatedDelegate handler = _pouleMatchesUpdated;
			if (handler != null)
				foreach(PouleUpdatedDelegate d in handler.GetInvocationList())
					d.BeginInvoke(PouleID, SerialEvent, null, null);
		}

		/// <summary>
		/// Invoke UpdateMatchEvent
		/// </summary>
		public void UpdateMatchEvent(int MatchID, SerialEventTypes SerialEvent)
		{
			MatchUpdatedDelegate handler = _matchUpdated;
			if (handler != null)
				foreach (MatchUpdatedDelegate d in handler.GetInvocationList())
				{
					d.BeginInvoke(MatchID, SerialEvent, null, null);
				}
		}

		/// <summary>
		/// Invoke UpdateSettings
		/// </summary>
		public void UpdateSettingsEvent(SerialEventTypes SerialEvent)
		{
			AllUpdatedDelegate handler = _settingsUpdated;
			if (handler != null)
				foreach (AllUpdatedDelegate d in handler.GetInvocationList())
				{
					d.BeginInvoke(SerialEvent, null, null);
				}
		}

		/// <summary>
		/// Invoke de juiste event voor de SerialUpdateMessage
		/// </summary>
		public void HandleSerialUpdateMessage(SerialUpdateMessage SerialUpdateMessage)
		{
			HandleSerialUpdateMessage(SerialUpdateMessage.Serials);
		}

		/// <summary>
		/// Invoke de juiste events voor alle SerialUpdates in de lijst
		/// </summary>
		public void HandleSerialUpdateMessage(IEnumerable<SerialUpdate> SerialUpdates)
		{
			//We gaan nu alle serials in de SerialUpdate bekijken en checken of onze data up to date is
			foreach (SerialUpdate serialupdate in SerialUpdates)
			{
				SerialDefinition serial = serialupdate.Serial;

				Console.WriteLine(serial.Type);

				//Als de SerialUpdatehandleing niet goed gaat betekend het dat de data out of date is
				if (!SerialCache.Instance.HandleSerialUpdate(serial))
				{
					Console.WriteLine("out of date");
					//Nu moeten we gaan kijken welke soort serial het is en dan 
					switch (serial.Type)
					{
						case SerialTypes.Settings:
							UpdateSettingsEvent(serialupdate.SerialEventType);
							break;
						case SerialTypes.AllUsers:
							UpdateAllUsersEvent(serialupdate.SerialEventType);
							break;
						case SerialTypes.AllPoules:
							UpdateAllPoulesEvent(serialupdate.SerialEventType);
							break;
						case SerialTypes.AllPlayers:
							UpdateAllPlayersEvent(serialupdate.SerialEventType);
							break;
						case SerialTypes.Poule:
							UpdatePouleEvent(serial.Category, serialupdate.SerialEventType);
							break;
						case SerialTypes.PoulePlanning:
							UpdatePoulePlanningEvent(serial.Category, serialupdate.SerialEventType);
							break;
						case SerialTypes.PouleTeams:
							UpdatePouleTeamsEvent(serial.Category, serialupdate.SerialEventType);
							break;
						case SerialTypes.PouleLadder:
							UpdatePouleLadderEvent(serial.Category, serialupdate.SerialEventType);
							break;
						case SerialTypes.PouleMatches:
							UpdatePouleMatchesEvent(serial.Category, serialupdate.SerialEventType);
							break;
						case SerialTypes.Match:
							UpdateMatchEvent(serial.Category, serialupdate.SerialEventType);
							break;
						case SerialTypes.AllMatches:
							UpdateAllMatchesEvent(serialupdate.SerialEventType);
							break;
						case SerialTypes.AllHistoryMatches:
							UpdateAllHistoryMatchesEvent(serialupdate.SerialEventType);
							break;
						default:
							break;
					}
				}
			}
		}

	}
}