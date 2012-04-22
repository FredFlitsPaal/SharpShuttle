using System;
using System.Collections;
using System.Windows.Forms;

namespace Shared.Sorters
{
	/// <summary>
	/// Sorteert op kolom
	/// </summary>
	public class AllColumnSorter : IComparer
	{
		/// <summary>
		/// De kolom waarop gesorteerd wordt
		/// </summary>
		private int col;
		/// <summary>
		/// De sorteervolgorde
		/// </summary>
		private SortOrder order;

		/// <summary>
		/// Lege constructor
		/// </summary>
		public AllColumnSorter()
		{
			col = 0;
			order = SortOrder.Ascending;
		}

		/// <summary>
		/// Constructor die een kolom en sorteerdvolgorde meekrijgt
		/// </summary>
		/// <param name="column"> De kolom waarop gesorteerd wordt </param>
		/// <param name="order"> De sorteervolgorde</param>
		public AllColumnSorter(int column, SortOrder order)
		{
			col = column;
			this.order = order;
		}

		/// <summary>
		/// Vergelijk 2 objecten
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public int Compare(object x, object y)
		{
			int returnVal;
			double result;

			if (Double.TryParse(((ListViewItem)x).SubItems[col].Text, out result))
			{
				double a = Double.Parse(((ListViewItem)x).SubItems[col].Text);
				double b = Double.Parse(((ListViewItem)y).SubItems[col].Text);

				if (a > b)
					returnVal = -1;
				else
					returnVal = 1;

				if (order == SortOrder.Descending)
					returnVal *= -1;

				return returnVal;
			}

			returnVal = String.Compare(((ListViewItem)x).SubItems[col].Text,
									((ListViewItem)y).SubItems[col].Text);
			//Bepaal of de sorteervolgorde aflopend is
			if (order == SortOrder.Descending)
				returnVal *= -1;
			return returnVal;
		}
	}
}
