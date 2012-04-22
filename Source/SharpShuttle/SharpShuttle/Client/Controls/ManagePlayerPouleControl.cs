using System.Collections.Generic;
using Client.Forms.ClientWizard.TournamentInitializationWizard;
using Client.Forms.PopUpWindows;
using Shared.Views;
using Shared.Data;
using Shared.Communication.Exceptions;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Shared.Communication.Serials;

namespace Client.Controls
{

	/// <summary>
	/// Businesslogica om de spelers in poules in te delen
	/// </summary>
	public class ManagePlayerPouleControl : ISerialFilter
	{
		/// <summary>
		/// De datacache
		/// </summary>
		private IDataCache cache = DataCache.Instance;

		/// <summary>
		/// De gemaakte indeling van spelers
		/// </summary>
		private Dictionary<int, PlayerPoulesViews> poulePlanning;

		/// <summary>
		/// Event voor als er gerefreshd wordt
		/// </summary>
		public event PoulesLayoutStep.ReloadEvent ReloadEvent;

		#region Ophalen van data

		/// <summary>
        /// Haal alle spelers op
        /// </summary>
        /// <returns>Lijst van playerpoulesviews</returns>
		public PlayerPoulesViews GetPlayers()
		{
			PlayerPoulesViews result = new PlayerPoulesViews();

			try
			{
				result = new PlayerPoulesViews(cache.GetAllPlayers());
			}

			catch (CommunicationException exc)
			{
				CatchCommunicationExceptions.Show(exc);
			}

			return result;
		}

        /// <summary>
        /// Haal alle poules op
        /// </summary>
		/// <returns>Lijst van pouleviews</returns>
		public PouleViews GetPoules()
		{
			PouleViews result = new PouleViews();

			try
			{
				result = new PouleViews(cache.GetAllPoules());
			}

			catch (CommunicationException exc)
			{
				CatchCommunicationExceptions.Show(exc);
			}

			return result;
		}


		/// <summary>
		/// Haalt alle ingeplande spelers van een poule op
		/// </summary>
		/// <param name="pouleid"> id van de poule </param>
		/// <returns> Lijst van ingeplande spelers</returns>
		public PlayerPoulesViews GetPoulePlanning(int pouleid)
		{
			PlayerPoulesViews result = new PlayerPoulesViews();

			try
			{
				result = new PlayerPoulesViews(cache.GetPoulePlanning(pouleid));
			}

			catch (CommunicationException exc)
			{
				CatchCommunicationExceptions.Show(exc);
			}

			return result;
		}

		#endregion

		#region Wijzigingen op de data
		/// <summary>
		/// Koppel de voorkeuren van een speler aan de poules
		/// </summary>
		/// <param name="player">De speler waar poules aan gekoppeld moeten worden</param>
		/// <param name="poules">Alle poules</param>
		public void SetPreferencesOfPlayer(PlayerPoulesView player, PouleViews poules)
		{
			string[] prefs = Regex.Split(player.Preferences, ", ");

			for (int i = 0; i < prefs.Length; i++)
			{
				PouleView poule = getPoule(poules, prefs[i]);
				if (poule != null)
					player.PreferencesPoules.Add(poule);
			}
		}

		/// <summary>
		/// Koppel de voorkeuren van de spelers aan de poules
		/// </summary>
		/// <param name="players">De spelers waar poules aan gekoppeld moeten worden</param>
		/// <param name="poules">Alle poules</param>
		public void SetPreferencesOfPlayers(PlayerPoulesViews players, PouleViews poules)
		{
			foreach (PlayerPoulesView player in players)
				SetPreferencesOfPlayer(player, poules);
		}

		/// <summary>
		/// Haalt een poule op uit een list van poules, gegeven een naam
		/// </summary>
		/// <param name="poules"> De lijst van poules </param>
		/// <param name="pouleName"> De poulenaam </param>
		/// <returns> De gegeven poule </returns>
		private static PouleView getPoule(PouleViews poules, string pouleName)
		{
			foreach (PouleView poule in poules)
				if ((poule.Discipline + ": " + poule.Niveau) == pouleName)
					return poule;

			return null;
		}

