using System.Windows.Forms;
using Client.Forms.ClientWizard.TournamentInitializationWizard;
using Client.Forms.PopUpWindows;
using Shared.Communication.Exceptions;
using Shared.Communication.Serials;
using Shared.Data;
using Shared.Views;
using SerialEventTypes=Shared.Communication.Serials.SerialEventTypes;

namespace Client.Controls
{

	/// <summary>
	/// Business logica voor het beheren van spelers
	/// </summary>
	public class PlayersControl : ISerialFilter
	{
		/// <summary>
		/// De datacache
		/// </summary>
		private IDataCache cache;

		/// <summary>
		/// Een lijst van alle spelers
		/// </summary>
		private PlayerViews allPlayers;

		/// <summary>
		/// Een lijst van alle poules
		/// </summary>
		private PouleViews allPoules;

		/// <summary>
		/// Event voor wanneer er gerefreshd wordt
		/// </summary>
		public event PlayersStep.ReloadEvent ReloadEvent;

		#region Constructor & Initialisatie

		/// <summary>
		/// Default constructor, haalt de datacache op
		/// </summary>
		public PlayersControl ()
		{
			cache = DataCache.Instance;
		}

		/// <summary>
		/// Het ophalen van de spelers uit de database.
		/// </summary>
		public void Initialize()
		{
			try
			{
				allPlayers = new PlayerViews(cache.GetAllPlayers());
				allPoules = new PouleViews(cache.GetAllPoules());
			}

			catch (CommunicationException exc)
			{
				CatchCommunicationExceptions.Show(exc);
			}
		}

		#endregion

		#region Ophalen en wegschrijven van Data

		/// <summary>
		/// Het ophalen van de spelerslijst.
		/// </summary>
		/// <returns>PlayerViews</returns>
		public PlayerViews GetAllPlayers()
		{
			if (allPlayers == null)
				Initialize();

			return allPlayers;
		}

		/// <summary>
		/// Het ophalen van de pouleslijst.
		/// </summary>
		/// <returns>PouleViews</returns>
		public PouleViews GetAllPoules()
		{
			if (allPoules == null)
				Initialize();

			return allPoules;
		}

