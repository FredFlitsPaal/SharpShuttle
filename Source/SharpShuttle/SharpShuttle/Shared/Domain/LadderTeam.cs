using System.Xml.Linq;
using Shared.Datastructures;
using System;

namespace Shared.Domain
{
	/// <summary>
	/// LadderTeam houdt een entry in de ladder bij. 
	/// Dit bestaat dus uit een team en de score erbij.
	/// </summary>
	[Serializable]
	public class LadderTeam : Abstract
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public LadderTeam() { }

		/// <summary>
		/// Maak een lege LadderTeam aan voor het begin van de poule
		/// </summary>
		/// <param name="pouleID"></param>
		/// <param name="rank"></param>
		/// <param name="team"></param>
		public LadderTeam(int pouleID, int rank, Team team)
		{
			Team = team;
			PouleID = pouleID;
			Rank = rank;
		}

		/// <summary>
		/// Maak een volledig ingevuld LadderTeam
		/// </summary>
		/// <param name="pouleID"> De poule waar het LadderTeam in zit </param>
		/// <param name="rank"> De ranking van het LadderTeam binnen de poule </param>
		/// <param name="matchesPlayed"> Het aantal gespeelde wedstrijden</param>
		/// <param name="setsWon"> Het aantal gewonnen sets </param>
		/// <param name="setsLost"> Het aantal verloren sets</param>
		/// <param name="pointsWon"> Het aantal behaalde punten </param>
		/// <param name="pointsLost"> Het aantal door tegenstanders behaalde punten</param>
		/// <param name="matchesWon"> Het aantal gewonnen wedstrijden </param>
		/// <param name="matchesLost"> Het aantal verloren wedstrijden </param>
		/// <param name="team"> Het team </param>
		public LadderTeam(int pouleID, int rank, int matchesPlayed,
			int setsWon, int setsLost, int pointsWon, int pointsLost,
			int matchesWon, int matchesLost, Team team)
		{
			MatchesLost = matchesLost;
			MatchesPlayed = matchesPlayed;
			MatchesWon = matchesWon;
			PointsLost = pointsLost;
			PointsWon = pointsWon;
			PouleID = pouleID;
			Rank = rank;
			SetsLost = setsLost;
			SetsWon = setsWon;
			Team = team;
		}

		/// <summary>
		/// De poule waar het LadderTeam in zit
		/// </summary>
		public int PouleID { get; set; }
		/// <summary>
		/// De ranking van het LadderTeam binnen de poule
		/// </summary>
		public int Rank { get; set; }
		/// <summary>
		/// Het aantal gespeelde wedstrijden
		/// </summary>
		public int MatchesPlayed { get; internal set; }
		/// <summary>
		/// Het aantal gewonnen sets
		/// </summary>
		public int SetsWon { get; internal set; }
		/// <summary>
		/// Het aantal verloren sets
		/// </summary>
		public int SetsLost { get; internal set; }
		/// <summary>
		/// Het aantal behaalde punten
		/// </summary>
		public int PointsWon { get; internal set; }
		/// <summary>
		/// Het aantal door tegenstanders behaalde punten
		/// </summary>
		public int PointsLost { get; internal set; }
		/// <summary>
		/// Het aantal gewonnen wedstrijden
		/// </summary>
		public int MatchesWon { get; internal set; }
		/// <summary>
		/// Het aantal verloren wedstrijden
		/// </summary>
		public int MatchesLost { get; internal set; }
		/// <summary>
		/// Het team
		/// </summary>
		public Team Team { get; set; }

		/// <summary>
		/// Het gemiddelde aantal gewonnen sets per match
		/// </summary>
		public float AverageSetsWon
		{
			get
			{
				if (MatchesPlayed == 0) return 0;
				return SetsWon / (float)MatchesPlayed;
			}
		}

		/// <summary>
		/// Het gemiddelde puntensaldo per match
		/// </summary>
		public float AverageScore{
			get
			{
				if (MatchesPlayed == 0) return 0;
				return (PointsWon - PointsLost) / (float)MatchesPlayed;
			}
		}

		/// <summary>
		/// Het TeamID
		/// </summary>
		public int TeamID { get { return Team.TeamID; } }

		/// <summary>
		/// Een XML representatie van het LadderTeam
		/// </summary>
		public XElement Xml
		{
			set
			{
				Rank = value.Attribute("rank").Value.ToInt32();
				MatchesPlayed = value.Attribute("matches").Value.ToInt32();
				SetsWon = value.Attribute("setswon").Value.ToInt32();
				SetsLost = value.Attribute("setslost").Value.ToInt32();
				PointsWon = value.Attribute("pointswon").Value.ToInt32();
				PointsLost = value.Attribute("pointslost").Value.ToInt32();
				MatchesWon = value.Attribute("matcheswon").Value.ToInt32();
				MatchesLost = value.Attribute("matcheslost").Value.ToInt32();
			}
		}

		/// <summary>
		/// Zet het LadderTeam om in een XML representatie
		/// </summary>
		/// <param name="element"> Het XElement waar de XML representatie in komt </param>
		public void ApplyXml(ref XElement element)
		{
			element.SetAttributeValue("matches", MatchesPlayed.ToString());
			element.SetAttributeValue("setswon", SetsWon.ToString());
			element.SetAttributeValue("setslost", SetsLost.ToString());
			element.SetAttributeValue("pointswon", PointsWon.ToString());
			element.SetAttributeValue("pointslost", PointsLost.ToString());
			element.SetAttributeValue("matcheswon", MatchesWon.ToString());
			element.SetAttributeValue("matcheslost", MatchesLost.ToString());
		}
	}
}
