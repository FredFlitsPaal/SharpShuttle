using System;
using System.Windows.Forms;
using Shared.Domain;
using Shared.Views;
using UserControls.AbstractWizard;
using Client.Controls;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using UserControls.NotificationControls;

namespace Client.Forms.ClientWizard.TournamentInitializationWizard
{
	/// <summary>
	/// Scherm waar spelers in poules ingedeeld worden
	/// </summary>
	public partial class PoulesLayoutStep : GenericAbstractWizardStep<object>
	{
		/// <summary>
		/// Businesslogica
		/// </summary>
		private ManagePlayerPouleControl control;
		/// <summary>
		/// De notification control
		/// </summary>
		private NotificationControl btnNotify;
		
		/// <summary>
		/// Lijst van alle spelers
		/// </summary>
		private PlayerPoulesViews playersViews;
		/// <summary>
		/// Lijst van alle poules
		/// </summary>
		private PouleViews poulesViews;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="enabled"></param>
		public delegate void EnabledInvoker(bool enabled);
		/// <summary>
		/// Event voor wanneer er gerefreshd wordt
		/// </summary>
		/// <param name="updateGUI"></param>
		public delegate void ReloadEvent(bool updateGUI);

		#region Constructor en Init methodes

		/// <summary>
		/// Default constructor
		/// </summary>
		public PoulesLayoutStep()
		{
			init();
		}

		/// <summary>
		/// Initialiseer GUI en wizard elementen
		/// </summary>
		private void init()
		{
			control = new ManagePlayerPouleControl();
			
			InitializeComponent();

			control.ReloadEvent += reload;

			createNotification();

			// Maak de default nodes voor de twee treeviews
			tvwPoules.CreateDefaultNodes();
			tvwPlayers.CreateDefaultNodes();

			// ContextMenuStrip functies
			cmsItemRemove.Click += removeCMenuItem_Click;
			cmsItemRemoveAllPlayers.Click += removeAllPlayersCMenuItem_Click;
			cmsItemRemovePlayers.Click += removePlayersCMenuItem_Click; 
			tvwPoules.MouseUp += tvwPoules_MouseUp;

			tvwPoules.DragDrop -= tvwPoules.poulesTreeView_DragDrop;
			tvwPoules.DragDrop += poulesTreeView_DragDrop;

			Text = "Poule Indeling";
			previous_visible = true;
			next_visible = true;
			finish_visible = false;
		}

		#endregion

		#region Notification

		/// <summary>
		/// Maakt de notification button
		/// </summary>
		private void createNotification()
		{
			btnNotify = new NotificationControl(Notification, control);
			btnNotify.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			btnNotify.Location = new Point(647, 385);
			btnNotify.Name = "btnNotify";
			btnNotify.Size = new Size(90, 23);
			btnNotify.TabIndex = 5;
			btnNotify.UseVisualStyleBackColor = true;
			Controls.Add(btnNotify);
			btnNotify.WorkStart += StartWork;
			btnNotify.WorkFinished += EndWork;
		}

		/// <summary>
		///	Reloadt het scherm of slaat alle spelerindelingen op,
		/// afhankelijk van de toestand van de notification control
		/// </summary>
		/// <param name="A"></param>
		public void Notification(NotificationControl.Action A)
		{
			if (A == NotificationControl.Action.Reload)
				updateData();
			else if (A == NotificationControl.Action.Save)
				control.SavePouleLayout(tvwPoules.GetAllPoules());
		}

		/// <summary>
		/// Begin van het notification werk
		/// </summary>
		/// <param name="action"></param>
		public void StartWork(NotificationControl.Action action)
		{
			if (InvokeRequired)
				Invoke(new EnabledInvoker(setEnable), false);
			else
				setEnable(false);
		}

		/// <summary>
		/// Einde van het notification werk
		/// </summary>
		/// <param name="action"></param>
		public void EndWork(NotificationControl.Action action)
		{
			if (InvokeRequired)
				Invoke(new EnabledInvoker(setEnable), true);
			else
				setEnable(true);
		}

