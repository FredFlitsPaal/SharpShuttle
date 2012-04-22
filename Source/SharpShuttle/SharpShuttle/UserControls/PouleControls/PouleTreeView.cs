using System.Drawing;
using Shared.Views;
using UserControls.AbstractControls;
using System.Windows.Forms;
using Shared.Domain;
using System.Collections.Generic;

namespace UserControls.PouleControls
{
	/// <summary>
	/// Een TreeView van poules, gebruikt voor het indelen van spelers in poules
	/// </summary>
	public class PouleTreeView : DomainTreeView<PouleViews, PouleView, Poule>
	{
		/// <summary>
		/// De hoofdknoop van de tree
		/// </summary>
		public TreeNode disciplineNode;

		/// <summary>
		/// Correcte poules zijn groen gekleurd
		/// </summary>
		public Color Correct = Color.MediumSpringGreen;
		/// <summary>
		/// Incorrecte poules zijn rood gekleurd
		/// </summary>
		public Color Incorrect = Color.OrangeRed;
		/// <summary>
		/// Half correcte poules zijn geel gekleurd
		/// </summary>
		public Color Semicorrect = Color.Yellow;
		/// <summary>
		/// Lege poules zijn aqua gekleurd
		/// </summary>
		public Color Empty = Color.Aqua;

		/// <summary>
		/// Constructor voor een poules treeview
		/// </summary>
		public PouleTreeView()
		{
			ItemDrag += treeView_ItemDrag;
			DragDrop += poulesTreeView_DragDrop;
			DragEnter += poulesTreeView_DragEnter;
			DragOver += poulesTreeView_DragOver;
		}

		/// <summary>
		/// Maakt de default nodes voor deze treeview
		/// </summary>
		public void CreateDefaultNodes()
		{
			disciplineNode = createNode("DisciplinesNode", "Disciplines");
			Nodes.Add(disciplineNode);
			disciplineNode.Expand();
		}

		/// <summary>
		/// Kijk of lijst van nodes een speler bevat met gegeven geslacht
		/// </summary>
		/// <param name="nodes">Lijst van nodes</param>
		/// <param name="gender">Het gezochte geslacht</param>
		/// <returns>Boolean of er een speler is met gegeven geslacht</returns>
		private static bool selectedContains(List<TreeNode> nodes, string gender)
		{
			foreach (TreeNode node in nodes)
				if (((PlayerPoulesView)node.Tag).Gender == gender)
					return true;

			return false;
		}

		#region DragDrop
		/// <summary>
		/// Bepaal effect als node in deze treeview geentered wordt
		/// </summary>
		/// <param name="send"></param>
		/// <param name="e"></param>
		private static void poulesTreeView_DragEnter(object send, DragEventArgs e)
		{
			// Het drag effect
			if (((List<TreeNode>)e.Data.GetData(typeof(List<TreeNode>))).Count > 0)
				if (((List<TreeNode>)e.Data.GetData(typeof(List<TreeNode>)))[0].Name == "Player")
					e.Effect = DragDropEffects.Move;
		}

		/// <summary>
		/// Het effect als er iets over een node gesleept wordt
		/// </summary>
		/// <param name="send"></param>
		/// <param name="e"></param>
		private void poulesTreeView_DragOver(object send, DragEventArgs e)
		{
			// Het drag effect
			List<TreeNode> nodes = (List<TreeNode>)e.Data.GetData(typeof(List<TreeNode>));
			if (nodes != null && nodes.Count > 0)
				if (nodes[0].Name == "Player")
				{
					Point p = new Point(e.X, e.Y);
					p = PointToClient(p);
					TreeNode node = GetNodeAt(p);
					// Het drag effect bij het slepen over een poulenode
					if (node != null && node.Name == "Poule")
					{
						if (((PouleView)node.Tag).getGender() == PouleView.Gender.M && selectedContains(nodes, "M"))
							e.Effect = DragDropEffects.Copy;
						else if (((PouleView)node.Tag).getGender() == PouleView.Gender.V && selectedContains(nodes, "V"))
							e.Effect = DragDropEffects.Copy;
						else if (((PouleView)node.Tag).getGender() == PouleView.Gender.U)
							e.Effect = DragDropEffects.Copy;
						else
							e.Effect = DragDropEffects.None;
					}
					else
						e.Effect = DragDropEffects.Move;
				}
		}

		/// <summary>
		/// Gedraggede nodes kunnen op bepaalde nodes gedropt worden
		/// </summary>
		/// <param name="send"></param>
		/// <param name="e"></param>
		public void poulesTreeView_DragDrop(object send, DragEventArgs e)
		{
			List<TreeNode> moveNodes = (List<TreeNode>)e.Data.GetData(typeof(List<TreeNode>));
			TreeNode destNode = ((TreeView)send).GetNodeAt(((TreeView)send).PointToClient(new Point(e.X, e.Y)));
			if (destNode != null)
				foreach (TreeNode node in moveNodes)
					if (destNode.Name == "Poule" && !(IsAssignedTo((PlayerPoulesView)node.Tag, (PouleView)destNode.Tag)))
							AddToPoule(node, destNode);
		}
		#endregion

