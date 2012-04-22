using System;
using System.Collections.Generic;
using Client.Forms.PopUpWindows;
using Shared.Data;
using Shared.Views;
using Shared.Domain;
using Shared.Communication.Exceptions;
using Client.Printers;
using Shared.Algorithms;

namespace Client.Controls
{

	/// <summary>
	/// Business logica voor het toevoegen van een poule
	/// </summary>
	public class AddPouleControl
	{
		/// <summary>
		/// De datacache
		/// </summary>
		IDataCache cache = DataCache.Instance;
		/// <summary>
		/// Methode die aangeroepen wordt als de playerlist geupdate is
		/// </summary>
		public delegate void UpdateList();

		/// <summary>
		/// Event voor als de playerlist geupdate wordt
		/// </summary>
		public event UpdateList PlayerListUpdated;

		/// <summary>
		/// Huidige poules
		/// </summary>
		private PouleViews poules = new PouleViews();

		/// <summary>
		/// De poule die toegevoegd wordt
		/// </summary>
		private PouleView poule = new PouleView();

		/// <summary>
		/// Alle spelers
		/// </summary>
		private PlayerViews players = new PlayerViews();

		/// <summary>
		/// De opule die toegevoegd wordt
		/// </summary>
		public PouleView Poule
		{
			get { return poule; }
			set { poule = value; }
		}

		#region Ophalen data
		/// <summary>
		/// Haal de spelers op die in de poule zitten
		/// </summary>
		/// <returns>Spelers</returns>
		public PlayerViews GetPoulePlayers()
		{
			return poule.Players;
		}

		/// <summary>
		/// Haal alle bestaande spelers op
		/// </summary>
		/// <returns></returns>
		public PlayerViews GetAllPlayers()
		{
			return players;
		}

		/// <summary>
		/// Haal alle vrouwelijke spelers op
		/// </summary>
		/// <returns></returns>
		public PlayerViews GetFemalePlayers()
		{
			PlayerViews result = new PlayerViews();
			foreach (PlayerView player in players)
				if (player.Gender == "V")
					result.Add(player);
			return result;
		}

		/// <summary>
		/// Haal alle mannelijke spelers op
		/// </summary>
		/// <returns></returns>
		public PlayerViews GetMalePlayers()
		{
			PlayerViews result = new PlayerViews();
			foreach (PlayerView player in players)
				if(player.Gender == "M")
					result.Add(player);
			return result;
		}

		/// <summary>
		/// Haal nieuwe data op van de server
		/// </summary>
		public void UpdateData()
		{
			try
			{
				poules = new PouleViews(cache.GetAllPoules());
				players = new PlayerViews(cache.GetAllPlayers());
			}

			catch (CommunicationException exc)
			{
				CatchCommunicationExceptions.Show(exc);
			}
		}

		/// <summary>
		/// Haal alle pouleteams op 
		/// </summary>
		/// <param name="pouleID">Poule id</param>
		/// <returns>Alle poule teams</returns>
		public IList<Team> GetPouleTeams(int pouleID)
		{
			IList<Team> teams = new List<Team>();
			try
			{
				teams = new List<Team>(cache.GetPouleTeams(pouleID));
			}
			catch (CommunicationException exc)
			{
				CatchCommunicationExceptions.Show(exc);
			}
			return teams;
		}

