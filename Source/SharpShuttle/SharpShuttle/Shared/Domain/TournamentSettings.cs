using System;
using System.Xml.Linq;
using Shared.Datastructures;

namespace Shared.Domain
{

	/// <summary>
	/// Toernooi instellingen
	/// </summary>
	[Serializable]
	public class TournamentSettings : Abstract
	{
		/// <summary>
		/// Het standaard aantal sets
		/// </summary>
		private const int default_sets = 3;
		/// <summary>
		/// Het standaard aantal velden
		/// </summary>
		private const int default_fields = 10;

		/// <summary>
		/// Het aantal sets
		/// </summary>
		private int _sets = 3;
		/// <summary>
		/// Het aantal velden
		/// </summary>
		private int _fields = 10;
		/// <summary>
		/// Het serienummer
		/// </summary>
		public Communication.Serials.SerialDefinition SerialNumber;

		/// <summary>
		/// Default constructor
		/// </summary>
		public TournamentSettings() { }

		/// <summary>
		/// Het aantal sets
		/// </summary>
		public int Sets
		{
			get { return _sets; }
			set
			{
				changed |= _sets != value;
				_sets = value; 
			}
		}

		/// <summary>
		/// Het aantal velden
		/// </summary>
		public int Fields
		{
			get { return _fields; }
			set
			{
				changed |= _fields != value;
				_fields = value;
			}
		}

		/// <summary>
		/// Een XML representatie van de toernooi instellingen
		/// </summary>
		public XElement Xml
		{
			set
			{
				//Controleer of we nog geen settings hebben
				if (value == null)
				{
					_fields = default_fields;
					_sets = default_sets;
					return;
				}

				_fields = value.Attribute("fields").Value.ToInt32();
				_sets = value.Attribute("sets").Value.ToInt32();
			}
			get
			{
				return new XElement("Settings",
					new XAttribute("fields", _fields.ToString()),
					new XAttribute("sets", _sets.ToString()));
			}
		}
	}
}
