using System;
using System.Windows.Forms;

namespace Shared.Sorters
{
	/// <summary>
	/// Sorteert een TeamListView
	/// </summary>
	public class TeamListViewSorter : AbstractItemSorter<ListViewItem>
	{

		/// <summary>
		/// Vergelijk 2 ListViewItems
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public override int Compare(ListViewItem x, ListViewItem y)
		{
			//We gaan kolom 0 bekijken, de Rank kolom
			int value1, value2;
			Int32.TryParse(x.Text, out value1);
			Int32.TryParse(y.Text, out value2);
			return Math.Sign(value1 - value2);
		}
	}
}
