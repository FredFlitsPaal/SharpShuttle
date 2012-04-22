using System;
using System.Windows.Forms;
using Client.Controls;
using Shared.Sorters;
using Shared.Views;
using UserControls.AbstractWizard;
using UserControls.NotificationControls;

namespace Client.Forms.ClientWizard.TournamentInitializationWizard
{
	/// <summary>
	/// Scherm waarin spelers aan het toernooi toegevoegd worden
	/// </summary>
	public partial class PlayersStep : GenericAbstractWizardStep<object>
    {
		/// <summary>
		/// Businesslogica
		/// </summary>
		private PlayersControl control;
		/// <summary>
		/// De notification control
		/// </summary>
		private NotificationControl btnNotify;

        // Op welke kolom de lijst het laatst is gesorteerd
        private int playersSort = -1;

		/// <summary>
		/// Methode die aangeroepen wordt als het wijzigen van een speler klaar is
		/// </summary>
		/// <param name="player"></param>
		public delegate void PlayerEditDoneEvent(PlayerView player);
		/// <summary>
		/// Methode die aangeroepen wordt als het importeren van toernooidata klaar is
		/// </summary>
		/// <param name="players"></param>
		/// <param name="poules"></param>
		public delegate void ImportDoneEvent(PlayerViews players, PouleViews poules);
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

		#region Constructor en Init methodes

		/// <summary>
		/// Default constructor
		/// </summary>
		public PlayersStep()
        {
			init();
		}

		/// <summary>
		/// Initialiseert GUI en wizard elementen
		/// </summary>
		private void init()
		{
			control = new PlayersControl();
			control.ReloadEvent += reload;

			InitializeComponent();

			createNotification();

			Text = "Inschrijvingen";
			previous_visible = true;
			next_visible = true;
			finish_visible = false;
		}

		/// <summary>
		/// wordt elke keer als erop volgende in het scherm wat voor dit scherm komt op nieuw aangeroepen
		/// hier zou je data van de server moeten worden geladen.
		/// </summary>
		public void SetForm()
		{
			control.Initialize();
			updatePlayersList();

			btnNotify.Status = NotificationControl.State.Default;
			btnNotify.Refresh();
		}

		#endregion

		#region Notificatie Control

		/// <summary>
		/// Maakt de notification control aan
		/// </summary>
		private void createNotification()
		{
			btnNotify = new NotificationControl(Notification, control);
			btnNotify.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			btnNotify.Location = new System.Drawing.Point(625, 430);
			btnNotify.Name = "btnNotify";
			btnNotify.Size = new System.Drawing.Size(90, 23);
			btnNotify.TabIndex = 9;
			btnNotify.UseVisualStyleBackColor = true;
			Controls.Add(btnNotify);
			btnNotify.WorkStart += StartWork;
			btnNotify.WorkFinished += EndWork;
		}

		/// <summary>
		///	Reloadt het scherm of slaat alle spelers op,
		/// afhankelijk van de toestand van de notification control
		/// </summary>
		/// <param name="A"></param>
		public void Notification(NotificationControl.Action A)
		{
			if (A == NotificationControl.Action.Reload)
				notificationUpdate();
			else if (A == NotificationControl.Action.Save)
				control.SaveAllPlayers();
		}

