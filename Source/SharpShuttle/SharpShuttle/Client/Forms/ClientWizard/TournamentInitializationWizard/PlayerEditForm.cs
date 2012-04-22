using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Client.Controls;
using Shared.Domain;
using Shared.Views;

namespace Client.Forms.ClientWizard.TournamentInitializationWizard
{
	/// <summary>
	/// Scherm waarin gegevens van een speler ingevoerd en gewijzigd kunnen worden
	/// </summary>
    public partial class PlayerEditForm : Form
    {
		/// <summary>
		/// Een event die aangeeft dat het wijzigen van de speler klaar is
		/// </summary>
		public event PlayersStep.PlayerEditDoneEvent EditDone;
		/// <summary>
		/// Methode die aangeroepen wordt als het wijzigen van de speler klaar is
		/// </summary>
		/// <param name="prefs"></param>
		public delegate void PreferencesEditDoneEvent(List<String> prefs);

		/// <summary>
		/// De speler die gewijzigd wordt
		/// </summary>
		protected PlayerView editPlayer;
		/// <summary>
		/// Businesslogica
		/// </summary>
		protected PlayersControl control;
		/// <summary>
		/// Lijsten met alle vrouwenpoules, mannenpoules, en voorkeurpoules voor de speler
		/// </summary>
		protected List<String> femalePoules, malePoules, selectedPrefs;
		/// <summary>
		/// Bool die aangeeft of het een nieuw aangemaakte speler is
		/// </summary>
		protected bool newPlayer;

		#region Constructor and initialization

		/// <summary>
		/// Contructor die een speler, een businesslogica object, en een bool die aangeeft of dit
		/// een nieuw aangemaakte speler is meekrijgt
		/// </summary>
		public PlayerEditForm(PlayerView player, bool isNewPlayer, PlayersControl playersControl)
		{
			newPlayer = isNewPlayer;
			control = playersControl;
			editPlayer = player;
			selectedPrefs = new List<string>();

			InitializeComponent();
			initializePoules();

			if (!isNewPlayer)
				fillInForm();
		}

		/// <summary>
		/// Vult de entryfields met waarden van een al bestaande speler
		/// </summary>
		protected void fillInForm()
		{
			Text = String.Format("Speler \"{0}\" Wijzigen", editPlayer.Name);
			btnAccept.Text = "Wijzigen";
			btnAccept.Enabled = false;

			txtName.Text = editPlayer.Name;
			txtClub.Text = editPlayer.Club;
			txtComment.Text = editPlayer.Comment;

			if (editPlayer.Gender.ToUpper() == "M")
				radMale.Select();
			else
				radFemale.Select();

			splitPreferences(editPlayer.Preferences);

			removeOldPrefs();
			updatePrefs();
		}

		/// <summary>
		/// Maakt de lijsten van poules die beschikbaar zijn voor vrouwen en voor mannen
		/// </summary>
		protected void initializePoules()
		{
			femalePoules = new List<String>();
			malePoules = new List<String>();

			foreach (PouleView pv in control.GetAllPoules())
			{
				if (pv.DisciplineEnum == Poule.Disciplines.FemaleDouble || 
					pv.DisciplineEnum == Poule.Disciplines.FemaleSingle)
					femalePoules.Add(String.Format("{0}: {1}", pv.Discipline, pv.Niveau));
				else if (pv.DisciplineEnum == Poule.Disciplines.MaleDouble || 
					pv.DisciplineEnum == Poule.Disciplines.MaleSingle)
					malePoules.Add(String.Format("{0}: {1}", pv.Discipline, pv.Niveau));
				else
				{
					femalePoules.Add(String.Format("{0}: {1}", pv.Discipline, pv.Niveau));
					malePoules.Add(String.Format("{0}: {1}", pv.Discipline, pv.Niveau));
				}
			}
		}

		#endregion

		#region Opslaan en validatie

