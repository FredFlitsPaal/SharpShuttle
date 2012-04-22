using System.Collections.Generic;
using Client.Forms.PopUpWindows;
using Shared.Views;
using Shared.Domain;
using Shared.Communication.Exceptions;
using Shared.Data;
using Shared.Algorithms;
using Client.Printers;
using System.Linq;
using System.Windows.Forms;
using Shared.Logging;
using System;

namespace Client.Controls
{

	/// <summary>
	/// Businesslogica voor het beheren van een poule
	/// </summary>
    class PouleControl
    {
		/// <summary>
		/// De datacache
		/// </summary>
        private IDataCache cache;

		/// <summary>
		/// Alle teams in de poule
		/// </summary>
		private TeamViews teams;

		/// <summary>
		/// Alle ladderteams in de poule
		/// </summary>
		private List<LadderTeam> ladderTeamsList;

		/// <summary>
		/// Views van alle ladderteams
		/// </summary>
		private LadderTeamViews ladderTeams;

		/// <summary>
		/// Views van alle huidige matches van deze poule
		/// </summary>
		private MatchViews matches;

		/// <summary>
		/// Views van alle matches van deze poule
		/// </summary>
		private MatchViews allMatches;

		/// <summary>
		/// Views van alle poules
		/// </summary>
		private PouleViews poules;

		/// <summary>
		/// Default constructor
		/// </summary>
        public PouleControl()
        {
            cache = DataCache.Instance;
            SelectedPouleID = -1;
		}

		/// <summary>
		/// Het ID van de poule
		/// </summary>
		public int SelectedPouleID
		{
			get;
			set;
		}

		#region Ophalen data uit cache

		/// <summary>
		/// Haal alle teams uit de cache
		/// </summary>
		public void LoadTeams()
		{
			if (SelectedPouleID == -1) return;
			try
			{
				teams = new TeamViews(cache.GetPouleTeams(SelectedPouleID));
				ladderTeamsList = cache.GetPouleLadder(SelectedPouleID);
				ladderTeams = new LadderTeamViews(ladderTeamsList);
			}
			catch (CommunicationException exc)
			{
				CatchCommunicationExceptions.Show(exc);
			}
		}

		/// <summary>
		/// Haal alle wedstrijden van deze poule uit de cache
		/// </summary>
		public void LoadMatches()
		{
			matches = new MatchViews();
			if (SelectedPouleID == -1) return;

			try
			{
				var pouleMatches = cache.GetPouleMatches(SelectedPouleID).Where(m => !m.Disabled);
				matches = new MatchViews(pouleMatches.ToList());
			}
			catch (CommunicationException exc)
			{
				CatchCommunicationExceptions.Show(exc);
			}

			// Haal de geschiedenis wedstrijden van deze poule op
			allMatches = new MatchViews(this.GetPoulePlayedMatches(SelectedPouleID));
			allMatches.AddRange(matches);
		}

		/// <summary>
		/// Haalt alle poules uit de cache
		/// </summary>
		public void LoadPoules()
		{
			poules = new PouleViews();
			try
			{
				poules = new PouleViews(cache.GetAllPoules());
			}
			catch (CommunicationException exc)
			{
				CatchCommunicationExceptions.Show(exc);
			}
		}

		/// <summary>
		/// Haalt alle teams uit een poule op
		/// </summary>
		/// <param name="pouleID"> Het polue ID</param>
		/// <returns> Alle teams in de poule </returns>
		public TeamViews GetPouleTeams(int pouleID)
		{
			TeamViews result = new TeamViews();

			try
			{
				result = new TeamViews(cache.GetPouleTeams(pouleID));
			}
			catch (CommunicationException exc)
			{
				CatchCommunicationExceptions.Show(exc);
			}

			return result;
		}

		/// <summary>
		/// Haal een poule op uit de cache
		/// </summary>
		/// <param name="pouleID">Pouleid</param>
		/// <returns>De poule</returns>
		public PouleView LoadPoule(int pouleID)
		{
			PouleView result = null;
			try
			{
				result = new PouleView(cache.GetPoule(pouleID));
			}
			catch (CommunicationException exc)
			{
				CatchCommunicationExceptions.Show(exc);
			}

			return result;
		}

