using System;
using Shared.Communication.Serials;
using System.Xml.Linq;
using Shared.Datastructures;

namespace Shared.Domain
{
	/// <summary>
	/// Een poule
	/// </summary>
	[Serializable]
	public class Poule : AbstractT<Poule>
	{
		/// <summary>
		/// De verschillende disciplines
		/// </summary>
		[Serializable]
        public enum Disciplines : long
		{
			/// <summary>
			/// Heren enkel
			/// </summary>
			MaleSingle = 0,
			/// <summary>
			/// Heren dubbel
			/// </summary>
			MaleDouble = 2,
			/// <summary>
			/// Vrouwen enkel
			/// </summary>
			FemaleSingle = 3,
			/// <summary>
			/// Vrouwen dubbel
			/// </summary>
			FemaleDouble = 4,
			/// <summary>
			/// Gemengd
			/// </summary>
			Mixed = 5,
			/// <summary>
			/// Unisex enkel
			/// </summary>
			UnisexSingle = 6,
			/// <summary>
			/// Unisex dubbel
			/// </summary>
			UnisexDouble = 7
		}

		/// <summary>
		/// Opmerkingen bij de poule
		/// </summary>
		private string _comment;
		/// <summary>
		/// De naam van de poule
		/// </summary>
		private string _name;
		/// <summary>
		/// Het niveau van de poule
		/// </summary>
		private string _niveau;
		/// <summary>
		/// De discipline van de poule
		/// </summary>
		private Disciplines _discipline;

		/// <summary>
		/// Default constructor
		/// </summary>
		public Poule() { }

		/// <summary>
		/// Maakt een poule zonder ID
		/// </summary>
		/// <param name="name"> Naam van de poule</param>
		/// <param name="discipline"> Discipline van de poule </param>
		/// <param name="level"> Niveau van de poule</param>
		/// <param name="comment"> Opmerkingen bij de poule</param>
		public Poule(string name, Disciplines discipline, string level, string comment)
		{
			Name = name;
            _comment = comment;
			Discipline = discipline;
			_niveau = level;
			Round = 0;
		}

		/// <summary>
		/// Maakt een poule met ID
		/// </summary>
		/// <param name="pouleID"> ID van de poule </param>
		/// <param name="name"> Naam van de poule </param>
		/// <param name="discipline"> Discipline van de poule </param>
		/// <param name="level"> Niveau van de poule </param>
		/// <param name="comment"> Opmerkingen bij de poule </param>
		public Poule(int pouleID, string name, Disciplines discipline, string level, string comment)
		{
			PouleID = pouleID;
			Name = name;
            _comment = comment;
			Discipline = discipline;
			_niveau = level;
			Round = 0;
		}

		/// <summary>
		/// Het ID van de poule
		/// </summary>
		public int PouleID
		{
			get; internal set;
		}

		/// <summary>
		/// De naam van de poule
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
		/// De discipline van de poule
		/// </summary>
		public Disciplines Discipline
		{
			get
			{
				return _discipline;
			}
			set
			{
				changed = true;
				_discipline = value;
			}
		}

		/// <summary>
		/// Het niveau van de poule
		/// </summary>
		public string Niveau
		{
			get{ return _niveau;}
			set
			{
				changed = true;
				_niveau = value;
			}
		}

		/// <summary>
		/// De ronde van de poule
		/// </summary>
		public int Round
		{
			get;
			internal set;
		}

		/// <summary>
		/// Is de poule gepland
		/// </summary>
		public bool IsPlanned
		{
			get;
			internal set;
		}

		/// <summary>
		/// Draait de poule op dit moment
		/// </summary>
		public bool IsRunning
		{
			get;
			internal set;
		}

		/// <summary>
		/// Opmerkingen bij de poule
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

		internal override void UpdateData(Poule newdata)
		{
			Comment = newdata.Comment;
			Discipline = newdata.Discipline;
			Niveau = newdata.Niveau;
			Name = newdata.Name;
			changed = false;
		}

		/// <summary>
		/// Een XML representatie van de poule
		/// </summary>
		public XElement Xml
		{
			set
			{
				PouleID = value.Attribute("id").Value.ToInt32();
				_name = value.Element("Name").Value;
				_discipline = (Disciplines)value.Element("Discipline").Value.ToInt64();
				_niveau = value.Element("Level").Value;
				Round = value.Element("Round").Value.ToInt32();
				IsPlanned = value.Element("IsPlanned").Value.ToBool();
				IsRunning = value.Element("IsRunning").Value.ToBool();
				Comment = value.Element("Comment").Value;
				changed = false;
			}
		}

		/// <summary>
		/// Zet de Poule om in een XML representatie
		/// </summary>
		/// <param name="element"> Het XElement waar de XML representatie in komt </param>
		public void ApplyXml(ref XElement element)
		{
			//De read/write attributen opslaan in de element
			element.Element("Name").Value = _name;
			element.Element("Discipline").Value = ((long)_discipline).ToString();
			element.Element("Level").Value = _niveau;
			element.Element("Comment").Value = _comment;
		}

		/// <summary>
		/// Een XML representatie van een "lege" poule
		/// </summary>
		public static XElement XmlDummy
		{
			get
			{
				return new XElement("Poule",
					new XAttribute("id", ""),
					new XElement("Name"),
					new XElement("Discipline"),
					new XElement("Level"),
					new XElement("Round"),
					new XElement("IsPlanned"),
					new XElement("IsRunning"),
					new XElement("Comment"),

					new XElement("Planning"),

					new XElement("Teams"),

					new XElement("Matches"));
			}
		}
	}
}
