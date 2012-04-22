using System;
using Shared.Domain;
using Shared.Views;
using UserControls.AbstractControls;

namespace UserControls.UserControls
{
	/// <summary>
	/// Een ComboBox waarmee gebruikers gekozen kunnen worden
	/// </summary>
    public class UserComboBox : DomainComboBox<UserViews, UserView, User>
    {
		/// <summary>
		/// Default constructor
		/// </summary>
        public UserComboBox()
        {
            DisplayMember = "Name";
            ValueMember = "Id";
        }

		/// <summary>
		/// De DataSource
		/// </summary>
        public new UserViews DataSource
        {
            get { return base.DataSource; }
            set { base.DataSource = value; }
        }

		/// <summary>
		/// De gekozen gebruiker
		/// </summary>
        public new int SelectedValue
        {
            get { return Convert.ToInt32(base.SelectedValue); }
            set { base.SelectedValue = value; }
        }
    }
}
