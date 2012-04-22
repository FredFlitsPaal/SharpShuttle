using System;
using System.Linq;
using System.IO;
using System.Xml.Linq;
using Shared.Datastructures;
using System.Xml.XPath;
using Shared.Domain;

namespace Server.Database
{
	/// <summary>
	/// Representeert de XML-database. <para/>
	/// Deze class is een singelton
	/// </summary>
	public class Database
	{
		/// <summary>
		/// Versie van bestandsformat
		/// </summary>
		private static string Version = "1.0";

		private XDocument _Document;

		private AtomicCounter32 _NextUserID;
		private AtomicCounter32 _NextPlayerID;
		private AtomicCounter32 _NextTeamID;
		private AtomicCounter32 _NextPouleID;
		private AtomicCounter32 _NextMatchID;

		/// <summary>
		/// Bestandsnaam van het bestand dat geladen is
		/// </summary>
		public string Filename;

		private readonly static Database singleton = new Database();
		/// <summary>
		/// De instance van deze singelton class
		/// </summary>
		public static Database Instance
		{
			get { return singleton; }
		}

		/// <summary>
		/// Open een bestaande database
		/// </summary>
		/// <param name="Filename">De filename van de Database</param>
		/// <returns>True als het lukt, anders False</returns>
		public bool Open(string Filename){
			if (! File.Exists(Filename))
				return false;

			try
			{
				_Document = XDocument.Load(Filename);
				//Eerst controleren we de versie van de xmlstructuur

				if (_Document.Root.Attribute("version").Value != Version)
					throw new Exception("Wrong document verison");

				//Nu alle counter laden
				_NextUserID = new AtomicCounter32(
					_Document.Root.Element("Counters").Element("UserID").Attribute("value").Value.ToInt32());
				_NextPlayerID = new AtomicCounter32(
					_Document.Root.Element("Counters").Element("PlayerID").Attribute("value").Value.ToInt32());
				_NextTeamID = new AtomicCounter32(
					_Document.Root.Element("Counters").Element("TeamID").Attribute("value").Value.ToInt32());
				_NextMatchID = new AtomicCounter32(
					_Document.Root.Element("Counters").Element("MatchID").Attribute("value").Value.ToInt32());
				_NextPouleID = new AtomicCounter32(
					_Document.Root.Element("Counters").Element("PouleID").Attribute("value").Value.ToInt32());
				
			}
			catch
			{
				_Document = null;
				return false;
			}

			Opened = true;

			this.Filename = Filename;

			return true;
		}

		/// <summary>
		/// Sla alle data op op de hardeschijf
		/// </summary>
		public void Save()
		{
			try
			{
				XElement counters = Document.Root.Element("Counters");
				counters.Element("UserID").SetAttributeValue("value", _NextUserID.Value);
				counters.Element("PlayerID").SetAttributeValue("value", _NextPlayerID.Value);
				counters.Element("TeamID").SetAttributeValue("value", _NextTeamID.Value);
				counters.Element("PouleID").SetAttributeValue("value", _NextPouleID.Value);
				counters.Element("MatchID").SetAttributeValue("value", _NextMatchID.Value);

				Document.Save(Filename, SaveOptions.None);
			}
			catch (Exception e)
			{
				Shared.Logging.Logger.Write("Server.Database.Database.Save", e.ToString());
			}
		}

		/// <summary>
		/// Maak een nieuwe database en open hem gelijk
		/// </summary>
		/// <param name="Filename">Het pad van de Database</param>
		/// <returns>True als het lukt, anders False</returns>
		public bool New(string Filename)
		{
			try
			{
				if (File.Exists(Filename))
					File.Delete(Filename);

				this.XmlDummy.Save(Filename);

				return Open(Filename);
			}
			catch (Exception e)
			{
				Shared.Logging.Logger.Write("Database.New", e.ToString());
				return false;
			}
		}

		/// <summary>
		/// Het XML document
		/// </summary>
		public XDocument Document
		{
			get { return _Document; }
		}

		/// <summary>
		/// De counter om UserID's mee te genereren
		/// </summary>
		public AtomicCounter32 NextUserID
		{
			get { return _NextUserID; }
		}

		/// <summary>
		/// De counter om PlayerID's mee te genereren
		/// </summary>
		public AtomicCounter32 NextPlayerID
		{
			get { return _NextPlayerID; }
		}

		/// <summary>
		/// De counter om TeamID's mee te genereren
		/// </summary>
		public AtomicCounter32 NextTeamID
		{
			get { return _NextTeamID; }
		}

		/// <summary>
		/// De counter om PouleID's mee te genereren
		/// </summary>
		public AtomicCounter32 NextPouleID
		{
			get { return _NextPouleID; }
		}

		/// <summary>
		/// De counter om MatchID's mee te genereren
		/// </summary>
		public AtomicCounter32 NextMatchID
		{
			get { return _NextMatchID; }
		}

		/// <summary>
		/// Geeft aan of de database al open is of niet
		/// </summary>
		public bool Opened
		{
			get;
			private set;
		}

