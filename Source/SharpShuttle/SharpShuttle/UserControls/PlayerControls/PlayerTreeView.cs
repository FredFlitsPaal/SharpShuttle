using Shared.Domain;
using Shared.Views;
using System.Windows.Forms;
using UserControls.AbstractControls;

namespace UserControls.PlayerControls
{
	/// <summary>
	/// Een TreeView van spelers
	/// </summary>
	public class PlayerTreeView : DomainTreeView<PlayerPoulesViews, PlayerPoulesView, Player>
	{
		/// <summary>
		/// De knoop waar alle spelers in staan
		/// </summary>
		public TreeNode allPlayersNode;
		/// <summary>
		/// De knoop waar alle mannelijke spelers in staan
		/// </summary>
		public TreeNode maleNode;
		/// <summary>
		/// De knoop waar alle vrouwelijke spelers in staan
		/// </summary>
		public TreeNode femaleNode;
		/// <summary>
		/// De knoop waar alle spelers zonder poule in staan
		/// </summary>
		public TreeNode unassignedAllNode;
		/// <summary>
		/// De knoop waar alle mannelijke spelers zonder poule in staan
		/// </summary>
		public TreeNode unassignedMaleNode;
		/// <summary>
		/// De knoop waar alle vrouwelijke spelers zonder poule in staan
		/// </summary>
		public TreeNode unassignedFemaleNode;
		
		/// <summary>
		/// Constructor voor een spelers treeview
		/// </summary>
		public PlayerTreeView()
		{
			ItemDrag += treeView_ItemDrag;
			DragDrop += treeView_DragDrop;
			DragEnter += treeView_DragEnter;
		}

		/// <summary>
		/// De datasource
		/// </summary>
		public new PlayerPoulesViews DataSource
		{
			get { return base.DataSource; }
			set { base.DataSource = value; }
		}

		/// <summary>
		/// Maakt de default nodes voor deze treeview
		/// </summary>
		public void CreateDefaultNodes()
		{
			TreeNode spelersNode = createNode("Spelers", "Spelers");
			Nodes.Add(spelersNode);
			spelersNode.Expand();
			allPlayersNode = createNode("AlleSpelers", "Alle spelers (0)");
			Nodes[0].Nodes.Add(allPlayersNode);
			maleNode = createNode("Mannen", "Mannen (0)");
			Nodes[0].Nodes.Add(maleNode);
			femaleNode = createNode("Vrouwen", "Vrouwen (0)");
			Nodes[0].Nodes.Add(femaleNode);
			unassignedAllNode = createNode("NietIngedeeld", "Oningedeelde spelers (0)");
			Nodes[0].Nodes.Add(unassignedAllNode);
			unassignedMaleNode = createNode("NietIngedeeld", "Oningedeelde mannen (0)");
			Nodes[0].Nodes.Add(unassignedMaleNode);
			unassignedFemaleNode = createNode("NietIngedeeld", "Oningedeelde vrouwen (0)");
			Nodes[0].Nodes.Add(unassignedFemaleNode);
		}

		#region Toevoegen/verwijderen van een speler
		/// <summary>
		/// Voeg een nieuwe speler toe aan de spelers boom
		/// </summary>
		/// <param name="player">De nieuwe speler</param>
		public void AddNewPlayer(PlayerPoulesView player)
		{
			allPlayersNode.Nodes.Add(createNode("Player", playerText(player), player));
			allPlayersNode.Text = "Alle spelers (" + allPlayersNode.Nodes.Count + ")";
			if (player.AssignedPoules.Count == 0)
				AddToUnassigned(player);
			if (player.Gender == "M")
			{
				maleNode.Nodes.Add(createNode("Player", playerText(player), player));
				maleNode.Text = "Mannen (" + maleNode.Nodes.Count + ")";
			}
			else
			{
				femaleNode.Nodes.Add(createNode("Player", playerText(player), player));
				femaleNode.Text = "Vrouwen (" + femaleNode.Nodes.Count + ")";
			}
		}

		/// <summary>
		/// Voegt speler toe aan unassigned node
		/// </summary>
		/// <param name="player">De speler</param>
		public void AddToUnassigned(PlayerPoulesView player)
		{
			if (player.Gender == "M")
			{
				unassignedMaleNode.Nodes.Add(createNode("Player", playerText(player), player));
				unassignedMaleNode.Text = "Oningedeelde mannen (" + unassignedMaleNode.Nodes.Count + ")";
			}
			else
			{
				unassignedFemaleNode.Nodes.Add(createNode("Player", playerText(player), player));
				unassignedFemaleNode.Text = "Oningedeelde vrouwen (" + unassignedFemaleNode.Nodes.Count + ")";
			}
			unassignedAllNode.Nodes.Add(createNode("Player", playerText(player), player));
			unassignedAllNode.Text = "Oningedeelde spelers (" + unassignedAllNode.Nodes.Count + ")";
		}

		/// <summary>
		/// Verwijdert speler van unassigned node
		/// </summary>
		/// <param name="player">De speler</param>
		public void RemoveFromUnassigned(PlayerPoulesView player)
		{
			if (removeFromNode(unassignedAllNode, player))
				unassignedAllNode.Text = "Oningedeelde spelers (" + unassignedAllNode.Nodes.Count + ")";
			if (player.Gender == "M")
				if (removeFromNode(unassignedMaleNode, player))
					unassignedMaleNode.Text = "Oningedeelde mannen (" + unassignedMaleNode.Nodes.Count + ")";
			else
				if (removeFromNode(unassignedFemaleNode, player))
					unassignedFemaleNode.Text = "Oningedeelde vrouwen (" + unassignedFemaleNode.Nodes.Count + ")";
		}

		/// <summary>
		/// Verwijdert de speler uit de nodeslijst van een node
		/// </summary>
		/// <param name="node">De node waar de speler in zit</param>
		/// <param name="player">De te verwijderen speler</param>
		/// <returns>Geeft aan of verwijderen gelukt is</returns>
		private static bool removeFromNode(TreeNode node, PlayerPoulesView player)
		{
			TreeNode temp = null;
			foreach (TreeNode tNode in node.Nodes)
				if (tNode.Tag == player)
					temp = tNode;
			if (temp != null)
			{
				temp.Remove();
				return true;
			}
			return false;
		}
		#endregion

		#region Hulpmethodes
		/// <summary>
		/// Voeg node toe aan de andere node
		/// </summary>
		/// <param name="node">De node waar de andere aan toegevoegd wordt</param>
		/// <param name="addNode">De node die wordt toegevoegd</param>
		private void addNode(TreeNode node, TreeNode addNode)
		{
			node.Nodes.Add(addNode);
		}

		/// <summary>
		/// Krijg de volledige naam van speler, plus zijn geslacht
		/// </summary>
		/// <param name="player">De speler</param>
		/// <returns>String met naam en geslacht</returns>
		private static string playerText(PlayerPoulesView player)
		{
			return player.Name + " (" + player.Gender + ")";
		}

		/// <summary>
		/// Krijg de treenode van de speler met gegeven id
		/// </summary>
		/// <param name="id">Het id van de speler</param>
		/// <returns>De node van de speler</returns>
		public TreeNode GetPlayerNode(int id)
		{
			foreach (TreeNode player in allPlayersNode.Nodes)
				if (((PlayerPoulesView)player.Tag).Id == id)
					return player;
			return null;
		}
		#endregion

	}
}
