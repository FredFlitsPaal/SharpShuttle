using System;
using System.Windows.Forms;
using Client.Controls;
using Shared.Logging;
using Shared.Views;
using UserControls.Docking;
using Shared.Sorters;
using Shared.Communication.Serials;
using Client.Forms.TeamProperties;
using Client.Forms.ScoresInput;
using System.Drawing;
using Client.Forms.PrintDialog;

namespace Client.Forms.PouleInformation
{
	/// <summary>
	/// Het poule overzicht scherm
	/// </summary>
    public partial class PouleInformationForm : BasicDockForm
    {
        /// <summary>
		/// Boolean in welke modus het scherm is (simpel/uitgebreid)
        /// </summary>
        bool simple = true;

		/// <summary>
		/// updating staat op true als UpdateControls wordt uitgevoerd.
		/// </summary>
		bool updating = false;

        /// <summary>
        /// De controller die alle data betreffende de poule bijhoudt
        /// </summary>
        PouleControl data = new PouleControl();
		TeamListViewSorter sorter = new TeamListViewSorter();
    	private PouleViews pv;

		private PouleInformationActionControl actionControl = new PouleInformationActionControl();

        #region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
        public PouleInformationForm()
        {
            InitializeComponent();

			lvwTeams.ListViewItemSorter = sorter;
			lvwTeamsSimple.ListViewItemSorter = sorter;

			func_SetDefaults();
            UpdateControls();

            updateMode();

			


			SerialTracker.Instance.PouleMatchesUpdated += Instance_PouleMatchesUpdated;
			SerialTracker.Instance.PouleUpdated += Instance_PouleUpdated;
			SerialTracker.Instance.PouleLadderUpdated += Instance_PouleLadderUpdated;
        }

		/// <summary>
		/// Update de wedstrijden van de poule
		/// </summary>
		/// <param name="pouleID"></param>
		/// <param name="serialEvent"></param>
		void Instance_PouleMatchesUpdated(int pouleID, SerialEventTypes serialEvent)
		{
			//Kijken of de pouleid klopt
			if (pouleID != data.SelectedPouleID) return;

			data.LoadMatches();
			if (InvokeRequired)
				Invoke(new MethodInvoker(UpdateControls));
			else
				UpdateControls();
		}

        #endregion

		#region Sluiten

		/// <summary>
		/// Zorg ervoor dat we niet meer in de SerialTracker geregistreerd staan
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PouleInformationForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			SerialTracker.Instance.PouleMatchesUpdated -= Instance_PouleMatchesUpdated;
			SerialTracker.Instance.PouleUpdated -= Instance_PouleUpdated;
			SerialTracker.Instance.PouleLadderUpdated -= Instance_PouleLadderUpdated;
		}

		#endregion

		#region Data/Controls

		/// <summary>
		/// Stelt de standaard functionaliteiten van het scherm in.
		/// </summary>
		public void func_SetDefaults()
		{
			btnPrintLadder.Enabled = actionControl.CheckPrintRankings();
			cmsItemEditTeam.Enabled = actionControl.CheckEditTeam();
			cmsItemPrintAgain.Enabled = actionControl.CheckAddMatchPapers();
			cmsItemSetScore.Enabled = actionControl.CheckEditScore();
			if (!actionControl.CheckEditScore() )
				lvwMatches.DoubleClick -= new System.EventHandler(matchListView_wedstrijden_DoubleClick);
		}

		/// <summary>
		/// Update alle GUI elementen
		/// </summary>
		public void UpdateControls()
        {
			updating = true;

			//Verander de inhoud van de poulelijst
			pv = data.GetPoules();
			cboPoules.DataSource = pv;

			//Als er nog geen item is geselecteerd dan proberen we de eerste poule te selecteren
			if (cboPoules.SelectedIndex == -1 || data.SelectedPouleID == -1)
				if (cboPoules.Items.Count > 0)
				{
					cboPoules.SelectedIndex = 0;
					data.SelectedPouleID = cboPoules.SelectedValue;
				}

			cboPoules.SelectedValue = data.SelectedPouleID;

			LadderTeamViews ladderTeams = data.GetLadderTeams();
            lvwTeams.DataSource = ladderTeams;
			lvwTeamsSimple.DataSource = ladderTeams;

            lvwMatches.DataSource = data.GetMatches(cb_allmatches.Checked);

			if (data.GetPoule(data.SelectedPouleID) != null)
				lblRound.Text = "Ronde " + data.GetPoule(data.SelectedPouleID).Domain.Round;
			else
				lblRound.Text = "Ronde " + 0;

			// Laat zien dat dit team inactief is
			foreach (ListViewItem it in lvwTeams.Items)
				if (lvwTeams.DataSource[it.Index].Domain.Team.IsInOperative)
					it.BackColor = Color.Pink;
			foreach (ListViewItem it in lvwTeamsSimple.Items)
				if (lvwTeamsSimple.DataSource[it.Index].Domain.Team.IsInOperative)
					it.BackColor = Color.Pink;

			// Controleer of de knop volgende ronde enabled mag zijn
			bool nextEnabled = true;
			bool previousEnabled = true;
			foreach (MatchView match in data.GetMatches(true))
				if (!(match.Domain.Played || match.Domain.Disabled))
					nextEnabled = false;
				else if (match.Domain.Played && !match.Domain.Disabled)
					previousEnabled = false;

			btnNextRound.Enabled = (nextEnabled && actionControl.CheckNextRound());
			btnRemoveMatches.Enabled = (previousEnabled && actionControl.CheckUndoRound());

			updating = false;
        }

