using System.Collections.Generic;
using System.Windows.Forms;
using Shared.Domain;
using Shared.Views;
using UserControls.AbstractControls;

namespace UserControls.TeamControls
{
	/// <summary>
	/// ListView voor teams in de "uitgebreid" mode van het poule overzicht
	/// </summary>
    public class TeamListView : DomainListView<LadderTeamViews, LadderTeamView, LadderTeam>
	{

		/// <summary>
		/// Default constructor
		/// </summary>
		public TeamListView()
		{
			//kolommen bepalen
			//PlayerView moet dus ook de volgende eigenschappen kennen (case sensitive):
			//Ranking, Id, Name, ScoreAvg, ScoreBalance, MatchesPlayed, MatchesWon
			ColumnHeader col;
			//Rang
			col = new ColumnHeader { Name = "Rank", Text = "Rang", DisplayIndex = 0 };
			Columns.Add(col);
			//TeamID
			col = new ColumnHeader {Name = "TeamId", Text = "Team ID", DisplayIndex = 1};
			Columns.Add(col);
			//Teamnaam
			col = new ColumnHeader {Name = "Name", Text = "Naam", DisplayIndex = 2};
			Columns.Add(col);
			//Speler1
			col = new ColumnHeader { Name = "Player1Name", Text = "Speler 1", DisplayIndex = 3 };
			Columns.Add(col);
			//Speler2
			col = new ColumnHeader { Name = "Player2Name", Text = "Speler 2", DisplayIndex = 4 };
			Columns.Add(col);
			//Gespeelde wedstrijden
			col = new ColumnHeader { Name = "MatchesPlayed", Text = "Gespeelde Wedstrijden", DisplayIndex = 5 };
			Columns.Add(col);
			//Gewonnen wedstrijden.
            col = new ColumnHeader { Name = "MatchesWon", Text = "Wedstrijden Gewonnen", DisplayIndex = 6 };
            Columns.Add(col);
            //Verloren wedstrijden.
            col = new ColumnHeader { Name = "MatchesLost", Text = "Wedstrijden Verloren", DisplayIndex = 7 };
            Columns.Add(col);
			//Sets gewonnen
			col = new ColumnHeader { Name = "SetsWon", Text = "Sets Gewonnen", DisplayIndex = 8 };
			Columns.Add(col);
            //Sets verloren
            col = new ColumnHeader { Name = "SetsLost", Text = "Sets Verloren", DisplayIndex = 9 };
            Columns.Add(col);
			//Gemiddeld gewonnen sets
			col = new ColumnHeader { Name = "AverageSetsWon", Text = "Gemiddeld gewonnen", DisplayIndex = 10 };
			Columns.Add(col);
			//MatchesPlayed
			col = new ColumnHeader { Name = "PointsWon", Text = "Punten Gewonnen", DisplayIndex = 11 };
			Columns.Add(col);
            //MatchesPlayed
            col = new ColumnHeader { Name = "PointsLost", Text = "Punten Verloren", DisplayIndex = 12 };
            Columns.Add(col);
            //MatchesPlayed
            col = new ColumnHeader { Name = "AverageScore", Text = "Puntensaldo", DisplayIndex = 13 };
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
