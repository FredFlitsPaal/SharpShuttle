using System.Windows.Forms;
using Shared.Domain;
using UserControls.AbstractControls;
using Shared.Views;

namespace UserControls.PlayerControls
{
	/// <summary>
	/// ListView met ingeschreven spelers
	/// </summary>
	public class RegisteredPlayerListView : DomainListView<PlayerViews, PlayerView, Player>
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public RegisteredPlayerListView()
		{
			//kolommen bepalen
			ColumnHeader col;

			//Naam
			col = new ColumnHeader {Name = "Name", Text = "Naam", DisplayIndex = 0, Width = 150};
			Columns.Add(col);
			//Geslacht
			col = new ColumnHeader {Name = "Gender", Text = "Gesl", DisplayIndex = 1, Width = 50};
			Columns.Add(col);
			//Club
			col = new ColumnHeader {Name = "Club", Text = "Club", DisplayIndex = 2, Width = 120};
			Columns.Add(col);
			//Voorkeuren
			col = new ColumnHeader {Name = "Preferences", Text = "Voorkeuren", DisplayIndex = 3, Width = 165};
			Columns.Add(col);
			//Opmerkingen
			col = new ColumnHeader {Name = "Comment", Text = "Opmerkingen", DisplayIndex = 4, Width = 113};
			Columns.Add(col);
		}

		/// <summary>
		/// De DataSource
		/// </summary>
		public new PlayerViews DataSource
		{
			get { return base.DataSource; }
			set { base.DataSource = value; }
		}

	}
}