		/// <summary>
		/// Update GUI elementen naar het wisselen tussen simpele en uitgebreide mode
		/// </summary>
        private void updateMode()
        {
            lvwTeams.Visible = !simple;
            lvwTeamsSimple.Visible = simple;
            spcTeamsMatches.Panel2Collapsed = simple;
			btnNextRound.Visible = !simple;
			btnRemoveMatches.Visible = !simple;
        	btnPrintLadder.Visible = !simple;
            if (simple)
            {
                btnDetail.Text = "Uitgebreid >>";
            }
            else
            {
                btnDetail.Text = "<< Simpel";
            }
        }
		
		/// <summary>
		/// Sla de score van een wedstrijd op
		/// </summary>
		/// <param name="match"> De wedstrijd </param>
		public void SaveScore(MatchView match)
		{
			data.SetScore(match);
			UpdateControls();
		}
        #endregion

        #region Buttons

		/// <summary>
		/// Open de printdialoog als de gebruiker op de printknop drukt
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnPrintLadder_Click(object sender, EventArgs e)
		{
			PrintDialogForm dialog = new PrintDialogForm();
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				if (dialog.choice == PrintWhat.Ranking)
					data.PrintLadder();
				else if (dialog.choice == PrintWhat.RankingAndMatches)
					data.PrintLadderAndMatches();
				else 
					Logger.Write("PrintWhat error", "Ongeldige keuze geleverd door PrintDialog");
			}
		}


		/// <summary>
		/// Wissel tussen simpele en uitgebreide weergave
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void button_Details_Click(object sender, EventArgs e)
        {
			simple = !simple;
			updateMode();
		}


		/// <summary>
		/// Verwijder alle wedstrijden van deze ronde na bevestiging van de gebruiker
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_RemoveWedstrijden_Click(object sender, EventArgs e)
		{
			//bekijken of de ronde uberhaupt verwijderd kan worden.
			if (data.RemovableRound())
			{
				//Geef de gebruiker de keus om nog te weigeren, waarschuwing dat de wedstrijden worden verwijderd
				if (MessageBox.Show(
				    	"Waarschuwing: De de wedstrijden uit deze ronde worden verwijderd, weet u zeker dat u door gaat?",
				    	"Sharp Shuttle - Huidige Ronde Verwijderen", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) ==
				    DialogResult.No)
					return;

				//Als het lukt om de ronde te verwijderen dan laten we het de gebruiker weten,
				//als het niet zo is zorgt RemoveRound ervoor dat de gebruiker weet wat er fout is
				if (data.RemoveRound())
				{
					UpdateControls();
					MessageBox.Show("De huidige ronde met wedstrijden is verwijderd.\r\nWedstrijden uit de vorige ronde zullen niet zichtbaar zijn omdat deze niet meer actief zijn.", "Sharp Shuttle - Huidige Ronde Verwijderen",
					                MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}

			else MessageBox.Show("De huidige ronde met wedstrijden kan niet worden verwijderd omdat er al wedstrijden gespeeld zijn of er zijn geen wedstrijden om te verwijderen."
				, "Sharp Shuttle - Huidige Ronde Verwijderen", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); 
		}

		/// <summary>
		/// Genereert alle wedstrijden voor de volgende ronde na bevestiging van de gebruiker
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void button_GenWedstrijden_Click(object sender, EventArgs e)
        {   
			//Geef de gebruiker de keus om nog te weigeren, waarschuwing dat de wedstrijden niet meer
			//mogen worden bewerkt.
			if (MessageBox.Show("U staat op het punt een ronde af te sluiten. Wijzigingen in uitslagen van deze ronde hebben hierna geen effect meer op de wedstrijden van aankomende ronde. Weet u zeker dat u door wilt gaan?",
				"Sharp Shuttle - Volgende Ronde Genereren", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
				return;

			
			//Als het lukt om de volgende ronde te genereren dan laten we het de gebruiker weten,
			//als het niet zo is zorgt FinishRound ervoor dat de gebruiker weet wat er fout is
			if (data.FinishRound())
				UpdateControls();
		}
		#endregion

		#region Listeners/Interactie
		/// <summary>
		/// Opent een scherm waar scores van een wedstrijd ingevuld kunnen worden 
		/// </summary>
		private void showScoreForm()
		{
			MatchView match = lvwMatches.DataSource[lvwMatches.SelectedIndices[0]];
			//Voer alleen een score in bij gestarte wedstrijd, of nadat de gebruiker geinformeerd is.
			if (match.StartTime.Length != 0 || match.Domain.Played ||
				DialogResult.Yes == MessageBox.Show("Deze wedstrijd is nog niet gestart, weet u zeker dat u een score in wilt vullen?",
				"Wedstrijd niet gestart", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
			{
				ScoreEditForm add = new ScoreEditForm(match, !match.Domain.Played);
				add.EditDone += SaveScore;
				add.ShowDialog(this);
			}
		}

		/// <summary>
		/// Update de GUI voor de geselecteerde poule
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pouleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
			//Als het scherm wordt geupdate moeten we niet opnieuw update aanroepen.
			if (updating) return;

			data.SelectedPouleID = cboPoules.SelectedValue;
			data.LoadTeams();
			data.LoadMatches();
            UpdateControls();
        }

		/// <summary>
		/// Opent het scoreformulier van de geselecteerde wedstrijd
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void matchListView_wedstrijden_DoubleClick(object sender, EventArgs e)
		{
			if (lvwMatches.SelectedItems.Count == 1)
				showScoreForm();
		}


		/// <summary>
		/// Ververs data bij update.
		/// </summary>
		/// <param name="PouleID"></param>
		/// <param name="SerialEvent"></param>
		void Instance_PouleUpdated(int PouleID, SerialEventTypes SerialEvent)
		{
			if (PouleID != 0)
			{
				data.LoadPoules();
				if (InvokeRequired)
					Invoke(new MethodInvoker(UpdateControls));
				else
					UpdateControls();
			}
		}

		/// <summary>
		/// Ververs data bij een update
		/// </summary>
		/// <param name="PouleID"></param>
		/// <param name="SerialEvent"></param>
		void Instance_PouleLadderUpdated(int PouleID, SerialEventTypes SerialEvent)
		{
			data.LoadTeams();
			if (InvokeRequired)
				Invoke(new MethodInvoker(UpdateControls));
			else
				UpdateControls();
		}

		/// <summary>
		/// Open rechtsclick menu.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void matchListView_wedstrijden_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				if (lvwMatches.SelectedItems.Count > 1)
					cmsGamesSetMenuItems("MultiSelected");
				else
					cmsGamesSetMenuItems("SingleSelected");
				cmsMatches.Show(lvwMatches, e.Location);
			}
		}

		/// <summary>
		/// Open rechtsclick menu.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void simpleTeamListView_Spelers_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				if (lvwTeamsSimple.SelectedIndices.Count == 1)
					cmsTeams.Show(lvwTeamsSimple, e.Location);
				if (lvwTeams.SelectedIndices.Count == 1)
					cmsTeams.Show(lvwTeams, e.Location);
			}
		}

		private void cb_allmatches_CheckedChanged(object sender, EventArgs e)
		{
			UpdateControls();
		}

		#endregion
		
		#region ContextMenuStrip
		/// <summary>
		/// Bepaal welke items in de wedstrijden contextmenu worden getoond
		/// </summary>
		/// <param name="kind"></param>
		private void cmsGamesSetMenuItems(string kind)
		{
			switch (kind)
			{
				case "MultiSelected":
					cmsItemSetScore.Visible = false;
					break;
				case "SingleSelected":
					cmsItemSetScore.Visible = true;
					break;
			}
		}

		/// <summary>
		/// Geeft gebruikers de mogelijkheid wedstrijdbriefjes opnieuw te printen.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void printWedstrijdbriefjeOpnieuwToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (int index in lvwMatches.SelectedIndices)
				data.PrintNote(lvwMatches.DataSource[index]);
		}

		/// <summary>
		/// Gebruiker wil score van wedstrijd invullen
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void vulScoreInToolStripMenuItem_Click(object sender, EventArgs e)
		{
			showScoreForm();
		}

		/// <summary>
		/// Gebruiker wil team gegevens wijzigen
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void wijzigTeamgegevensToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LadderTeamView team = simple ? lvwTeamsSimple.DataSource[lvwTeamsSimple.SelectedIndices[0]] 
				: lvwTeams.DataSource[lvwTeams.SelectedIndices[0]];
			PouleView poule = (PouleView)cboPoules.SelectedItem;
			TeamView teamView = data.GetLadderTeam(team);
			if (team != null)
			{
				TeamPropertiesForm teamProp = new TeamPropertiesForm(poule, teamView);
				teamProp.EditDone += TeamChanged;
				teamProp.ShowDialog();
			}
			else
				MessageBox.Show("Geselecteerde team niet gevonden in de team lijst.", "Team Fout", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		/// <summary>
		/// Voer teamwijzigingen door
		/// </summary>
		public void TeamChanged()
		{
			data.SetTeamChanges();
			UpdateControls();
		}

    	#endregion

    }
}
