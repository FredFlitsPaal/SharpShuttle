using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Client.Controls;
using Shared.Views;
using Shared.Parsers;

namespace Client.Forms.ClientWizard.TournamentInitializationWizard
{
	/// <summary>
	/// Scherm waarmee een inschrijvingslijst geimporteerd kan worden
	/// </summary>
    public partial class ImportPlayersForm : Form
    {
		/// <summary>
		/// Een event die aangeeft dat het importeren afgerond is
		/// </summary>
		public event PlayersStep.ImportDoneEvent ImportDone;
    	
		/// <summary>
		/// Alle geimporteerde spelers
		/// </summary>
		protected PlayerViews players;
		/// <summary>
		/// Alle poules
		/// </summary>
		protected PouleViews poules;
		/// <summary>
		/// Alle voorkeurspoules
		/// </summary>
		protected PouleViews allPrefPoules;
		/// <summary>
		/// Alle bestanden waaruit inschrijvingen geimporteerd zijn
		/// </summary>
    	protected static List<String> importedFiles;

		#region Constructor

		/// <summary>
		/// Default costructor die een businesslogica object voor spelerbeheer meekrijgt
		/// </summary>
		/// <param name="control"></param>
		public ImportPlayersForm(PlayersControl control)
		{
			InitializeComponent();
			
			players = new PlayerViews();
			poules = control.GetAllPoules();
		}

		#endregion

		#region Buttons

		/// <summary>
		/// De button accepteer om de importeeractie toe te voegen.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void btnAccept_Click(object sender, EventArgs e)
        {
			if (importedFiles == null)
				importedFiles = new List<String>();

			if (importedFiles.Contains(txtFile.Text.Trim()))
			{
				DialogResult reImport = MessageBox.Show(this,
				"U heeft het ingegeven bestand al een keer geïmporteerd.\nWeet u zeker dat u het bestand nog eens wilt importeren?",
				"Opnieuw Importeren", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

				if (reImport == DialogResult.No) return;
			}

			if (readImport())
			{
				if (ImportDone != null)
				{
					importedFiles.Add(txtFile.Text.Trim());
					PouleViews newPoules = checkPreferences(players, allPrefPoules);
					ImportDone(players, newPoules);
				}

				Close();
			}
			else
			{
				MessageBox.Show(this,
				"Het bestand dat ingegeven is in de tekstbox is niet te parsen.\n\nSelecteer een geldig bestand"
				, "Incorrect Bestand", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
        }

		/// <summary>
		/// De button openen werpt een diloag op.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "XML bestanden | *.xml";
            if (dlg.ShowDialog() == DialogResult.OK)
                txtFile.Text = dlg.FileName;
		}

		#endregion

		#region Events

		/// <summary>
		/// Als de Textbox changed is.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtBFile_TextChanged(object sender, EventArgs e)
		{
			if (txtFile.Text.Trim().Length > 0)
				btnAccept.Enabled = true;
			else
				btnAccept.Enabled = false;
		}

		#endregion

		#region Inlezen van import
		
		/// <summary>
		/// Leest het gegeven bestand in
		/// </summary>
		/// <returns>Geeft aan of het inlezen gelukt is</returns>
		private bool readImport()
		{
			String file = txtFile.Text.Trim();

			try
			{
				PlayerViews importedPlayers = XMLParser.readFile(file, out allPrefPoules);
				players.AddRange(importedPlayers);
			}
			catch (Exception)
			{
				return false;
			}

			return true;
		}

		#endregion

		#region Hulpmethodes

		/// <summary>
		/// Controleert of de voorkeuren van spelers wel bestaan
		/// </summary>
		/// <param name="importedPlayers">Geimporteerde spelers</param>
		/// <param name="prefPoules">De voorkeuren van de geimporteerde spelers</param>
		/// <returns>De nieuwe poules</returns>
		private PouleViews checkPreferences(PlayerViews importedPlayers, PouleViews prefPoules)
		{
			PouleViews newPoules = new PouleViews();

			foreach (PouleView poule in prefPoules)
				// Voorkeurs poule bestaat niet
				if (!poules.ContainsPoule(poule.Discipline, poule.Niveau))
					newPoules.Add(poule);

			if (newPoules.Count > 0)
			{
				string preferences = "De volgende voorkeuren bestaan niet: \n\n";

				foreach (PouleView poule in newPoules)
					preferences += poule.Discipline + " " + poule.Niveau + "\n";

				if (newPoules.Count == 1)
					preferences += "\nWilt u deze poule toevoegen?";
				else
					preferences += "\nWilt u deze poules toevoegen?";

				if (MessageBox.Show(preferences, "Voorkeuren", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					return newPoules;
				else
					// Verwijder alle niet bestaande poules uit de voorkeuren van de spelers
					foreach (PlayerView player in importedPlayers)
						removePrefPoules(player, prefPoules);
			}
			return new PouleViews();
		}

		/// <summary>
		/// Verwijdert alle voorkeuren die in de lijst van poules zitten
		/// </summary>
		/// <param name="player">De speler</param>
		/// <param name="prefPoules">Alle poules</param>
		private void removePrefPoules(PlayerView player, PouleViews prefPoules)
		{
			string[] prefs = player.PreferencesList;

			for (int i = 0; i < prefs.Length; i++)
				foreach (PouleView poule in prefPoules)
					if (!(poule.Discipline == PlayerView.GetDiscipline(prefs[i]) && poule.Niveau == PlayerView.GetNiveau(prefs[i])))
						player.RemovePreference(prefs[i]);
		}

		#endregion
	}
}