		/// <summary>
		/// leest de entryfileds in
		/// </summary>
		protected void savePlayer()
		{
			editPlayer.Name = txtName.Text.Trim();
			editPlayer.Club = txtClub.Text.Trim();
			editPlayer.Preferences = combinePreferences();
			editPlayer.Comment = txtComment.Text.Trim();
			editPlayer.Gender = radMale.Checked ? "M" : "V";
		}

		/// <summary>
		/// Bekijkt of er verandering zijn.
		/// </summary>
		protected void checkForChanges()
		{
			bool name = txtName.Text.Trim() != editPlayer.Name;
			bool club = txtClub.Text.Trim() != editPlayer.Club;
			bool com = txtComment.Text.Trim() != editPlayer.Comment;
			bool gender;

			if (radMale.Checked)
				gender = editPlayer.Gender.ToUpper() != "M";
			else
				gender = editPlayer.Gender.ToUpper() != "V";

			bool pref = combinePreferences() != editPlayer.Preferences;

			if (name || club || gender || com || gender || pref)
				btnAccept.Enabled = true;
			else
				btnAccept.Enabled = false;
		}

		/// <summary>
		/// Kijkt of er geen illegale charcters zijn gebruikt.
		/// </summary>
		/// <param name="toCheck"></param>
		/// <returns></returns>
		protected static bool containsIllegalCharacters(string toCheck)
		{
			// Deze regex laat het volgende toe:
			// - de string moet met een letter beginnen en eindigen.
			// - Alleen de speciale tekens - _ en spatie zijn toegestaan.
			// - Meerdere voorkomens van speciale tekens mag, mits ze 
			//   gescheiden worden door een letter.
			Regex pattern = new Regex("^[\\w]+(([-_ ][\\w])?[\\w]*)*$");
			return !pattern.IsMatch(toCheck);
		}

		#endregion

		#region Buttons

