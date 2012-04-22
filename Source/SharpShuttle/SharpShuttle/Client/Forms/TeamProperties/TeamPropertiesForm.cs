using System;
using System.Windows.Forms;
using Shared.Views;
using UserControls.PlayerControls;
using Shared.Domain;
using Client.Controls;

namespace Client.Forms.TeamProperties
{
	/// <summary>
	/// Scherm waar gegevens van een team gewijzigd kan worden en een team
	/// inactief gemaakt kan worden
	/// </summary>
	public partial class TeamPropertiesForm : Form
	{
		/// <summary>
		/// Het team dat gewijzigd wordt
		/// </summary>
		private TeamView editTeam;
		/// <summary>
		/// Is dit team inactief
		/// </summary>
		private bool inactive = true;
		/// <summary>
		/// De poule waar het team in speelt
		/// </summary>
		private PouleView poule;
		/// <summary>
		/// Is het een geslachtspecifiek team
		/// </summary>
		private bool fixedGender = true;
		/// <summary>
		/// Business logica
		/// </summary>
		private PlayersListControl control = new PlayersListControl();
		/// <summary>
		/// Business logica
		/// </summary>
		private PlayerUserControl usedPlayerControl;
		/// <summary>
		/// Methode die aangeroepen wordt als het wijzigen van het team klaar is
		/// </summary>
		public delegate void TeamEditDone();
		/// <summary>
		/// Event die aangeeft dat het wijzigen van het team klaar is
		/// </summary>
		public event TeamEditDone EditDone;

		#region Constructor
		/// <summary>
		/// Maakt een scherm gegeven een view van het team en de
		/// poule waar het team in zit
		/// </summary>
		/// <param name="poule"></param>
		/// <param name="team"></param>
		public TeamPropertiesForm(PouleView poule, TeamView team)
		{
			InitializeComponent();
			editTeam = team;
			inactive = team.Domain.IsInOperative;
			this.poule = poule;
			checkPoule(poule);
			fillForm();
			playerPanel1.OpenPlayersList += openPlayersList;
			playerPanel2.OpenPlayersList += openPlayersList;
		}

		#endregion

		#region Hulpmethodes
		/// <summary>
		/// Haal de andere spelers op die in deze poule zitten.
		/// </summary>
		/// <param name="player">De speler waarvan je zijn medespelers zoekt</param>
		/// <returns>Alle andere spelers</returns>
		private PlayerViews getOtherPlayers(Player player)
		{
			PlayerViews players = control.GetPoulePlayers(poule.Id);

            // Huidige speler hoort niet in de otherplayers lijst
			PlayerView playerView = null;
			foreach (PlayerView pl in players)
				if (pl.Id == player.PlayerID)
					playerView = pl;
			if (playerView != null)
				players.Remove(playerView);

			return players;
		}

		/// <summary>
		/// Bepaal of de nieuwe spelers van het andere geslacht mogen zijn 
		/// </summary>
		/// <param name="poule"></param>
		private void checkPoule(PouleView poule)
		{
			if (poule.DisciplineEnum == Poule.Disciplines.UnisexDouble ||
				poule.DisciplineEnum == Poule.Disciplines.UnisexSingle)
				fixedGender = false;
		}

		/// <summary>
		/// Opent de lijst van spelers
		/// </summary>
		/// <param name="player"></param>
		/// <param name="plControl"> </param>
		private void openPlayersList(Player player, PlayerUserControl plControl)
		{
			usedPlayerControl = plControl;
			PlayersListForm playersList;
			if (fixedGender)
				if (player.Gender == "M")
				{
					PlayerViews players = control.GetMalePlayers(getOtherPlayers(player));
					playersList = new PlayersListForm(players, player);
				}
				else
					playersList = new PlayersListForm(control.GetFemalePlayers(getOtherPlayers(player)), player);
			else
				playersList = new PlayersListForm(control.GetAllPlayers(getOtherPlayers(player)), player);
			playersList.SelectDone += new PlayersListForm.PlayerSelectDone(setPlayer);
			playersList.ShowDialog(this);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="player"></param>
		private void setPlayer(Player player)
		{
			usedPlayerControl.Player = player;
		}

		/// <summary>
		/// Vult dit scherm met de gegevens van het team
		/// </summary>
		private void fillForm()
		{
			if (inactive)
				setInactive();
			lblId.Text = "Team: " + editTeam.TeamId;
			txtTeamName.Text = editTeam.Name;

			playerPanel1.Player = editTeam.Player1;
			playerPanel1.SetPlayerNumber(1);

			playerPanel2.Player = editTeam.Player2;
			playerPanel2.SetPlayerNumber(2);
			if (editTeam.Player2 == null)
				playerPanel2.Visible = false;
		}

		/// <summary>
		/// Maak het team inactief
		/// </summary>
		private void setInactive()
		{
			inactive = true;
			playerPanel1.SetInactive();
			playerPanel2.SetInactive();
		}

		#endregion

		#region Buttons
		/// <summary>
		/// Voert wijzigingen van het team door
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOk_Click(object sender, EventArgs e)
		{
			if (editTeam.Player1.PlayerID != playerPanel1.Player.PlayerID && (playerPanel2.Player == null || editTeam.Player2.PlayerID != playerPanel2.Player.PlayerID))
				control.UpdatePoulePlanning(poule.Id, editTeam, playerPanel1.Player, playerPanel2.Player);
			if (editTeam.Player1.PlayerID != playerPanel1.Player.PlayerID)
				editTeam.Player1 = playerPanel1.Player;
			if (playerPanel2.Player != null)
				if (editTeam.Player2.PlayerID != playerPanel2.Player.PlayerID)
					editTeam.Player2 = playerPanel2.Player;
			if (inactive)
				editTeam.Domain.SetInOperative();
			if (editTeam.Name != txtTeamName.Text)
				editTeam.Name = txtTeamName.Text;
			if (editTeam.Domain.Changed)
				if (EditDone != null)
					EditDone();
			Close();
		}

		/// <summary>
		/// Sluit venster zonder wijzigingen op te slaan
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		/// <summary>
		/// Maak dit team inactief
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnNonActive_Click(object sender, EventArgs e)
		{
			setInactive();
		}
		#endregion
	}
}
