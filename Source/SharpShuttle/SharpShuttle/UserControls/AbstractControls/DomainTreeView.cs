using System;using System.Windows.Forms;
using Shared.Domain;
using Shared.Views;
using System.Collections.Generic;

namespace UserControls.AbstractControls
{
	/// <summary>
	/// Een abstracte treeview voor domeinobjecten
	/// </summary>
	/// <typeparam name="ViewTypes"> Het Views type </typeparam>
	/// <typeparam name="ViewType"> Het View type </typeparam>
	/// <typeparam name="DomainType"> Het type domeinobject </typeparam>
	public abstract class DomainTreeView<ViewTypes, ViewType, DomainType> : TreeView
		where ViewType : AbstractView<DomainType>
		where ViewTypes : AbstractViews<ViewType, DomainType>
		where DomainType : Abstract
	{
		/// <summary>
		/// De Datasource
		/// </summary>
		private ViewTypes dataSource;

		/// <summary>
		/// De datasource
		/// </summary>
		public virtual ViewTypes DataSource
		{
			get { return dataSource; }
			set { dataSource = value; }
		}

		#region Aanmaken nodes
		/// <summary>
		/// Maakt een nieuwe TreeNode met een tag object
		/// </summary>
		/// <param name="name"></param>
		/// <param name="text"></param>
		/// <param name="tag"></param>
		/// <returns></returns>
		protected TreeNode createNode(string name, string text, Object tag)
		{
			TreeNode node = createNode(name, text);
			node.Tag = tag;
			return node;
		}

		/// <summary>
		/// Maakt een nieuwe TreeNode
		/// </summary>
		/// <param name="name"></param>
		/// <param name="text"></param>
		/// <returns></returns>
		protected TreeNode createNode(string name, string text)
		{
			TreeNode node = new MultiTreeNode();
			node.Name = name;
			node.Text = text;
			return node;
		}
		#endregion

		#region Standaard dragdrop functies
		/// <summary>
		/// Bepaal welke nodes gedragged mogen worden
		/// </summary>
		/// <param name="send"></param>
		/// <param name="e"></param>
		protected void treeView_ItemDrag(object send, ItemDragEventArgs e)
		{
			// Valideer of node gemoved mag worden
			TreeNode node = (TreeNode)e.Item;
			if (node.Name == "Player")
			{
				// Unselected node wordt gedragged
				if (!selectedNodes.Contains(node))
					selectSingleNode(node);
				DoDragDrop(selectedNodes, DragDropEffects.Move | DragDropEffects.Copy);
			}
		}
		/// <summary>
		/// Zet effect als node in deze treeview geentered wordt
		/// </summary>
		/// <param name="send"></param>
		/// <param name="e"></param>
		protected void treeView_DragEnter(object send, DragEventArgs e)
		{
			// Het drag effect
			e.Effect = DragDropEffects.Move;
		}

		/// <summary>
		/// Lege functie voor droppen op deze treeview
		/// </summary>
		/// <param name="send"></param>
		/// <param name="e"></param>
		protected void treeView_DragDrop(object send, DragEventArgs e)
		{
		}
		#endregion

		#region Multi-Select
		/// <summary>
		/// De geselecteerde node
		/// </summary>
		private TreeNode selectedNode;
		/// <summary>
		/// De geselecteerde node
		/// </summary>
		public new TreeNode SelectedNode
		{
			get { return selectedNode; }
			set { selectedNode = value; }
		}
		/// <summary>
		/// Geselecteerde nodes
		/// </summary>
		private List<TreeNode> selectedNodes = new List<TreeNode>();
		/// <summary>
		/// Geselecteerde nodes
		/// </summary>
		public List<TreeNode> SelectedNodes
		{
			get { return selectedNodes; }
			set { selectedNodes = value; }
		}

		#region Overrided Methodes
		/// <summary>
		/// Deselecteer de nodes als focus verloren is
		/// </summary>
		/// <param name="e"></param>
		protected override void OnLostFocus(EventArgs e)
		{
			DeselectNodes();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnBeforeSelect(TreeViewCancelEventArgs e)
		{
			e.Cancel = true;
			base.OnBeforeSelect(e);
		}

		/// <summary>
		/// Selecteert de node
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			TreeNode node = GetNodeAt(e.Location);

			if (node != null)
				if (e.Button == MouseButtons.Left || (e.Button == MouseButtons.Right && !selectedNodes.Contains(node)))
					if (ModifierKeys == Keys.None && selectedNodes.Contains(node))
						selectNode(node);

			base.OnMouseUp(e);
		}

		/// <summary>
		/// Selecteert de node
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.SelectedNode = null;
			TreeNode node = GetNodeAt(e.Location);
			if (node != null)
				if (e.Button == MouseButtons.Left || (e.Button == MouseButtons.Right && !selectedNodes.Contains(node)))
					if (!(ModifierKeys == Keys.None && selectedNodes.Contains(node)))
						selectNode(node);
			base.OnMouseDown(e);
		}
		#endregion