		/// <summary>
		/// Haal alle al gespeelde matches uit een poule op
		/// </summary>
		/// <param name="pouleID"> Het poule ID </param>
		/// <returns> Alle al gespeelde matches uit de poule </returns>
		public List<Match> GetPoulePlayedMatches(int pouleID)
		{
			List<Match> result = new List<Match>();
			try
			{
				var pouleMatches = cache.GetPouleHistoryMatches(pouleID).Where(m => !m.Disabled);
				result = pouleMatches.ToList();
			}
			catch (CommunicationException exc)
			{
				CatchCommunicationExceptions.Show(exc);
			}

			return result;
		}

		#endregion

		#region Ophalen van data

		/// <summary>
		/// Haalt, gegeven een ladderteam, het bijbehorende team op
		/// </summary>
		/// <param name="ladderTeam"> Het ladderteam </param>
		/// <returns> Het bijbehorende team </returns>
		public TeamView GetLadderTeam(LadderTeamView ladderTeam)
		{
			foreach (TeamView team in teams)
				if (ladderTeam.TeamId == team.TeamId)
					return team;

			return null;
		}

		/// <summary>
		/// Haalt alle matches op uit de cache
		/// </summary>
		/// <returns> Alle matches </returns>
		public MatchViews GetMatches(bool onlyCurrentRound)
        {
			if (matches == null)
				LoadMatches();

            if (!onlyCurrentRound)
				return allMatches;
            return matches;
        }

		/// <summary>
		/// Haal alle teams op
		/// </summary>
		/// <returns> Alle teams </returns>
		public TeamViews GetTeams()
        {
			if (teams == null)
				LoadTeams();
			return teams;
		}

		/// <summary>
		/// Haal alle ladderteams op
		/// </summary>
		/// <returns> Alle ladderteams </returns>
		public LadderTeamViews GetLadderTeams()
		{
			if (ladderTeams == null)
				LoadTeams();
			return ladderTeams;
		}

		/// <summary>
		/// Haalt een poule op, gegeven een pouleID
		/// </summary>
		/// <param name="pouleID"> Het poule ID </param>
		/// <returns> De bijbehorende poule </returns>
		public PouleView GetPoule(int pouleID)
		{
			if (poules == null)
				LoadPoules();
			return poules.getPoule(pouleID);
		}

		/// <summary>
		/// Haalt alle poules op
		/// </summary>
		/// <returns> Alle poules </returns>
        public PouleViews GetPoules()
        {
			if (poules == null)
				LoadPoules();
			return poules;
        }

		/// <summary>
		/// Vraagt het huidige rondenummer op
		/// </summary>
		/// <returns>Het huidige rondenummer</returns>
        public int GetRoundNumber()
        {
			PouleView poule = LoadPoule(SelectedPouleID);
			if (poule != null)
				return poule.Domain.Round;
			return 0;
        }
		#endregion

		#region Opslaan van data

		/// <summary>
		/// Sla de score van een wedstrijd op
		/// </summary>
		/// <param name="match"> De wedstrijd </param>
		public void SetScore(MatchView match)
		{
			if (match.EndTime.Length == 0)
			{
				match.Domain.EndTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
				match.Domain.Court = 0;
			}
			CommunicationException exc;
			if (!cache.SetMatch(match.Domain, out exc))
			{
				CatchCommunicationExceptions.Show(exc);
			}
		}

		/// <summary>
		/// Sla alle doorgevoerde teamveranderingen op
		/// </summary>
		public void SetTeamChanges()
		{
			CommunicationException exc;
			if (teams.Count > 0)
				if (cache.SetPouleTeams(SelectedPouleID, teams.GetChangeTrackingList(), out exc))
					LoadTeams();
				else
				{
					CatchCommunicationExceptions.Show(exc);
				}
		}

		#endregion

		#region Afhandelen rondes

		/// <summary>
		/// Er wordt gekeken of de ronde verwijdert kan worden. 
		/// de wedstrijden worden nog niet verwijderd.
		/// </summary>
		/// <returns> Een bool die anageeft of het verwijderen mogelijk is.</returns>
		public bool RemovableRound ()
		{
			if (SelectedPouleID == -1) return false;

			int Round = GetRoundNumber();

			if (Round == 0)
				return false;
			
			//Controleren of er wedstrijden in deze ronde zijn die al gespeeld zijn.
			foreach (MatchView match in GetMatches(false))
				if (match.Round == Round && match.Domain.Played)
				{
					return false;
				}

			return true;
		}

