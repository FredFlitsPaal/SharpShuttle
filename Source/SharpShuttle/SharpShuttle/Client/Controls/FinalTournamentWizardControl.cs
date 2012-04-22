using System.Collections.Generic;
using Client.Forms.ClientWizard.TournamentInitializationWizard;
using Client.Forms.PopUpWindows;
using Shared.Data;
using Shared.Domain;
using Shared.Views;
using Shared.Communication.Exceptions;

namespace Client.Controls
{

	/// <summary>
	/// Business logica voor het afronden van de initialisatie van een toernooi
	/// </summary>
	public class FinalTournamentWizardControl
	{
		/// <summary>
		/// 
		/// </summary>
		public event FinalInitStep.ReloadEvent ReloadEvent;

		/// <summary>
		/// De datacache	
		/// </summary>
		IDataCache cache = DataCache.Instance;

		#region Ophalen data
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
		/// Haal alle pouleteams op
		/// </summary>
		/// <returns>Lijst van teams</returns>
		public List<Team> GetPouleTeams(int id)
		{
			List<Team> result = new List<Team>();

			try
			{
				result = new List<Team>(cache.GetPouleTeams(id));
			}

			catch (CommunicationException exc)
			{
				CatchCommunicationExceptions.Show(exc);
			}

			return result;
		}

		/// <summary>
		/// Haal alle poulematches op
		/// </summary>
		/// <returns>Lijst van wedstrijden</returns>
		public IList<Match> GetPouleMatches(int pouleID)
		{
			IList<Match> result = new List<Match>();

			try
			{
				result = cache.GetPouleMatches(pouleID);
			}
			
			catch (CommunicationException exc)
			{
				CatchCommunicationExceptions.Show(exc);
			}

			return result;
		}
		#endregion

		#region Opslaan data
		/// <summary>
		/// Ga naar de volgende ronde
		/// </summary>
		/// <param name="pouleID">Id van de poule die naar de volgende ronde gaat</param>
		/// <param name="newMatches">De nieuwe matches voor de volgende ronde</param>
		public void SetPouleNextRound(int pouleID, List<Match> newMatches)
		{
			CommunicationException exc;

			if (!cache.SetPouleNextRound(pouleID, newMatches, out exc))
			{
				if (CatchCommunicationExceptions.Show(exc) == CatchCommunicationExceptions.ActionResult.Reload)
				{
					ReloadEvent(true);
				}
			}
			
		}

		/// <summary>
		/// Het opslaan van alle poules. 
		/// </summary>
		/// <param name="poules">De poules</param>
		public void SetAllPoules(PouleViews poules)
		{
			CommunicationException exc;

			if (!cache.SetAllPoules(poules.GetChangeTrackingList(), out exc))
			{
				if (CatchCommunicationExceptions.Show(exc) == CatchCommunicationExceptions.ActionResult.Reload)
				{
					ReloadEvent(true);
				}
			}
		}
		#endregion

	}
}
