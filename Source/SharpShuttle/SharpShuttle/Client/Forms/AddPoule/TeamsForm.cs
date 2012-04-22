using System;
using System.Drawing;
using System.Windows.Forms;
using Shared.Views;
using UserControls.TeamControls;
using Client.Controls;
using Shared.Domain;

namespace Client.Forms.AddPoule
{
	/// <summary>
	/// Scherm waarin teams gevormd worden voor een poule die tijdens
	/// het toernooi wordt aangemaakt
	/// </summary>
	public partial class TeamsForm : Form
	{
		/// <summary>
		/// De notification control
		/// </summary>
		private TeamsControl control;
		/// <summary>
		/// Methode die aangeroepen ordt als alle teams gevormd zijn
		/// </summary>
		/// <param name="teams"></param>
		public delegate void TeamDone(TeamViews teams);

		/// <summary>
		/// Event die aangeeft dat alle teams gevormd zijn
		/// </summary>
		public event TeamDone DoneTeaming;

		/// <summary>
		/// Alle gevormde teams
		/// </summary>
		private TeamViews teams = new TeamViews();

		/// <summary>
		/// De poule
		/// </summary>
		private PouleView poule;

		/// <summary>
		/// Initialiseert het scherm met een poule en lijst van spelers
		/// De twee scrollbars worden ook aan elkaar gelinkt
		/// </summary>
		/// <param name="poule"> De poule </param>
		/// <param name="players"> De spelers </param>
		public TeamsForm(PouleView poule, PlayerViews players)
		{
			control = new TeamsControl();
			
			InitializeComponent();
			this.poule = poule;

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

			int prepos = lvwPlayer1.ScrollPosition;

			control.CreatePouleTeams(players, poule);

			ShowTeamsPlayers(0);

			lvwPlayer1.ScrollPosition = prepos;
		}

		#region Weergave
		/// <summary>
		/// Geeft de spelers in de listviews weer;
		/// </summary>
		/// <param name="pouleID"> De poule ID</param>
		private void ShowTeamsPlayers(int pouleID)
		{
			PlayerViews pv1;
			PlayerViews pv2;

			int scrollPos = lvwPlayer1.ScrollPosition;

			pv1 = control.GetTeamPlayers(pouleID, out pv2);

			lvwPlayer1.DataSource = pv1;
			lvwPlayer2.DataSource = pv2;

			lvwPlayer1.ScrollPosition = scrollPos;
			lvwPlayer2.ScrollPosition = scrollPos;
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

			if (fromList != toList && poule.DisciplineEnum == Poule.Disciplines.Mixed)
				e.Effect = DragDropEffects.None;
			else 
				e.Effect = DragDropEffects.Move;
		}

		/// <summary>
		/// Registreert het droppen op de player listviews, en handelt deze af.
		/// </summary>
		/// <param name="send"></param>
		/// <param name="e"></param>
		private void lvPlayers_DragDrop(object send, DragEventArgs e)
		{
			ListViewItem fromItem = (ListViewItem) e.Data.GetData(typeof (ListViewItem));
			TeamPlayerListView fromList = (TeamPlayerListView) fromItem.ListView;

			TeamPlayerListView toList = (TeamPlayerListView) send;
			Point cp = toList.PointToClient(new Point(e.X, e.Y));
			ListViewItem toItem = toList.GetItemAt(cp.X, cp.Y);

			int fromIndex = fromItem.Index;
			int toIndex = toItem.Index;

			if (fromItem == toItem) return;

			Boolean mixed = control.IsMixed(0);

			if ((mixed && toList != fromList)) return;

			fromList.Items.Remove(fromItem);
			toList.Items.Insert(toIndex, fromItem);
			toList.Items.Remove(toItem);
			fromList.Items.Insert(fromIndex, toItem);

			control.SwitchPlayers(0, fromList == lvwPlayer1, toList == lvwPlayer1, fromIndex, toIndex);

		}

		#endregion

		#region Buttons
		/// <summary>
		/// Rond het vormen van teams af, genereert het DoneTeaming event,
		/// en sluit het scherm
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOk_Click(object sender, EventArgs e)
		{
			if (DoneTeaming != null)
			{
				teams = control.GetTeams(0);
				DoneTeaming(teams);
				Close();
			}
		}

		/// <summary>
		/// Sluit het scherm
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
