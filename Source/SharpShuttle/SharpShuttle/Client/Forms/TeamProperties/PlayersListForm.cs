using System;
using System.Windows.Forms;
using Shared.Sorters;
using Shared.Views;
using Shared.Domain;

namespace Client.Forms.TeamProperties
{
	/// <summary>
	/// Scherm waar tijden het toernooi spelers toegevoegd/verwijderd/gewijzigd kunnen worden
	/// </summary>
	public partial class PlayersListForm : Form
	{
		/// <summary>
		/// De speler die veranderd moet worden
		/// </summary>
		private Player selectedPlayer;

		/// <summary>
		/// Methode die wordt aangeroepen als de gebruiker op selecteer heeft geklikt
		/// </summary>
		/// <param name="player"></param>
		public delegate void PlayerSelectDone(Player player);
		/// <summary>
		/// Event dat gegenereerd wordt als de gebruiker op selecteer heeft geklikt
		/// </summary>
		public event PlayerSelectDone SelectDone;
		/// <summary>
		/// Op welke kolom de lijst het laatst is gesorteerd
		/// </summary>
		private int playersSort = -1;

		/// <summary>
		/// Constructor die een lijst van spelers en een geselecteerde speler meekrijgt
		/// </summary>
		/// <param name="players"></param>
		/// <param name="player"></param>
		public PlayersListForm(PlayerViews players, Player player)
		{
			InitializeComponent();
			selectedPlayer = player;
			setPlayersList(players);
		}

		/// <summary>
		/// Bind de lijst van spelers aan de listview
		/// </summary>
		private void setPlayersList(PlayerViews players)
		{
			int scrollPos = lvwPlayers.ScrollPosition;
			lvwPlayers.DataSource = players;
			lvwPlayers.ScrollPosition = scrollPos;
			lvwPlayers.MultiSelect = false;
			lvwPlayers.HideSelection = false;
			for(int i = 0; i < lvwPlayers.Items.Count; i++)
				if (((PlayerView)lvwPlayers.Items[i].Tag).Id == selectedPlayer.PlayerID)
					lvwPlayers.Items[i].Selected = true;
			lvwPlayers.Select();
		}

		#region Buttons
		/// <summary>
		/// Gebruiker drukt op selecteren
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSelect_Click(object sender, EventArgs e)
		{
			if (SelectDone != null && lvwPlayers.SelectedItems.Count == 1)
			{
				SelectDone(((PlayerView) lvwPlayers.SelectedItems[0].Tag).Domain);
				Close();
			}
		}

		/// <summary>
		/// Gebruiker drukt op annuleren
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		/// <summary>
		/// Het sorteren van de spelerslijst per kolom.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lvwPlayers_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			// Als de kolom nu een andere kolom is
			if (e.Column != playersSort)
			{
				// Update welke kolom je hebt gesorteerd
				playersSort = e.Column;

				// Zet het sorteren nu op oplopend
				lvwPlayers.Sorting = SortOrder.Ascending;
			}
			else
			{
				// Als het dezelfde kolom was, kijk dan of hij
				// vorige keer oplopend was, en doe nu het tegenovergestelde
				if (lvwPlayers.Sorting == SortOrder.Ascending)
					lvwPlayers.Sorting = SortOrder.Descending;
				else
					lvwPlayers.Sorting = SortOrder.Ascending;
			}

			// Laat de lijst sorteren
			lvwPlayers.Sort();

			// Stel nu een nieuwe sorteerder in
			lvwPlayers.ListViewItemSorter = new AllColumnSorter(e.Column, lvwPlayers.Sorting);
		}
		#endregion
	}
}
