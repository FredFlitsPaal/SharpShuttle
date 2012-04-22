using System.Windows.Forms;
using Shared.Views;
using Shared.Domain;

namespace UserControls.AbstractControls
{
	/// <summary>
	/// Een abstracte UserControl voor domeinobjecten
	/// </summary>
	/// <typeparam name="ViewTypes"> Het type views</typeparam>
	/// <typeparam name="ViewType"> Het type view </typeparam>
	/// <typeparam name="DomainType"> Het type domeinobject </typeparam>
	public abstract class DomainUserControl<ViewTypes, ViewType, DomainType> : UserControl
		where ViewType : AbstractView<DomainType>
		where ViewTypes : AbstractViews<ViewType, DomainType>
		where DomainType : Abstract
	{
		/// <summary>
		/// De datasource
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
	}
}
