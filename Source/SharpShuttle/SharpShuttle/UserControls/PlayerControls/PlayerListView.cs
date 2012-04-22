using System.Collections.Generic;
using System.Windows.Forms;
using Shared.Domain;
using Shared.Views;
using UserControls.AbstractControls;

namespace UserControls.PlayerControls
{
	/// <summary>
	/// ListView van spelers
	/// </summary>
	public class PlayerListView : DomainListView<PlayerViews, PlayerView, Player>
	{
		/// <summary>
		/// Default constructor, initialiseert alle kolommen
		/// </summary>
		public PlayerListView()
		{
			//kolommen bepalen
			//PlayerView moet dus ook de volgende eigenschappen kennen (case sensitive):
			//Ranking, Id, Name, ScoreAvg, ScoreBalance, MatchesPlayed, MatchesWon
			ColumnHeader col;

			//Ranking
			col = new ColumnHeader {Name = "Ranking", Text = "#", DisplayIndex = 0};
			Columns.Add(col);
			//Id
			col = new ColumnHeader {Name = "Id", Text = "Id", DisplayIndex = 1};
			Columns.Add(col);
			//Name
			col = new ColumnHeader { Name = "Name", Text = "Naam", DisplayIndex = 2 };
			Columns.Add(col);
			//ScoreAvg
			col = new ColumnHeader { Name = "ScoreAvg", Text = "Gem. Score", DisplayIndex = 3 };
			Columns.Add(col);
			//ScoreBalance
			col = new ColumnHeader { Name = "ScoreBalance", Text = "Score Saldo", DisplayIndex = 4 };
			Columns.Add(col);
			//MatchesPlayed
			col = new ColumnHeader { Name = "MatchesPlayed", Text = "Wedstijden Gespeeld", DisplayIndex = 5 };
			Columns.Add(col);
			//MatchesWon
			col = new ColumnHeader { Name = "MatchesWon", Text = "Wedstrijden Gewonnen", DisplayIndex = 6 };
			Columns.Add(col);
		}

		/// <summary>
		/// De datasource
		/// </summary>
		public new PlayerViews DataSource
		{
			get { return base.DataSource; }
			set { base.DataSource = value; }
		}
	}
}