		/// <summary>
		/// als ok de ok button wordt geklikt
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAccept_Click(object sender, EventArgs e)
		{
			string name = txtName.Text.Trim();

			if (name.Length == 0)
				MessageBox.Show("Voer een naam in voor de speler.", "Ongeldige Naam", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			else if (containsIllegalCharacters(name))
				MessageBox.Show("De opgegeven naam is niet geldig.\n" +
					"(De naam moet beginnen en eindigen met een letter\n" +
					"en alleen de leestekens -_ en spatie zijn toegestaan)", "Ongeldige Naam", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			else
			{
				savePlayer();

				if (EditDone != null)
					EditDone(editPlayer);

				Close();
			}
		}

		/// <summary>
		/// Het klikken op de button voorkeur toevoegen.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAddPref_Click(object sender, EventArgs e)
		{
			PreferenceEditForm newForm;

			if (radMale.Checked)
				newForm = new PreferenceEditForm(selectedPrefs, malePoules);
			else 
				newForm = new PreferenceEditForm(selectedPrefs, femalePoules);

			newForm.EditDone += Preference_EditDone;
			newForm.ShowDialog(this);
		}

		/// <summary>
		/// Het klikken op de button verwijderen voorkeur.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDeletePref_Click(object sender, EventArgs e)
		{
			if (ltbPrefs.SelectedIndices.Count == 1)
			{
				String pitem = ltbPrefs.SelectedItem.ToString();

				DialogResult result = MessageBox.Show(this,
					String.Format("Weet u zeker dat u de voorkeur {0} wilt verwijderen?", pitem)
										, "Voorkeur Verwijderen", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

				if (result == DialogResult.Yes)
					selectedPrefs.Remove(ltbPrefs.SelectedItem.ToString());
			}
			else if (ltbPrefs.SelectedIndices.Count > 1)
			{
				DialogResult result = MessageBox.Show(this, "Weet u zeker dat u de geselecteerde" + 
					"voorkeuren wilt verwijderen?", "Voorkeuren Verwijderen", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

				if (result == DialogResult.Yes)
					foreach (object p in ltbPrefs.SelectedItems)
						selectedPrefs.Remove(p.ToString());
			}

			updatePrefs();
		}

		#endregion 

		#region Events

		/// <summary>
		/// checkt of er veranderingen hebben plaatsgevonden
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void entryChanged(object sender, EventArgs e)
		{
			if (sender == txtName)
			{
				string input = txtName.Text.Trim();

				if (containsIllegalCharacters(input))
					txtName.BackColor = System.Drawing.Color.Pink;
				else
					txtName.BackColor = System.Drawing.Color.White;
			}

			if (!newPlayer)
				checkForChanges();
		}

		/// <summary>
		/// Vraagt de gebruiker of hij echt het geslacht van een speler wil veranderen
		/// Als dit veranderd wordt worden geslachtsspecifieke voorkeuren verwijderd
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void genderChanged(object sender, EventArgs e)
		{
			if (selectedPrefs.Count > 0)
			{
				DialogResult result = MessageBox.Show(this,
					String.Format("Weet u zeker dat u het geslacht van \"{0}\" wilt wijzigen?\n" +
						"De geslacht specifieke voorkeuren worden dan verwijderd", 
						txtName.Text.Trim()), "Geslacht Wijzigen", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

				if (result == DialogResult.Yes)
				{
					for (int i = 0; i < selectedPrefs.Count; i++)
					{
						if (!selectedPrefs[i].ToLower().Contains("mixed") &&
							!selectedPrefs[i].ToLower().Contains("unisex"))
						{
							selectedPrefs.RemoveAt(i);
							i--;
						}
					}

					updatePrefs();
				}
				else if (radMale.Checked) 
					radFemale.Checked = true;
				else if (radFemale.Checked) 
					radMale.Checked = true;
			}
		}

		/// <summary>
		/// Voegt de voorkeuren toe aan selectedPrefs en update het scherm
		/// </summary>
		/// <param name="prefs"> De lijst van voorkeuren </param>
		public void Preference_EditDone(List<String> prefs)
		{
			if (prefs != null)
			{
				selectedPrefs.AddRange(prefs);
				updatePrefs();
			}
		}

    	#endregion

		#region Hulpmethodes

		/// <summary>
		/// Scheidt de voorkeuren string in eem lijst van voorkeuren
		/// </summary>
		/// <param name="prefs"></param>
		private void splitPreferences(String prefs)
		{
			if (prefs == "")
				return;

			string[] sPrefs = Regex.Split(prefs, ", ");

			if (sPrefs.Length > 0)
				selectedPrefs = new List<string> (sPrefs);
		}

		/// <summary>
		/// Comibineert alle voorkeuren to 1 string van alle voorkeuren met ,
		/// </summary>
		/// <returns></returns>
		private String combinePreferences()
		{
			StringBuilder builder = new StringBuilder();
			string delimiter = "";

			foreach (string item in selectedPrefs)
			{
				builder.Append(delimiter);
				builder.Append(item);
				delimiter = ", ";       
			}

			return builder.ToString();
		}

		/// <summary>
		/// Het verwijderen van niet meer bestaande voorkeuren.
		/// </summary>
		private void removeOldPrefs()
		{
			if (radMale.Checked)
			{
				for (int i = 0; i < selectedPrefs.Count; i++)
				{
					if (!malePoules.Contains(selectedPrefs[i]))
					{
						selectedPrefs.Remove(selectedPrefs[i]);
						i--;
					}
				}
			}

			else if (radFemale.Checked)
			{
				for (int i = 0; i < selectedPrefs.Count; i++)
				{
					if (!femalePoules.Contains(selectedPrefs[i]))
					{
						selectedPrefs.Remove(selectedPrefs[i]);
						i--;
					}
				}
			}
		
		}

		/// <summary>
		/// update de voorkeuren
		/// </summary>
		private void updatePrefs()
		{
			ltbPrefs.DataSource = null;
			ltbPrefs.DataSource = selectedPrefs;
		}

		#endregion
    }
}