using System;
using System.Windows.Forms;
using System.Collections;

namespace Shared.Sorters
{
	/// <summary>
	/// Abstracte sorteer-klasse
	/// </summary>
	/// <typeparam name="T"> Het type objecten dat gesorteerd wordt </typeparam>
	public abstract class AbstractItemSorter<T> : IComparer
	{
			/// <summary>
			/// De kolom die gesorteerd wordt
			/// </summary>
			private int columnToSort;
			/// <summary>
			/// De volgorde waarin gesorteerd wordt
			/// </summary>
			private SortOrder orderOfSort;

			/// <summary>
			/// Klasse constructor. Initialiseert  verschillende elementen
			/// </summary>
			public AbstractItemSorter()
			{
				// Initialize the column to '0'
				columnToSort = 0;

				// Initialize the sort order to 'none'
				orderOfSort = SortOrder.None;
			}

			int IComparer.Compare(object x, object y)
			{
				try{
					T item1 = (T)x;
					T item2 = (T)y;

					return Compare(item1, item2);

				}catch{
					return 0;
				}
			}

			/// <summary>
			/// Deze methode is de abstracte implementatie van het Type T sorteren
			/// </summary>
			/// <param name="x"></param>
			/// <param name="y"></param>
			/// <returns></returns>
			public virtual int Compare(T x, T y)
			{
				throw new NotImplementedException();
			}

			/// <summary>
			/// De kolom waar op gesorteerd wordt
			/// </summary>
			public int SortColumn
			{
				set
				{
					columnToSort = value;
				}
				get
				{
					return columnToSort;
				}
			}

			/// <summary>
			/// De volgorde waarin gesorteerd wordt
			/// </summary>
			public SortOrder Order
			{
				set
				{
					orderOfSort = value;
				}
				get
				{
					return orderOfSort;
				}
			}

		}

}
