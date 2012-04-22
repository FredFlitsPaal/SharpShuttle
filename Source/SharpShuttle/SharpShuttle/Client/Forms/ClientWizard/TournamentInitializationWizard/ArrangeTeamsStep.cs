using System;
using System.Drawing;
using System.Windows.Forms;
using Client.Controls;
using Shared.Views;
using UserControls.AbstractWizard;
using UserControls.NotificationControls;
using UserControls.TeamControls;

namespace Client.Forms.ClientWizard.TournamentInitializationWizard
{

	/// <summary>
	/// WizardStep v
	/// </summary>
	public partial class ArrangeTeamsStep : GenericAbstractWizardStep<object>
    {
		/// <summary>
		/// Businesslogica
		/// </summary>
		private TeamsControl control;

		/// <summary>
		/// De notificationcontrol
		/// </summary>
		private NotificationControl btnNotify;

		/// <summary>
		/// Event waardoor er wordt gereload
		/// </summary>
		/// <param name="updateGUI"></param>
		public delegate void ReloadEvent(bool updateGUI);

		//NOTE wordt niet gebruikt
		//private int selectedValue = -1;

		#region Constructor & init

		/// <summary>
		/// Roept de init methode en start de gegevensverwerking.
		/// </summary>
		public ArrangeTeamsStep()
		{
			init();
		}