		/// <summary>
		/// Verwijdert de huidige ronde zodat scores uit vorige rondes gewijzigd
		/// kunnen worden die het ladder algoritme beinvloeden
		/// Dit kan alleen als er nog geen wedstrijden in deze ronde al gespeeld zin
		/// </summary>
		/// <returns> Een bool die aangeeft of het verwijderen mogelijk is </returns>
		public bool RemoveRound()
		{
			//Verwijder de huidige ronde
			CommunicationException exc;
			if (!cache.PouleRemoveRound(SelectedPouleID, out exc))
			{
				Logger.Write("PouleControl", "Fout bij aanroep PouleRemoveRound " + exc);
				CatchCommunicationExceptions.Show(exc);
				return false;
			}

			return true;
		}
		
		/// <summary>
		/// Het afsluiten van de huidige ronde.
		/// </summary>
		/// <returns></returns>
		public bool FinishRound()
        {
			if (SelectedPouleID == -1) return false;

			//Eerst moeten we controleren of alle wedstrijden gespeeld zijn,
			//anders mogen we niet verder gaan.
			foreach (MatchView match in GetMatches(false))
				if (! (match.Domain.Played || match.Domain.Disabled))
				{
					MessageBox.Show("De volgende ronde kan nog niet van start gaan omdat niet alle wedstrijden ingevulde scores hebben.", "Sharp Shuttle - Volgende Ronde Genereren", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return false;
				}

			//Beeindig de huidige ronde.
			CommunicationException exc;
			
			if (!cache.PouleFinishRound(SelectedPouleID, out exc))
			{	
				Logger.Write("PouleControl", "Fout bij aanroep PouleFinishRound " + exc);
				CatchCommunicationExceptions.Show(exc);
			}
			
			if (GetRoundNumber() >= getAmountActiveTeams()-1)
			{
				MessageBox.Show("De volgende ronde kan niet worden gestart aangezien alle mogelijke wedstrijden voor deze poule al gespeeld zijn. De wedstrijddata is wel opgeslagen.", "Sharp Shuttle - Volgende Ronde Genereren", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}
			
			//Start een nieuwe ronde op.
            Team notplaying;

			List<LadderTeam> sortedLadderTeams = ladderTeamsList;
			sortedLadderTeams.Sort((team1, team2) => team1.Rank.CompareTo(team2.Rank));
			List<Match> historymatches = GetPoulePlayedMatches(SelectedPouleID);
			int nextroundnumber = GetRoundNumber() + 1;
			
			List<Team> sortedTeams = new List<Team>((from team in sortedLadderTeams select team.Team));
			List<Match> newMatches = Algorithms.GenerateLadder(sortedTeams,
                nextroundnumber, historymatches, out notplaying);


			//Sla de wedstrijden voor de nieuwe ronde op.
			if (!cache.SetPouleNextRound(SelectedPouleID, newMatches, out exc))
			{
				CatchCommunicationExceptions.Show(exc);
			}

			ICollection<Match> correctDataMatches = GetMatches(false).GetDomainList();
			MatchNotePrinter.Printer.AddMatches(correctDataMatches);
			return true;
		}

		/// <summary>
		/// Verkrijg het aantal actieve teams
		/// </summary>
		/// <returns></returns>
		private int getAmountActiveTeams()
		{
			TeamViews teams = GetPouleTeams(SelectedPouleID);
			int numberOfTeams = 0;

			for (int i = 0; i < teams.Count; i++)
				if (!teams[i].Domain.IsInOperative)
					numberOfTeams++;
			
			//rond af naar volgende even waarde.
			numberOfTeams += (numberOfTeams & 1);

			return numberOfTeams;
		}

		#endregion

		#region Printer
		
		/// <summary>
		/// Voegt nieuw wedstrijdbriefje toe aan de printqueue. Kan gebruikt worden bij verlies origineel.
		/// </summary>
		/// <param name="viewtoprint"> De wedstrijd </param>
		public void PrintNote(MatchView viewtoprint)
		{
			MatchNotePrinter.Printer.AddMatch(viewtoprint.Domain);
		}

		/// <summary>
		/// Print de huidige ladder scores uit
		/// </summary>
		public void PrintLadder()
		{
			RankingPrinter.PrintRanking(GetPoule(SelectedPouleID).Domain, ladderTeamsList);
		}

		public void PrintLadderAndMatches()
		{
			Poule poule = GetPoule(SelectedPouleID).Domain;
			List<Match> matches = new List<Match>(GetPoulePlayedMatches(poule.PouleID));
			List<MatchView> currentmatches = new List<MatchView>(GetMatches(false));

			foreach (MatchView mv in currentmatches)
				if (mv.Domain.Played)
					matches.Add(mv.Domain);

			RankingPrinter.PrintRankingAndMatches(poule, ladderTeamsList, matches);
		}

    	#endregion
	}
}
