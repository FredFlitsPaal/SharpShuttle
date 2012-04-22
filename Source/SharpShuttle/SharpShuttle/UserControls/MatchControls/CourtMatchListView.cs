using System.Windows.Forms;
using Shared.Domain;
using Shared.Views;
using UserControls.AbstractControls;

namespace UserControls.MatchControls
{
	/// <summary>
	/// Een ListView voor de wedstrijden in het veldoverzicht
	/// </summary>
	public class CourtMatchListView : DomainListView<MatchViews, MatchView, Match>
	{
		/// <summary>
		/// Default constructor, initialiseert alle kolommen
		/// </summary>
		public CourtMatchListView()
		{
			//kolommen bepalen
			ColumnHeader col;

			//'MatchID'
			col = new ColumnHeader {Name = "MatchID", Text = "Wedstrijd ID", DisplayIndex = 0, Width = 75};
			Columns.Add(col);
			//Team1
			col = new ColumnHeader {Name = "Team1Player1", Text = "Team 1 Speler 1", DisplayIndex = 1, Width = 120};
			Columns.Add(col);
			//Team1
			col = new ColumnHeader {Name = "Team1Player2", Text = "Team 1 Speler 2", DisplayIndex = 2, Width = 120};
			Columns.Add(col);
			//Team2 
			col = new ColumnHeader {Name = "Team2Player1", Text = "Team 2 Speler 1", DisplayIndex = 3, Width = 120};
			Columns.Add(col);
			//Team2
			col = new ColumnHeader {Name = "Team2Player2", Text = "Team 2 Speler 2", DisplayIndex = 4, Width = 120};
			Columns.Add(col);
			//PouleName
			col = new ColumnHeader {Name = "PouleName", Text = "Poule", DisplayIndex = 5, Width = 150};
			Columns.Add(col);
			//Round
			col = new ColumnHeader {Name = "Round", Text = "Ronde", DisplayIndex = 6, Width = 50};
			Columns.Add(col);
  
		}

		/// <summary>
		/// De datasource van de listview
		/// </summary>
        public new MatchViews DataSource
		{
			get { return base.DataSource; }
			set { base.DataSource = value; }
		}
	}
}
