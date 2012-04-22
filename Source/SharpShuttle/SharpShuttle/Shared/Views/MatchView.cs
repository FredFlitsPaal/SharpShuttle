using Shared.Domain;

namespace Shared.Views
{
	/// <summary>
	/// Deze view beschrijft een wedstrijd
	/// </summary>
	public class MatchView : AbstractView <Match>
	{
		#region Constructors
		
		/// <summary>
		/// Maakt een Matchview van een wedstrijd domeinobject
		/// </summary>
		/// <param name="match"></param>
		public MatchView(Match match)
		{
			data = match;
		}

		#endregion

		/// <summary>
		/// Het wedstrijd domeinobject
		/// </summary>
		public override Match Domain
		{
			get { return data; }
		}

		#region Eigenschappen MatchView

		/// <summary>
		/// Het ID van de wedstrijd
		/// </summary>
		public string MatchID
		{
			get { return data.MatchID.ToString(); }
		}

		/// <summary>
		/// De ronde van de wedstrijd
		/// </summary>
		public int Round
		{
			get { return data.Round; }
		}

		/// <summary>
		/// Het wedstrijdnummer van de wedstrijd
		/// </summary>
		public int MatchNumber
		{
			get { return data.MatchNumber; }
		}

		/// <summary>
		/// Het veld waarop de wedstrijd gespeeld wordt
		/// </summary>
		public int Court
		{
			get { return data.Court; }
			set { data.Court = value;  }
		}

		/// <summary>
		/// De naam van de poule van de wedstrijd
		/// </summary>
		public string PouleName
		{
			get { return data.PouleName; }
		}

		/// <summary>
		/// Team 1 van de wedstrijd
		/// </summary>
		public string Team1
		{
			get { return data.TeamA.Name ?? data.TeamA.Player1.Name; }
		}

		/// <summary>
		/// Team 2 van de wedstrijd
		/// </summary>
		public string Team2
		{
            get { return data.TeamB.Name ?? data.TeamB.Player1.Name; }
		}

		/// <summary>
		/// Speler 1 van team 1 van de wedstrijd
		/// </summary>
		public string Team1Player1
		{
			get { return data.TeamA.Player1.Name; }
		}

		/// <summary>
		/// Speler 2 van team 1 van de wedstrijd
		/// Returnt een lege string als het een 1-speler team is
		/// </summary>
		public string Team1Player2
		{
			get
			{
				if (data.TeamA.Player2 == null)
					return "";
				return data.TeamA.Player2.Name;
			}
		}

		/// <summary>
		/// Speler 1 van team 2 van de wedstrijd
		/// </summary>
		public string Team2Player1
		{
			get { return data.TeamB.Player1.Name; }
		}

		/// <summary>
		/// Speler 2 van team 2 van de wedstrijd.
		/// Returnt een lege string als het een 1-speler team is
		/// </summary>
		public string Team2Player2
		{
			get
			{
				if (data.TeamB.Player2 == null)
					return "";
				return data.TeamB.Player2.Name;
			}
		}

		/// <summary>
		/// De starttijd van de wedstrijd
		/// </summary>
		public string StartTime
		{
			get { return data.StartTime;  }
			set { data.StartTime = value;  }
		}

		/// <summary>
		/// De eindtijd van de wedstrijd
		/// </summary>
		public string EndTime
		{
			get { return data.EndTime; }
			set { data.EndTime = value; }
		}

		/// <summary>
		/// De status van de wedstrijd
		/// </summary>
		public string Status
		{
			get 
			{
				if (data.Played)
					return "Afgelopen";

				if (StartTime.Length > 0 && EndTime.Length == 0)
					return "Bezig";

				if (StartTime.Length > 0 && EndTime.Length > 0)
					return "Afgelopen, geen score";

				return "Nog niet afgelopen";
			}
		}

		/// <summary>
		/// Aantal gewonnen sets van team 1
		/// </summary>
        public int SetsWonTeam1
        {
			get { return data.SetsTeamA; }
			set { data.SetsTeamA = value; }
        }

		/// <summary>
		/// Aantal gewonnen sets van team 2
		/// </summary>
        public int SetsWonTeam2
        {
			get { return data.SetsTeamB; }
			set { data.SetsTeamB = value; }
        }
        
		/// <summary>
		/// Aantal behaalde punten van team 1
		/// </summary>
        public int PointsTeam1
        {
			get { return data.ScoreTeamA; }
			set { data.ScoreTeamA = value; }
        }

		/// <summary>
		/// Aantal behaalde punten van team 2
		/// </summary>
        public int PointsTeam2
        {
			get { return data.ScoreTeamB; }
			set { data.ScoreTeamB = value; }
        }

		/// <summary>
		/// Een string die de eindscore in sets weergeeft
		/// </summary>
        public string Score
        {
            get
            {
                if (SetsWonTeam2 == 0 && SetsWonTeam1 == 0 && !data.Played)
                    return "";
                return (SetsWonTeam1 + " - " + SetsWonTeam2);
            }
		}

		/// <summary>
		/// Opmerkingen bij de wedstrijd
		/// </summary>
		public string Comment
		{
			get { return data.Comment; }
			set { data.Comment = value; }
		}

		#endregion
	}
}
