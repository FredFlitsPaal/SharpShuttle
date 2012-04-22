using System;
using System.Windows.Forms;
using Shared.Views;
using Client.Controls;
using System.Text.RegularExpressions;

namespace Client.Forms.ClientWizard.TournamentInitializationWizard
{
	/// <summary>
	/// Scherm waarin gegevens van een poule gewijzigd kunnen worden
	/// </summary>
	public partial class PouleEditForm : Form
	{
		/// <summary>
		/// Event die aangeeft dat het wijzigen van de poule klaar is
		/// </summary>
		public event NiveausPoulesStep.PouleEditDoneEvent EditDone;

		/// <summary>
		/// De poule die gewijzigd wordt
		/// </summary>
		protected PouleView editPoule;
		
		/// <summary>
		/// Business logica
		/// </summary>
		protected ManageNiveauPouleControl control;
		
		/// <summary>
		/// Bool die aangeeft of de naam van de poule gewijzigd is
		/// </summary>
		protected bool nameChanged;
		
		/// <summary>
		/// Bool die aangeeft of het om een nieuw aangemaakte poule gaat
		/// </summary>
		protected bool newPoule;

		#region Constructor en initialisatie

		/// <summary>
		/// Constructor die een poule, een businesslogica object en een bool die aangeeft
		/// of het een nieuw aangemaakte poule is mee krijgt
		/// </summary>
		/// <param name="poule"></param>
		/// <param name="isNewPoule"></param>
		/// <param name="pouleControl"></param>
		public PouleEditForm(PouleView poule, bool isNewPoule, ManageNiveauPouleControl pouleControl)
		{
			newPoule = isNewPoule;
			control = pouleControl;
			editPoule = poule;

			InitializeComponent();
			initializeDisciplines();
			initializeLevels();
			fillInForm();
		}

		/// <summary>
		/// Initialistatie van de disciplinelijst.
		/// </summary>
		protected void initializeDisciplines()
		{
			string[] disciplines = editPoule.getDisciplines();
			cboDiscipline.Items.AddRange(disciplines);
			cboDiscipline.SelectedIndex = 0;
		}

		/// <summary>
		/// Initialisatie van de niveaulijst.
		/// </summary>
		protected void initializeLevels()
		{
			foreach (string niveau in control.Niveaus)
				cboNiveau.Items.Add(niveau);

			cboNiveau.SelectedIndex = 0;
		}

		/// <summary>
		/// Initialisatie van tekstvelden en buttons.
		/// </summary>
		protected void fillInForm()
		{
			if (newPoule)
				fillInNameBox();
			else
			{
				Text = String.Format("Poule \"{0}\" Wijzigen", editPoule.Name);
				btnAccept.Text = "Wijzigen";
				btnAccept.Enabled = false;

				txtName.Text = editPoule.Name;
				cboDiscipline.Text = editPoule.Discipline;
				cboNiveau.Text = editPoule.Niveau;
				txtComments.Text = editPoule.Comment;

				if (editPoule.Name.Equals(editPoule.Discipline + " " + editPoule.Niveau))
					nameChanged = false;
			}
		}

		/// <summary>
		/// Poulenaam gelijk maken aan "discipline niveau".
		/// </summary>
		protected void fillInNameBox()
		{
			txtName.Text = cboDiscipline.Text + " " + cboNiveau.Text;
			nameChanged = false;
		}

		#endregion

		#region Opslaan en validatie

		/// <summary>
		/// Opslaan van de gewijzigde pouledata.
		/// </summary>
		protected void savePoule()
		{
			editPoule.Name = txtName.Text.Trim();
			editPoule.Discipline = cboDiscipline.Text;
			editPoule.Niveau = cboNiveau.Text;
			editPoule.Comment = txtComments.Text.Trim();
		}

