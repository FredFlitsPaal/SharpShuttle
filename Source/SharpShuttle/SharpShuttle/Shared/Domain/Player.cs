using System;
using System.Xml.Linq;
using Shared.Datastructures;

namespace Shared.Domain
{
	/// <summary>
	/// Een speler
	/// </summary>
	[Serializable]
	public class Player : AbstractT<Player>
	{
		/// <summary>
		/// Naam van de speler
		/// </summary>
		private string _name;
		/// <summary>
		/// Geslacht van de speler
		/// </summary>
		private string _gender;
		/// <summary>
		/// Vereniging van de speler
		/// </summary>
		private string _club;
		/// <summary>
		/// Poulevoorkeuren van de speler
		/// </summary>
		private string _preferences;
		/// <summary>
		/// Opmerkingen bij de speler
		/// </summary>
		private string _comment;

		/// <summary>
		/// Default constructor
		/// </summary>
		public Player() { }

		/// <summary>
		/// Maakt een speler aan zonder ID
		/// </summary>
		/// <param name="Name"> Naam van de speler</param>
		/// <param name="Gender"> Geslacht van de speler</param>
		/// <param name="Club"> Vereniging van de speler</param>
		/// <param name="Preferences"> Poulevoorkeuren van de speler </param>
		/// <param name="Comment"> Opmerkingen bij de speler </param>
		public Player(string Name, string Gender, string Club, string Preferences, string Comment)
		{
			this.Name = Name;
			this.Gender = Gender;
			this.Club = Club;
			this.Preferences = Preferences;
			this.Comment = Comment;
		}

		/// <summary>
		/// Maakt een speler aan met ID
		/// </summary>
		/// <param name="playerID"></param>
		/// <param name="name"></param>
		/// <param name="gender"></param>
		/// <param name="club"></param>
		/// <param name="preferences"></param>
		/// <param name="comment"></param>
		public Player(int playerID, string name, string gender, string club, string preferences, string comment)
		{
			PlayerID = playerID;
			Name = name;
			Gender = gender;
			Club = club;
			Preferences = preferences;
			Comment = comment;
		}

		/// <summary>
		/// Het ID van de speler
		/// </summary>
		public int PlayerID
		{
			get; internal set;
		}

		/// <summary>
		/// De naam van de speler
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
		/// Het geslacht van de speler
		/// </summary>
		public string Gender
		{
			get
			{
				return _gender;
			}
			set
			{
				changed = true;
				_gender = value;
			}
		}

		/// <summary>
		/// De vereniging van de speler
		/// </summary>
		public string Club
		{
			get
			{
				return _club;
			}
			set
			{
				changed = true;
				_club = value;
			}
		}

		/// <summary>
		/// Poulevoorkeuren van de speler
		/// </summary>
		public string Preferences
		{
			get
			{
				return _preferences;
			}
			set
			{
				changed = true;
				_preferences = value;
			}
		}

		/// <summary>
		/// Opmerkingen bij de speler
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

		internal override void UpdateData(Player newdata)
		{
			Name = newdata.Name;
			Gender = newdata.Gender;
			Club = newdata.Club;
			Preferences = newdata.Preferences;
			Comment = newdata.Comment;
			changed = false;
		}

		/// <summary>
		/// XML representatie van de speler
		/// </summary>
		public XElement Xml
		{
			set
			{
				PlayerID = value.Attribute("id").Value.ToInt32();
				_gender = value.Attribute("gender").Value;
				_name = value.Element("Name").Value;
				_club = value.Element("Club").Value;
				_preferences = value.Element("Preferences").Value;
				Comment = value.Element("Comment").Value;
				changed = false;
			}
			get
			{
				return new XElement("Player",
					new XAttribute("id", PlayerID.ToString()),
					new XAttribute("gender", _gender),
					new XElement("Name", _name),
					new XElement("Club", _club),
					new XElement("Preferences", _preferences),
					new XElement("Comment", _comment));
			}
		}

		/// <summary>
		/// XML representatie van een "lege" speler
		/// </summary>
		public static XElement XmlDummy
		{
			get
			{
				return new XElement("Player",
					new XAttribute("id", ""),
					new XAttribute("gender", ""),
					new XElement("Name"),
					new XElement("Club"),
					new XElement("Preferences"),
					new XElement("Comment"));
			}
		}

		/// <summary>
		/// Zet de speler om in een XML representatie
		/// </summary>
		/// <param name="element"> Het XElement waar de XML representatie in komt </param>
		public void ApplyXml(ref XElement element)
		{
			//De read/write attributen opslaan in de element
			element.Element("Name").Value = _name;
			element.SetAttributeValue("gender", Gender);
			element.Element("Club").Value = _club;
			element.Element("Preferences").Value = _preferences;
			element.Element("Comment").Value = _comment;
		}
	}
}
