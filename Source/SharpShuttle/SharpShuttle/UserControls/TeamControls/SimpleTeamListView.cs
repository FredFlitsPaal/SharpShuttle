using System.Collections.Generic;
using System.Windows.Forms;
using Shared.Domain;
using Shared.Views;
using UserControls.AbstractControls;

namespace UserControls.TeamControls
{
	/// <summary>
	/// ListView voor teams in de "simpel" mode van het poule overzicht
	/// </summary>
    public class SimpleTeamListView : DomainListView<LadderTeamViews, LadderTeamView, LadderTeam>
    {
		/// <summary>
		/// Default constructor
		/// </summary>
        public SimpleTeamListView()
        {
			//kolommen bepalen
			//PlayerView moet dus ook de volgende eigenschappen kennen (case sensitive):
			//Ranking, Id, Name, ScoreAvg, ScoreBalance, MatchesPlayed, MatchesWon
            ColumnHeader col;

			//Rank
			col = new ColumnHeader { Name = "Rank", Text = "Rang", DisplayIndex = 0 };
			Columns.Add(col);
            
			//TeamName
			col = new ColumnHeader {Name = "Name", Text = "Team", DisplayIndex = 1, Width = 200};
			Columns.Add(col);
			
			//MatchesPlayed
			col = new ColumnHeader {Name = "MatchesPlayed", Text = "Gespeelde Wedstrijden", DisplayIndex = 2, Width = 200};
			Columns.Add(col);

			//Gemmidelde Aantal gewonnen sets
			col = new ColumnHeader {Name = "AverageSetsWon", Text = "Gemiddeld gewonnen sets", DisplayIndex = 3, Width = 200};
			Columns.Add(col);

			//Gemmidelde Score (Saldo)
			col = new ColumnHeader {Name = "AverageScore", Text = "Gemiddeld saldo", DisplayIndex = 4, Width = 200};
			Columns.Add(col);
        }

		/// <summary>
		/// De DataSource
		/// </summary>
        public new LadderTeamViews DataSource
        {
            get { return base.DataSource; }
            set { base.DataSource = value; }
        }
    }
}