		/// <summary>
		/// Initialiseert de componenten van het scherm
		/// Ook worden de twee scrollbars aan elkaar gelinkt
		/// </summary>
		private void init()
		{
			control = new TeamsControl();

			InitializeComponent();

			createNotification();

			control.ReloadEvent += reload;

			//Drag en drop voor lvPlayer1
			lvwPlayer1.ItemDrag += lvPlayers_ItemDrag;
			lvwPlayer1.DragDrop += lvPlayers_DragDrop;
			lvwPlayer1.DragEnter += move_DragEnter;

			//Drag en drop voor lvPlayer2
			lvwPlayer2.ItemDrag += lvPlayers_ItemDrag;
			lvwPlayer2.DragDrop += lvPlayers_DragDrop;
			lvwPlayer2.DragEnter += move_DragEnter;

			//scrolbars aan elkaar linken
			lvwPlayer1.onScroll += lvPlayer1_OnScroll;
			lvwPlayer2.onScroll += lvPlayer2_OnScroll;

			Text = "Teamindelingen";
			previous_visible = true;
			next_visible = true;
			finish_visible = false;
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

		#region Notificatie Control

		/// <summary>
		/// Aanmaken van de Notificatie Button.
		/// </summary>
		private void createNotification()
		{
			btnNotify = new NotificationControl(Notification, control);
			btnNotify.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			btnNotify.Location = new Point(593, 3);
			btnNotify.Name = "btnNotify";
			btnNotify.Size = new Size(90, 23);
			btnNotify.TabIndex = 4;
			btnNotify.UseVisualStyleBackColor = true;
			pnlConfirmAll.Controls.Add(btnNotify);
		}

		/// <summary>
		/// Handelt de de notificatie af van de notificatiecontrol/button
		/// </summary>
		/// <param name="A"></param>
		public void Notification(NotificationControl.Action A)
		{
			if (A == NotificationControl.Action.Reload)
				notificationUpdate();

			else if (A == NotificationControl.Action.Save)
				control.SaveAllTeams();
		}

		/// <summary>
		/// Methode die wordt aangeropen om de data te herladen
		/// </summary>
		private void notificationUpdate()
		{
			if (this.InvokeRequired)
			{
				Invoke(new MethodInvoker(notificationUpdate));
				return;
			}

			control.Initialize();

			int prepos = lvwPlayer1.ScrollPosition;

			PouleViews pouleItems = control.GetDoublePoules();
			cboPoules.DataSource = pouleItems;

			if (pouleItems.Count > 0)
				ShowTeamsPlayers(cboPoules.SelectedValue);
			
			lvwPlayer1.ScrollPosition = prepos;
		}

		#endregion

		#region Form inladen & Saven

		/// <summary>
		/// wordt elke keer als in het scherm voor dit scherm op volgende wordt geklikt aangeroepen
		/// </summary>
		public void SetForm()
		{
			control.Initialize();

			PouleViews pouleItems = control.GetDoublePoules();
			cboPoules.DataSource = pouleItems;

			if (pouleItems.Count > 0)
				ShowTeamsPlayers(cboPoules.SelectedValue);

			btnNotify.Status = NotificationControl.State.Default;
			btnNotify.Refresh();
		}

		/// <summary>
		/// Wordt aangeroepen als er op OK gedrukt wordt in de savedialog
		/// </summary>
		public override void Save()
		{
			control.SaveAllTeams();
		}

		#endregion

		#region Drag and Drop

		/// <summary>
		/// Starten van het draggen
		/// </summary>
		/// <param name="send"></param>
		/// <param name="e"></param>
        private void lvPlayers_ItemDrag(object send, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        /// <summary>
        /// Methode die het draggen weergeeft als er gedragd wordt, door icoon bij de muis
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void move_DragEnter(object send, DragEventArgs e)
        {
			ListViewItem fromItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
			TeamPlayerListView fromList = (TeamPlayerListView)fromItem.ListView;
			TeamPlayerListView toList = (TeamPlayerListView)send;

			if (fromList != toList && control.IsMixed(cboPoules.SelectedValue))
				e.Effect = DragDropEffects.None;

			else e.Effect = DragDropEffects.Move;
        }

        /// <summary>
        /// Methode die het droppen in listviews afhandelt. Het wisselen van spelers in een team
        /// wordt via de methode switchPlayer in de TeamsControl afgehandeld.
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
		private void lvPlayers_DragDrop(object send, DragEventArgs e)
		{
			ListViewItem fromItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
			TeamPlayerListView fromList = (TeamPlayerListView)fromItem.ListView;

			TeamPlayerListView toList = (TeamPlayerListView)send;
			Point cp = toList.PointToClient(new Point(e.X, e.Y));
			ListViewItem toItem = toList.GetItemAt(cp.X, cp.Y);

			int fromIndex = fromItem.Index;
			int toIndex = toItem.Index;

			if (fromItem == toItem) return;

			Boolean mixed = control.IsMixed((cboPoules.SelectedValue));

			if ((mixed && toList != fromList)) return;

			fromList.Items.Remove(fromItem);
			toList.Items.Insert(toIndex, fromItem);
			toList.Items.Remove(toItem);
			fromList.Items.Insert(fromIndex, toItem);

			control.SwitchPlayers(cboPoules.SelectedValue, fromList == lvwPlayer1, toList == lvwPlayer1, fromIndex, toIndex);
			btnNotify.Changed();
		}

		#endregion

		#region Scrollbars

		/// <summary>
		/// koppelt de lvPlayer2 aan de lvPlayer1, als de lvPlayer2 wordt gescrold dan
		/// wordt lvPlayer1 ook gescrold
		/// </summary>
		/// <param name="send"></param>
		/// <param name="e"></param>
		private void lvPlayer1_OnScroll(object send, ScrollEventArgs e)
		{
			lvwPlayer2.ScrollPosition = lvwPlayer1.ScrollPosition;
		}

		/// <summary>
		/// koppelt de lvplayer1 aan de lvPlayer2
		/// </summary>
		/// <param name="send"></param>
		/// <param name="e"></param>
		private void lvPlayer2_OnScroll(object send, ScrollEventArgs e)
		{
			lvwPlayer1.ScrollPosition = lvwPlayer2.ScrollPosition;
		}

		#endregion

		#region UserControls

		/// <summary>
		/// Geeft de spelers in de listviews weer;
		/// </summary>
		/// <param name="pouleID"></param>
		private void ShowTeamsPlayers(int pouleID)
		{
			PlayerViews pv1 = new PlayerViews();
			PlayerViews pv2 = new PlayerViews();

			int scrollPos = lvwPlayer1.ScrollPosition;

			pv1 = control.GetTeamPlayers(pouleID, out pv2);
			
			lvwPlayer1.DataSource = pv1;
			lvwPlayer2.DataSource = pv2;

			lvwPlayer1.ScrollPosition = scrollPos;
			lvwPlayer2.ScrollPosition = scrollPos;
		}

		/// <summary>
		/// Afhandelen van een verandering van selectie inde cmbPoules
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cmbPoules_SelectedIndexChanged(object sender, EventArgs e)
		{
			ShowTeamsPlayers(cboPoules.SelectedValue);
		}

		#endregion

		#region #region AbstractWizardStep<object> overschrijvende methodes

		/// <summary>
		/// Lege override van readInput
		/// </summary>
		/// <returns></returns>
		protected override object readInput()
		{
			return null;
		}

		/// <summary>
		/// Override van validateNextInt die teams opslaat en altijd true returnt 
		/// </summary>
		/// <returns></returns>
		protected override bool validateNextInt()
		{
			Save();
			
			return true;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		protected override bool validatePreviousInt()
		{
			bool valid = true;
			if (control.isChanged())
				valid = saveDialog("Opslaan Poule-Indeling", "Wilt U de veranderingen in de team indeling(en) opslaan?");
			return valid;
		}

		#endregion

	}
}