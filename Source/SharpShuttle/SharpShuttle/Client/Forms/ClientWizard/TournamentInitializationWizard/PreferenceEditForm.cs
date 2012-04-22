using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Client.Forms.ClientWizard.TournamentInitializationWizard
{
	/// <summary>
	/// Scherm waar de voorkeurpoules van een speler gewijzigd kunnen worden
	/// </summary>
	public partial class PreferenceEditForm : Form
	{
		/// <summary>
		/// Event die aangeeft dat het wijzigen van de voorkeuren klaar is
		/// </summary>
		public event PlayerEditForm.PreferencesEditDoneEvent EditDone;

		#region Constructor en initialisatie

		/// <summary>
		/// Constructor die een lijst van voorkeuren en beschikbare poules meekrijgt
		/// </summary>
		/// <param name="prefs"></param>
		/// <param name="poules"></param>
		public PreferenceEditForm(List<String> prefs, List<String> poules)
		{
			InitializeComponent();
			fillListBox(prefs, poules);

			ltbPrefs.SelectedItem = null;
		}

		/// <summary>
		/// Vult de listbox met alle voorkeuren die beschikbaar zijn voor de speler.
		/// </summary>
		/// <param name="prefs"></param>
		/// <param name="poules"></param>
		protected void fillListBox(List<String> prefs, List<String> poules)
		{
			List<String> availablePrefs = new List<String>(poules);

			foreach (String p in prefs)
			{
				for (int i = 0; i < availablePrefs.Count; i++)
				{
					if (p == availablePrefs[i])
					{
						availablePrefs.RemoveAt(i);
						i--;
					}
				}
			}

			ltbPrefs.DataSource = availablePrefs;
		}

		#endregion

		#region Inlezen van data

		/// <summary>
		/// Het inlezen van de data in de form.
		/// </summary>
		/// <returns></returns>
		protected List<String> readForm()
		{
			List<String> newPrefs = new List<string>();

			foreach (object p in ltbPrefs.SelectedItems)
				newPrefs.Add(p.ToString());

			return newPrefs;
		}

		#endregion

		#region Buttons

		/// <summary>
		/// Het registreren van een klik op de accept button.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAccept_Click(object sender, EventArgs e)
		{
			List <String> newPrefs = readForm();

			if (EditDone != null)
				EditDone(newPrefs);

			Close();
		}

		#endregion

		#region Events

		/// <summary>
		/// Wordt aangeroepen al de slectie van de lisbox is veranderd.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listBPrefs_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ltbPrefs.SelectedItems.Count > 0)
				btnAccept.Enabled = true;
			else 
				btnAccept.Enabled = false;
		}

		#endregion
	}
}
