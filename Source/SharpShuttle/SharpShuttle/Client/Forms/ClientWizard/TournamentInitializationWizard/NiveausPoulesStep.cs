using System;
using System.Windows.Forms;
using Shared.Views;
using Client.Controls;
using UserControls.AbstractWizard;
using UserControls.NotificationControls;
using Shared.Sorters;

namespace Client.Forms.ClientWizard.TournamentInitializationWizard
{
	/// <summary>
	/// Scherm waarin de verschillende spelniveaus en poules aangemaakt worden
	/// </summary>
	public partial class NiveausPoulesStep : GenericAbstractWizardStep<object>
	{
		/// <summary>
		/// Business logica
		/// </summary>
		private ManageNiveauPouleControl control;

		/// <summary>
		/// De notificiation control
		/// </summary>
		private NotificationControl btnNotify;

		/// <summary>
		/// Op welke kolom de lijst het laatst is gesorteerd
		/// </summary>
		private int poulesSort = -1;

		/// <summary>
		/// Methode die aangeroepen wordt wanneer het wijzigen van een niveau klaar is
		/// </summary>
		/// <param name="oldNiveau"></param>
		/// <param name="newNiveau"></param>
		public delegate void NiveauEditDoneEvent(string oldNiveau, string newNiveau);

		/// <summary>
		/// Methode die aangeroepen wordt wanneer het wijzigen van een poule klaar is
		/// </summary>
		/// <param name="poule"></param>
		public delegate void PouleEditDoneEvent(PouleView poule);
		/// <summary>
		/// Methode die aangeroepen wordt als er gerefreshd wordt
		/// </summary>
		/// <param name="updateGUI"></param>
		public delegate void ReloadEvent(bool updateGUI);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="enabled"></param>
		public delegate void EnabledInvoker(bool enabled);

		#region Constructor en initialisatie

		/// <summary>
		/// Default constructor
		/// </summary>
		public NiveausPoulesStep()
		{
			init();
		}

		/// <summary>
		/// Initialisatie van de step.
		/// </summary>
		private void init()
		{
			control = new ManageNiveauPouleControl();
			control.ReloadEvent += reload;
			
			InitializeComponent();
			createNotification();

			Text = "Definiëer Niveaus en Poules";
			previous_visible = false;
			next_visible = true;
			finish_visible = false;
		}

		/// <summary>
		/// Initialisatie van de data.
		/// </summary>
		public void SetForm()
		{
			updateData();

			btnNotify.Status = NotificationControl.State.Default;
			btnNotify.Refresh();
		}

		#endregion

		#region Notification

		/// <summary>
		/// Notificationbutton aanmaken.
		/// </summary>
		private void createNotification()
		{
			btnNotify = new NotificationControl(Notification, control);
			btnNotify.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			btnNotify.Location = new System.Drawing.Point(339, 267);
			btnNotify.Name = "btnNotify";
			btnNotify.Size = new System.Drawing.Size(90, 23);
			btnNotify.TabIndex = 6;
			btnNotify.UseVisualStyleBackColor = true;
			tbpPoules.Controls.Add(btnNotify);
			btnNotify.WorkStart += StartWork;
			btnNotify.WorkFinished += EndWork;
		}

		/// <summary>
		/// Notification event afhandelen.
		/// </summary>
		/// <param name="action">NotificationControl Action</param>
		public void Notification(NotificationControl.Action action)
		{
			if (action == NotificationControl.Action.Reload)
				notificationUpdate();
			else if (action == NotificationControl.Action.Save)
				control.SavePoules();
		}

