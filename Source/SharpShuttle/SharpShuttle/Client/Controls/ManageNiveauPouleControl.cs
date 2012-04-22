using System.Windows.Forms;
using Client.Forms.ClientWizard.TournamentInitializationWizard;
using Client.Forms.PopUpWindows;
using Shared.Data;
using Shared.Views;
using Shared.Communication.Exceptions;
using System.Collections.Generic;
using Shared.Communication.Serials;
using SerialEventTypes = Shared.Communication.Serials.SerialEventTypes;

namespace Client.Controls
{

	/// <summary>
	/// Business logica om de verschillende niveaus van de poules te beheren
	/// </summary>
	public class ManageNiveauPouleControl : ISerialFilter
	{

		/// <summary>
		/// De datacache
		/// </summary>
		private IDataCache cache;

		/// <summary>
		/// Alle poules
		/// </summary>
		private PouleViews poules;

		/// <summary>
		/// Alle niveaus
		/// </summary>
		private List<string> niveaus;

		/// <summary>
		/// Event voor wanneer er gerefresht wordt
		/// </summary>
		public event NiveausPoulesStep.ReloadEvent ReloadEvent;

		#region Constructor en initialisatie

		/// <summary>
		/// Default constructor, haalt de cache op en initialiseert niveaus
		/// </summary>
		public ManageNiveauPouleControl()
		{
			cache = DataCache.Instance;
			niveaus = new List<string>();
		}

		/// <summary>
		/// Het ophalen van de spelers uit de database.
		/// </summary>
		public void Initialize()
		{
			try
			{
				poules = new PouleViews(cache.GetAllPoules());

				// Nieuwe niveaus (van nieuw bijgekomen poules) aan de niveaulijst toevoegen
				foreach (PouleView poule in poules)
					if (!niveaus.Contains(poule.Niveau))
						niveaus.Add(poule.Niveau);
			}
			catch (CommunicationException exc)
			{
				CatchCommunicationExceptions.Show(exc);
			}
		}

		#endregion

		#region Ophalen, verwerken en wegschrijven van data

		/// <summary>
		/// Alle poules
		/// </summary>
		public PouleViews Poules
		{
			get { return poules; }
		}

		/// <summary>
		/// Alle niveaus
		/// </summary>
		public List<string> Niveaus
		{
			get { return niveaus; }
		}

		/// <summary>
		/// Het toevoegen van een poule.
		/// </summary>
		/// <param name="poule">Poule die toegevoegd moet worden</param>
		public void AddPoule(PouleView poule)
		{
			poules.Add(poule);
		}

		/// <summary>
		/// Het verwijderen van een poule.
		/// </summary>
		/// <param name="poule">Poule die verwijderd moet worden</param>
		public void RemovePoule(PouleView poule)
		{
			poules.Remove(poule);
		}

		/// <summary>
		/// Het toevoegen van een niveau.
		/// </summary>
		/// <param name="niveau">Niveau die toegevoegd moet worden</param>
		public void AddNiveau(string niveau)
		{
			niveaus.Add(niveau);
		}

		/// <summary>
		/// Het verwijderen van een niveau.
		/// </summary>
		/// <param name="niveau">Niveau die verwijderd moet worden</param>
		/// <returns>Boolean of er een poule verwijderd is</returns>
		public bool RemoveNiveau(string niveau)
		{
			niveaus.Remove(niveau);
			
			List<PouleView> poulesToRemove = new List<PouleView>();

			// Sla alle poules, die een associatie hebben met het te verwijderen niveau, op in poulesToRemove
			foreach (PouleView poule in poules)
				if (poule.Niveau.Equals(niveau))
					poulesToRemove.Add(poule);

			// Verwijder alle poules in poulesToRemove 
			foreach (PouleView poule in poulesToRemove)
				poules.Remove(poule);

			// Return een boolean of er een poule verwijderd is
			if (poulesToRemove.Count > 0)
				return true;
			
			return false;
		}