		/// <summary>
		/// Link spelers aan de poules waar ze voor ingeschreven staan en andersom
		/// </summary>
		/// <param name="players">Alle spelers</param>
		/// <param name="poules">Alle poules</param>
		public void CombinePlayersAndPoules(PlayerPoulesViews players, PouleViews poules)
		{
			//poulePlanning = new Dictionary<int, ChangeTrackingList<Player>>();
			poulePlanning = new Dictionary<int, PlayerPoulesViews>();

			// Vul de dictionary met poules en de planning daarvan
			foreach (PouleView poule in poules)
				if (!poulePlanning.ContainsKey(poule.Id))
					poulePlanning.Add(poule.Id, GetPoulePlanning(poule.Id));

			foreach (int id in poulePlanning.Keys)
			{
				PouleView poule = poules.getPoule(id);
				PlayerPoulesViews removeList = new PlayerPoulesViews();

				// Koppel elke playerview aan de bijbehorende pouleview en andersom
				// met behulp van de pouleplanning
				foreach (PlayerPoulesView player in poulePlanning[id])
				{
					PlayerPoulesView playerView = players.getPlayer(player.Id);

					// Controleer of speler bestaat en of hij/zij wel in deze poule mag spelen
					if (playerView != null && (
						(playerView.Gender == "M" && poule.getGender() == PouleView.Gender.M) ||
						(playerView.Gender == "M" && poule.getGender() == PouleView.Gender.U) ||
						(playerView.Gender == "V" && poule.getGender() == PouleView.Gender.V) ||
						(playerView.Gender == "V" && poule.getGender() == PouleView.Gender.U)
						))
					{
						playerView.AssignedPoules.Add(poule);
						poule.Players.Add(playerView);
						poule.incAmountPlayers(player.Gender);
					}
					else
						removeList.Add(player);
				}

				// Verwijder de spelers uit de pouleplanning als de spelers 
				// niet bestaan/niet thuis horen in deze poule
				foreach (PlayerPoulesView player in removeList)
					poulePlanning[id].Remove(player);
			}
		}

