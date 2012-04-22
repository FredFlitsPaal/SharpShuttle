using Shared.Domain;

namespace Shared.Views
{
	/// <summary>
	/// Beschrijft een LadderTeam
	/// </summary>
	public class LadderTeamView : AbstractView <LadderTeam>
	{
		#region Constructors

		/// <summary>
		/// Maakt een LadderTeamView van een LadderTeam domeinobject
		/// </summary>
		/// <param name="team"> Het LadderTeam </param>
		public LadderTeamView(LadderTeam team)
		{
			data = team;
		}

		#endregion

		/// <summary>
		/// Het LadderTeam domeinobject
		/// </summary>
		public override LadderTeam Domain
		{
			get { return data; }
		}

		#region Eigenschappen van een LadderTeamView

		/// <summary>
		/// Het ID van het LadderTeam
		/// </summary>
		public int TeamId
		{
		    get { return data.TeamID; }
		}

		/// <summary>
		/// De naam van het LadderTeam
		/// </summary>
        public string Name
        {
			get { return data.Team.Name ?? ""; }
			set { data.Team.Name = value; }
        }

		/// <summary>
		/// Speler 1 van het LadderTeam
		/// </summary>
		public Player Player1
		{
			get { return data.Team.Player1; }
			set { data.Team.Player1 = value; }
		}

		/// <summary>
		/// Speler 2 van het LadderTeam
		/// </summary>
		public Player Player2
		{
			get { return data.Team.Player2; }
			set { data.Team.Player2 = value; }
		}

		/// <summary>
		/// De ranking van het LadderTeam
		/// </summary>
		public int Rank
		{
			get { return data.Rank; }
		}

		/// <summary>
		/// Aantal behaalde punten van het LadderTeam
		/// </summary>
		public int PointsWon
		{
			get { return data.PointsWon; }
		}

		/// <summary>
		/// Aantal door tegenstanders behaalde punten van het LadderTeam
		/// </summary>
		public int PointsLost
		{
			get { return data.PointsLost; }
		}

		/// <summary>
		/// Aantal gewonnen sets van het LadderTeam
		/// </summary>
		public int SetsWon
		{
			get { return data.SetsWon; }
		}

		/// <summary>
		/// Aantal door tegenstanders gewonnen sets van het LadderTeam
		/// </summary>
        public int SetsLost
        {
			get { return data.SetsLost; }
        }

		/// <summary>
		/// Aantal gewonnen wedstrijden van het LadderTeam
		/// </summary>
        public int MatchesWon
        {
			get { return data.MatchesWon; }
        }

		/// <summary>
		/// Aantal gespeelde wedstrijden van het LadderTeam
		/// </summary>
        public int MatchesPlayed
        {
			get { return data.MatchesPlayed; }
        }

		/// <summary>
		/// Aantal door tegenstanders gewonnen wedstrijden van het LadderTeam
		/// </summary>
        public int MatchesLost
        {
			get { return data.MatchesLost; }
        }

		/// <summary>
		/// Het totaal puntensaldo van het LadderTeam
		/// </summary>
        public int Score
        {
            get { return PointsWon - PointsLost; }
        }

		/// <summary>
		/// Gemiddelde aantal gewonnen sets per wedstrijd van het LadderTeam
		/// </summary>
        public float AverageSetsWon
        {
			get { return data.AverageSetsWon; }
        }

		/// <summary>
		/// Het gemiddelde puntensaldo per wedstrijd van het LadderTeam
		/// </summary>
        public float AverageScore
        {
            get { return data.AverageScore; }
		}

		/// <summary>
		/// Speler 1 van het LadderTeam
		/// </summary>
		public string Player1Name
		{
			get { return data.Team.Player1.Name; }
		}

		/// <summary>
		/// Speler 2 van het LadderTeam
		/// Returnt een lege string als het een 1-speler team is
		/// </summary>
		public string Player2Name
		{
			get
			{
				if (Player2 == null)
					return "";
				return data.Team.Player2.Name;
			}
		}

		#endregion

	}
}