		#region Nieuwe Nodes
		/// <summary>
		/// Voegt een discipline toe aan de discipline tree view
		/// </summary>
		/// <param name="name">Naam van de discpline</param>
		private TreeNode addDiscipline(string name)
		{
			TreeNode node = createNode("Discipline", name);
			disciplineNode.Nodes.Add(node);
			return node;
		}

		/// <summary>
		/// Voegt een poule toe aan zijn bijbehorende discipline
		/// </summary>
		/// <param name="poule">De poule die moet worden toegevoegd</param>
		public void AddPoule(PouleView poule)
		{
			TreeNode pouleNode = createNode("Poule", pouleText(poule), poule);
			pouleCorrectness(pouleNode);

			// Voeg de spelers die geplanned staan voor deze poule toe aan de poule node
			foreach (PlayerPoulesView player in poule.Players)
				pouleNode.Nodes.Add(createNode("Player", player.Name + " (" + player.Gender + ")", player));

			pouleNode.Text = pouleText(poule);
			pouleCorrectness(pouleNode);

			// Zoek naar bijbehorende discipline node
            foreach (TreeNode node in disciplineNode.Nodes)
            {
                if (node.Text == poule.Discipline)
                {
                    node.Nodes.Add(pouleNode);
                    return;
                }
            }
			// Voeg discipline toe als hij nog niet bestaat.
		    addDiscipline(poule.Discipline).Nodes.Add(pouleNode);
		}
		#endregion

		#region Spelers toevoegen aan/verwijderen van poule
		/// <summary>
		/// Verwijdert spelersnode van een poulenode
		/// </summary>
		/// <param name="playerNode">De spelersnode</param>
		/// <param name="pouleNode">De poulenode</param>
		public void RemoveFromPoule(TreeNode playerNode, TreeNode pouleNode)
		{
			// Verwijder speler van poule
			PlayerPoulesView player = (PlayerPoulesView)playerNode.Tag;
			PouleView poule = (PouleView)pouleNode.Tag;
			removePlayerFromPoule(player, poule);
			player.AssignedPoules.Remove(poule);
			playerNode.Remove();
			pouleCorrectness(pouleNode);
			pouleNode.Text = pouleText(poule);
		}

		/// <summary>
		/// Voegt spelersnode aan poulenode
		/// </summary>
		/// <param name="playerNode">De spelersnode</param>
		/// <param name="pouleNode">De poulenode</param>
		public void AddToPoule(TreeNode playerNode, TreeNode pouleNode)
		{
			PlayerPoulesView player = (PlayerPoulesView)playerNode.Tag;
			PouleView poule = (PouleView)pouleNode.Tag;
			// Voeg speler toe aan de poule
			if ((player.Gender == "M" && (poule.getGender() == PouleView.Gender.M || poule.getGender() == PouleView.Gender.U))
				|| (player.Gender == "V" && (poule.getGender() == PouleView.Gender.V || poule.getGender() == PouleView.Gender.U)))
			{
				addPlayerToPoule(player, poule);
				player.AssignedPoules.Add(poule);
				MultiTreeNode cloneNode = (MultiTreeNode)playerNode.Clone();
				cloneNode.BackColor = BackColor;
				cloneNode.ForeColor = ForeColor;
				pouleNode.Nodes.Add(cloneNode);
				pouleCorrectness(pouleNode);
				pouleNode.Text = pouleText(poule);
			}
		}

		/// <summary>
		/// Voegt speler toe aan een poule
		/// </summary>
		/// <param name="player">De speler</param>
		/// <param name="poule">De poule</param>
		private static void addPlayerToPoule(PlayerPoulesView player, PouleView poule)
		{
			poule.incAmountPlayers(player.Gender);
			poule.Players.Add(player);
		}

		/// <summary>
		/// Verwijdert speler van een poule
		/// </summary>
		/// <param name="player">De speler</param>
		/// <param name="poule">De poule</param>
		private static void removePlayerFromPoule(PlayerPoulesView player, PouleView poule)
		{
			poule.decAmountPlayers(player.Gender);
			poule.Players.Remove(player);
		}
		#endregion

		#region Poule Methodes
		/// <summary>
		/// Verkrijg de naam van een poule, plus aantal spelers
		/// </summary>
		/// <param name="poule">De poule</param>
		/// <returns>De naam van de poule en aantal spelers</returns>
		private static string pouleText(PouleView poule)
		{
			return "Poule " + poule.Niveau + " (" + poule.AmountPlayers + ")";
		}