		/// <summary>
		/// Sla de huidige indeling op 
		/// </summary>
		/// <param name="poules">Alle poules</param>
		public void SavePouleLayout(PouleViews poules)
		{
			CommunicationException exc;

			foreach (PouleView poule in poules)
			{
				if (cache.SetPoulePlanning(poule.Id, poulePlanning[poule.Id].GetChangeTrackingList(), out exc))
					poulePlanning[poule.Id] = GetPoulePlanning(poule.Id);
				
				else if (exc is DataReferenceException)
				{
					if (MessageBox.Show("De data wordt herladen omdat er oude referenties zijn.",
						"Oude Referenties", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
						ReloadEvent(true);
				}

				else
				{
					if (CatchCommunicationExceptions.Show(exc) == CatchCommunicationExceptions.ActionResult.Reload)
					{
						ReloadEvent(true);
					}
				}
				
			}
		}

		/// <summary>
		/// Voeg een speler toe aan een poule
		/// </summary>
		/// <param name="playerView"> De speler </param>
		/// <param name="poule"> De poule </param>
		public void AddPlayerToPoule(PlayerPoulesView playerView, PouleView poule)
		{
			poulePlanning[poule.Id].Add(playerView);
		}

		/// <summary>
		/// Verwijder een speler uit een poule
		/// </summary>
		/// <param name="playerView"> De speler </param>
		/// <param name="poule"> De poule </param>
		public void RemovePlayerFromPoule(PlayerPoulesView playerView, PouleView poule)
		{
			poulePlanning[poule.Id].Remove(getPlayer(poulePlanning[poule.Id], playerView.Id));
		}
		#endregion

		#region Hulp methodes

		/// <summary>
		/// Haal een speler met een gegeven id uit een lijst van spelers
		/// </summary>
		/// <param name="players"> De lijst van spelers </param>
		/// <param name="id"> De speler id </param>
		/// <returns> De speler </returns>
		private static PlayerPoulesView getPlayer(PlayerPoulesViews players, int id)
		{
			foreach (PlayerPoulesView pl in players)
				if (pl.Id == id)
					return pl;

			return null;
		}

		/// <summary>
		/// Geeft aan of de indeling veranderd is
		/// </summary>
		/// <returns> Een bool die aangeeft of de indeling veranderd is </returns>
		public bool IsChanged()
		{
			if (poulePlanning != null)
				foreach (int id in poulePlanning.Keys)
					if (poulePlanning[id].GetChangeTrackingList().Changed)
						return true;

			return false;
		}
		#endregion

		#region Notification

		/// <summary>
		/// UpdateAllPoulesEvent, UpdateAllPlayersEvent en UpdatePoulePlanningEvent
		/// zijn van belang
		/// </summary>
		public FilterFlags FilterFlags { get { return (FilterFlags.UpdateAllPoulesEvent | FilterFlags.UpdateAllPlayersEvent | FilterFlags.UpdatePoulePlanningEvent); } }

		/// <summary>
		/// UpdateAllPoulesEvent is van belang
		/// </summary>
		/// <param name="SerialEvent"></param>
		/// <returns></returns>
		public bool UpdateAllPoulesEvent(SerialEventTypes SerialEvent) { return true; }

		/// <summary>
		/// UpdateAllPlayersEvent is van belang
		/// </summary>
		/// <param name="SerialEvent"></param>
		/// <returns></returns>
		public bool UpdateAllPlayersEvent(SerialEventTypes SerialEvent) { return true; }

		/// <summary>
		/// UpdateAllMatchesEvent is niet van belang
		/// </summary>
		/// <param name="SerialEvent"></param>
		/// <returns></returns>
		public bool UpdateAllMatchesEvent(SerialEventTypes SerialEvent) { return false; }

		/// <summary>
		/// UpdateAllHistoryMatchesEvent is niet van belang
		/// </summary>
		/// <param name="SerialEvent"></param>
		/// <returns></returns>
		public bool UpdateAllHistoryMatchesEvent(SerialEventTypes SerialEvent) { return false; }

		/// <summary>
		/// UpdatePouleEvent is niet van belang
		/// </summary>
		/// <param name="PouleID"></param>
		/// <param name="SerialEvent"></param>
		/// <returns></returns>
		public bool UpdatePouleEvent(int PouleID, SerialEventTypes SerialEvent) { return false; }

		/// <summary>
		/// UpdatePoulePlanningEvent is van belang
		/// </summary>
		/// <param name="PouleID"></param>
		/// <param name="SerialEvent"></param>
		/// <returns></returns>
		public bool UpdatePoulePlanningEvent(int PouleID, SerialEventTypes SerialEvent) { return true; }

		/// <summary>
		/// UpdatePouleTeamsEvent is niet van belang
		/// </summary>
		/// <param name="PouleID"></param>
		/// <param name="SerialEvent"></param>
		/// <returns></returns>
		public bool UpdatePouleTeamsEvent(int PouleID, SerialEventTypes SerialEvent) { return false; }

		/// <summary>
		/// UpdatePouleLadderEvent is niet van belang
		/// </summary>
		/// <param name="PouleID"></param>
		/// <param name="SerialEvent"></param>
		/// <returns></returns>
		public bool UpdatePouleLadderEvent(int PouleID, SerialEventTypes SerialEvent) { return false; }

		/// <summary>
		/// UpdatePouleMatchesEvent is niet van belang
		/// </summary>
		/// <param name="PouleID"></param>
		/// <param name="SerialEvent"></param>
		/// <returns></returns>
		public bool UpdatePouleMatchesEvent(int PouleID, SerialEventTypes SerialEvent) { return false; }

		/// <summary>
		/// UpdateMatchEvent is niet van belang
		/// </summary>
		/// <param name="MatchID"></param>
		/// <param name="SerialEvent"></param>
		/// <returns></returns>
		public bool UpdateMatchEvent(int MatchID, SerialEventTypes SerialEvent) { return false; }

		/// <summary>
		/// UpdateSettings is niet van belang
		/// </summary>
		/// <param name="SerialEvent"></param>
		/// <returns></returns>
		public bool UpdateSettings(SerialEventTypes SerialEvent) { return false; }

		#endregion

	}
}
