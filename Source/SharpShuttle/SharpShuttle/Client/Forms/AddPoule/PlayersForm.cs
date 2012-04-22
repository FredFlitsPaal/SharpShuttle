using System;
using System.Windows.Forms;
using Shared.Sorters;
using Shared.Views;

namespace Client.Forms.AddPoule
{
	/// <summary>
	/// Scherm waar spelers gekozen kunnen worden om aan een poule toe te voegen
	/// </summary>
	public partial class PlayersForm : Form
	{
		/// <summary>
		/// Methode die aangeroepen wordt als de gebruiker op selecteer heeft geklikt
		/// </summary>
		/// <param name="players"></param>
		public delegate void SelectPlayerDone(PlayerViews players);

		/// <summary>
		/// Event dat gegenereerd wordt als de gebruiker op selecteer heeft geklikt
		/// </summary>
		public event SelectPlayerDone SelectDone;
		/// <summary>
		/// Op welke kolom de lijst het laatst is gesorteerd
		/// </summary>
		private int playersSort = -1;

		/// <summary>
		/// Start het scherm op met een lijst van spelers
		/// </summary>
		/// <param name="players"></param>
		public PlayersForm(PlayerViews players)
		{
			InitializeComponent();
			lvwPlayers.DataSource = players;
			lblAmount.Text = players.Count.ToString();
		}

		#region Buttons
		/// <summary>
		/// Start het SelectDone event met alle geselecteerde spelers
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSelect_Click(object sender, EventArgs e)
		{
			PlayerViews players = new PlayerViews();
			for (int i = 0; i < lvwPlayers.SelectedItems.Count; i++)
				players.Add((PlayerView)lvwPlayers.SelectedItems[i].Tag);

			if (SelectDone != null)
				SelectDone(players);
			Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		/// <summary>
		/// Het sorteren van de spelerslijst per kolom.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lvwPoulePlayers_ColumnClick(object sender, ColumnClickEventArgs e)
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