		#region Selecteren van nodes
		/// <summary>
		/// Selecteert de node of voegt hem toe aan de huidige selectie van nodes
		/// als shift ingedrukt wordt
		/// </summary>
		/// <param name="node"></param>
		private void selectNode(TreeNode node)
		{
			if (node.Name == "Player" && selectedNode.Name == "Player")
				// Gebruiker heeft de control-knop gebruikt
				if (ModifierKeys == Keys.Control)
				{
					toggleNode(node, !selectedNodes.Contains(node));
				}
				// Gebruiker heeft de shift-knop gebruikt
				else if (ModifierKeys == Keys.Shift && node.Parent == selectedNode.Parent)
				{
					// Selecteer alle nodes tussen de geselecteerde node en waar de gebruiker heeft geklikt
					DeselectNodes();
					toggleNode(selectedNode, true);
					// Loop naar beneden
					if (node.Index < selectedNode.Index)
					{
						while (node.Index != selectedNode.Index)
						{
							toggleNode(node, true);
							node = node.NextVisibleNode;
						}
					}
					// Loop naar boven
					else if (node.Index > selectedNode.Index)
					{
						while (node.Index != selectedNode.Index)
						{
							toggleNode(node, true);
							node = node.PrevVisibleNode;
						}
					}
				}
				// Gebruiker heeft maar 1 node geselecteerd
				else
					selectSingleNode(node);
			else
				selectSingleNode(node);
		}

		/// <summary>
		/// Er is maar 1 node geselecteerd
		/// </summary>
		/// <param name="node"></param>
		private void selectSingleNode(TreeNode node)
		{
			DeselectNodes();
			selectedNode = node;
			toggleNode(node, true);
		}

		/// <summary>
		/// Zet de kleur van de treenode als hij geselecteerd/gedeselecteerd wordt
		/// </summary>
		/// <param name="node"></param>
		/// <param name="selected"></param>
		private void toggleNode(TreeNode node, bool selected)
		{
			MultiTreeNode mNode = (MultiTreeNode)node;
			if (selected)
			{
				if (!selectedNodes.Contains(node))
				{
					selectedNodes.Add(node);
					mNode.saveCurrentColors();
				}
				mNode.highLight();
			}
			else
			{
				selectedNodes.Remove(node);
				mNode.resetColors();
			}
		}

		/// <summary>
		/// Haal alle geselecteerde nodes uit de lijst
		/// </summary>
		public void DeselectNodes()
		{
			if (selectedNodes != null)
			{
				foreach (TreeNode node in selectedNodes)
				{
					MultiTreeNode mNode = (MultiTreeNode)node;
					mNode.resetColors();
				}
				selectedNodes.Clear();
			}
		}
		#endregion
		#endregion
	}
}
