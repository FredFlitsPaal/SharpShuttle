using System;
using Shared.Communication.Serials;

namespace Shared.Communication.Serials
{
	/// <summary>
	/// Flags om aan te geven welke events je wilt ontvangen
	/// </summary>
	[Flags]
	public enum FilterFlags
	{
		/// <summary>
		/// UpdateAllPoules event
		/// </summary>
		UpdateAllPoulesEvent,
		/// <summary>
		/// UpdateAllPlayers event
		/// </summary>
		UpdateAllPlayersEvent,
		/// <summary>
		/// UpdateAllMatches event
		/// </summary>
		UpdateAllMatchesEvent,
		/// <summary>
		/// UpdateAllHistoryMatches event
		/// </summary>
		UpdateAllHistoryMatchesEvent,
		/// <summary>
		/// UpdatePoule event
		/// </summary>
		UpdatePouleEvent,
		/// <summary>
		/// UpdatePoulePlanning event
		/// </summary>
		UpdatePoulePlanningEvent,
		/// <summary>
		/// UpdatePouleTeams event
		/// </summary>
		UpdatePouleTeamsEvent,
		/// <summary>
		/// UpdatePouleLadder event
		/// </summary>
		UpdatePouleLadderEvent,
		/// <summary>
		/// UpdatePouleMatches event
		/// </summary>
		UpdatePouleMatchesEvent,
		/// <summary>
		/// UpdateMatch event
		/// </summary>
		UpdateMatchEvent,
		/// <summary>
		/// UpdateSettings event
		/// </summary>
		UpdateSettings
	}

	/// <summary>
	/// Interface om te events weg te filteren
	/// </summary>
	public interface ISerialFilter
	{
		/// <summary>
		/// Selecteert welke events van belang zijn
		/// </summary>
		FilterFlags FilterFlags { get; }

		/// <summary>
		/// Filtert UpdateAllPoulesEvent
		/// </summary>
		/// <param name="SerialEvent">Type event</param>
		bool UpdateAllPoulesEvent(SerialEventTypes SerialEvent);

		/// <summary>
		/// Filtert UpdateAllPlayersEvent
		/// </summary>
		/// <param name="SerialEvent">Type event</param>
		bool UpdateAllPlayersEvent(SerialEventTypes SerialEvent);

		/// <summary>
		/// Filtert UpdateAllMatchesEvent
		/// </summary>
		/// <param name="SerialEvent">Type event</param>
		bool UpdateAllMatchesEvent(SerialEventTypes SerialEvent);

		/// <summary>
		/// Filtert UpdateAllHistoryMatchesEvent
		/// </summary>
		/// <param name="SerialEvent">Type event</param>
		bool UpdateAllHistoryMatchesEvent(SerialEventTypes SerialEvent);

		/// <summary>
		/// Filtert UpdatePouleEvent
		/// </summary>
		/// <param name="PouleID">ID van de poule</param>
		/// <param name="SerialEvent">Type event</param>
		bool UpdatePouleEvent(int PouleID, SerialEventTypes SerialEvent);

		/// <summary>
		/// Filtert UpdatePoulePlanningEvent
		/// </summary>
		/// <param name="PouleID">ID van de poule</param>
		/// <param name="SerialEvent">Type event</param>
		bool UpdatePoulePlanningEvent(int PouleID, SerialEventTypes SerialEvent);

		/// <summary>
		/// Filtert UpdatePouleTeamsEvent
		/// </summary>
		/// <param name="PouleID">ID van de poule</param>
		/// <param name="SerialEvent">Type event</param>
		bool UpdatePouleTeamsEvent(int PouleID, SerialEventTypes SerialEvent);

		/// <summary>
		/// Filtert UpdatePouleLadderEvent
		/// </summary>
		/// <param name="PouleID">ID van de poule</param>
		/// <param name="SerialEvent">Type event</param>
		bool UpdatePouleLadderEvent(int PouleID, SerialEventTypes SerialEvent);

		/// <summary>
		/// Filtert UpdatePouleMatchesEvent
		/// </summary>
		/// <param name="PouleID">ID van de poule</param>
		/// <param name="SerialEvent">Type event</param>
		bool UpdatePouleMatchesEvent(int PouleID, SerialEventTypes SerialEvent);

		/// <summary>
		/// Filtert UpdateMatchEvent
		/// </summary>
		/// <param name="MatchID">ID van de match</param>
		/// <param name="SerialEvent">Type event</param>
		bool UpdateMatchEvent(int MatchID, SerialEventTypes SerialEvent);

		/// <summary>
		/// Filtert UpdateSettings
		/// </summary>
		/// <param name="SerialEvent">Type event</param>
		bool UpdateSettings(SerialEventTypes SerialEvent);
	}
}