		/// <summary>
		/// Herlaad het scherm, waarbij all wijzigingen verloren gaan
		/// </summary>
		private void notificationUpdate()
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(notificationUpdate));
				return;
			}

			control.Initialize();
			updatePlayersList();
		}

		/// <summary>
		/// Begin van het notification werk
		/// </summary>
		/// <param name="action"></param>
		public void StartWork(NotificationControl.Action action)
		{
			if (this.InvokeRequired)
				this.Invoke(new EnabledInvoker(setEnable), false);
			else
				setEnable(false);
		}

		/// <summary>
		/// Einde van het notification werk
		/// </summary>
		/// <param name="action"></param>
		public void EndWork(NotificationControl.Action action)
		{
			if (this.InvokeRequired)
				this.Invoke(new EnabledInvoker(setEnable), true);
			else
				setEnable(true);
		}

		/// <summary>
		/// Zet de enabled van alle formelementen
		/// </summary>
		/// <param name="enabled"></param>
		private void setEnable(bool enabled)
		{
			btnAdd.Enabled = enabled;
			btnEdit.Enabled = enabled;
			btnImport.Enabled = enabled;
			btnRemove.Enabled = enabled;
		}

		#endregion

		#region Save en laadmethode voor de ListView

		/// <summary>
		/// Wordt aangeroepen als er op OK gedrukt wordt in de savedialog
		/// </summary>
		public override void Save()
		{
			control.SaveAllPlayers();
		}

		/// <summary>
		/// bind de lijst van spelers opniew aan de listview
		/// </summary>
		private void updatePlayersList()
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(updatePlayersList));
				return;
			}

			int scrollPos = lvwPlayers.ScrollPosition;
			PlayerViews pv = control.GetAllPlayers();

			if (pv.Count < 1)
			{
				btnEdit.Enabled = false;
				btnRemove.Enabled= false;
			}

			else
			{
				btnEdit.Enabled = true;
				btnRemove.Enabled = true;
			}

			lblAmount.Text = pv.Count.ToString();

			lvwPlayers.DataSource = pv;
			lvwPlayers.ScrollPosition = scrollPos;
		}

		#endregion

        #region Eventhandlers Buttons

		/// <summary>
		/// Wordt aangeroepen als op de Importeer knop wordt geklikt.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void btnImport_Click(object sender, EventArgs e)
        {
            ImportPlayersForm import = new ImportPlayersForm(control);
			import.ImportDone += importPlayersForm_ImportDone;
            import.ShowDialog(this);
        }

		/// <summary>
		/// Wordt aangeroepen als op de Toevoegen knop wordt geklikt.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            PlayerEditForm newForm = new PlayerEditForm(new PlayerView(), true, control);

			newForm.EditDone += addPlayerForm_EditDone;
            newForm.ShowDialog(this);
        }

		/// <summary>
        /// Wordt aangeroepen als op de Wijzig knop wordt geklikt.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
			if (lvwPlayers.SelectedItems.Count == 1)
			{
				PlayerView player = (PlayerView) lvwPlayers.SelectedItems[0].Tag;
				
				PlayerEditForm newForm = new PlayerEditForm(player, false, control);

				newForm.EditDone += editPlayerForm_EditDone;
				newForm.ShowDialog(this);
			}
        }

        /// <summary>
        /// Wordt anageroepen als op de Verwijder knop wordt geklikt.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemove_Click(object sender, EventArgs e)
        {
			int selection = lvwPlayers.SelectedItems.Count;

			if (selection == 1)
			{
				PlayerView selectedPlayer = (PlayerView) lvwPlayers.SelectedItems[0].Tag;

				DialogResult result = MessageBox.Show(this, String.Format("Weet u zeker dat u {0} wilt verwijderen?"
											, selectedPlayer.Name), "Deelnemers verwijderen", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			
				if (result == DialogResult.Yes)
				{
					control.RemovePlayer(selectedPlayer);
					updatePlayersList();
				}
			}
			else if (selection > 1)
			{
				DialogResult result = MessageBox.Show(this, "Weet u zeker dat u de geselecteerde spelers wilt verwijderen?"
														, "Deelnemers verwijderen", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

				if (result == DialogResult.Yes)
				{
					for (int i = selection-1; i >= 0; i--)
					{
						control.RemovePlayer((PlayerView)lvwPlayers.SelectedItems[i].Tag);
					}
					updatePlayersList();
				}
			}

			if (control.IsChanged())
				btnNotify.Changed();
        }

		/// <summary>
		/// Dubbelklik op een record in de listview
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lvwPlayers_DoubleClick(object sender, EventArgs e)
		{
			ListViewItem item = lvwPlayers.SelectedItems[0];

			if (item != null)
			{
				 btnEdit_Click(sender, e);
			}
		}
       
		/// <summary>
		/// Het sorteren van de spelerslijst per kolom.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lvwPlayers_ColumnClick(object sender, ColumnClickEventArgs e)
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

		#region Events gekoppeld aan de externe form

		/// <summary>
		/// De event voor het toevoegen van 1 speler.
		/// </summary>
		/// <param name="player"></param>
		private void addPlayerForm_EditDone(PlayerView player)
		{
			if (player != null)
			{
				control.AddPlayer(player);
				updatePlayersList();
				btnNotify.Changed();
			}
		}

		/// <summary>
		/// De event voor het wijzijgen van 1 speler.
		/// </summary>
		/// <param name="player"></param>
		private void editPlayerForm_EditDone(PlayerView player)
		{
			if (player != null)
			{
				updatePlayersList();
				btnNotify.Changed();
			}
		}

		/// <summary>
		/// De event voor het importeren van spelers.
		/// </summary>
		/// <param name="players"></param>
		/// <param name="poules"></param>
		private void importPlayersForm_ImportDone(PlayerViews players , PouleViews poules)
		{
			if (players != null)
			{
				control.AddPlayers(players);
				updatePlayersList();
				btnNotify.Changed();
			}

			if (poules != null )
				if (poules.Count > 0)
				{
					control.AddPoules(poules);
					control.SavePoules();
				}
		}

		/// <summary>
		/// De event die de data reload
		/// </summary>
		/// <param name="updateGUI"></param>
		private void reload (bool updateGUI)
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
		/// Ga pas terug na bevestiging van de gebruiker
		/// </summary>
		/// <returns></returns>
		protected override bool validatePreviousInt()
		{
			bool valid = true;
			if (control.IsChanged())
				valid = saveDialog("Opslaan Spelers", "Wilt U de veranderingen in de spelers opslaan?");
			return valid;
		}

		/// <summary>
		/// Slaat de spelers op en returnt altijd true
		/// </summary>
		/// <returns></returns>
		protected override bool validateNextInt()
		{
			Save();
			return true;
		}

		#endregion

    }
}
