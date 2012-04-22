using System;
using System.Data;
using System.Xml.Linq;
using Shared.Datastructures;

namespace Shared.Domain
{
	/// <summary>
	/// Een gebruiker
	/// </summary>
	[Serializable]
    public class User : Abstract
    {
		/// <summary>
		/// Naam van de gebruiker
		/// </summary>
		private string _name;
		/// <summary>
		/// Beschrijving van de gebruiker
		/// </summary>
		private string _info;
		/// <summary>
		/// Rollen die de gebruiker heeft
		/// </summary>
		private Roles _roles;
		/// <summary>
		/// Het ID van de gebruiker
		/// </summary>
		private long _userid;

		/// <summary>
		/// gebruikers rollen
		/// </summary>
		[Serializable, Flags]
		public enum Roles : long
		{
			/// <summary>
			/// Poulebeheer
			/// </summary>
			Poules		=  1,
			/// <summary>
			/// Spelerbeheer
			/// </summary>
			Players		=  2,
			/// <summary>
			/// Teamkoppeling
			/// </summary>
			Grouping	=  4,
			/// <summary>
			/// Ladderbeheer
			/// </summary>
			Ladder		=  8,
			/// <summary>
			/// Scores invoeren
			/// </summary>
			Scores		= 16,
			/// <summary>
			/// Veldbeheer
			/// </summary>
			Courts		= 32,
			/// <summary>
			/// Alleen maar leesrechten
			/// </summary>
			Reader		= 64
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public User() { }

		/// <summary>
		/// Maakt een gebruiker direct uit een database rij
		/// </summary>
		/// <param name="row"> de database rij</param>
		public User(DataRow row)
		{
			_userid = row.Field<long>("UserID");
			_name = row.Field<string>("Name");
			_info = row.Field<string>("Info");
		}

		/// <summary>
		/// Maakt een gebruiker met een ID en naam
		/// </summary>
		/// <param name="UserID"> ID van de gebruiker</param>
		/// <param name="Name"> Naam van de gebruiker</param>
        public User(long UserID, string Name)
        {
			_userid = UserID;
			_name = Name;
        }

		/// <summary>
		/// Maakt een gebruiker met id, naam en info
		/// </summary>
		/// <param name="UserID"> ID van de gebruiker </param>
		/// <param name="Name"> Naam van de gebruiker </param>
		/// <param name="Info"> Beschrijving van de gebruiker </param>
        public User(long UserID, string Name, string Info)
        {
            _userid = UserID;
            _name = Name;
            _info = Info;
			_roles = new Roles();
        }

		/// <summary>
		/// Maakt een gebruiker met id, naam, info en rollen
		/// </summary>
		/// <param name="UserID"> ID van de gebruiker </param>
		/// <param name="Name"> Naam van de gebruiker </param>
		/// <param name="Info"> Beschrijving van de gebruiker </param>
		/// <param name="Roles"> Rollen van de gebruiker </param>
		public User(long UserID, string Name, string Info, Roles Roles)
		{
			_userid = UserID;
			_name = Name;
			_info = Info;
			_roles = Roles;
		}

		/// <summary>
		/// ID van de gebruiker
		/// </summary>
        public long UserID
        {
			get { return _userid; }
			internal set { _userid = value; }
        }

		/// <summary>
		/// Naam van de gebruiker
		/// </summary>
        public string Name
        {
			get { return _name; }
			set { _name = value; }
        }

		/// <summary>
		/// Beschrijving van de gebruiker
		/// </summary>
        public string Info
        {
			get { return _info; }
			set { _info = value; }
        }

		/// <summary>
		/// Rollen van de gebruiker
		/// </summary>
		public Roles Role
		{
			get { return _roles; }
		}

		/// <summary>
		/// XML representatie van de gebruiker
		/// </summary>
		public XElement Xml
		{
			set
			{
				_userid = value.Attribute("id").Value.ToInt64();
				_name = value.Element("Name").Value;
				_roles = (Roles)value.Element("Role").Value.ToInt64();
				_info = value.Element("Info").Value;
			}
			get
			{
				return new XElement("User",
					new XAttribute("id",_userid.ToString()),
					new XElement("Name",_name),
					new XElement("Role", ((long)_roles).ToString()),
					new XElement("Info", _info));
			}
		}

    }
}
