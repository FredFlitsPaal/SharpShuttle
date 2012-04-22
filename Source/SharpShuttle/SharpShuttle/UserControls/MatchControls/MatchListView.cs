using System.Windows.Forms;
using Shared.Domain;
using Shared.Views;
using UserControls.AbstractControls;

namespace UserControls.MatchControls
{
	/// <summary>
	/// Algemene ListView van wedstrijden
	/// </summary>
	public class MatchListView : DomainListView<MatchViews, MatchView, Match>
	{
		/// <summary>
		/// Default constructor, initialiseert alle kolommen
		/// </summary>
        public MatchListView()
		{
			//kolommen bepalen
			//MatchView moet dus ook de volgende eigenschappen kennen (case sensitive):
			//MatchID, Team1, Team2, PouleName, Status, Score, Comment
			ColumnHeader col;

			//'MatchID'
			col = new ColumnHeader {Name = "MatchID", Text = "Wedstrijd ID", DisplayIndex = 0, Width = 75};
			Columns.Add(col);
			//Team1
			col = new ColumnHeader {Name = "Team1", Text = "Team 1", DisplayIndex = 1, Width = 200};
			Columns.Add(col);
			//Team2
			col = new ColumnHeader {Name = "Team2", Text = "Team 2", DisplayIndex = 2, Width = 200};
			Columns.Add(col);
			//PouleName
			col = new ColumnHeader {Name = "PouleName", Text = "Poule Naam", DisplayIndex = 3, Width = 200};
			Columns.Add(col);
			//Rondenummer
			col = new ColumnHeader {Name = "Round", Text = "Ronde", DisplayIndex = 4, Width = 75};
			Columns.Add(col);
			//Status
			col = new ColumnHeader {Name = "Status", Text = "Status", DisplayIndex = 5, Width = 120};
			Columns.Add(col);
			//Score
			col = new ColumnHeader {Name = "Score", Text = "Score", DisplayIndex = 6, Width = 75};
			Columns.Add(col);
            //Comment
            col = new ColumnHeader {Name = "Comment", Text = "Opmerkingen", DisplayIndex = 7, Width = 200};
			Columns.Add(col);
		}

		/// <summary>
		/// De datasource
		/// </summary>
        public new MatchViews DataSource
		{
			get { return base.DataSource; }
			set { base.DataSource = value; }
		}
	}
}
