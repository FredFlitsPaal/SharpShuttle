using System;
using Shared.Domain;
using Shared.Views;
using UserControls.AbstractControls;

namespace UserControls.PouleControls
{
	/// <summary>
	/// ComboBox voor poules
	/// </summary>
	public class PouleComboBox : DomainComboBox<PouleViews, PouleView, Poule>
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public PouleComboBox()
		{
			DisplayMember = "Name";
			ValueMember = "Id";
		}

		/// <summary>
		/// De datasource
		/// </summary>
		public new PouleViews DataSource
		{
			get { return base.DataSource; }
			set { base.DataSource = value; }
		}

		/// <summary>
		/// De geselecteerde PouleView
		/// </summary>
		public new int SelectedValue
		{
			get { return Convert.ToInt32(base.SelectedValue); }
			set { base.SelectedValue = value; }
		}
	}
}
