using System.Xml.Linq;
using Shared.Datastructures;
using System;

namespace Shared.Domain
{
	/// <summary>
	/// Een team
	/// </summary>
	[Serializable]
	public class Team : Abstract
	{
		/// <summary>
		/// Is het team inactief
		/// </summary>
		private bool _isinoperative;
		/// <summary>
		/// De naam van het team
		/// </summary>
		private string _name = "";
		/// <summary>
		/// Speler 1
		/// </summary>
		private Player _player1;
		/// <summary>
		/// Speler 2
		/// </summary>
		private Player _player2;

		/// <summary>
		/// Is de actief-toestand van het team veranderd
		/// </summary>
		public bool InOperativeChanged;

		/// <summary>
		/// Constructor zonder ID of actief-toestand voor een 2-speler team
		/// </summary>
		/// <param name="name"> Naam van het team </param>
		/// <param name="p1"> Speler 1 </param>
		/// <param name="p2"> Speler 2 </param>
		public Team(string name, Player p1, Player p2)
		{
			_name = name;
			Player1 = p1;
			Player2 = p2;
		}

		/// <summary>
		/// Constructor zonder actief-toestand voor een 2-speler team
		/// </summary>
		/// <param name="teamID"> Het Team ID</param>
		/// <param name="name"> Naam van het team </param>
		/// <param name="p1"> Speler 1 </param>
		/// <param name="p2"> Speler 1</param>
        public Team(int teamID, string name, Player p1, Player p2)
        {
            TeamID = teamID;
            _name = name;
            Player1 = p1;
            Player2 = p2;
        }

		/// <summary>
		/// Volledige constructor voor een 2-speler team
		/// </summary>
		/// <param name="teamID"> Het Team ID</param>
		/// <param name="name"> Naam van het team </param>
		/// <param name="p1"> Speler 1 </param>
		/// <param name="p2"> Speler 2 </param>
		/// <param name="isInOperative"> Is het team inactief </param>
		public Team(int teamID, string name, Player p1, Player p2, bool isInOperative)
		{
			TeamID = teamID;
			_name = name;
			_player1 = p1;
			_player2 = p2;
			_isinoperative = isInOperative;
		}

		/// <summary>
		/// Constructor voor teams met 1 speler
		/// </summary>
		/// <param name="name"> Naam van het team </param>
		/// <param name="p1"> De speler</param>
		public Team(string name, Player p1)
		{
			_name = name;
			Player1 = p1;
		}
		
		/// <summary>
		/// Constructor voor teams met 1 speler zonder naam
		/// </summary>
		/// <param name="p1"> De speler </param>
		public Team(Player p1)
		{
			Player1 = p1;
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public Team() { }

		/// <summary>
		/// Vervangt alle gegevens van dit team met een nieuw team
		/// </summary>
		/// <param name="p"> Het nieuwe team</param>
		public void UpdateData(Team p)
		{
			Name = p.Name;
			Player1 = p.Player1;
			Player2 = p.Player2;
			IsInOperative = p.IsInOperative;
		}

		/// <summary>
		/// Het team ID
		/// </summary>
		public int TeamID
		{
			get;
			internal set;
		}

		/// <summary>
		/// Speler 1
		/// </summary>
		public Player Player1
		{
			get
			{
				return _player1;
			}
			set
			{
				changed = true;
				_player1 = value;
			}
		}

		/// <summary>
		/// Speler 2
		/// </summary>
		public Player Player2
		{
			get
			{
				return _player2;
			}
			set
			{
				changed = true;
				_player2 = value;
			}
		}

		/// <summary>
		/// De naam van het team
		/// </summary>
		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				changed = true;
				_name = value;
			}
		}

		/// <summary>
		/// Is het team inactief
		/// </summary>
		public bool IsInOperative
		{
			get
			{
				return _isinoperative;
			}
			private set
			{
				InOperativeChanged |= value != _isinoperative;
				changed |= value != _isinoperative;
				_isinoperative = value;
			}
		}

		/// <summary>
		/// Maak het team inactief
		/// </summary>
		public void SetInOperative()
		{
			IsInOperative = true;			
		}

		/// <summary>
		/// Een XML representatie van het team
		/// </summary>
		public XElement Xml
		{
			set
			{
				TeamID = value.Attribute("id").Value.ToInt32();
				_isinoperative = value.Attribute("inoperative").Value.ToBool();
				_name = value.Element("Name").Value;
			}
		}

		/// <summary>
		/// Een XML representatie van een "leeg" team
		/// </summary>
		public static XElement XmlDummy
		{
			get
			{
				return new XElement("Team",
					new XAttribute("id", ""),
					new XAttribute("inoperative", ""),
					new XAttribute("rank", ""),
					new XAttribute("matches", ""),
					new XAttribute("setswon", ""),
					new XAttribute("setslost", ""),
					new XAttribute("pointswon", ""),
					new XAttribute("pointslost", ""),
					new XAttribute("matcheswon", ""),
					new XAttribute("matcheslost", ""),
					new XElement("Name"),
					new XElement("Player1", new XAttribute("id", "")),
					new XElement("Player2", new XAttribute("id", "")));
			}
		}

		/// <summary>
		/// Zet het team om in een XML representatie
		/// </summary>
		/// <param name="element"> Het XElement waar de XML representatie in komt </param>
		public void ApplyXml(ref XElement element)
		{
			//Voeg de data toe aan de element
			element.SetAttributeValue("inoperative", _isinoperative.ToXMLString());
			element.Element("Name").Value = _name;

			if (_player1 == null)
			{
				element.Element("Player1").SetAttributeValue("id", _player2 != null ? _player2.PlayerID.ToString() : "");
				element.Element("Player2").SetAttributeValue("id", "");
			}
			else
			{
				element.Element("Player1").SetAttributeValue("id", _player1 != null ? _player1.PlayerID.ToString() : "");
				element.Element("Player2").SetAttributeValue("id", _player2 != null ? _player2.PlayerID.ToString() : "");
			}
		}
	}
}
