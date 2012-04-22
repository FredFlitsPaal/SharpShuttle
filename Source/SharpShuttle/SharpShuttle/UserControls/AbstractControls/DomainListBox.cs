using System.Windows.Forms;
using Shared.Domain;
using Shared.Views;

namespace UserControls.AbstractControls
{
	/// <summary>
	/// Abstracte ListBox voor domeinobjecten
	/// </summary>
	/// <typeparam name="ViewTypes"> Het viewstype van het domeinobject </typeparam>
	/// <typeparam name="ViewType"> Het viewtype van het domeinobject </typeparam>
	/// <typeparam name="DomainType"> Het type domeinobject </typeparam>
    public abstract class DomainListBox<ViewTypes, ViewType, DomainType> : ListBox
        where ViewType : AbstractView<DomainType>
        where ViewTypes : AbstractViews<ViewType, DomainType>
		where DomainType : Abstract
    {
		/// <summary>
		/// De datasource van de ListBox
		/// </summary>
        public new ViewTypes DataSource
        {
            get { return base.DataSource as ViewTypes; }
			set { DataSource = value; }
        }
    }
}
