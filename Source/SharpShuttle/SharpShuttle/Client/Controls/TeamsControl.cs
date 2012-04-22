using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Client.Forms.ClientWizard.TournamentInitializationWizard;
using Client.Forms.PopUpWindows;
using Shared.Algorithms;
using Shared.Communication.Serials;
using Shared.Views;
using Shared.Domain;
using Shared.Data;
using Shared.Communication.Exceptions;

namespace Client.Controls
{
	/// <summary>
	/// Businesslogica voor het indelen van teams
	/// </summary>
	class TeamsControl : ISerialFilter
	{
		/// <summary>
		/// De datacache
		/// </summary>
		private IDataCache cache;

		/// <summary>
		/// Alle poules
		/// </summary>
		private PouleViews allPoules;

		/// <summary>
		/// Alle dubbele poules
		/// </summary>
		private PouleViews doublePoules;

		/// <summary>
		/// Voor elke poule een lijst met de spelers in die poule
		/// </summary>
		private Dictionary <int, List <Player>> allPlayersPerPoule;

		/// <summary>
		/// Voorelke poule een lijst met de teams in die poule
		/// </summary>
		private Dictionary <int, TeamViews> teams;

		public event ArrangeTeamsStep.ReloadEvent ReloadEvent;

		#region Constructor & Initialisatie

		/// <summary>
		/// Default constructor
		/// </summary>
		public TeamsControl()
        {
            cache = DataCache.Instance;
        }

		/// <summary>
		/// Haalt data uit de database en start met het indelen van teams
		/// </summary>
		public void Initialize()
		{
			try
			{
				allPoules = new PouleViews(cache.GetAllPoules());

				allPlayersPerPoule = new Dictionary<int, List<Player>>();
				teams = new Dictionary<int, TeamViews>();

				foreach (PouleView p in allPoules)
				{
					allPlayersPerPoule[p.Id] = new List<Player>(cache.GetPoulePlanning(p.Id));
					teams[p.Id] = new TeamViews(cache.GetPouleTeams(p.Id));
				}

				ArrangeAllTeams();
			}

			catch (CommunicationException exc)
			{
				CatchCommunicationExceptions.Show(exc);
			}
		}

		/// <summary>
		/// Om tijdens een toernooi een team indeling te maken moet er een 1 team lijst gemaakt worden
		/// </summary>
		public void CreatePouleTeams(PlayerViews players, PouleView poule)
		{
			allPoules = new PouleViews();
			allPoules.Add(poule);

			doublePoules = new PouleViews();
			doublePoules.Add(poule);

			allPlayersPerPoule = new Dictionary <int, List <Player>> ();
			allPlayersPerPoule[0] = new List<Player>();

			foreach (PlayerView p in players)
			{
				allPlayersPerPoule[0].Add(p.Domain);
			}

			teams = new Dictionary <int, TeamViews> ();
			teams[0] = new TeamViews();

			ArrangeAllTeams();
		}

		#endregion

		#region Return Weergaves

		/// <summary>
		/// Geeft alle dubbelPoules terug in een PouleViews
		/// </summary>
		/// <returns> alle dubbelPoules </returns>
		public PouleViews GetDoublePoules()
		{
			if (allPoules == null)
				Initialize();
			
			doublePoules = new PouleViews();

			foreach (PouleView p in allPoules)
			{
				if (p.DisciplineEnum == Poule.Disciplines.FemaleDouble ||
				    p.DisciplineEnum == Poule.Disciplines.MaleDouble ||
					p.DisciplineEnum == Poule.Disciplines.Mixed ||
					p.DisciplineEnum == Poule.Disciplines.UnisexDouble)
				{
					if (allPlayersPerPoule.ContainsKey(p.Id))
						if (allPlayersPerPoule[p.Id].Count > 0)
							doublePoules.Add(p);
				}
			}

			return doublePoules;
		}

		/// <summary>
		/// Geeft gegeven een pouleID 2 lijsten terug 1 playerviews voor Teamlid 1 en 
		/// PlayerViews2 voor Teamlid 2
		/// </summary>
		/// <param name="pouleID"> Het poule ID </param>
		/// <param name="players2">out parameter voor de playerviews van speler 2</param>
		/// <returns> De playerviews van speler 1 </returns>
		public PlayerViews GetTeamPlayers(int pouleID, out PlayerViews players2)
		{
			PlayerViews playerViews1 = new PlayerViews();
			PlayerViews playerViews2 = new PlayerViews();

			if (doublePoules.isPoule(pouleID))
			{
				foreach (TeamView t in teams[pouleID])
				{
					playerViews1.Add(new PlayerView(t.Player1));
					playerViews2.Add(new PlayerView(t.Player2));
				}
			}

			players2 = playerViews2;
			return playerViews1;
		}

		/// <summary>
		/// Geeft de teams terug van een poule
		/// </summary>
		/// <param name="pouleID"></param>
		/// <returns></returns>
		public TeamViews GetTeams(int pouleID)
		{
			return teams[pouleID];
		}

		#endregion

		#region Verwissel TeamSpelers

