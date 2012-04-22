using System;
using System.Windows.Forms;
using Client.Controls;
using Shared.Views;
using UserControls.Docking;
using Shared.Communication.Serials;
using Shared.Sorters;

namespace Client.Forms.ScoresInput
{
	/// <summary>
	/// Een scherm met een overzicht van alle huidige wedstrijden en bijbehorende scores
	/// </summary>
	public partial class ScoreForm : BasicDockForm
	{
		/// <summary>
		/// Business logica
		/// </summary>
		private ManageScoreControl control;

		/// <summary>
		/// Op welke kolom de lijst het laatst is gesorteerd
		/// </summary>
		private int matchesSort = -1;

        #region Constructor

		/// <summary>
		/// Default constructor
		/// Subscribet zich voor PouleUpdated, AllMatchesUpdated en AllHistoryMatchesUpdated
		/// </summary>
        public ScoreForm()
        {
            InitializeComponent();

            SerialTracker.Instance.PouleUpdated += instance_PouleUpdated;
			SerialTracker.Instance.AllMatchesUpdated += instance_MatchesUpdated;
			SerialTracker.Instance.AllHistoryMatchesUpdated += instance_MatchesUpdated;

			control = new ManageScoreControl();
			updateData();
		}

        #endregion

		#region Data
		
		/// <summary>
		/// Update zijn data en daarmee de GUI
		/// </summary>
		private void updateData()
		{
			control.UpdateMatches();
			updateMatchList();
		}

		#endregion

		#region MatchList

		/// <summary>
		/// Update de GUI wedstrijdlijst
		/// </summary>
		private void updateMatchList()
        {
			bool checkSearchBox = false;
			bool isLookingFor = false;
			bool matchWithScore = false;

			if (!txtSearch.Text.Equals("Zoeken..."))
				checkSearchBox = true;

			MatchViews matches = new MatchViews();

			foreach (MatchView match in control.Matches)
			{
				if (checkSearchBox)
					isLookingFor = (
						match.MatchID.Contains(txtSearch.Text) ||
						match.Team1.ToLower().Contains(txtSearch.Text.ToLower()) ||
						match.Team2.ToLower().Contains(txtSearch.Text.ToLower()) ||
						match.Status.ToLower().Contains(txtSearch.Text.ToLower()) ||
						match.PouleName.ToLower().Contains(txtSearch.Text.ToLower()) ||
						match.Score.ToLower().Contains(txtSearch.Text.ToLower())
					);

				if (!checkSearchBox || isLookingFor)
				{
					matches.Add(match);

					if (!matchWithScore && match.Domain.Played)
						matchWithScore = true;
				}
			}

			lvwMatches.DataSource = matches;

			btnSetScore.Enabled = lvwMatches.SelectedItems.Count > 0;

        }

        #endregion

        #region Buttons