		/// <summary>
		/// Zet de enabled van alle formelementen
		/// </summary>
		/// <param name="enabled"></param>
		private void setEnable(bool enabled)
		{
			btnGenLayout.Enabled = enabled;
			tvwPoules.Enabled = enabled;
			tvwPlayers.Enabled = enabled;
			if (enabled)
			{
				resetTreeViews();
				addPoules(poulesViews);
				addPlayers(playersViews);
			}
		}

		#endregion

		#region Laden en saven

		/// <summary>
		/// wordt elke keer als erop volgende in het scherm wat voor dit scherm komt op nieuw aangeroepen
		/// hier zou je data van de server moeten worden geladen.
		/// </summary>
		public void SetForm()
		{
			btnNotify.Reload();
		}

		/// <summary>
		/// Update alle data
		/// </summary>
		private void updateData()
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(updateData));
				return;
			}

			playersViews = control.GetPlayers();
			poulesViews = control.GetPoules();
			control.CombinePlayersAndPoules(playersViews, poulesViews);
			control.SetPreferencesOfPlayers(playersViews, poulesViews);
		}

		/// <summary>
		/// Wordt aangeroepen als er op OK gedrukt wordt in de savedialog
		/// </summary>
		public override void Save()
		{
			control.SavePouleLayout(tvwPoules.GetAllPoules());
		}

		/// <summary>
		/// Event die een reload doet.
		/// </summary>
		/// <param name="updateGUI"></param>
		private void reload(bool updateGUI)
		{
			btnNotify.Reload();
		}

		#endregion

		#region Treeview methods

		/// <summary>
		/// Schoont alle nodes op
		/// </summary>
		private void resetTreeViews()
		{
			tvwPlayers.allPlayersNode.Nodes.Clear();
			tvwPlayers.unassignedAllNode.Nodes.Clear();
			tvwPlayers.unassignedMaleNode.Nodes.Clear();
			tvwPlayers.unassignedFemaleNode.Nodes.Clear();
			tvwPlayers.maleNode.Nodes.Clear();
			tvwPlayers.femaleNode.Nodes.Clear();
			tvwPoules.disciplineNode.Nodes.Clear();
		}

		/// <summary>
		/// Voegt poules toe aan de discipline boom
		/// </summary>
		/// <param name="poules"></param>
		private void addPoules(PouleViews poules)
		{
			foreach (PouleView poule in poules)
				tvwPoules.AddPoule(poule);
		}

		/// <summary>
		/// Voeg spelers toe aan de spelers boom
		/// </summary>
		/// <param name="players"></param>
		private void addPlayers(PlayerPoulesViews players)
		{
			foreach (PlayerPoulesView player in players)
				tvwPlayers.AddNewPlayer(player);
		}

		/// <summary>
		/// Zet alle spelers in de poules naar hun voorkeur
		/// </summary>
		/// <param name="players"></param>
		/// <param name="disciplines"></param>
		private void assignPlayers(TreeNode players, TreeNode disciplines)
		{
			foreach (TreeNode playerNode in players.Nodes)
				foreach (PouleView poule in ((PlayerPoulesView)playerNode.Tag).PreferencesPoules)
				{
					TreeNode pouleNode = getPouleNode(disciplines, poule);
					if (pouleNode != null)
						addToPoule(playerNode, pouleNode);
				}
		}

		/// <summary>
		/// Zoek de treenode die bij de gegeven poule hoort
		/// </summary>
		/// <param name="disciplines"></param>
		/// <param name="poule"></param>
		/// <returns></returns>
		private TreeNode getPouleNode(TreeNode disciplines, PouleView poule)
		{
			foreach (TreeNode discipline in disciplines.Nodes)
				foreach (TreeNode pouleNode in discipline.Nodes)
					if (((PouleView)pouleNode.Tag).Id == poule.Id)
						return pouleNode;

			return null;
		}

		/// <summary>
		/// Verwijder speler uit een poule
		/// </summary>
		/// <param name="playerNode"></param>
		/// <param name="pouleNode"></param>
		private void removeFromPoule(TreeNode playerNode, TreeNode pouleNode)
		{
			// Verwijder speler van poule
			PlayerPoulesView player = (PlayerPoulesView)playerNode.Tag;
			PouleView poule = (PouleView)pouleNode.Tag;
			control.RemovePlayerFromPoule(player, poule);
			tvwPoules.RemoveFromPoule(playerNode, pouleNode);

			if (player.AssignedPoules.Count == 0)
				tvwPlayers.AddToUnassigned(player);
			btnNotify.Changed();
		}

		/// <summary>
		/// Voegt een speler toe aan een poule
		/// </summary>
		/// <param name="playerNode"></param>
		/// <param name="pouleNode"></param>
		private void addToPoule(TreeNode playerNode, TreeNode pouleNode)
		{
			PlayerPoulesView player = (PlayerPoulesView)playerNode.Tag;
			PouleView poule = (PouleView)pouleNode.Tag;

			// Voeg speler toe aan de poule
			if (!tvwPoules.IsAssignedTo(player, poule))
				if ((player.Gender == "M" && (poule.getGender() == PouleView.Gender.M || poule.getGender() == PouleView.Gender.U))
					|| (player.Gender == "V" && (poule.getGender() == PouleView.Gender.V || poule.getGender() == PouleView.Gender.U)))
				{
					control.AddPlayerToPoule(player, poule);
					tvwPoules.AddToPoule(playerNode, pouleNode);

					if (player.AssignedPoules.Count == 1)
						tvwPlayers.RemoveFromUnassigned(player);

					showPlayerDetail(player);
				}
			btnNotify.Changed();
		}

		/// <summary>
		/// Controleer of alle poules leeg zijn
		/// </summary>
		/// <returns></returns>
		private bool allPoulesEmpty()
		{
			return (tvwPoules.GetAllPoules().Count == tvwPoules.GetPoules(tvwPoules.Empty).Count);
		}

		/// <summary>
		/// Drag drop van spelers op een poule
		/// </summary>
		/// <param name="send"></param>
		/// <param name="e"></param>
		private void poulesTreeView_DragDrop(object send, DragEventArgs e)
		{
			List<TreeNode> moveNodes = (List<TreeNode>)e.Data.GetData(typeof(List<TreeNode>));
			TreeNode destNode = ((TreeView)send).GetNodeAt(((TreeView)send).PointToClient(new Point(e.X, e.Y)));

			if (destNode != null)
				foreach (TreeNode node in moveNodes)
					if (destNode.Name == "Poule" && !(tvwPoules.IsAssignedTo((PlayerPoulesView)node.Tag, (PouleView)destNode.Tag)))
						addToPoule(node, destNode);
		}

		#endregion

		#region Details label
		/// <summary>
		/// Laat details van de geselecteerde speler of poule zien
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tvwPoules_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (e.Node.Name == "Player")
				showPlayerDetail((PlayerPoulesView)e.Node.Tag);
			else if (e.Node.Name == "Poule")
				showPouleDetail((PouleView)e.Node.Tag);
		}

		/// <summary>
		/// Laat details van de geselecteerde speler of poule zien
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tvwPoules_AfterSelect(object sender, TreeNodeMouseClickEventArgs e)
		{
			if (e.Node.Name == "Player")
				showPlayerDetail((PlayerPoulesView)e.Node.Tag);
			else if (e.Node.Name == "Poule")
				showPouleDetail((PouleView)e.Node.Tag);
		}

		/// <summary>
		/// Laat details van de geselecteerde speler zien
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tvwPlayers_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (e.Node.Name == "Player")
				showPlayerDetail((PlayerPoulesView)e.Node.Tag);
		}

		/// <summary>
		/// Laat details van de geselecteerde speler zien
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tvwPlayers_AfterSelect(object sender, TreeNodeMouseClickEventArgs e)
		{
			if (e.Node.Name == "Player")
				showPlayerDetail((PlayerPoulesView)e.Node.Tag);
		}

		/// <summary>
		/// Zet gegevens over een speler in de detailbox
		/// </summary>
		/// <param name="player"></param>
		private void showPlayerDetail(PlayerPoulesView player)
		{
			txtInfo.Text = playerDetail(player);
		}

		/// <summary>
		/// Zet gegevens over een poule in de detailbox
		/// </summary>
		/// <param name="poule"></param>
		private void showPouleDetail(PouleView poule)
		{
			txtInfo.Text = pouleDetail(poule);
		}

		/// <summary>
		/// Maakt een detail overview van een speler
		/// </summary>
		/// <param name="player">De speler</param>
		/// <returns>String met details over de speler</returns>
		private static string playerDetail(PlayerPoulesView player)
		{
			string result = "";
			result += "Speler\n\n";
			result += "Spelers ID: \t" + player.Id + "\n";
			result += "Naam: \t\t" + player.Name + "\n";
			result += "Geslacht: \t" + player.Gender + "\n";
			result += "Club: \t\t" + player.Club + "\n\n";
			result += "Voorkeuren: " + "\n";
			string[] prefs = Regex.Split(player.Preferences, ", ");

			for (int i = 0; i < prefs.Length; i++)
				result += prefs[i] + "\n";

			result += "\n";
			result += "Ingedeeld in: " + "\n";

			foreach (PouleView poule in player.AssignedPoules)
				result += poule.Name + " (" + poule.Discipline + ": " + poule.Niveau + ")" + "\n";

			return result;
		}

		/// <summary>
		/// Maakt een detail overview van de poule
		/// </summary>
		/// <param name="poule">De poule</param>
		/// <returns>String met details over de poule</returns>
		private static string pouleDetail(PouleView poule)
		{
			string result = "";
			result += "Poule\n\n";
			result += "Poule ID: \t\t" + poule.Id + "\n";
			result += "Naam: \t\t" + poule.Name + "\n";
			result += "Discipline: \t" + poule.Discipline + "\n";
			result += "Niveau: \t\t" + poule.Niveau + "\n";
			result += "Aantal spelers: \t" + poule.AmountPlayers + "\n";
			if (poule.DisciplineEnum == Poule.Disciplines.Mixed || 
				poule.DisciplineEnum == Poule.Disciplines.UnisexDouble ||
				poule.DisciplineEnum == Poule.Disciplines.UnisexSingle)
			{
				result += "Aantal mannen: \t" + poule.AmountMen + "\n";
				result += "Aantal vrouwen: \t" + poule.AmountWomen + "\n";
			}
			result += "\n";
			result += "Correctheid: \t" + poule.Correctness + "\n";
			return result;
		}

		/// <summary>
		/// Zet de correctheid van een poule om naar een string
		/// </summary>
		/// <param name="poules"></param>
		/// <returns></returns>
		private static string correctnessToString(PouleViews poules)
		{
			string result = "";
			foreach (PouleView poule in poules)
			{
				result += poule.Discipline + " " + poule.Niveau;
				result += " : " + poule.Correctness;
				result += "\n";
			}

			return result;
		}

		#endregion

		#region ContextMenuStrip

		/// <summary>
		/// Eén speler wordt verwijderd
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void removeCMenuItem_Click(object sender, EventArgs e)
		{
			// Verkrijg de geselecteerde node
			TreeNode node = tvwPoules.SelectedNode;
			removeFromPoule(node, node.Parent);
		}

		/// <summary>
		/// Meerdere spelers worden verwijderd
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void removePlayersCMenuItem_Click(object sender, EventArgs e)
		{
			foreach (TreeNode node in tvwPoules.SelectedNodes)
				removeFromPoule(node, node.Parent);

			tvwPoules.DeselectNodes();
		}

		/// <summary>
		/// Alle spelers uit een poule worden verwijderd
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void removeAllPlayersCMenuItem_Click(object sender, EventArgs e)
		{
			// Verkrijg de geselecteerde node
			TreeNode node = tvwPoules.SelectedNode;

			while (node.Nodes.Count > 0)
				removeFromPoule(node.Nodes[0], node);
		}

		/// <summary>
		/// Haalt de contextmenustrip op
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tvwPoules_MouseUp(object sender, MouseEventArgs e)
		{
			// Menu tonen als rechtermuisknop wordt ingedrukt
			if (e.Button == MouseButtons.Right)
			{
				// Pak de node die aangeklikt wordt
				Point p = new Point(e.X, e.Y);
				TreeNode node = tvwPoules.GetNodeAt(p);

				if (node != null)
				{
					// Selecteer de node waarop geklikt wordt
					tvwPoules.SelectedNode = node;

					// Zoek de juiste items
					switch (node.Name)
					{
						case "Player":
							if (tvwPoules.SelectedNodes.Count > 1)
							{
								setMenuItemsVisibility("RemoveMultiple");
								cmsPoules.Show(tvwPoules, p);
							}
							else
							{
								setMenuItemsVisibility("Remove");
								cmsPoules.Show(tvwPoules, p);
							}
							break;
						case "Poule":
							setMenuItemsVisibility("RemoveAll");
							cmsPoules.Show(tvwPoules, p);
							break;
					}
				}
			}
		}

		/// <summary>
		/// Bepaal welke items zichtbaar zijn
		/// </summary>
		/// <param name="kind"></param>
		private void setMenuItemsVisibility(string kind)
		{
			switch (kind)
			{
				case "Remove":
					cmsItemRemoveAllPlayers.Visible = false;
					cmsItemRemovePlayers.Visible = false;
					cmsItemRemove.Visible = true;
					break;
				case "RemoveMultiple":
					cmsItemRemoveAllPlayers.Visible = false;
					cmsItemRemovePlayers.Visible = true;
					cmsItemRemove.Visible = false;
					break;
				case "RemoveAll":
					cmsItemRemoveAllPlayers.Visible = true;
					cmsItemRemovePlayers.Visible = false;
					cmsItemRemove.Visible = false;
					break;
			}
		}

		#endregion

		#region Buttons

		/// <summary>
		/// Zet spelers automatisch in de poules van hun voorkeur(en)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnGenLayout_Click(object sender, EventArgs e)
		{
			assignPlayers(tvwPlayers.allPlayersNode, tvwPoules.disciplineNode);
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
		/// Gaat alleen door als er een geldige indeling is gemaakt
		/// </summary>
		/// <returns></returns>
		protected override bool validateNextInt()
		{
			bool next = true;
			string error;

			
			if (next)
			{
				//Alle poules zijn leeg of er bestaan geen poules
				if (allPoulesEmpty())
				{
					MessageBox.Show("Alle poules zijn leeg of er bestaan geen poules.", "Lege poules", MessageBoxButtons.OK, MessageBoxIcon.Error);
					next = false;
				}
			}

			if (next)
			{
				error = correctnessToString(tvwPoules.GetPoules(tvwPoules.Incorrect));
				if (error != "")
				{
					MessageBox.Show("De volgende foute poules werden geconstateerd: \n\n" + error, "Foute Indeling", MessageBoxButtons.OK, MessageBoxIcon.Error);
					next = false;
				}
			}

			if (next)
			{
				error = correctnessToString(tvwPoules.GetPoules(tvwPoules.Semicorrect));
				if (error != "")
					if (MessageBox.Show("De volgende complicaties werden geconstateerd: \n\n" + error + "\nWilt U toch met deze indeling doorgaan?", "Indeling", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
						next = false;
			}

			//Er zijn nog niet ingedeelde spelers, ga alleen door met bevestiging van de gebruiker
			if (next && tvwPlayers.unassignedAllNode.Nodes.Count > 0)
				if (MessageBox.Show("Niet iedereen is ingedeeld. Wilt U toch verder gaan?", "Indeling", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					next = true;
				else
					next = false;

			if (next)
			{
				error = correctnessToString(tvwPoules.GetPoules(tvwPoules.Empty));
				if (error != "")
					if (MessageBox.Show("De volgende poules zijn leeg: \n\n" + error + "\n Wilt U toch met deze indeling doorgaan?", "Indeling", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
						next = false;
			}

			if (next)
			{
				Save();
			}
			return next;
		}

		/// <summary>
		/// Ga alleen terug met bevestiging van de gebruiker
		/// </summary>
		/// <returns></returns>
		protected override bool validatePreviousInt()
		{
			bool valid = true;
			if (control.IsChanged())
				valid = saveDialog("Opslaan Poule-Indeling", "Wilt U de veranderingen in de poule indeling(en) opslaan?");
			return valid;
		}

		#endregion

	}

}
