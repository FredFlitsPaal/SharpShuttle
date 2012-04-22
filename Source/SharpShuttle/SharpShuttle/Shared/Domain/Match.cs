using System;
using Shared.Communication.Serials;
using System.Xml.Linq;
using Shared.Datastructures;

namespace Shared.Domain
{

	/// <summary>
	/// Representatie van wie gewonnen heeft
	/// </summary>
	[Serializable]
	public enum Winner : byte
	{
		/// <summary>
		/// Team A heeft gewonnen
		/// </summary>
		TeamA = 0,
		/// <summary>
		/// Team B heeft gewonnen
		/// </summary>
		TeamB = 1,
		/// <summary>
		/// De wedstrijd is geeindigd in gelijkspel
		/// </summary>
		Draw = Byte.MaxValue
	}

	/// <summary>
	/// Een wedstrijd
	/// </summary>
	[Serializable]
    public class Match : AbstractT<Match>
    {
		/// <summary>
		/// De score van team a
		/// </summary>
		private int _scoreteama;
		/// <summary>
		/// De score van team b
		/// </summary>
		private int _scoreteamb;
		/// <summary>
		/// Het aantal gewonnen sets van team a
		/// </summary>
		private int _setsteama;
		/// <summary>
		/// Het aantal gewonnen sets van team b
		/// </summary>
		private int _setsteamb;
		/// <summary>
		/// Het veld waarop de wedstrijd wordt gespeeld
		/// </summary>
		private int _court;
		/// <summary>
		/// De begintijd 
		/// </summary>
		private string _starttime;
		/// <summary>
		/// De eindtijd
		/// </summary>
		private string _endtime;
		/// <summary>
		/// Opmerkingen bij de wedstrijd
		/// </summary>
		private string _comment;
		/// <summary>
		/// Is de score gewijzigd
		/// </summary>
		private bool _scorechanged;
		/// <summary>
		/// Informatie over alle sets
		/// </summary>
		private string _setdata;

		/// <summary>
		/// Default constructor
		/// </summary>
        public Match()
        {
        }

		/// <summary>
		/// Maakt een nieuwe wedstrijd zonder poule, ingevulde scores of toegekend veld
		/// </summary>
		/// <param name="t1"> Team a </param>
		/// <param name="t2"> Team b </param>
		/// <param name="comment"> Opmerkingen bij de wedstrijd</param>
		public Match(Team t1, Team t2, string comment)
		{
			TeamA = t1;
			TeamB = t2;
			_comment = comment;
			SetsTeamA = 0;
			SetsTeamA = 0;
			ScoreTeamA = 0;
			ScoreTeamA = 0;
			PouleID = -1;
		    StartTime = "";
		    EndTime = "";
		}

		/// <summary>
		/// Maakt een nieuwe wedstrijd zonder ingevulde scores of toegekend veld
		/// </summary>
		/// <param name="pouleID"> De poule waar de wedstrijd in gespeeld wordt</param>
		/// <param name="round"> De ronde waar de wedstrijd in gespeeld wordt</param>
		/// <param name="matchNumber"> Het nummer van de wedstrijd</param>
		/// <param name="t1"> Team a</param>
		/// <param name="t2"> Team b </param>
		/// <param name="comment"> Opmerkingen bij de wedstrijd</param>
        public Match(int pouleID, int round, int matchNumber, Team t1, Team t2, string comment)
        {
            TeamA = t1;
            TeamB = t2;
            _comment = comment;
			SetsTeamA = 0;
			SetsTeamA = 0;
			ScoreTeamA = 0;
			ScoreTeamA = 0;
			PouleID = pouleID;
            StartTime = "";
            EndTime = "";
            Round = round;
            MatchNumber = (short)matchNumber;
        }