		/// <summary>
		/// Zet de correctheid van een poule
		/// </summary>
		/// <param name="pouleNode">De poule</param>
		private void pouleCorrectness(TreeNode pouleNode)
		{
			PouleView poule = (PouleView)pouleNode.Tag;
			if (poule.AmountPlayers == 0)
				pouleEmpty(pouleNode);
			else if (poule.DisciplineEnum == Poule.Disciplines.Mixed)
			{
				if (poule.AmountPlayers < 8)
					pouleIncorrect(pouleNode, "Te weinig spelers. Minimaal 8 nodig.");
				else if (poule.AmountMen != poule.AmountWomen)
					pouleIncorrect(pouleNode, "Aantal mannen komt niet overeen met het aantal vrouwen");
				else if (poule.AmountPlayers % 2 == 0 && poule.AmountPlayers % 4 != 0)
					pouleSemiCorrect(pouleNode, "Niet genoeg spelers om even aantal teams te vormen");
				else if (poule.AmountPlayers % 4 != 0)
					pouleIncorrect(pouleNode, "Spelersaantal niet deelbaar door 4");
				else
					pouleCorrect(pouleNode, "Correct");
			}
			else if (isDoublePoule(poule))
			{
				if (poule.AmountPlayers < 8)
					pouleIncorrect(pouleNode, "Te weinig spelers. Minimaal 8 nodig.");
				else if (poule.AmountPlayers % 2 == 0 && poule.AmountPlayers % 4 != 0)
					pouleSemiCorrect(pouleNode, "Niet genoeg spelers om even aantal teams te vormen");
				else if (poule.AmountPlayers % 4 != 0)
					pouleIncorrect(pouleNode, "Spelersaantal niet deelbaar door 4");
				else
					pouleCorrect(pouleNode, "Correct");
			}
			else
			{
				if (poule.AmountPlayers < 4)
					pouleIncorrect(pouleNode, "Te weinig spelers. Minimaal 4 nodig.");
				else if (poule.AmountPlayers % 2 == 0)
					pouleCorrect(pouleNode, "Correct");
				else if (poule.AmountPlayers % 2 != 0)
					pouleSemiCorrect(pouleNode, "Oneven aantal spelers");
			}
		}

		/// <summary>
		/// Lege poule
		/// </summary>
		/// <param name="pouleNode"></param>
		private void pouleEmpty(TreeNode pouleNode)
		{
			((MultiTreeNode)pouleNode).OriginalBackColor = Empty;
			((PouleView)pouleNode.Tag).Correctness = "Geen spelers";
		}

		/// <summary>
		/// Incorrecte poule
		/// </summary>
		/// <param name="pouleNode"></param>
		/// <param name="text"></param>
		private void pouleIncorrect(TreeNode pouleNode, string text)
		{
			((MultiTreeNode)pouleNode).OriginalBackColor = Incorrect;
			((PouleView)pouleNode.Tag).Correctness = text;
		}

		/// <summary>
		/// Correct poule
		/// </summary>
		/// <param name="pouleNode"></param>
		/// <param name="text"></param>
		private void pouleCorrect(TreeNode pouleNode, string text)
		{
			((MultiTreeNode)pouleNode).OriginalBackColor = Correct;
			((PouleView)pouleNode.Tag).Correctness = text;
		}

		/// <summary>
		/// Incorrecte poule, maar mag in deze status doorgaan
		/// </summary>
		/// <param name="pouleNode"></param>
		/// <param name="text"></param>
		private void pouleSemiCorrect(TreeNode pouleNode, string text)
		{
			((MultiTreeNode)pouleNode).OriginalBackColor = Semicorrect;
			((PouleView)pouleNode.Tag).Correctness = text;
		}

		/// <summary>
		/// Controleer of poule een dubbelpoule is
		/// </summary>
		/// <param name="poule">De poule</param>
		/// <returns>Of poule dubbel is of niet</returns>
		private static bool isDoublePoule(PouleView poule)
		{
			if (poule.KindofTeam == PouleView.Kind.Dubbel)
				return true;
			return false;
		}

		/// <summary>
		/// Controleer of speler in bepaalde poule zit
		/// </summary>
		/// <param name="player">De speler</param>
		/// <param name="poule">De poule</param>
		/// <returns>Of de speler in de poule zit</returns>
		public bool IsAssignedTo(PlayerPoulesView player, PouleView poule)
		{
			foreach (PouleView pView in player.AssignedPoules)
			{
				if (pView == poule)
					return true;
			}
			return false;
		}

		/// <summary>
		/// Verkrijg alle poules
		/// </summary>
		/// <returns>Alle poules</returns>
		public PouleViews GetAllPoules()
		{
			PouleViews poules = new PouleViews();
			foreach (TreeNode discipline in disciplineNode.Nodes)
				foreach (TreeNode poule in discipline.Nodes)
						poules.Add((PouleView)poule.Tag);
			return poules;
		}

		/// <summary>
		/// Verkrijg de poules met een bepaalde correctheid
		/// </summary>
		/// <param name="correctColor">De kleur van de gezochte poules</param>
		/// <returns>Alle poules met de gegeven kleur</returns>
		public PouleViews GetPoules(Color correctColor)
		{
			PouleViews poules = new PouleViews();
            foreach (TreeNode discipline in disciplineNode.Nodes)
            {
                foreach (TreeNode poule in discipline.Nodes)
                {
                    if (poule.BackColor == correctColor)
                        poules.Add((PouleView) poule.Tag);
                }
            }
		    return poules;
		}
		#endregion
	}
}