		/// <summary>
		/// Opent een ScoreEditForm waarin de score van de geselecteerde wedstrijd
		/// ingevuld of aangepast kan worden
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void btnSetScore_Click(object sender, EventArgs e)
        {
			if (lvwMatches.SelectedItems.Count == 1)
			{
				MatchView match = (MatchView)lvwMatches.SelectedItems[0].Tag;
				//Voer alleen een score in bij gestarte wedstrijd, of nadat de gebruiker geinformeerd is.
				if (match.StartTime.Length != 0 || match.Domain.Played ||
						DialogResult.Yes == MessageBox.Show("Deze wedstrijd is nog niet gestart, weet u zeker dat u een score in wilt vullen?",
						"Wedstrijd niet gestart", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
				{
					ScoreEditForm editForm = new ScoreEditForm(match, !match.Domain.Played);
					editForm.EditDone += editScoreForm_EditDone;
					editForm.ShowDialog(this);
				}
			}
        }

		/// <summary>
		/// Simuleer het klikken op de Score Invoeren knop alsop een wedstrijd gedubbelklikt
		/// wordt
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void listVMatches_DoubleClick(object sender, EventArgs e)
        {
			btnSetScore_Click(sender, e);
        }

		#endregion

		#region Events

		/// <summary>
		/// Het "Wedstrijden" label heeft standaard focus
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void scoreForm_Click(object sender, EventArgs e)
		{
			lblMatches.Focus();
		}

		/// <summary>
		/// Verwijder de "Zoeken..." text uit de searchbox als hij geselecteerd wordt
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtBSearch_GotFocus(object sender, EventArgs e)
		{
			if (txtSearch.Text.Equals("Zoeken..."))
				txtSearch.Text = "";
		}

		/// <summary>
		/// Als de searchbox leeg is wanneer hij gedeselecteerd wordt, laat dan de
		/// "Zoeken..." text weer verschijnen
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtBSearch_LostFocus(object sender, EventArgs e)
		{
			if (txtSearch.Text.Length == 0)
				txtSearch.Text = "Zoeken...";
		}

		/// <summary>
		/// Laat een nieuwe lijst zijn als de gebruiker in de searchbox typt
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtBSearch_TextChanged(object sender, EventArgs e)
		{
			updateMatchList();
		}

		/// <summary>
		/// Past de tekst op de score invoeren knop aan afhankelijk van of het om
		/// het invoeren van een nieuwe score of het wijzigen van een oude score gaat
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listVMatches_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lvwMatches.SelectedItems.Count == 1)
			{
				btnSetScore.Enabled = true;
				MatchView match = (MatchView)lvwMatches.SelectedItems[0].Tag;

				btnSetScore.Text = match.Domain.Played ? "Score Wijzigen" : "Score Invoeren";
			}
		}

		/// <summary>
		/// Sorteer de wedstrijden op geselecteerde kolom
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listVMatches_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			// Als de kolom nu een andere kolom is
			if (e.Column != matchesSort)
			{
				// Update welke kolom je hebt gesorteerd
				matchesSort = e.Column;

				// Zet het sorteren nu op oplopend
				lvwMatches.Sorting = SortOrder.Ascending;
			}
			else
			{
				// Als het dezelfde kolom was, kijk dan of hij
				// vorige keer oplopend was, en doe nu het tegenovergestelde
				if (lvwMatches.Sorting == SortOrder.Ascending)
					lvwMatches.Sorting = SortOrder.Descending;
				else
					lvwMatches.Sorting = SortOrder.Ascending;
			}

			// Laat de lijst sorteren
			lvwMatches.Sort();

			// Stel nu een nieuwe sorteerder in
			lvwMatches.ListViewItemSorter = new AllColumnSorter(e.Column, lvwMatches.Sorting);
		}

		/// <summary>
		/// Als van een wedstrijd de score is ingevoerd/aangepast, update dan de data
		/// en de GUI
		/// </summary>
		/// <param name="match"></param>
		private void editScoreForm_EditDone(MatchView match)
		{
			if (match != null)
			{
				if (match.EndTime.Length == 0)
				{
					match.Domain.EndTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
					match.Domain.Court = 0;
				}
				control.SetScore(match);
				updateData();
			}
		}

		private void instance_PouleUpdated(int PouleID, SerialEventTypes SerialEvent)
		{
			if (InvokeRequired)
				Invoke(new MethodInvoker(updateData));
			else
				updateMatchList();
		}

		/// <summary>
		/// Als de server een wijziging doorgeeft, update de GUI
		/// </summary>
		/// <param name="SerialEvent"></param>
		private void instance_MatchesUpdated(SerialEventTypes SerialEvent)
		{
			if (InvokeRequired)
				Invoke(new MethodInvoker(updateData));
			else
				updateMatchList();
		}

		/// <summary>
		/// Unsubscribe van PouleUpdated, AllMatchesUpdated en AllHistoryMatchesupdated
		/// bij het sluiten van het scherm
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void scoreForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			SerialTracker.Instance.PouleUpdated -= instance_PouleUpdated;
			SerialTracker.Instance.AllMatchesUpdated -= instance_MatchesUpdated;
			SerialTracker.Instance.AllHistoryMatchesUpdated -= instance_MatchesUpdated;
		}

		#endregion
	}
}