		/// <summary>
		/// Test of poule <paramref name="PouleID"/> bestaat
		/// </summary>
		/// <param name="PouleID">Het ID van de poule waarvan je wil weten of hij bestaat</param>
		/// <returns>True als er een poule waarvoor ID==<paramref name="PouleID"/></returns>
		public bool PouleExists(int PouleID)
		{
			return Document.Root.XPathSelectElements(string.Format("./Poules/Poule[@id='{0}']", PouleID)).Count() != 0;
		}
		/// <summary>
		/// Test of player <paramref name="PlayerID"/> bestaat
		/// </summary>
		/// <param name="PlayerID">Het ID van de player waarvan je wil weten of hij bestaat</param>
		/// <returns>True als er een player waarvoor ID==<paramref name="PlayerID"/></returns>
		public bool PlayerExists(int PlayerID)
		{
			return Document.Root.XPathSelectElements(string.Format("./Players/Player[@id='{0}']", PlayerID)).Count() != 0;
		}

		/// <summary>
		/// Haalt een speler op uit de database
		/// </summary>
		/// <param name="PlayerID">ID van de speler om op te halen</param>
		/// <returns>Een XElement waarin de informatie van de speler staat</returns>
		public XElement GetPlayer(int PlayerID)
		{
			if (!PlayerExists(PlayerID))
				return null;

			return Document.Root.XPathSelectElement(string.Format("./Players/Player[@id='{0}']", PlayerID));
		}

		/// <summary>
		/// Test of er een team bestaat waarvoor ID==<paramref name="TeamID"/>
		/// </summary>
		/// <param name="TeamID">Het ID om naar te zoeken</param>
		/// <returns>True als er een team bestaat met dit ID</returns>
		public bool TeamExists(int TeamID)
		{
			return Document.Root.XPathSelectElements(string.Format("./Poules/Poule/Teams/Team[@id='{0}']", TeamID)).Count() != 0;
		}

		/// <summary>
		/// Test of er een match bestaat waarvoor ID==<paramref name="MatchID"/>
		/// </summary>
		/// <param name="MatchID">Het ID om naar te zoeken</param>
		/// <returns>True als er een match bestaat met dit ID</returns>
		public bool MatchExists(int MatchID)
		{
			return Document.Root.XPathSelectElement(string.Format("./Poules/Poule/Matches/Match[@id='{0}']", MatchID)) != null;
		}

		/// <summary>
		/// Haalt een match op uit de database
		/// </summary>
		/// <param name="MatchID">ID van de match om op te halen</param>
		/// <returns>XElement waarin de data van de match staat</returns>
		public XElement GetMatch(int MatchID)
		{
			return Document.Root.XPathSelectElement(string.Format("./Poules/Poule/Matches/Match[@id='{0}']", MatchID));
		}

		/// <summary>
		/// Haalt een match op uit de database als Domain.Match
		/// </summary>
		/// <param name="MatchID">ID van de match om op te halen</param>
		/// <returns>null als de match niet gevonden was, anders de Match zelf</returns>
		public Match GetDomainMatch(int MatchID)
		{
			XElement match = GetMatch(MatchID);

			if (match == null) return null;

			return new Match
			{
				Xml = match,
				TeamA = GetDomainTeam(match.Element("TeamA").Attribute("id").Value.ToInt32()),
				TeamB = GetDomainTeam(match.Element("TeamB").Attribute("id").Value.ToInt32()),
			};
		}

		/// <summary>
		/// Haalt een team op uit de database
		/// </summary>
		/// <param name="TeamID">ID van de team om op te halen</param>
		/// <returns>XElement waarin de data van de team staat</returns>
		public XElement GetTeam(int TeamID)
		{
			return Document.Root.XPathSelectElement(string.Format("./Poules/Poule/Teams/Team[@id='{0}']", TeamID));
		}

		/// <summary>
		/// Haalt een poule op uit de database
		/// </summary>
		/// <param name="PouleID">ID van de poule om op te halen</param>
		/// <returns>XElement waarin de data van de poule staat</returns>
		public XElement GetPoule(int PouleID)
		{
			return Document.Root.XPathSelectElement(string.Format("./Poules/Poule[@id='{0}']", PouleID));
		}

		/// <summary>
		/// Haalt een team op uit de database als Domain.Team
		/// </summary>
		/// <param name="TeamID">ID van het team om op te halen</param>
		/// <returns>null als het team niet gevonden was, anders het Team zelf</returns>
		public Team GetDomainTeam(int TeamID)
		{
			XElement team = GetTeam(TeamID);
	
			if (team == null)
				return null;

			return new Team
			{
				Xml = team,
				Player1 = GetDomainPlayer(
					team.Element("Player1").Attribute("id").Value.ToInt32())
				,
				Player2 = GetDomainPlayer(
					team.Element("Player2").Attribute("id").Value.ToInt32()),
				Changed = false
			};
		}

		/// <summary>
		/// Haalt een speler op uit de database als Domain.Player
		/// </summary>
		/// <param name="PlayerID">ID van de speler om op te halen</param>
		/// <returns>null als de speler niet gevonden was, anders de Player zelf</returns>
		public Player GetDomainPlayer(int PlayerID)
		{
			XElement player = GetPlayer(PlayerID);
			if (player == null) return null;

			return new Player { Xml = player };				
		}

