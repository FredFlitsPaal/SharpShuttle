using System;
using System.Windows.Forms;

namespace Client.Forms.PrintDialog
{
	/// <summary>
	/// Ranking of ranking en matches
	/// </summary>
	public enum PrintWhat 
	{
		/// <summary>
		/// Ranking
		/// </summary>
		Ranking,
		/// <summary>
		/// ranking en matches
		/// </summary>
		RankingAndMatches
	}

	/// <summary>
	/// Scherm waarop de gebruiker kan kiezen wat hij wil printen
	/// </summary>
	public partial class PrintDialogForm : Form
	{
		/// <summary>
		/// Wat er geprint moet worden
		/// </summary>
		public PrintWhat choice;

		/// <summary>
		/// Default constructor
		/// </summary>
		public PrintDialogForm()
		{
			InitializeComponent();
		}

		#region Event Handlers

		/// <summary>
		/// Update de radiobuttons
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonScores_CheckedChanged(object sender, EventArgs e)
		{
			if (radRankings.Checked)
			{
				radBoth.Checked = false;
			}
		}

		/// <summary>
		/// Update de radiobuttons
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonBoth_CheckedChanged(object sender, EventArgs e)
		{
			if (radBoth.Checked)
			{
				radRankings.Checked = false;
			}
		}

		/// <summary>
		/// Sluit het scherm
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		/// <summary>
		/// Voert de printkeuze door
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnPrint_Click(object sender, EventArgs e)
		{
			if (radRankings.Checked)
				choice = PrintWhat.Ranking;
			else choice = PrintWhat.RankingAndMatches;
		}

		#endregion
	}
}
