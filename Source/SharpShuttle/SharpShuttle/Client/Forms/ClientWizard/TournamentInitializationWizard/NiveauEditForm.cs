using System;
using System.Windows.Forms;
using Client.Controls;
using System.Text.RegularExpressions;

namespace Client.Forms.ClientWizard.TournamentInitializationWizard
{
	/// <summary>
	/// Scherm waar een nieuw niveau aangemaakt of een oud niveau gewijzigd kan
	/// worden
	/// </summary>
	public partial class NiveauEditForm : Form
	{
		/// <summary>
		/// Event die aangeeft dat het wijzigen van het niveau klaar is
		/// </summary>
		public event NiveausPoulesStep.NiveauEditDoneEvent EditDone;

		/// <summary>
		/// Is dit een nieuw niveau of wordt er een bestaand niveau gewijzigd
		/// </summary>
		protected bool newNiveau;
		/// <summary>
		/// De originele naam van het niveau
		/// </summary>
		protected string editNiveau;
		/// <summary>
		/// Businesslogica
		/// </summary>
		protected ManageNiveauPouleControl control;

		#region Constructor

		/// <summary>
		/// Maak een scherm met een gegeven niveaunaam en businesslogica object en een 
		/// bool die aangeeft of het een nieuw niveau is
		/// </summary>
		/// <param name="niveau"> De oorspronkelijke naam van het niveau </param>
		/// <param name="isNewNiveau"> Is het een nieuw niveau of wordt er een bestaand niveau gewijzigd</param>
		/// <param name="niveauControl"> Het businesslogica object</param>
		public NiveauEditForm(string niveau, bool isNewNiveau, ManageNiveauPouleControl niveauControl)
		{
			newNiveau = isNewNiveau;
			control = niveauControl;
			editNiveau = niveau;

			InitializeComponent();

			if (!newNiveau)
			{
				Text = String.Format("Niveau \"{0}\" Wijzigen", niveau);
				btnAccept.Text = "Wijzigen";
				txtNiveau.Text = niveau;
				btnAccept.Enabled = false;
			}
		}

		#endregion

		#region Validatie

		/// <summary>
		/// Enabled van de button aanpassen door te kijken of er wijzigingen zijn gedaan.
		/// </summary>
		protected void checkForChanges()
		{
			btnAccept.Enabled = !txtNiveau.Text.Trim().Equals(editNiveau);
		}

		/// <summary>
		/// Kijken of de niveaunaam illegale tekens bevat.
		/// </summary>
		/// <param name="toCheck">String die gevalideerd moet worden</param>
		/// <returns>Boolean of de string illegale tekens bevat</returns>
		protected static bool containsIllegalCharacters(string toCheck)
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
			string input = txtNiveau.Text.Trim();

			// Geef een foutmelding als een van de if statements waar wordt gemaakt
			if(input.Length == 0)
				MessageBox.Show("Voer een niveau in.", "Ongeldige Invoer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			else if (containsIllegalCharacters(input))
				MessageBox.Show("Het opgegeven niveau is niet geldig.\n" +
					"(De naam moet beginnen en eindigen met een letter of cijfer \n" +
					"en alleen de leestekens -_' en spatie zijn toegestaan)", "Ongeldige Invoer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			else if (!control.CheckForValidNiveau(editNiveau, input))
				MessageBox.Show("Het opgegeven niveau bestaat al.", "Ongeldige Invoer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			else
			{
				if (EditDone != null)
					EditDone(editNiveau, input);

				Close();
			}
		}

		#endregion

		#region Events

		/// <summary>
		/// Als de naam van het niveau gewijzigd wordt.
		/// </summary>
		/// <param name="sender">Sender object</param>
		/// <param name="e">EventArgs</param>
		private void txtBNiveau_TextChanged(object sender, EventArgs e)
		{
			string input = txtNiveau.Text.Trim();

			// Controleer de input en geef de gebruiker feedback
			if (containsIllegalCharacters(input) || !control.CheckForValidNiveau(editNiveau, input))
				txtNiveau.BackColor = System.Drawing.Color.Pink;
			else
				txtNiveau.BackColor = System.Drawing.Color.White;

			// Bij het wijzigen van een niveau: kijk of er iets veranderd is
			if (!newNiveau)
				checkForChanges();
		}

		#endregion
	}
}