		/// <summary>
		/// Enabled van de button aanpassen door te kijken of er wijzigingen zijn gedaan.
		/// </summary>
		protected void checkForChanges()
		{
			bool name = !txtName.Text.Trim().Equals(editPoule.Name);
			bool discipline = !cboDiscipline.Text.Equals(editPoule.Discipline);
			bool niveau = !cboNiveau.Text.Equals(editPoule.Niveau);
			bool comment = !txtComments.Text.Trim().Equals(editPoule.Comment);

			if (name || discipline || niveau || comment)
				btnAccept.Enabled = true;
			else
				btnAccept.Enabled = false;
		}

		/// <summary>
		/// Kijken of de poulenaam illegale tekens bevat.
		/// </summary>
		/// <param name="toCheck">String die gevalideerd moet worden</param>
		/// <returns>Boolean of de string illegale tekens bevat</returns>
		protected static  bool containsIllegalCharacters(string toCheck)
		{
			// Deze regex laat het volgende toe:
			// - de string moet met een letter of natuurlijk getal beginnen en eindigen.
			// - Alleen de speciale tekens - _ ' en spatie zijn toegestaan.
			// - Meerdere voorkomens van speciale tekens mag, mits ze gescheiden worden door een
			//   letter of natuurlijk getal.
			Regex pattern = new Regex("^[\\w\\n]+(([-_' ][\\w\\n])?[\\w\\n]*)*$");
			return !pattern.IsMatch(toCheck);
		}

		#endregion

		#region Buttons

		/// <summary>
		/// Als er op accepteren wordt geklikt.
		/// </summary>
		/// <param name="sender">Sender object</param>
		/// <param name="e">EventArgs</param>
		private void btnAccept_Click(object sender, EventArgs e)
		{
			string name = txtName.Text.Trim();

			// Geef een foutmelding als een van de if statements waar wordt gemaakt
			if (name.Length == 0)
				MessageBox.Show("Voer een naam in voor de poule.", "Ongeldige Poulenaam", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			else if (containsIllegalCharacters(name))
				MessageBox.Show("De opgegeven poulenaam is niet geldig.\n" +
					"(De naam moet beginnen en eindigen met een letter of cijfer \n" +
					"en alleen de leestekens -_' en spatie zijn toegestaan)", "Ongeldige Poulenaam", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			else if(!control.CheckForValidPouleName(editPoule.Name, name))
				MessageBox.Show("Er bestaat al een poule met deze naam.", "Ongeldige Poulenaam", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			else
			{
				savePoule();
				
				if (EditDone != null)
					EditDone(editPoule);
				
				Close();
			}
		}

		#endregion

		#region Events

		/// <summary>
		/// Als de index van de discipline- of poulelijst veranderd.
		/// </summary>
		/// <param name="sender">Sender object</param>
		/// <param name="e">EventArgs</param>
		private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!nameChanged)
				fillInNameBox();

			// Bij het wijzigen van een poule: kijk of er iets veranderd is
			if (!newPoule)
				checkForChanges();
		}

		/// <summary>
		/// Als de poulenaam wordt aangepast.
		/// </summary>
		/// <param name="sender">Sender object</param>
		/// <param name="e">EventArgs</param>
		private void txtBName_TextChanged(object sender, EventArgs e)
		{
			nameChanged = true;

			string input = txtName.Text.Trim();

			// Controleer de input en geef de gebruiker feedback
			if (containsIllegalCharacters(input) || !control.CheckForValidPouleName(editPoule.Name, input))
				txtName.BackColor = System.Drawing.Color.Pink;
			else
				txtName.BackColor = System.Drawing.Color.White;

			// Bij het wijzigen van een poule: kijk of er iets veranderd is
			if (!newPoule)
				checkForChanges();
		}

		/// <summary>
		/// Als het opmerkingenveld worden aangepast.
		/// </summary>
		/// <param name="sender">Sender object</param>
		/// <param name="e">EventArgs</param>
		private void txtBComment_TextChanged(object sender, EventArgs e)
		{
			// Bij het wijzigen van een poule: kijk of er iets veranderd is
			if (!newPoule)
				checkForChanges();
		}

		#endregion
	}
}
