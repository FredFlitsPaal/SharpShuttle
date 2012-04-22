using System.Windows.Forms;
using Shared.Domain;
using Shared.Views;

namespace UserControls.AbstractControls
{
	/// <summary>
	/// Abstracte ComboBox voor domeinobjecten
	/// </summary>
	/// <typeparam name="ViewTypes"> Het viewstype van het domeinobject</typeparam>
	/// <typeparam name="ViewType"> Het viewtype van het domeinobject </typeparam>
	/// <typeparam name="DomainType"> Het type domeinobject </typeparam>
    public abstract class DomainComboBox<ViewTypes, ViewType, DomainType> : ComboBox
        where ViewType : AbstractView <DomainType>
        where ViewTypes : AbstractViews<ViewType, DomainType>
		where DomainType : Abstract
    {

		/// <summary>
		/// De datasource van de ComboBox
		/// </summary>
		public new ViewTypes DataSource
        {
            get { return base.DataSource as ViewTypes; }
            set { base.DataSource = value; }
        }

		//TODO: maak dit generiek, zodat een AbstractView object erin/eruit gaat
		//public new ViewType SelectedValue
		//{
		//    get
		//    {
				
		//    }
		//    set
		//    {
				
		//    }
		//}
    }
}
