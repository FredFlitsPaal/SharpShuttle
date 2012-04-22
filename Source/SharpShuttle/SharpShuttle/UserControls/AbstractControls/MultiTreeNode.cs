using System.Drawing;
using System.Windows.Forms;

namespace UserControls.AbstractControls
{
	/// <summary>
	/// Een extensie van TreeNode die van kleur kan veranderen
	/// </summary>
	public class MultiTreeNode : TreeNode
	{
		/// <summary>
		/// De oorspronkelijke voorgrondkleur
		/// </summary>
		private Color originalForeColor;
		/// <summary>
		/// De oorspronkelijke achtergrondkleur
		/// </summary>
		private Color originalBackColor;
		/// <summary>
		/// Wordt de node gehighlight
		/// </summary>
		public bool highlighted;

		/// <summary>
		/// Default constructor
		/// </summary>
		public MultiTreeNode()
		{
			originalForeColor = ForeColor;
			originalBackColor = BackColor;
			highlighted = false;
		}

		/// <summary>
		/// De oorspronkelijke voorgrondkleur
		/// </summary>
		public Color OriginalForeColor
		{
			get { return originalForeColor; }
			set
			{
				originalForeColor = value;
				if (!highlighted)
					ForeColor = value;
			}
		}

		/// <summary>
		/// De oorspronkelijke achtergrondkleur
		/// </summary>
		public Color OriginalBackColor
		{
			get { return originalBackColor; }
			set
			{
				originalBackColor = value;
				if (!highlighted)
					BackColor = value;
			}
		}

		/// <summary>
		/// Creeer een kloon van dit object
		/// </summary>
		/// <returns></returns>
		public override object Clone()
		{
			MultiTreeNode clone = new MultiTreeNode();
			clone.Name = Name;
			clone.Text = Text;
			clone.Tag = Tag;
			return clone;
		}

		/// <summary>
		/// Bewaar de huidige kleur van deze node
		/// </summary>
		public void saveCurrentColors()
		{
			originalBackColor = BackColor;
			originalForeColor = ForeColor;
		}

		/// <summary>
		/// Reset de back en fore kleuren van deze node 
		/// </summary>
		public void resetColors()
		{
			highlighted = false;
			BackColor = originalBackColor;
			ForeColor = originalForeColor;
		}

		/// <summary>
		/// Highlight deze node
		/// </summary>
		public void highLight()
		{
			highlighted = true;
			BackColor = SystemColors.Highlight;
			ForeColor = SystemColors.HighlightText;
		}

	}
}