		/// <summary>
		/// Wisselt 2 spelers van team of van plaats in het team
		/// </summary>
		/// <param name="PouleID"></param>
		/// <param name="fromList">true voor spelerlijst 1, false voor spelerlijst 2</param>
		/// <param name="toList">true voor spelerlijst 1, false voor spelerlijst 2</param>
		/// <param name="fromIndex">index van het team</param>
		/// <param name="toIndex">index van het team</param>
		public void SwitchPlayers(int PouleID, bool fromList, bool toList, int fromIndex, int toIndex)
		{
			TeamView fromTeam = teams[PouleID][fromIndex];
			TeamView toTeam = teams[PouleID][toIndex];

			if (fromList && toList)// allebei speler1 lijst
			{
				Player fromPlayer = fromTeam.Player1;
				Player toPlayer = toTeam.Player1;

				fromTeam.Player1 = toPlayer;
				toTeam.Player1 = fromPlayer;
				setDefaultName(fromTeam);
				setDefaultName(toTeam);
			}
			else if (!fromList && !toList)// allebei in speler2 lijst
			{
				Player fromPlayer = fromTeam.Player2;
				Player toPlayer = toTeam.Player2;

				fromTeam.Player2 = toPlayer;
				toTeam.Player2 = fromPlayer;
				setDefaultName(fromTeam);
				setDefaultName(toTeam);
			}
			else if (fromList && !toList)//1 in speler1lijst 1 in speler2 lijst
			{
				Player fromPlayer = fromTeam.Player1;
				Player toPlayer = toTeam.Player2;

				fromTeam.Player1 = toPlayer;
				toTeam.Player2 = fromPlayer;
				setDefaultName(fromTeam);
				setDefaultName(toTeam);
			}
			else if (!fromList && toList)
			{
				Player fromPlayer = fromTeam.Player2;
				Player toPlayer = toTeam.Player1;

				fromTeam.Player2 = toPlayer;
				toTeam.Player1 = fromPlayer;
				setDefaultName(fromTeam);
				setDefaultName(toTeam);
			}
		}

		#endregion

		#region Indelen van Teams

		/// <summary>
		/// Maakt van de spelers in de poules teams
		/// </summary>
		private void ArrangeAllTeams()
		{
			if (allPoules.Count <= 0) return;
			
			foreach (PouleView p in allPoules)
			{
				if (p.DisciplineEnum == Poule.Disciplines.FemaleSingle || p.DisciplineEnum == Poule.Disciplines.MaleSingle
						|| p.DisciplineEnum == Poule.Disciplines.UnisexSingle)
					ArrangeSingleTeams(p);
				else ArrangeDoubleTeamsProposal(p);
			}
		}

		/// <summary>
		/// Maakt voor elke enkel speler een team aan met die speler erin.
		/// </summary>
		/// <param name="poule"></param>
		private void ArrangeSingleTeams(PouleView poule)
		{
			List<Player> unteamedPlayers = CheckValidTeams(poule, false);

			if (unteamedPlayers.Count <= 0) return;

			foreach (Player pl in unteamedPlayers)
			{
				teams[poule.Id].Add(new TeamView(new Team(pl.Name, pl)));
			}
			
			Shared.Logging.Logger.Write("Enkelspeler wordt Team", String.Format("Teamskoppeling voor EnkelPoule {0}", poule.Id));
		}

		/// <summary>
		/// Geeft een voorstel via de algoritmes van een teamindeling
		/// </summary>
		/// <param name="poule"> De poule waar teams ingedeeld worden </param>
		private void ArrangeDoubleTeamsProposal (PouleView poule)
		{
			List<Player> unteamedPlayers = CheckValidTeams(poule, true);

			if (unteamedPlayers.Count <= 0) return;

			if (poule.DisciplineEnum == Poule.Disciplines.Mixed)
			{
				foreach (Team t in Algorithms.MatchMixedDoubles(unteamedPlayers))
					teams[poule.Id].Add(new TeamView(t));
		
				Shared.Logging.Logger.Write("MatchMixedDoubles Algoritme", String.Format("Algoritme aanroep van de MatchMixedDoubles voor poule {0}", poule.Id));
			}

			else
			{
				foreach (Team t in Algorithms.MatchDoubles(unteamedPlayers))
					teams[poule.Id].Add(new TeamView(t));

				Shared.Logging.Logger.Write("MatchDoubles Algoritme", String.Format("Algoritme aanroep van de MatchDoubles voor poule {0}", poule.Id));
			}
		}

		#endregion

		#region Opslaan Teams

		/// <summary>
		/// Het opslaan van de teams in de database
		/// </summary>
		public void SaveAllTeams()
		{
			CommunicationException exc;

			foreach (PouleView p in allPoules)
			{
				if (cache.SetPouleTeams(p.Id, teams[p.Id].GetChangeTrackingList(), out exc))
					teams[p.Id] = new TeamViews (cache.GetPouleTeams(p.Id));

				else
				{
					if (CatchCommunicationExceptions.Show(exc) == CatchCommunicationExceptions.ActionResult.Reload)
					{
						ReloadEvent(true);
					}
				}
			}
		}

