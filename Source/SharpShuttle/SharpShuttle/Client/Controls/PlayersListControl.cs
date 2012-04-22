using Client.Forms.PopUpWindows;
using Shared.Data;
using Shared.Views;
using Shared.Communication.Exceptions;
using Shared.Domain;

namespace Client.Controls
{
	/// <summary>
	/// Businesslogica voor het beheren van spelers in poules
	/// </summary>
	public class PlayersListControl
	{

		/// <summary>
		/// De datacache
		/// </summary>
		private IDataCache cache;

		/// <summary>
		/// Default constructor, haalt de datacache op
		/// </summary>
		public PlayersListControl()
		{
			cache = DataCache.Instance;
		}

		#region Ophalen van spelers
		/// <summary>
		/// Haalt alle spelers op van de datacache
		/// </summary>
		/// <returns>Alle spelers</returns>
		private PlayerViews getPlayers()
		{
			PlayerViews players = new PlayerViews();
			try
			{
				players = new PlayerViews(cache.GetAllPlayers());
			}
			catch (CommunicationException exc)
			{
				CatchCommunicationExceptions.Show(exc);
			}
			
			return players;
		}

		/// <summary>
		/// Haal alle spelers op
		/// </summary>
		/// <param name="exludedPlayers">Spelers die je niet in de lijst wilt hebben</param>
		/// <returns>Lijst van alle spelers</returns>
		public PlayerViews GetAllPlayers(PlayerViews exludedPlayers)
		{
			PlayerViews players = getPlayers();
			RemoveExludedPlayers(players, exludedPlayers);
			return players;
		}

		/// <summary>
		/// Haal alle vrouwelijke spelers op
		/// </summary>
		/// <param name="exludedPlayers">Spelers die je niet in de lijst wilt hebben</param>
		/// <returns>Lijst van alle vrouwelijke spelers</returns>
		public PlayerViews GetFemalePlayers(PlayerViews exludedPlayers)
		{
			PlayerViews players = new PlayerViews();

			foreach (PlayerView player in getPlayers())
				if (player.Gender == "V")
					players.Add(player);

			RemoveExludedPlayers(players, exludedPlayers);
			return players;
		}

		/// <summary>
		/// Haal alle mannelijke spelers op
		/// </summary>
		/// <param name="exludedPlayers">Spelers die je niet in de lijst wilt hebben</param>
		/// <returns>Lijst van alle mannelijke spelers</returns>
		public PlayerViews GetMalePlayers(PlayerViews exludedPlayers)
		{
			PlayerViews players = new PlayerViews();

			foreach (PlayerView player in getPlayers())
				if (player.Gender == "M")
					players.Add(player);

			RemoveExludedPlayers(players, exludedPlayers);
			return players;
		}

        /// <summary>
        /// Haal alle spelers op die in de poule zitten
        /// </summary>
        /// <param name="pouleid">Poule id</param>
        /// <returns>Alle poule spelers</returns>
		public PlayerViews GetPoulePlayers(int pouleid)
		{
			PlayerViews players = new PlayerViews();
			try
			{
				players = new PlayerViews(cache.GetPoulePlanning(pouleid));
			}
			catch (CommunicationException exc)
			{
				CatchCommunicationExceptions.Show(exc);
			}
			
			return players;
		}
		#endregion

		#region Opslaan van data
		/// <summary>
		/// Update de pouleplanning door oude spelers eruit te halen en nieuwe spelers erin te zetten
		/// </summary>
		/// <param name="pouleID"> Het ID van de poule </param>
		/// <param name="team">Het oude team</param>
		/// <param name="pl1">Nieuwe speler</param>
		/// <param name="pl2">Nieuwe speler</param>
		public void UpdatePoulePlanning(int pouleID, TeamView team, Player pl1, Player pl2)
		{
			PlayerViews players = GetPoulePlayers(pouleID);
			PlayerViews removeList = new PlayerViews();

			foreach (PlayerView player in players)
			{
				if (player.Id == team.Player1.PlayerID)
					removeList.Add(player);
				if (team.Player2 != null)
					if (player.Id == team.Player2.PlayerID)
						removeList.Add(player);
			}

			// Verwijder oude spelers uit de pouleplanning
			foreach (PlayerView player in removeList)
				players.Remove(player);

			// Voeg nieuwe spelers toe aan de pouleplanning
			players.Add(new PlayerView(pl1));
			if (pl2 != null)
				players.Add(new PlayerView(pl2));

			CommunicationException exc;
			if (!cache.SetPoulePlanning(pouleID, players.GetChangeTrackingList(), out exc))
			{
				CatchCommunicationExceptions.Show(exc);
			}
		}
		#endregion

		#region Hulp methodes
		/// <summary>
		/// Verwijdert de spelers die je niet in de alle spelers lijst wilt hebben
		/// </summary>
		/// <param name="allPlayers">Alle spelers</param>
		/// <param name="exludedPlayers">De ongewenste spelers</param>
		/// <returns>Alle spelers exclusief de ongewenste</returns>
		public PlayerViews RemoveExludedPlayers(PlayerViews allPlayers, PlayerViews exludedPlayers)
		{
			PlayerViews removeList = new PlayerViews();

			foreach(PlayerView player in allPlayers)
				foreach(PlayerView exlPlayer in exludedPlayers)
					if (player.Id == exlPlayer.Id)
						removeList.Add(player);

			foreach (PlayerView player in removeList)
				allPlayers.Remove(player);

			return allPlayers;
		}
		#endregion
	}
}