		/// <summary>
		/// Maak een afgeronde wedstrijd
		/// </summary>
		/// <param name="pouleID"> De poule waar de wedstrijd in gespeeld wordt </param>
		/// <param name="round"> De ronde waar de wedstrijd in gespeeld wordt </param>
		/// <param name="matchNumber"> Het nummer van de wedstrijd </param>
		/// <param name="played"> Is de wedstrijd al gespeeld </param>
		/// <param name="teamA"> Team a</param>
		/// <param name="teamB"> Team b</param>
		/// <param name="scoreTeamA"> De score van team a </param>
		/// <param name="scoreTeamB"> De score van team b</param>
		/// <param name="setsTeamA"> Het aantal gewonnen sets van team a </param>
		/// <param name="setsTeamB"> Het aantal gewonnen sets van team b </param>
		/// <param name="court"></param>
		/// <param name="startTime"></param>
		/// <param name="endTime"></param>
		/// <param name="comment"></param>
		public Match(int pouleID, int round, short matchNumber, bool played, Team teamA, Team teamB, int scoreTeamA, int scoreTeamB, int setsTeamA, int setsTeamB, int court, String startTime, String endTime, String comment){
			StartTime = startTime;        
            Comment = comment;
            EndTime = endTime;
            Court = court;
            MatchNumber = matchNumber;
            Played = played;
			PouleID = pouleID;
            Round = round;
			TeamA = teamA;
			TeamB = teamB;
            _scoreteama = scoreTeamA;
            _scoreteamb = scoreTeamB;
            _setsteama = setsTeamA;
            _setsteamb = setsTeamB;
			changed = false;
		}

		/// <summary>
		/// Is de score gewijzigd
		/// </summary>
		public bool ScoreChanged
		{
			get
			{
				return _scorechanged;
			}
		}

		/// <summary>
		/// Het wedstrijd ID
		/// </summary>
		public int MatchID
		{
			get;
			internal set;
		}

		/// <summary>
		/// De poule waarin gespeeld wordt
		/// </summary>
		public int PouleID
		{
			get;
			set;
		}

		/// <summary>
		/// De naam van de poule waarin gespeeld wordt
		/// </summary>
		public string PouleName
		{
			get;
			private set;
		}

		/// <summary>
		/// De ronde van de poule waarin gespeeld wordt
		/// </summary>
		public int Round
		{
			get;
			set;
		}

		/// <summary>
		/// Het wedstrijdnummer
		/// </summary>
		public short MatchNumber
		{
			get;
			set;
		}

		/// <summary>
		/// Team a
		/// </summary>
		public Team TeamA
		{
			get;
			set;
		}

		/// <summary>
		/// Team b
		/// </summary>
		public Team TeamB
		{
			get; 
			set;
		}

		/// <summary>
		/// Het aantal gewonnen sets van team a
		/// </summary>
        public int SetsTeamA
        {
			get
			{
				return _setsteama;
			}
			set
			{
				changed |= _setsteama != value;
				_scorechanged |= _setsteama != value;
				_setsteama = value;
			}
        }

		/// <summary>
		/// Het aantal gewonnen sets van team b
		/// </summary>
        public int SetsTeamB
        {
			get
			{
				return _setsteamb;
			}
			set
			{
				changed |= _setsteamb != value;
				_scorechanged |= _setsteamb != value;
				_setsteamb = value;
			}
        }

		/// <summary>
		/// De score van team a
		/// </summary>
        public int ScoreTeamA
        {
			get
			{
				return _scoreteama;
			}
			set
			{
				changed |= _scoreteama != value;
				_scorechanged |= _scoreteama != value;
				_scoreteama = value;
			}
        }

		/// <summary>
		/// De score van team b
		/// </summary>
        public int ScoreTeamB
        {
			get
			{
				return _scoreteamb;
			}
			set
			{
				changed |= _scoreteamb != value;
				_scorechanged |= _scoreteamb != value;
				_scoreteamb = value;
			}
        }

		/// <summary>
		/// Informatie over alle sets
		/// </summary>
		public string SetData
		{
			get
			{
				return _setdata;
			}
			set
			{
				changed |= _setdata != value;
				_scorechanged |= _setdata != value;
				_setdata = value;
			}
		}

		/// <summary>
		/// Het veld waarop gespeeld wordt
		/// </summary>
		public int Court
		{
			get
			{
				return _court;
			}
			set
			{
				changed |= _court != value;
				_court = value;
			}
		}

		/// <summary>
		/// De starttijd
		/// </summary>
		public string StartTime
		{
			get
			{
				return _starttime;
			}
			set
			{
				changed = true;
				_starttime = value;
			}
		}