		#endregion

		#region Hulpmethodes

		/// <summary>
		/// Checkt of een team al bestaat en verwijdert de spelers dan uit de spelerslijst.
		/// Ook wordt er gekeken of een team bestaat uit leden die in de poule zijn geplanned,
		/// Als er een teamlid is dat niet in het team zit wordt het team verwijderd.
		/// </summary>
		/// <param name="poule">de poule</param>
		/// <param name="isDouble">bool of de poule een dubbelpoule is</param>
		/// <returns></returns>
		private List<Player> CheckValidTeams(PouleView poule, bool isDouble)
		{
			List <Player> players = new List<Player>(allPlayersPerPoule[poule.Id]);
			TeamViews pouleTeams = teams[poule.Id];

			if (players.Count < 1)
			{
				teams[poule.Id].Clear();
			}

			else if (pouleTeams.Count > 0)
			{
				for (int i = 0; i < pouleTeams.Count; i++)
				{
					if (!isDouble)
					{
						if (!PlayerIdInList(pouleTeams[i].Player1.PlayerID, poule))
						{
							teams[poule.Id].Remove(pouleTeams[i]);
							i--; //anders skip je 1 team, namelijk
							//het team dat eerst op i+1 stond en nu op i
						}
						else players.RemoveAll(item => item.PlayerID == pouleTeams[i].Player1.PlayerID);
					}
					else
					{
						if (!PlayerIdInList(pouleTeams[i].Player1.PlayerID, poule) ||
							!PlayerIdInList(pouleTeams[i].Player2.PlayerID, poule))
						{
							teams[poule.Id].Remove(pouleTeams[i]);
							i--;
						}

						if (PlayerIdInList(pouleTeams[i].Player1.PlayerID, poule) &&
							PlayerIdInList(pouleTeams[i].Player2.PlayerID, poule))
						{
							players.RemoveAll(item => item.PlayerID == pouleTeams[i].Player1.PlayerID);
							players.RemoveAll(item => item.PlayerID == pouleTeams[i].Player2.PlayerID);
						}
					}
				}
			}

			return players;
		}

		/// <summary>
		/// Gegeven een PlayerId en een poule wordt er gekeken of het PlayerId in de poule voorkomt
		/// </summary>
		/// <param name="playerId"> De player ID</param>
		/// <param name="poule"> De poule </param>
		/// <returns> Een bool die aangeeft of de speler in de poule voorkomt</returns>
		private bool PlayerIdInList(int playerId, PouleView poule)
		{
			foreach (Player pl in allPlayersPerPoule[poule.Id])
			{
				if (playerId == pl.PlayerID)
					return true;
			}

			return false;
		}

		/// <summary>
		/// Zet de standaard naam van een team
		/// </summary>
		/// <param name="team"></param>
		private static void setDefaultName(TeamView team)
		{
			team.Name = team.Player1.Name + " + " + team.Player2.Name;
		}

		/// <summary>
		/// Geeft een boolean terug of er een team is veranderd.
		/// </summary>
		/// <returns></returns>
		public bool isChanged()
		{
			if (teams != null)
				foreach (int id in teams.Keys)
					if (teams[id].GetChangeTrackingList().Changed)
						return true;

			return false;
		}

		/// <summary>
		/// Checkt of een gegeven poule een mixed poule is.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public bool IsMixed(int id)
		{
			foreach (PouleView p in allPoules)
			{
				if (p.Id == id && p.DisciplineEnum == Poule.Disciplines.Mixed)
					return true;
			}

			return false;
		}

		#endregion

		#region NotificatieControl

		public FilterFlags FilterFlags { get { return (FilterFlags.UpdatePoulePlanningEvent | FilterFlags.UpdateAllPoulesEvent | FilterFlags.UpdatePouleEvent); } }

		public bool UpdateAllPoulesEvent(SerialEventTypes SerialEvent) { return true; }

		public bool UpdateAllPlayersEvent(SerialEventTypes SerialEvent) { return false; }

		public bool UpdateAllMatchesEvent(SerialEventTypes SerialEvent) { return false; }

		public bool UpdateAllHistoryMatchesEvent(SerialEventTypes SerialEvent) { return false; }

		public bool UpdatePouleEvent(int PouleID, SerialEventTypes SerialEvent) { return true; }

		public bool UpdatePoulePlanningEvent(int PouleID, SerialEventTypes SerialEvent) { return false; }

		public bool UpdatePouleTeamsEvent(int PouleID, SerialEventTypes SerialEvent) { return false; }

		public bool UpdatePouleLadderEvent(int PouleID, SerialEventTypes SerialEvent) { return false; }

		public bool UpdatePouleMatchesEvent(int PouleID, SerialEventTypes SerialEvent) { return false; }

		public bool UpdateMatchEvent(int MatchID, SerialEventTypes SerialEvent) { return false; }
		
		public bool UpdateSettings(SerialEventTypes SerialEvent){ return false; }

		#endregion


	}
}