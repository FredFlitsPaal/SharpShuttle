using System;
using System.Windows.Forms;
using Client.Controls;
using Client.Forms.ClientWizard.TournamentInitializationWizard;
using Shared.Views;
using UserControls.NotificationControls;
using Shared.Sorters;

namespace Client.Forms.AddPlayer
{

	/// <summary>
	/// Scherm waar nieuwe spelers kunnen worden toegevoegd aan het toernooi
	/// </summary>
	public partial class AddPlayerForm : Form
	{
		/// <summary>
		/// Businesslogica
		/// </summary>
		private PlayersControl control = new PlayersControl();

		/// <summary>
		/// De notification control
		/// </summary>
		private NotificationControl btnNotify;

		// Op welke kolom de lijst het laatst is gesorteerd
		private int playersSort = -1;

		/// <summary>
		/// Methode die wordt aangeroepen als het wijzigen van een speler klaar is
		/// </summary>
		/// <param name="player"> De speler die gewijzigd is</param>
		public delegate void PlayerEditDoneEvent(PlayerView player);
		/// <summary>
		/// Methode die wordt aangeroepen als het importeren van toernooigegevens klaar is
		/// </summary>
		/// <param name="players"> De geimporteerde spelers </param>
		/// <param name="poules"> De geimporteerde poules</param>
		public delegate void ImportDoneEvent(PlayerViews players, PouleViews poules);
		/// <summary>
		/// Methode die wordt aangeroepen als er een update is
		/// </summary>
		/// <param name="updateGUI"> Moet de gui ook geupdate worden</param>
		public delegate void ReloadEvent(bool updateGUI);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="enabled"></param>
		public delegate void EnabledInvoker(bool enabled);

		/// <summary>
		/// Default constructor
		/// </summary>
		public AddPlayerForm()
		{
			InitializeComponent();
			control.ReloadEvent += reload;
			createNotification();
			notificationUpdate();
		}

		#region Notification
		/// <summary>
		/// Maakt de notification control aan
		/// </summary>
		private void createNotification()
		{
			btnNotify = new NotificationControl(Notification, control);
			btnNotify.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			btnNotify.Location = new System.Drawing.Point(637, 459);
			btnNotify.Name = "btnNotify";
			btnNotify.Size = new System.Drawing.Size(90, 23);
			btnNotify.TabIndex = 9;
			btnNotify.UseVisualStyleBackColor = true;
			Controls.Add(btnNotify);
			btnNotify.WorkStart += StartWork;
			btnNotify.WorkFinished += EndWork;
		}

		/// <summary>
		/// Doet, afhankelijk van de toestand van de notification control een
		/// reload of een save
		/// </summary>
		/// <param name="A"></param>
		public void Notification(NotificationControl.Action A)
		{
			if (A == NotificationControl.Action.Reload)
				Invoke(new MethodInvoker(notificationUpdate));

			else if (A == NotificationControl.Action.Save)
				Invoke(new MethodInvoker(control.SaveAllPlayers));
		}

		/// <summary>
		/// Initialiseert de businesslogica en update de spelerlijst
		/// </summary>
		private void notificationUpdate()
		{
			control.Initialize();
			updatePlayersList();
		}

		/// <summary>
		/// Begin van het notification werk
		/// </summary>
		/// <param name="action"></param>
		public void StartWork(NotificationControl.Action action)
		{
			Invoke(new EnabledInvoker(setEnable), false);
		}

		/// <summary>
		/// Einde van het notification werk
		/// </summary>
		/// <param name="action"></param>
		public void EndWork(NotificationControl.Action action)
		{
			Invoke(new EnabledInvoker(setEnable), true);
		}

		/// <summary>
		/// Zet de enabled van alle formelementen
		/// </summary>
		/// <param name="enabled"></param>
		private void setEnable(bool enabled)
		{
			btnAdd.Enabled = enabled;
			btnEdit.Enabled = enabled;
			btnRemove.Enabled = enabled;
		}

		#endregion

		#region Save en laadmethode voor de ListView

		/// <summary>
		/// Bind de lijst van spelers opniew aan de listview
		/// </summary>
		private void updatePlayersList()
		{
			int scrollPos = lvwPlayers.ScrollPosition;
			PlayerViews pv = control.GetAllPlayers();

			if (pv.Count < 1)
			{
				btnEdit.Enabled = false;
				btnRemove.Enabled = false;
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
		/// De event die de data reload
		/// </summary>
		/// <param name="updateGUI"></param>
		private void reload(bool updateGUI)
		{
			btnNotify.Reload();
		}
		#endregion

		#region Buttons

		/// <summary>
		/// Opent een PlayerEditForm waar een nieuwe speler ingevoerd kan worden
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
		/// Opent een PlayerEditForm om de geselecteerde speler aan te passen
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnEdit_Click(object sender, EventArgs e)
		{
			if (lvwPlayers.SelectedItems.Count == 1)
			{
				PlayerView player = (PlayerView)lvwPlayers.SelectedItems[0].Tag;
				PlayerEditForm newForm = new PlayerEditForm(player, false, control);

				newForm.EditDone += editPlayerForm_EditDone;
				newForm.ShowDialog(this);
			}
		}

		/// <summary>
		/// Verwijdert de geselecteerde speler(s)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnRemove_Click(object sender, EventArgs e)
		{
			int selection = lvwPlayers.SelectedItems.Count;

			if (selection == 1)
			{
				PlayerView selectedPlayer = (PlayerView)lvwPlayers.SelectedItems[0].Tag;

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
					for (int i = selection - 1; i >= 0; i--)
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

		/// <summary>
		/// Alle spelers worden opgeslagen in de cache
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOk_Click(object sender, EventArgs e)
		{
			if (btnNotify.Status == NotificationControl.State.ExternallyChanged && control.GetAllPlayers().GetChangeTrackingList().Changed)
				MessageBox.Show("De data is out of date. U kunt de wijzigingen niet doorvoeren.", "Out of Date", MessageBoxButtons.OK, MessageBoxIcon.Error);
			else if (btnNotify.Status == NotificationControl.State.Default || btnNotify.Status == NotificationControl.State.UnsavedChanges)
				control.SaveAllPlayers();

			Close();
		}

		/// <summary>
		/// Het scherm wordt gesloten
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}
		#endregion
	}
}