		/// <summary>
		/// De eindtijd
		/// </summary>
		public string EndTime
		{
			get
			{
				return _endtime;
			}
			set
			{
				changed = true;
				_endtime = value;
			}
		}

		/// <summary>
		/// Is de wedstrijd al gespeeld
		/// </summary>
		public bool Played
		{
			get;
			internal set;
		}

		/// <summary>
		/// Is de wedstrijd in de cache opgeslagen
		/// </summary>
		public bool Cached
		{
			get;
			internal set;
		}

		/// <summary>
		/// Is de wedstrijd disabled
		/// </summary>
		public bool Disabled
		{
			get;
			private set;
		}

		/// <summary>
		/// Opmerkingen bij de wedstrijd
		/// </summary>
		public string Comment
		{
			get
			{
				return _comment;
			}
			set
			{
				changed = true;
				_comment = value;
			}
		}

		/// <summary>
		/// Het serienummer
		/// </summary>
		public SerialDefinition SerialNumber
		{
			get;
			internal set;
		}

		/// <summary>
		/// De winnaar van de wedstrijd
		/// </summary>
		public Winner Winner
		{
			get
			{
				//Bepalen wie er gewonnen heeft
				if (SetsTeamA > SetsTeamB)
					return Winner.TeamA;
				if (SetsTeamA < SetsTeamB)
					return Winner.TeamB;
				return Winner.Draw;
			}
		}

		internal override void UpdateData(Match newdata)
		{
			SetsTeamA = newdata.SetsTeamA;
			SetsTeamB = newdata.SetsTeamB;
			ScoreTeamA = newdata.ScoreTeamA;
			ScoreTeamB = newdata.ScoreTeamB;
			Court = newdata.Court;
			StartTime = newdata.StartTime;
			EndTime = newdata.EndTime;
			Comment = newdata.Comment;

			changed = false;
		}

		/// <summary>
		/// Deze methode wordt gebruikt om de ladder score te updaten zodat de nieuwe score 
		/// verwerkt is in de ladder
		/// </summary>
		/// <param name="teama"></param>
		/// <param name="teamb"></param>
		public bool UpdateLadder(ref LadderTeam teama, ref LadderTeam teamb)
		{
			//Controleer eerst of het dezelfde wedstrijd is:
			if (TeamA.TeamID != teama.TeamID || TeamB.TeamID != teamb.TeamID)
				return false;

			teama.PointsWon += ScoreTeamA;
			teamb.PointsWon += ScoreTeamB;

			teama.PointsLost += ScoreTeamB;
			teamb.PointsLost += ScoreTeamA;

			teama.SetsWon += SetsTeamA;
			teamb.SetsWon += SetsTeamB;

			teama.SetsLost += SetsTeamB;
			teamb.SetsLost += SetsTeamA;

			teama.MatchesPlayed++;
			teamb.MatchesPlayed++;

			if (Winner == Winner.TeamA)
			{
				teama.MatchesWon ++;
				teamb.MatchesLost ++;
			}
			else if (Winner == Winner.TeamB)
			{
				teama.MatchesLost ++;
				teamb.MatchesWon ++;
			}
			
			//Na het invullen van een wedstrijd zal hij ook gespeeld zijn.
			Played = true;

			return true;
		}

