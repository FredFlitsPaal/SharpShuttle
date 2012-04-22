using System;
using System.Drawing;
using UserControls.AbstractControls;
using Shared.Views;
using Shared.Domain;

namespace UserControls.PlayerControls
{
	/// <summary>
	/// Een Speler User Control
	/// </summary>
	public partial class PlayerUserControl : DomainUserControl<PlayerViews, PlayerView, Player>
	{
		/// <summary>
		/// De speler die de control omvat
		/// </summary>
		private Player player;

		/// <summary>
		/// De methode die aangeropen wordt als de spelerlijst geopend is
		/// </summary>
		/// <param name="player"></param>
		/// <param name="control"></param>
		public delegate void PlayerSelect(Player player, PlayerUserControl control);
		/// <summary>
		/// Event die aangeeft dat de spelerlijst geopend is
		/// </summary>
		public event PlayerSelect OpenPlayersList;

		/// <summary>
		/// Default constructor
		/// </summary>
		public PlayerUserControl()
		{
			InitializeComponent();
		}

		/// <summary>
		/// De speler die de control omvat
		/// </summary>
		public Player Player
		{
			get { return player; }
			set
			{
				player = value;
				if (player != null)
					tbPlayer.Text = player.Name;
			}
		}

		/// <summary>
		/// Zet hoeveelste speler dit is
		/// </summary>
		/// <param name="number"></param>
		public void SetPlayerNumber(int number)
		{
			lbPlayer.Text = "Speler " + number + ":";
		}

		/// <summary>
		/// Zet dit team op inactief
		/// </summary>
		public void SetInactive()
		{
			tbPlayer.BackColor = SystemColors.Control;
			btnPlayer.Enabled = false;
		}

		/// <summary>
		/// Haal een lijst van spelers zien
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnPlayer_Click(object sender, EventArgs e)
		{
			if (OpenPlayersList != null)
				OpenPlayersList(player, this);
		}
	}
}