using Shared.Domain;


namespace Client.Printers
{
	/// <summary>
	/// Bevat alle informatie van een wedstrijd die nodig is om een wedstrijdbriefje te printen.
	/// </summary>
	public class MatchNote
	{
		/// <summary>
		/// Maakt een nieuw wedstrijdbriefje.
		/// </summary>
		/// <param name="match">De wedstrijd die als bron dient.</param>
		public MatchNote(Match match)
		{
			MatchId = match.MatchID.ToString();
			TeamA = match.TeamA;
			TeamB = match.TeamB;
			PouleName = match.PouleName;
			Round = match.Round.ToString();
		}

		/// <summary>
		/// Het ID van de wedstrijd.
		/// </summary>
		public string MatchId
		{
			get;
			set;
		}

		/// <summary>
		/// Het eerste team van de wedstrijd.
		/// </summary>
		public Team TeamA
		{
			get;
			set;
		}

		/// <summary>
		/// Het tweede team van de wedstrijd.
		/// </summary>
		public Team TeamB
		{
			get;
			set;
		}

		/// <summary>
		/// De naam van de poule waarin de wedstrijd zit.
		/// </summary>
		public string PouleName
		{
			get;
			set;
		}

		/// <summary>
		/// Het rondenummer waarin de wedstrijd word gespeeld.
		/// </summary>
		public string Round
		{
			get;
			set;
		}
	}
}