		/// <summary>
		/// Haal alle poulematches op
		/// </summary>
		/// <param name="pouleID"> De poule waar wedstrijden uitgehaald worden</param>
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
				CatchCommunicationExceptions.Show(exc);
			}
		}

		/// <summary>
		/// Voegt een nieuwe poule toe
		/// </summary>
		/// <param name="name"> Naam van de poule </param>
		/// <param name="discipline"> Discipline van de poule </param>
		/// <param name="niveau"> Niveau van de poule </param>
		/// <param name="comments"> Comments die bij de poule horen </param>
		/// <param name="teams"> De teams in de poule </param>
		public void AddPoule(string name, string discipline, string niveau, string comments, TeamViews teams)
		{
			poule.Name = name;
			poule.Discipline = discipline;
			poule.Niveau = niveau;
			poule.Comment = comments;
			poules.Add(poule);
			CommunicationException exc;

			if (cache.SetAllPoules(poules.GetChangeTrackingList(), out exc))
			{
				poules = new PouleViews(cache.GetAllPoules());
			}
			else
			{
				CatchCommunicationExceptions.Show(exc);
			}

			// Haal de poule id op
			int pouleID = 0;
			foreach (PouleView pouleView in poules)
				if (pouleView.Name == poule.Name)
					pouleID = pouleView.Id;

			try
			{
				ChangeTrackingList<Player> poulePlanning = cache.GetPoulePlanning(pouleID);
				poulePlanning.AddRange(poule.Players.GetDomainList());
				if (!cache.SetPoulePlanning(pouleID, poulePlanning, out exc))
				{
					CatchCommunicationExceptions.Show(exc);
				}
			}
			catch (CommunicationException exc2)
			{
				CatchCommunicationExceptions.Show(exc2);
			}

			TeamViews teamsList = new TeamViews();
			try
			{
				teamsList = new TeamViews(cache.GetPouleTeams(pouleID));
				foreach (TeamView team in teams)
					teamsList.Add(team);

				if (cache.SetPouleTeams(pouleID, teamsList.GetChangeTrackingList(), out exc))
				{
					teamsList = new TeamViews(cache.GetPouleTeams(pouleID));
				}
				else
					CatchCommunicationExceptions.Show(exc);
			}
			catch (CommunicationException exc2)
			{
				CatchCommunicationExceptions.Show(exc2);
			}

			if (teamsList.Count > 0)
			{
				// Start nieuwe ronde voor deze poule
				Team notPlayingTeam;

				IList<Match> matches = GetPouleMatches(pouleID);
				List<Match> newMatches = Algorithms.GenerateLadder(new List<Team>(teamsList.GetDomainList()), 1, matches, out notPlayingTeam);

				MatchNotePrinter.Printer.AddMatches(newMatches);

				SetPouleNextRound(pouleID, newMatches);
			}

		}
		#endregion

		#region Pouleplanning
		/// <summary>
		/// Voeg spelers toe aan de poule
		/// </summary>
		/// <param name="players">De toe te voegen spelers</param>
		public void AddPlayersToPoule(PlayerViews players)
		{
			foreach (PlayerView newPlayer in players)
			{
				bool playerIsNew = true;

				foreach (PlayerView player in poule.Players)
					if (newPlayer.Id == player.Id)
						playerIsNew = false;

				if (playerIsNew)
				{
					poule.incAmountPlayers(newPlayer.Gender);
					poule.Players.Add(newPlayer);
				}
			}

			if (PlayerListUpdated != null)
				PlayerListUpdated();
		}

		/// <summary>
		/// Haal een speler uit de poule
		/// </summary>
		/// <param name="player">De te verwijderen speler</param>
		public void RemovePlayerFromPoule(PlayerView player)
		{
			if (poule.Players.Contains(player))
			{
				poule.Players.Remove(player);
				poule.decAmountPlayers(player.Gender);
			}

			if (PlayerListUpdated != null)
				PlayerListUpdated();
		}

		/// <summary>
		/// Haal meerdere spelers uit de poule
		/// </summary>
		/// <param name="players">De te verwijderen spelers</param>
		public void RemovePlayersFromPoule(PlayerViews players)
		{
			foreach (PlayerView player in players)
			{
				if (poule.Players.Contains(player))
				{
					poule.Players.Remove(player);
					poule.decAmountPlayers(player.Gender);
				}
			}
			if (PlayerListUpdated != null)
				PlayerListUpdated();
		}
		#endregion

		#region HulpMethodes
		/// <summary>
		/// Controleer de naam van de poule
		/// </summary>
		/// <param name="newName">Naam van de poule</param>
		/// <returns>Of de naam geldig is</returns>
		public bool CheckForValidPouleName(string newName)
		{
			foreach (PouleView poule in poules)
				if (poule.Name.Equals(newName, StringComparison.OrdinalIgnoreCase))
					return false;

			return true;
		}
		#endregion
	}
}