		/// <summary>
		/// Haalt de scores van een team op uit de database
		/// </summary>
		/// <param name="TeamID">ID van het team om de scores van op te halen</param>
		/// <returns>XElement waarin de scores van het team staat</returns>
		public XElement GetLadderTeam(int TeamID)
		{
			return Document.Root.XPathSelectElement(string.Format("./Poules/Poule/Teams/Team[@id='{0}']", TeamID));
		}

		/// <summary>
		/// Haalt de scores van een team op als Domain.LadderTeam
		/// </summary>
		/// <param name="TeamID">ID van het team on de scores van op te halen</param>
		/// <returns>null als het team niet bestaat, anders een LadderTeam met de scores</returns>
		public LadderTeam GetDomainLadderTeam(int TeamID)
		{
			XElement ladderteam = GetLadderTeam(TeamID);
			if (ladderteam == null) return null;

			return new LadderTeam
			{
				Xml = ladderteam,
				PouleID = ladderteam.Parent.Parent.Attribute("id").Value.ToInt32(),
				Team = new Team
				{
					Xml = ladderteam,
					Player1 = GetDomainPlayer(
						ladderteam.Element("Player1").Attribute("id").Value.ToInt32())
					,
					Player2 = GetDomainPlayer(
					ladderteam.Element("Player2").Attribute("id").Value.ToInt32())
				}
			};
		}

		/// <summary>
		/// Test of een speler in poules zit
		/// </summary>
		/// <param name="PlayerID">Het ID van de speler om te testen</param>
		/// <param name="PouleName">De poules waar de speler nog in zit</param>
		/// <returns>True als de speler nog in minstens 1 poule zit</returns>
		public bool PlayerHasReference(int PlayerID, out string PouleName)
		{
			PouleName = "";

			var poules = Document.Root.XPathSelectElements(
				string.Format("./Poules/Poule[Planning/Player[@id='{0}'] or Teams/Team/Player1[@id='{0}'] or Teams/Team/Player2[@id='{0}']]", PlayerID));
			if (poules.Count() == 0)
				return false;

			foreach (var poule in poules)
			{
				PouleName += poule.Element("Name").Value + ", ";
			}

			PouleName = PouleName.Substring(0, PouleName.Length - 2);
			return true;
		}

		/// <summary>
		/// Zorgt ervoor dat openstaande wedstrijden in de huidige ronde van de poule inactief worden
		/// zodat ze niet meer met de ladder mee doen.
		/// </summary>
		/// <param name="poule">id van de poule</param>
		/// <param name="TeamID">id van het team dat inactief wordt</param>
		public void SetPouleTeamInOperative(ref XElement poule, int TeamID)
		{
			//ga alle wedstrijden uit de huidige ronde bekijken
			int round = poule.Element("Round").Value.ToInt32();

			string query = string.Format("./Matches/Match[@round='{0}' and (TeamA[@id='{1}'] or TeamB[@id='{1}'])]",
											round, TeamID);

			var matches = poule.XPathSelectElements(query);

			//Nu elke wedstrijd die aan onze criterium voldoet disablen
			foreach (var match in matches)
			{
				match.SetAttributeValue("disabled", true.ToXMLString());
			}
		}

		/// <summary>
		/// Maakt een nieuw XDocument aan met alleen standaard elementen
		/// </summary>
		private XDocument XmlDummy
		{
			get
			{
				return new XDocument(
					new XElement("SharpShuttleDatabase",
						new XAttribute("version", Database.Version),

						new XComment("This element represents the settings of this tournament"),
						(new TournamentSettings()).Xml,

						new XComment("This element represents the internal counter of the database"),
						new XElement("Counters",
							new XElement("UserID", new XAttribute("value", "0")),
							new XElement("PlayerID", new XAttribute("value", "0")),
							new XElement("TeamID", new XAttribute("value", "0")),
							new XElement("MatchID", new XAttribute("value", "0")),
							new XElement("PouleID", new XAttribute("value", "0"))),

						new XComment("This elements represents all the available users in the tournament manager"),
						new XElement("Users",
							new XElement("User",
								new XAttribute("id", "1"),
								new XElement("Name", "Beheerder"),
								new XElement("Role", "127"),
								new XElement("Info", "Deze gebruiker heeft volledige toegangsrechten")),
							new XElement("User",
								new XAttribute("id", "2"),
								new XElement("Name", "Score invoerder"),
								new XElement("Role", "80"),
								new XElement("Info", "Deze gebruiker heeft rechten om scores in te voeren.")),
							new XElement("User",
								new XAttribute("id", "3"),
								new XElement("Name", "Alleen lezen"),
								new XElement("Role", "64"),
								new XElement("Info", "Deze gebruiker heeft enkel rechten om informatie te bekijken."))),

						new XComment("This element represents all the players in the tournament"),
						new XElement("Players"),

						new XComment("This element represents all the poules that are played in the tournament including the teams, matches and the scores"),
						new XElement("Poules")));
			}
		}
	}
}