		/// <summary>
		/// Deze functie maakt de huidige score in de ladderteams ongedaan zodat de nieuwe match ingevuld kan worden
		/// </summary>
		/// <param name="teama"></param>
		/// <param name="teamb"></param>
		public bool UndoLadder(ref LadderTeam teama, ref LadderTeam teamb)
		{
			//Controleer eerst of het dezelfde wedstrijd is:

			if (TeamA.TeamID != teama.TeamID || TeamB.TeamID != teamb.TeamID)
				return false;

			teama.PointsWon -= this.ScoreTeamA;
			teamb.PointsWon -= this.ScoreTeamB;

			teama.PointsLost -= this.ScoreTeamB;
			teamb.PointsLost -= this.ScoreTeamA;

			teama.SetsWon -= this.SetsTeamA;
			teamb.SetsWon -= this.SetsTeamB;

			teama.SetsLost -= this.SetsTeamB;
			teamb.SetsLost -= this.SetsTeamA;

			//Als deze wedstrijd al gespeeld is, betekend het ook dat hij al is ingevuld.
			//Daarom moeten we het aantal gespeelde wedstrijden weer updaten

			if (Played)
			{
				//Als de wedstrijd al gespeeld is, moeten we de gewonnen / verloren sets er even afhalen

				if (Winner == Winner.TeamA)
				{
					teama.MatchesWon--;
					teamb.MatchesLost--;
				}
				else if (Winner == Winner.TeamB)
				{
					teama.MatchesLost--;
					teamb.MatchesWon--;
				}

				teama.MatchesPlayed--;
				teamb.MatchesPlayed--;
			}

			return true;
		}

		/// <summary>
		/// XML representatie van de wedstrijd
		/// </summary>
		public XElement Xml
		{
			set
			{
				this.MatchID = value.Attribute("id").Value.ToInt32();
				this.Round = value.Attribute("round").Value.ToInt32();
				this.MatchNumber = value.Attribute("matchnumber").Value.ToInt16();
				this.PouleID = value.Parent.Parent.Attribute("id").Value.ToInt32();
				this.PouleName = value.Parent.Parent.Element("Name").Value;
				this.Played = value.Attribute("played").Value.ToBool();
				this.Cached = value.Attribute("cached").Value.ToBool();
				this._court = value.Element("Field").Value.ToInt32();
				this._starttime = value.Element("StartTime").Value;
				this._endtime = value.Element("EndTime").Value;
				this._comment = value.Element("Comment").Value;
				this._scoreteama = value.Element("TeamA").Attribute("score").Value.ToInt32();
				this._scoreteamb = value.Element("TeamB").Attribute("score").Value.ToInt32();
				this._setsteama = value.Element("TeamA").Attribute("sets").Value.ToInt32();
				this._setsteamb = value.Element("TeamB").Attribute("sets").Value.ToInt32();
				this._setdata = value.Element("SetData").Value;

				//Nu controleren of het een match is waarvan de scoren iet mag worden ingevuld
				Disabled = value.Attribute("disabled") != null && value.Attribute("disabled").Value.ToBool();
			}
		}

		/// <summary>
		/// Levert een XML representatie van een "lege" wedstrijd
		/// </summary>
		public static XElement XmlDummy
		{
			get
			{
				return new XElement("Match",
					new XAttribute("id", ""),
					new XAttribute("round", ""),
					new XAttribute("matchnumber", ""),
					new XAttribute("played", false.ToXMLString()),
					new XAttribute("cached", false.ToXMLString()),
					new XElement("TeamA",
						new XAttribute("id", ""),
						new XAttribute("sets", ""),
						new XAttribute("score", "")),

					new XElement("TeamB",
						new XAttribute("id", ""),
						new XAttribute("sets", ""),
						new XAttribute("score", "")),

					new XElement("SetData", ""),

					new XElement("Field"),
					new XElement("StartTime"),
					new XElement("EndTime"),
					new XElement("Comment"));
			}
		}

		/// <summary>
		/// Zet de wedstrijd om in een XML representatie
		/// </summary>
		/// <param name="element"> Het XElement waar de XML representatie in komt </param>
		public void ApplyXml(ref XElement element)
		{
			//Pas alle gegevens toe op element
			XElement teama = element.Element("TeamA");
			XElement teamb = element.Element("TeamB");

			teama.SetAttributeValue("sets", SetsTeamA.ToString());
			teama.SetAttributeValue("score", ScoreTeamA.ToString());
			teamb.SetAttributeValue("sets", SetsTeamB.ToString());
			teamb.SetAttributeValue("score", ScoreTeamB.ToString());

			element.SetAttributeValue("played", Played.ToXMLString());

			element.SetElementValue("SetData", _setdata);

			element.SetElementValue("Field", Court.ToString());
			element.SetElementValue("StartTime", StartTime);
			element.SetElementValue("EndTime", EndTime);
			element.SetElementValue("Comment", Comment);
		}
    }
}
