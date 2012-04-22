using Shared.Domain;
using Shared.Views;
using UserControls.AbstractControls;

namespace UserControls.ServerControls
{
	/// <summary>
	/// Een ComboBox om servers te selecteren
	/// </summary>
    public class ServerComboBox : DomainComboBox<ServerViews, ServerView, Server>
    {
		/// <summary>
		/// Default constructor
		/// </summary>
        public ServerComboBox()
        {
            DisplayMember = "Address";
            ValueMember = "Address";
        }

		/// <summary>
		/// De DataSource
		/// </summary>
        public new ServerViews DataSource
        {
            get { return base.DataSource; }
            set { base.DataSource = value; }
        }
    }
}