		/// <summary>
		/// Notification event afhandelen in geval van een reload.
		/// </summary>
		private void notificationUpdate()
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(notificationUpdate));
				return;
			}

			updateData();
		}

		/// <summary>
		/// Begin van het notification werk.
		/// </summary>
		/// <param name="action">NotificationControl Action</param>
		public void StartWork(NotificationControl.Action action)
		{
			if (InvokeRequired)
				Invoke(new EnabledInvoker(setEnable), false);
			else
				setEnable(false);
		}

		/// <summary>
		/// Einde van het notification werk.
		/// </summary>
		/// <param name="action">NotificationControl Action</param>
		public void EndWork(NotificationControl.Action action)
		{
			if (InvokeRequired)
				Invoke(new EnabledInvoker(setEnable), true);
			else
				setEnable(true);
		}

		#endregion

		#region Opslaan en updaten

		/// <summary>
		/// Saven van de data.
		/// </summary>
		public override void Save()
		{
			control.SavePoules();
		}

		/// <summary>
		/// Updaten van de data.
		/// </summary>
		private void updateData()
		{
			control.Initialize();
			updateNiveauList();
			updatePouleList();
		}

		/// <summary>
		/// Niveaulijst updaten.
		/// </summary>
		private void updateNiveauList()
		{
			ltbNiveau.Items.Clear();

			foreach (string niveau in control.Niveaus)
				ltbNiveau.Items.Add(niveau);

			updateNiveauButtons();
		}

		/// <summary>
		/// Poulelijst updaten.
		/// </summary>
		private void updatePouleList()
		{
			lvwPoules.DataSource = control.Poules;
			updatePouleButtons();
		}

		/// <summary>
		/// Update de zichtbaarheid van de buttons van tabblad niveaus.
		/// </summary>
		private void updateNiveauButtons()
		{
			if (ltbNiveau.Items.Count > 0)
			{
				btnAddPoule.Enabled = true;
				btnChangeNiveau.Enabled = true;
				btnDeleteNiveau.Enabled = true;
			}
			else
			{
				btnAddPoule.Enabled = false;
				btnChangeNiveau.Enabled = false;
				btnDeleteNiveau.Enabled = false;
			}
		}

		/// <summary>
		/// Update de zichtbaarheid van de buttons van tabblad poules.
		/// </summary>
		private void updatePouleButtons()
		{
			lvwPoules.DataSource = control.Poules;
			if (lvwPoules.Items.Count > 0)
			{
				btnChangePoule.Enabled = true;
				btnDeletePoule.Enabled = true;
			}
			else
			{
				btnChangePoule.Enabled = false;
				btnDeletePoule.Enabled = false;
			}
		}

		/// <summary>
		/// Zet de enabled van alle buttons.
		/// </summary>
		/// <param name="enable">Boolean zichtbaarheid</param>
		private void setEnable(bool enable)
		{
			if (enable)
			{
				btnAddNiveau.Enabled = true;
				updateNiveauButtons();
				updatePouleButtons();
			}
			else
			{
				btnAddNiveau.Enabled = false;
				btnDeleteNiveau.Enabled = false;
				btnChangeNiveau.Enabled = false;
				btnAddPoule.Enabled = false;
				btnDeletePoule.Enabled = false;
				btnChangePoule.Enabled = false;
			}
		}

		#endregion

		#region Buttons

		/// <summary>
		/// Het toevoegen van een nieuwe niveau.
		/// </summary>
		/// <param name="sender">Sender object</param>
		/// <param name="e">EventArgs</param>
		private void addNiveauButton_Click(object sender, EventArgs e)
		{
			NiveauEditForm editForm = new NiveauEditForm("", true, control);

			editForm.EditDone += addNiveauForm_EditDone;
			editForm.ShowDialog(this);
		}

		/// <summary>
		/// Het veranderen van een bestaand niveau.
		/// </summary>
		/// <param name="sender">Sender object</param>
		/// <param name="e">EventArgs</param>
		private void changeNiveauButton_Click(object sender, EventArgs e)
		{
			if (ltbNiveau.SelectedItems.Count == 1)
			{
				string niveau = ltbNiveau.SelectedItems[0].ToString();
				NiveauEditForm editForm = new NiveauEditForm(niveau, false, control);

				editForm.EditDone += editNiveauForm_EditDone;
				editForm.ShowDialog(this);
			}
		}

		/// <summary>
		/// Het verwijderen van niveau('s).
		/// </summary>
		/// <param name="sender">Sender object</param>
		/// <param name="e">EventArgs</param>
		private void removeNiveauButton_Click(object sender, EventArgs e)
		{
			int selection = ltbNiveau.SelectedItems.Count;

			if (selection > 0)
			{
				DialogResult result = MessageBox.Show(this, "Weet u zeker dat u de geselecteerde niveaus" +
				" wilt verwijderen?\nAlle poules met deze niveaus worden hiermee ook verwijderd.",
				"Niveaus Verwijderen", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

				if (result == DialogResult.Yes)
				{
					bool pouleChanged = false;
					bool pouleRemoved = false;

					for (int i = 0; i < selection; i++)
					{
						pouleRemoved = control.RemoveNiveau(ltbNiveau.SelectedItems[i].ToString());

						if (pouleRemoved && !pouleChanged)
							pouleChanged = true;
					}

					if (pouleChanged)
						btnNotify.Changed();

					updateNiveauList();
					updatePouleList();
				}
			}
		}

		/// <summary>
		/// Het toevoegen van een nieuwe poule.
		/// </summary>
		/// <param name="sender">Sender object</param>
		/// <param name="e">EventArgs</param>
		private void addPouleButton_Click(object sender, EventArgs e)
		{
			PouleEditForm editForm = new PouleEditForm(new PouleView(), true, control);

			editForm.EditDone += addPouleForm_EditDone;
			editForm.ShowDialog(this);
		}

		/// <summary>
		/// Het veranderen van een bestaande poule.
		/// </summary>
		/// <param name="sender">Sender object</param>
		/// <param name="e">EventArgs</param>
		private void changePouleButton_Click(object sender, EventArgs e)
		{
			if (lvwPoules.SelectedItems.Count == 1)
			{
				PouleView poule = (PouleView)lvwPoules.SelectedItems[0].Tag;
				PouleEditForm editForm = new PouleEditForm(poule, false, control);

				editForm.EditDone += editPouleForm_EditDone;
				editForm.ShowDialog(this);
			}
		}

		/// <summary>
		/// Geselecteerde poule(s) verwijderen.
		/// </summary>
		/// <param name="sender">Sender object</param>
		/// <param name="e">EventArgs</param>
		private void removePouleButton_Click(object sender, EventArgs e)
		{
			int selection = lvwPoules.SelectedItems.Count;

			if (selection > 0)
			{
				DialogResult result = MessageBox.Show(this, "Weet u zeker dat u de geselecteerde poules" +
				" wilt verwijderen?", "Poules Verwijderen", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

				if (result == DialogResult.Yes)
				{
					for (int i = 0; i < selection; i++)
						control.RemovePoule((PouleView)lvwPoules.SelectedItems[i].Tag);

					btnNotify.Changed();
					updatePouleList();
				}
			}
		}

		#endregion

		#region Events

		/// <summary>
		/// Het sorteren van de poulelijst wanneer de gebruiker op een kolom heeft geklikt.
		/// </summary>
		/// <param name="sender">Sender object</param>
		/// <param name="e">ColumnClickEventArgs</param>
		private void pouleList_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			// Als de kolom nu een andere kolom is
			if (e.Column != poulesSort)
			{
				// Update welke kolom je hebt gesorteerd
				poulesSort = e.Column;

				// Zet het sorteren nu op oplopend
				lvwPoules.Sorting = SortOrder.Ascending;
			}
			else
			{
				// Als het dezelfde kolom was, kijk dan of hij
				// vorige keer oplopend was, en doe nu het tegenovergestelde
				if (lvwPoules.Sorting == SortOrder.Ascending)
					lvwPoules.Sorting = SortOrder.Descending;
				else
					lvwPoules.Sorting = SortOrder.Ascending;
			}

			// Laat de lijst sorteren
			lvwPoules.Sort();

			// Stel nu een nieuwe sorteerder in
			lvwPoules.ListViewItemSorter = new AllColumnSorter(e.Column, lvwPoules.Sorting);
		}

		/// <summary>
		/// Afhandelen van event als er een niveau toegevoegd is.
		/// </summary>
		/// <param name="oldNiveau">Deze parameter wordt genegeerd</param>
		/// <param name="newNiveau">Nieuwe niveau</param>
		private void addNiveauForm_EditDone(string oldNiveau, string newNiveau)
		{
			if (newNiveau != null)
			{
				control.AddNiveau(newNiveau);
				updateNiveauList();
			}
		}

		/// <summary>
		/// Afhandelen van event als er een niveau gewijzigd is.
		/// </summary>
		/// <param name="oldNiveau">Oude niveau</param>
		/// <param name="newNiveau">Nieuwe niveau</param>
		private void editNiveauForm_EditDone(string oldNiveau, string newNiveau)
		{
			if (oldNiveau != null && newNiveau != null)
			{
				bool pouleChanged = control.ChangeNiveau(oldNiveau, newNiveau);
				
				if(pouleChanged)
					btnNotify.Changed();

				updateNiveauList();
				updatePouleList();
			}
		}

		/// <summary>
		/// Afhandelen van event als er een poule toegevoegd is.
		/// </summary>
		/// <param name="poule">Gewijzigde PouleView</param>
		private void addPouleForm_EditDone(PouleView poule)
		{
			if (poule != null)
			{
				control.AddPoule(poule);
				btnNotify.Changed();
				updatePouleList();
			}
		}

		/// <summary>
		/// Afhandelen van event als er een poule gewijzigd is.
		/// </summary>
		/// <param name="poule">Gewijzigde PouleView</param>
		private void editPouleForm_EditDone(PouleView poule)
		{
			if (poule != null)
			{
				btnNotify.Changed();
				updatePouleList();
			}
		}

		/// <summary>
		/// De event die de data herlaadt.
		/// </summary>
		/// <param name="updateGUI">Boolean updaten</param>
		private void reload(bool updateGUI)
		{
			btnNotify.Reload();
		}

		#endregion

		#region AbstractWizardStep<object> overschrijvende methodes

		/// <summary>
		/// Lege override
		/// </summary>
		/// <returns></returns>
		protected override object readInput()
		{
			return null;
		}

		/// <summary>
		/// Gaat alleen door als er minstens 1 poule gemaakt is
		/// </summary>
		/// <returns></returns>
		protected override bool validateNextInt()
		{
			if (lvwPoules.Items.Count == 0)
			{
				MessageBox.Show("Er zijn geen poules gedefinieerd.\nCreëer tenminste 1 "+
				"poule om door te gaan.", "Geen poules gedefinieerd", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				Save();
				return true;
			}

			return false;
		}

		#endregion
	}
}
