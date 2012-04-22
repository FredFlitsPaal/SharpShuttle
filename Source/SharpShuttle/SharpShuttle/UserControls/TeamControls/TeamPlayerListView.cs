﻿using System.Windows.Forms;
﻿using Shared.Domain;
﻿using Shared.Views;
using UserControls.AbstractControls;

namespace UserControls.TeamControls
{
	/// <summary>
	/// Lijst van spelers met relevante informatie voor het vormen van teams
	/// </summary>
	public class TeamPlayerListView : DomainListView<PlayerViews, PlayerView, Player>
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public TeamPlayerListView()
		{
			InitializeAutoScroll();
			IsOnScroll = true;

			//kolommen bepalen
			//PlayerView moet dus ook de volgende eigenschappen kennen (case sensitive):
			//Ranking, Id, Name, ScoreAvg, ScoreBalance, MatchesPlayed, MatchesWon
			ColumnHeader col;

			//Name
			col = new ColumnHeader { Name = "Name", Text = "Naam", DisplayIndex = 0 };
			col.Width = 144;
			Columns.Add(col);
			//Gender
			col = new ColumnHeader { Name = "Gender", Text = "Gesl", DisplayIndex = 1 };
			col.Width = 35;
			Columns.Add(col);
			//Club
			col = new ColumnHeader { Name = "Club", Text = "Club", DisplayIndex = 2 };
			col.Width = 115;
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