		/// <summary>
		/// Het wijzigen van een niveau.
		/// </summary>
		/// <param name="oldNiveau">Het oude niveau</param>
		/// <param name="newNiveau">Het gewijzigde niveau</param>
		/// <returns>Boolean of er een poule gewijzigd is</returns>
		public bool ChangeNiveau(string oldNiveau, string newNiveau)
		{
			bool pouleChanged = false;

			// Verander het oude niveau in de niveaulijst door het nieuwe niveau
			int index = niveaus.IndexOf(oldNiveau);
			niveaus[index] = newNiveau;

			// Verander alle poules die een associatie hebben met het oude niveau
			foreach (PouleView poule in poules)
			{
				if (poule.Niveau.Equals(oldNiveau))
				{
					if (poule.Name.Equals(poule.Discipline + " " + poule.Niveau))
						poule.Name = poule.Discipline + " " + newNiveau;

					poule.Niveau = newNiveau;
					pouleChanged = true;
				}
			}

			// Return een boolean of er een poule gewijzigd is
			return pouleChanged;
		}

		/// <summary>
		/// Het wegschrijven van data naar de database.
		/// </summary>
		public void SavePoules()
		{
			CommunicationException exc;

			if (cache.SetAllPoules(poules.GetChangeTrackingList(), out exc))
				Initialize();
			else if (CatchCommunicationExceptions.Show(exc) == CatchCommunicationExceptions.ActionResult.Reload)
				ReloadEvent(true);
		}

		#endregion

		#region Validatie

		/// <summary>
		/// Check voor een geldige poulenaam.
		/// </summary>
		/// <param name="oldName">De oude naam van de poule</param>
		/// <param name="newName">De nieuwe naam voor de poule</param>
		/// <returns></returns>
		public bool CheckForValidPouleName(string oldName, string newName)
		{
			if (!newName.Equals(oldName, System.StringComparison.OrdinalIgnoreCase))
			{
				foreach (PouleView poule in poules)
					if (poule.Name.Equals(newName, System.StringComparison.OrdinalIgnoreCase))
						return false;
			}

			return true;
		}

		/// <summary>
		/// Check voor een geldige niveaunaam.
		/// </summary>
		/// <param name="oldNiveau">De oude naam van het niveau</param>
		/// <param name="newNiveau">De nieuwe naam voor het niveau</param>
		/// <returns></returns>
		public bool CheckForValidNiveau(string oldNiveau, string newNiveau)
		{
			if (!newNiveau.Equals(oldNiveau, System.StringComparison.OrdinalIgnoreCase))
			{
				foreach (string niveau in niveaus)
					if (niveau.Equals(newNiveau, System.StringComparison.OrdinalIgnoreCase))
						return false;
			}

			return true;
		}

		#endregion

		#region NotificatieControl

		/// <summary>
		/// UpdateAllPoulesEvent en UpdatePouleEvent zijn van belang
		/// </summary>
		public FilterFlags FilterFlags { get { return (FilterFlags.UpdateAllPoulesEvent | FilterFlags.UpdatePouleEvent); } }

		/// <summary>
		/// UpdateAllPoulesEvent is van belang
		/// </summary>
		/// <param name="SerialEvent"></param>
		/// <returns></returns>
		public bool UpdateAllPoulesEvent(SerialEventTypes SerialEvent) { return true; }

		/// <summary>
		/// UpdateAllPlayersEvent is niet van belang
		/// </summary>
		/// <param name="SerialEvent"></param>
		/// <returns></returns>
		public bool UpdateAllPlayersEvent(SerialEventTypes SerialEvent) { return false; }

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
		/// UpdatePouleEvent is van belang
		/// </summary>
		/// <param name="PouleID"></param>
		/// <param name="SerialEvent"></param>
		/// <returns></returns>
		public bool UpdatePouleEvent(int PouleID, SerialEventTypes SerialEvent) { return true; }

		/// <summary>
		/// UpdatePoulePlanningEvent is niet van belang
		/// </summary>
		/// <param name="PouleID"></param>
		/// <param name="SerialEvent"></param>
		/// <returns></returns>
		public bool UpdatePoulePlanningEvent(int PouleID, SerialEventTypes SerialEvent) { return false; }

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