		/// <summary>
		/// Het wegschrijven van alle spelers.
		/// </summary>
		public void SaveAllPlayers()
		{
			CommunicationException exc;

			if (cache.SetAllPlayers(allPlayers.GetChangeTrackingList(), out exc))
			{
				allPlayers = new PlayerViews(cache.GetAllPlayers());
			}

			else if (exc is DataReferenceException)
			{
				MessageBox.Show(exc.Message, "Er zijn nog referenties naar de speler", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			else
			{
				if (CatchCommunicationExceptions.Show(exc) == CatchCommunicationExceptions.ActionResult.Reload)
					ReloadEvent(true);
			}
		}

		/// <summary>
		/// Het wegschrijven van alle poules.
		/// </summary>
		public void SavePoules()
		{
			CommunicationException exc;

			if (cache.SetAllPoules(allPoules.GetChangeTrackingList(), out exc))
			{
				allPoules = new PouleViews(cache.GetAllPoules());
			}

			else
			{
				if (CatchCommunicationExceptions.Show(exc) == CatchCommunicationExceptions.ActionResult.Reload)
					ReloadEvent(true);
			}
		}

		/// <summary>
		/// Geeft aan of de spelerslijst is veranderd.
		/// </summary>
		/// <returns>Bool</returns>
		public bool IsChanged()
		{
			if (allPlayers != null)
				if (allPlayers.GetChangeTrackingList().Changed)
					return true;
			return false;
		}

		#endregion

		#region Bewerkmethodes Player

		/// <summary>
		/// Het toevoegen van 1 speler.
		/// </summary>
		/// <param name="player">PlayerView van 1 speler</param>
		public void AddPlayer(PlayerView player)
		{
			allPlayers.Add(player);
		}

		/// <summary>
		/// Het verwijderen van 1 speler.
		/// </summary>
		/// <param name="player">PlayerView van 1 speler</param>
		public void RemovePlayer (PlayerView player)
		{
			allPlayers.Remove(player);
		}

		/// <summary>
		/// Het toevoegen van meerdere spelers.
		/// </summary>
		/// <param name="players">PlayerViews van spelers</param>
		public void AddPlayers (PlayerViews players)
		{
			allPlayers.AddRange(players);
		}

		#endregion

		#region Bewerkmethodes Poules

		/// <summary>
		/// Het toevoegen van meerdere poules.
		/// </summary>
		/// <param name="poules">PouleViews van poules</param>
		public void AddPoules (PouleViews poules)
		{
			allPoules.AddRange(poules);
		}

		#endregion

		#region NotificatieControl

		/// <summary>
		/// UpdateAllPlayersEvent, UpdateAllPoulesEvent en UpdatePouleEvent
		/// zijn van belang
		/// </summary>
		public FilterFlags FilterFlags { get { return (FilterFlags.UpdateAllPlayersEvent | FilterFlags.UpdateAllPoulesEvent | FilterFlags.UpdatePouleEvent); } }

		/// <summary>
		/// UpdteAllPoulesEvent is van belang
		/// </summary>
		/// <param name="SerialEvent"></param>
		/// <returns></returns>
		public bool UpdateAllPoulesEvent(SerialEventTypes SerialEvent){return true;}

		/// <summary>
		/// UpdteAllPlayersEvent is van belang
		/// </summary>
		/// <param name="SerialEvent"></param>
		/// <returns></returns>
		public bool UpdateAllPlayersEvent(SerialEventTypes SerialEvent){return true;}

		/// <summary>
		/// UpdateAllMatchesEvent is niet van belang
		/// </summary>
		/// <param name="SerialEvent"></param>
		/// <returns></returns>
		public bool UpdateAllMatchesEvent(SerialEventTypes SerialEvent){return false;}

		/// <summary>
		/// UpdateAllHistoryMatchesEvent is niet van belang
		/// </summary>
		/// <param name="SerialEvent"></param>
		/// <returns></returns>
		public bool UpdateAllHistoryMatchesEvent(SerialEventTypes SerialEvent){return false;}

		/// <summary>
		/// UpdatePouleEvent is van belang
		/// </summary>
		/// <param name="PouleID"></param>
		/// <param name="SerialEvent"></param>
		/// <returns></returns>
		public bool UpdatePouleEvent(int PouleID, SerialEventTypes SerialEvent){return true;}

		/// <summary>
		/// UpdatePoulePlanningEvent is niet van belang
		/// </summary>
		/// <param name="PouleID"></param>
		/// <param name="SerialEvent"></param>
		/// <returns></returns>
		public bool UpdatePoulePlanningEvent(int PouleID, SerialEventTypes SerialEvent){return false;}

		/// <summary>
		/// UpdatePouleTeamsEvent is niet van belang
		/// </summary>
		/// <param name="PouleID"></param>
		/// <param name="SerialEvent"></param>
		/// <returns></returns>
		public bool UpdatePouleTeamsEvent(int PouleID, SerialEventTypes SerialEvent){return false;}

		/// <summary>
		/// UpdatePouleLadderEvent is niet van belang
		/// </summary>
		/// <param name="PouleID"></param>
		/// <param name="SerialEvent"></param>
		/// <returns></returns>
		public bool UpdatePouleLadderEvent(int PouleID, SerialEventTypes SerialEvent){return false;}

		/// <summary>
		/// UpdatePouleMatchesEvent is niet van belang
		/// </summary>
		/// <param name="PouleID"></param>
		/// <param name="SerialEvent"></param>
		/// <returns></returns>
		public bool UpdatePouleMatchesEvent(int PouleID, SerialEventTypes SerialEvent){return false;}

		/// <summary>
		/// UpdateMatchEvent is niet van belang
		/// </summary>
		/// <param name="MatchID"></param>
		/// <param name="SerialEvent"></param>
		/// <returns></returns>
		public bool UpdateMatchEvent(int MatchID, SerialEventTypes SerialEvent){return false;}

		/// <summary>
		/// UpdateSettings is niet van belang
		/// </summary>
		/// <param name="SerialEvent"></param>
		/// <returns></returns>
		public bool UpdateSettings(SerialEventTypes SerialEvent){return false;}

		#endregion

	}
}
