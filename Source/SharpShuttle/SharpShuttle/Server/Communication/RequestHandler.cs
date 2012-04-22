using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using Shared.Communication.Exceptions;
using Shared.Communication.Messages;
using Shared.Communication.Messages.Requests;
using Shared.Communication.Messages.Responses;
using Shared.Communication.Serials;
using Shared.Datastructures;
using Shared.Domain;
using Shared.Logging;
using Shared.Extensions;

namespace Server.Communication
{
	/// <summary>
	/// Handelt requests af
	/// </summary>
	public class RequestHandler
	{
		/// <summary>
		/// Het database document
		/// </summary>
		private static XDocument Document
		{
			get { return Database.Database.Instance.Document; }
		}

		#region Request Operations

		/// <summary>
		/// Vult een GetAllUsers request in
		/// </summary>
		public static void GetAllUsers(long ClientID, RequestGetAllUsers request)
		{
			//Maak een nieuwe response aan
			ResponseGetAllUsers response = new ResponseGetAllUsers(request.MessageID);

			try
			{
				//Vul de gebruikers
				response.Users = (from usr in Document.Root.Element("Users").Elements("User")
								  select new User
								  {
									  Xml = usr
								  }).ToList();
			}
			//TODO Iets zinnigs in de catch zetten
			catch (Exception e)
			{
				Logger.Write("RequestHandler.GetAllUsers", e.ToString());
			}

			//Geef de Serial mee van de Users
			response.NewSerialDefinitions.Add(SerialCache.Instance.GetOrAdd(new SerialDefinition(SerialTypes.AllUsers)));

			//Zorg ervoor dat we nu de Reponse sturen
			Communication.Send(ClientID, response);
		}

		/// <summary>
		/// Vult een GetAllPoules request in
		/// </summary>
		public static void GetAllPoules(long ClientID, RequestGetAllPoules request)
		{

			try
			{
				//Maak een nieuwe response aan
				ResponseGetAllPoules response = new ResponseGetAllPoules(request.MessageID);

				//Vul de Poules
				response.Poules = (from poule in Document.Root.Element("Poules").Elements("Poule")
								   select new Poule()
								   {
									   Xml = poule
								   }).ToList();

				//Geef de Serial mee van de Poules
				response.NewSerialDefinitions.Add(SerialCache.Instance.GetOrAdd(new SerialDefinition(SerialTypes.AllPoules)));

				foreach (Poule p in response.Poules)
				{
					response.NewSerialDefinitions.Add(
						SerialCache.Instance.GetOrAdd(new SerialDefinition(SerialTypes.Poule, p.PouleID)));
				}

				//Zorg ervoor dat we nu de Reponse sturen
				Communication.Send(ClientID, response);
			}
			//TODO Iets zinnigs in de catch zetten
			catch (Exception e)
			{
				Logger.Write("RequestHandler.GetAllUsers", e.ToString());
			}
		}

		/// <summary>
		/// Vult een SetAllPoules request in
		/// </summary>
		public static void SetAllPoules(long ClientID, RequestSetAllPoules request)
		{
			//Maak een nieuwe response aan
			ResponseSetAllPoules response = new ResponseSetAllPoules(request.MessageID);

			//Maak een serialupdate message
			//NOTE msg wordt nooit gebruikt
			SerialUpdateMessage msg = new SerialUpdateMessage();

			//Eerst moeten we controleren of de data up to date is
			if (SerialCache.Instance.IsOutOfDate(request.Poules.SerialNumber))
			{
				//De data is nu up to date
				response.Exception = new DataOutOfDateException();

				Communication.Send(ClientID, response);
				return;
			}

			//All Poule changes
			SerialEventTypes et = SerialEventTypes.None;

			//Nu moeten we de hele lijst met veranderingen doorlopen
			foreach (var item in request.Poules.Adds)
			{
				//Maak een nieuwe Poule aan
				XElement poule = Poule.XmlDummy;
				poule.SetAttributeValue("id", Database.Database.Instance.NextPouleID.Increment());
				item.ApplyXml(ref poule);

				//Voeg de poule toe aan de database
				Document.Root.Element("Poules").Add(poule);

				//Zorg ervoor dat de item niet meer is veranderd
				item.Changed = false;

				//Zorg dat we updates opslaan
				et = et | SerialEventTypes.Added;
				response.SerialUpdateMessages.Add(
					new SerialUpdate(SerialCache.Instance.IncreasePoule(item.PouleID), SerialEventTypes.Added));
			}

			foreach (var item in request.Poules.Deletes)
			{
				//Verwijder de poule met de pouleid
				XElement poule = Database.Database.Instance.GetPoule(item.PouleID);
				if (poule != null)
				{
					poule.Remove();
					et = et | SerialEventTypes.Removed;
					response.SerialUpdateMessages.Add(
						new SerialUpdate(SerialCache.Instance.IncreasePoule(item.PouleID), SerialEventTypes.Removed));
				}
			}

			//Nu moeten we nog kijken welke elementen er zijn veranderd
			foreach (var item in request.Poules)
			{
				if (item.Changed)
				{
					XElement poule = Database.Database.Instance.GetPoule(item.PouleID);
					if (poule == null) continue;

					//Verander nu de data in de poule
					item.ApplyXml(ref poule);

					et = et | SerialEventTypes.Changed;
					response.SerialUpdateMessages.Add(
						new SerialUpdate(SerialCache.Instance.IncreasePoule(item.PouleID), SerialEventTypes.Changed));
				}
			}

			//Nu moeten we alle serienummers updaten
			response.SerialUpdateMessages.Add(
				new SerialUpdate(
					SerialCache.Instance.IncreaseVersion(new SerialDefinition(SerialTypes.AllPoules)),
					et));

			//Als we klaar zijn sturen we een leeg bericht terug,
			//zodat de client weet dat het gelukt is
			Communication.Send(ClientID, response);

			//Stuur nu een update naar alle ander clients
			Communication.SendAllExcept(ClientID, new SerialUpdateMessage(response.SerialUpdateMessages));

			//Sla de veranderingen op op de hardeschijf
			Database.Database.Instance.Save();
		}

		/// <summary>
		/// Vult een GetAllPlayers request in
		/// </summary>
		public static void GetAllPlayers(long ClientID, RequestGetAllPlayers request)
		{
			//Maak een nieuwe response aan
			ResponseGetAllPlayers response = new ResponseGetAllPlayers(request.MessageID);
			try
			{
				//Vul de players
				response.Players = (from player in Document.Root.Element("Players").Elements("Player")
									select new Player()
									{
										Xml = player
									}).ToList();
			}
			//TODO Iets zinnigs in de catch zetten
			catch (Exception e)
			{
				Logger.Write("RequestHandler.GetAllPlayers", e.ToString());
			}
			//Geef de Serial mee van de PLayers
			response.NewSerialDefinitions.Add(
				SerialCache.Instance.GetOrAdd(
					new SerialDefinition(SerialTypes.AllPlayers)));

			//Zorg ervoor dat we nu de Reponse sturen
			Communication.Send(ClientID, response);
		}

		/// <summary>
		/// Vult een SetAllPlayers request in
		/// </summary>
		public static void SetAllPlayers(long ClientID, RequestSetAllPlayers request)
		{
			//Maak een response aan
			ResponseSetAllPlayers response = new ResponseSetAllPlayers(request.MessageID);

			//Eerst kijken we of de data up to date is
			if (SerialCache.Instance.IsOutOfDate(request.Players.SerialNumber))
			{
				//Maak een error
				response.Exception = new DataOutOfDateException();
				Communication.Send(ClientID, response);
				return;
			}

			//Nu moeten we kijken of een player een reference heeft naar een poule
			//(dus of hij in een planning of in een team zit)
			string referenceerrors = "", poules;
			bool referenceerror = false;

			foreach (var item in request.Players.Deletes)
			{
				if (Database.Database.Instance.PlayerHasReference(item.PlayerID, out poules))
				{
					//Maak een fout aan
					referenceerrors += item.Name + ": " + poules + "\r\n";
					referenceerror = true;
				}
			}

			//Als er reference errors zijn dan geven we een fout terug
			if (referenceerror)
			{
				response.Exception = new DataReferenceException(referenceerrors);
				Communication.Send(ClientID, response);
				return;
			}

			//Nu gaan we alle veranderingen doorvoeren
			SerialEventTypes et = SerialEventTypes.None;

			//Nu moeten we de hele lijst met veranderingen doorlopen
			foreach (var item in request.Players.Adds)
			{
				//Maak een nieuwe Player aan
				XElement player = Player.XmlDummy;
				player.SetAttributeValue("id", Database.Database.Instance.NextPlayerID.Increment());
				item.ApplyXml(ref player);

				//Voeg de player toe aan de database
				Document.Root.Element("Players").Add(player);

				//Zorg ervoor dat de item niet meer is veranderd
				item.Changed = false;

				//Zorg dat we updates opslaan
				et = et | SerialEventTypes.Added;
			}

			foreach (var item in request.Players.Deletes)
			{
				//Verwijder de player met de playerid (references zijn al gecontroleer)
				XElement player = Database.Database.Instance.GetPlayer(item.PlayerID);
				if (player != null)
				{
					player.Remove();
					et = et | SerialEventTypes.Removed;
				}
			}

			//Nu moeten we nog kijken welke elementen er zijn veranderd
			foreach (var item in request.Players)
			{
				if (item.Changed)
				{
					XElement player = Database.Database.Instance.GetPlayer(item.PlayerID);
					if (player == null) continue;

					//Verander nu de data in de poule
					item.ApplyXml(ref player);

					et = et | SerialEventTypes.Changed;
				}
			}

			//Als er niks is veranderd hoeft er ook niet te worden geupdate
			if (et == SerialEventTypes.None)
			{
				//Stuur dan de lege response
				Communication.Send(ClientID, response);
				return;
			}

			//De nieuwe serialinvullen bij de changes
			response.SerialUpdateMessages.Add(
				new SerialUpdate(
					SerialCache.Instance.IncreaseVersion(
						new SerialDefinition(SerialTypes.AllPlayers)),
				et));

			//Zorg ervoor dat we nu de Reponse sturen
			Communication.Send(ClientID, response);

			//Zeg alle andere clients dat de data is geupdate
			Communication.SendAllExcept(ClientID, new SerialUpdateMessage(response.SerialUpdateMessages));

			//Sla de veranderingen op op de hardeschijf
			Database.Database.Instance.Save();
		}

		/// <summary>
		/// Vult een GetPoule request in
		/// </summary>
		public static void GetPoule(long ClientID, RequestGetPoule request)
		{
			//Maak een nieuwe response aan
			ResponseGetPoule response = new ResponseGetPoule(request.MessageID);

			//Eerst kijken of er wel een poule is met PouleID
			if (!Database.Database.Instance.PouleExists(request.PouleID))
			{
				//Als er geen poules zijn dan moeten we een fout teruggeven
				response.Exception = new DataDoesNotExistsException();

				Communication.Send(ClientID, response);
				return;
			}

			//Vul de poule
			Poule p = (from poule in Document.Root.Element("Poules").Elements("Poule")
					   where poule.Attribute("id") != null &&
							 poule.Attribute("id").Value == request.PouleID.ToString()
					   select new Poule { Xml = poule }).First();

			response.Poule = p;

			//Geef de Serial mee van de Poule
			response.NewSerialDefinitions.Add(
				SerialCache.Instance.GetOrAdd(
					new SerialDefinition(SerialTypes.Poule, response.Poule.PouleID)));

			//Zorg ervoor dat we nu de Reponse sturen
			Communication.Send(ClientID, response);
		}

		/// <summary>
		/// Vult een SetPoule request in
		/// </summary>
		public static void SetPoule(long ClientID, RequestSetPoule request)
		{
			//Maak een nieuwe response aan
			ResponseSetPoule response = new ResponseSetPoule(request.MessageID);

			//Eerst kijken of de poule wel bestaat
			if (!Database.Database.Instance.PouleExists(request.PouleID))
			{
				//Als er geen poules zijn dan moeten we een fout teruggeven
				response.Exception = new DataDoesNotExistsException();

				Communication.Send(ClientID, response);
				return;
			}

			//Nu moeten we kijken of de data up to date is door het serienummer te checken
			if (SerialCache.Instance.IsOutOfDate(request.Poule.SerialNumber))
			{
				//De data is nu up to date
				response.Exception = new DataOutOfDateException();

				Communication.Send(ClientID, response);
				return;
			}

			//Als de data niet is veranderd dan hoeven we ook niet te updaten
			if (!request.Poule.Changed)
			{
				Communication.Send(ClientID, response);
				return;
			}

			//Nu moeten we de Poule ophalen en dan zijn data 'zetten'
			XElement poule = Database.Database.Instance.GetPoule(request.PouleID);
			request.Poule.ApplyXml(ref poule);

			//Nu moeten we de serial van de Poule updaten
			response.SerialUpdateMessages.Add(new SerialUpdate(
					SerialCache.Instance.IncreasePoule(request.PouleID),
					SerialEventTypes.Changed));

			//Alle poules zijn dus ook veranderd
			response.SerialUpdateMessages.Add(new SerialUpdate(
					SerialCache.Instance.IncreaseVersion(new SerialDefinition(SerialTypes.AllPoules)),
					SerialEventTypes.Changed));

			//Zorg ervoor dat we nu de Reponse sturen
			Communication.Send(ClientID, response);

			//Stuur nu alle andere clients een bericht van de updates
			Communication.SendAllExcept(ClientID, new SerialUpdateMessage(response.SerialUpdateMessages));

			//Sla de veranderingen op op de hardeschijf
			Database.Database.Instance.Save();
		}

		/// <summary>
		/// Vult een GetPoulePlanning request in
		/// </summary>
		public static void GetPoulePlanning(long ClientID, RequestGetPoulePlanning request)
		{
			//Maak een nieuwe response aan
			ResponseGetPoulePlanning response = new ResponseGetPoulePlanning(request.MessageID);

			//Eerst kijken of er wel een poule is met PouleID
			if (!Database.Database.Instance.PouleExists(request.PouleID))
			{
				//Als er geen poules zijn dan moeten we een fout teruggeven
				response.Exception = new DataDoesNotExistsException();

				Communication.Send(ClientID, response);
				return;
			}

			//Maak een XPath query
			string query = string.Format("./Poules/Poule[@id='{0}']/Planning/Player", request.PouleID);

			IEnumerable<long> players = (from p in Document.Root.XPathSelectElements(query)
										  select p.Attribute("id").Value.ToInt64());

			response.Players = (from pl in Document.Root.Element("Players").Elements("Player")
								where pl.Attribute("id") != null &&
								players.Contains(pl.Attribute("id").Value.ToInt64())
								select new Player
								{
									Xml = pl
								}).ToList();

			//Geef de Serial mee van de Planning
			response.NewSerialDefinitions.Add(
				SerialCache.Instance.GetOrAdd(
					new SerialDefinition(SerialTypes.PoulePlanning, request.PouleID)));

			//Zorg ervoor dat we nu de Reponse sturen
			Communication.Send(ClientID, response);
		}

		/// <summary>
		/// Vult een SetPoulePlanning request in
		/// </summary>
		public static void SetPoulePlanning(long ClientID, RequestSetPoulePlanning request)
		{
			//Maak een response aan
			ResponseSetPoulePlanning response = new ResponseSetPoulePlanning(request.MessageID);

			//Eerst kijken of de poule wel bestaat
			if (!Database.Database.Instance.PouleExists(request.PouleID))
			{
				//Maak een error aan
				response.Exception = new DataDoesNotExistsException();
				Communication.Send(ClientID, response);
				return;
			}

			//Nu controleren of het serienummer uit de lijst klopt
			if (SerialCache.Instance.IsOutOfDate(request.Players.SerialNumber))
			{
				//Maak een error aan
				response.Exception = new DataOutOfDateException();
				Communication.Send(ClientID, response);
				return;
			}

			XElement players = new XElement("temp");

			//Eerst moeten we controleren of alle players in de playerlijst wel bestaan
			foreach (var player in request.Players)
			{
				if (!Database.Database.Instance.PlayerExists(player.PlayerID))
				{
					//Maak een fout aan en stuur de response terug
					response.Exception = new DataReferenceException();
					Communication.Send(ClientID, response);
					return;
				}

				players.Add(new XElement("Player",
					new XAttribute("id", player.PlayerID.ToString())));
			}

			//Als we hier zijn aangekomen kunnen we de planning veranderen
			XElement planning = Document.Root.XPathSelectElement(
				string.Format("./Poules/Poule[@id='{0}']/Planning", request.PouleID));

			planning.ReplaceNodes(players.Nodes());

			//Verhoog nu de Serialnummer voor deze poule
			response.SerialUpdateMessages.Add(
				new SerialUpdate(
					SerialCache.Instance.IncreaseVersion(
						new SerialDefinition(SerialTypes.PoulePlanning, request.PouleID))
					, SerialEventTypes.Changed));

			//Zorg ervoor dat we nu de Reponse sturen
			Communication.Send(ClientID, response);

			//Stuur alle andere clients een SerialUpdateMessage
			Communication.SendAllExcept(ClientID, new SerialUpdateMessage(response.SerialUpdateMessages));

			//Sla de veranderingen op op de hardeschijf
			Database.Database.Instance.Save();
		}

		/// <summary>
		/// Vult een GetPouleTeams request in
		/// </summary>
		public static void GetPouleTeams(long ClientID, RequestGetPouleTeams request)
		{
			//Maak een nieuwe response aan
			ResponseGetPouleTeams response = new ResponseGetPouleTeams(request.MessageID);

			//Kijk eerst of de poule bestaat
			if (!Database.Database.Instance.PouleExists(request.PouleID))
			{
				//Als er geen poules zijn dan moeten we een fout teruggeven
				response.Exception = new DataDoesNotExistsException();

				Communication.Send(ClientID, response);
				return;
			}

			//Vul de Teamlijst
			string query = string.Format("./Poules/Poule[@id='{0}']/Teams/Team", request.PouleID);
			response.Teams = (from t in Document.Root.XPathSelectElements(query)
							  select new Team
							  {
								  Xml = t,
								  Player1 = Database.Database.Instance.GetDomainPlayer(
										t.Element("Player1").Attribute("id").Value.ToInt32())
								  ,
								  Player2 = Database.Database.Instance.GetDomainPlayer(
										t.Element("Player2").Attribute("id").Value.ToInt32()),
								  Changed = false
							  }).ToList();

			//Geef de Serial mee van de Teams
			response.NewSerialDefinitions.Add(
				SerialCache.Instance.GetOrAdd(
					new SerialDefinition(SerialTypes.PouleTeams, request.PouleID)));

			//Zorg ervoor dat we nu de Reponse sturen
			Communication.Send(ClientID, response);
		}

		/// <summary>
		/// Vult een SetPouleTeams request in
		/// </summary>
		public static void SetPouleTeams(long ClientID, RequestSetPouleTeams request)
		{
			//Houd bij of er een wedstrijd in deze poule inoperative wordt
			bool InOperativeChanged = false;

			//Maak een nieuwe response aan
			ResponseSetPouleTeams response = new ResponseSetPouleTeams(request.MessageID);

			XElement poule = Database.Database.Instance.GetPoule(request.PouleID);

			//Controleer eerst of de poule wel bestaat
			if (poule == null)
			{
				//Maak een error aan
				response.Exception = new DataDoesNotExistsException();
				Communication.Send(ClientID, response);
				return;
			}

			//Kijk of de data up to date is
			if (SerialCache.Instance.IsOutOfDate(request.Teams.SerialNumber))
			{
				//Maak een error aan
				response.Exception = new DataOutOfDateException();
				Communication.Send(ClientID, response);
				return;
			}

			//Nu kijken wat de status is van de poule is,
			//als hij al aan het spelen is mogen er geen teams worden toegevoegd of verwijderd
			bool IsRunning = poule.Element("IsRunning").Value.ToBool();

			if (IsRunning && (request.Teams.Adds.Count > 0 || request.Teams.Deletes.Count > 0))
			{
				//Maak een error aan
				response.Exception = new PouleAlreadyRunningException();
				Communication.Send(ClientID, response);
				return;
			}

			//Nu gaan we alle data updaten
			SerialEventTypes et = SerialEventTypes.None;

			//Nu moeten we de hele lijst met veranderingen doorlopen
			foreach (var item in request.Teams.Adds)
			{
				//Maak een nieuwe Team aan
				XElement team = Team.XmlDummy;
				team.SetAttributeValue("id", Database.Database.Instance.NextTeamID.Increment());
				item.ApplyXml(ref team);

				//Voeg de team toe aan de database
				poule.Element("Teams").Add(team);

				//Zorg ervoor dat de item niet meer is veranderd
				item.Changed = false;

				//Zorg dat we updates opslaan
				et = et | SerialEventTypes.Added;
			}

			foreach (var item in request.Teams.Deletes)
			{
				//Verwijder de poule met de pouleid
				XElement team = Database.Database.Instance.GetTeam(item.TeamID);
				if (team != null)
				{
					team.Remove();
					et = et | SerialEventTypes.Removed;
				}
			}

			//Nu moeten we nog kijken welke elementen er zijn veranderd
			foreach (var item in request.Teams)
			{
				if (item.Changed)
				{
					XElement team = Database.Database.Instance.GetTeam(item.TeamID);
					if (team == null) continue;

					//Verander nu de data in de poule
					item.ApplyXml(ref team);

					et = et | SerialEventTypes.Changed;

					//Nu moeten we controleren of het team inactief is geworden om zo de wedstrijden te updaten
					if (item.InOperativeChanged)
					{
						InOperativeChanged = true;
						Database.Database.Instance.SetPouleTeamInOperative(ref poule, item.TeamID);
					}
				}
			}

			//Nu kijken of er iets is veranderd
			if (et == SerialEventTypes.None)
			{
				Communication.Send(ClientID, response);
				return;
			}

			//Als de poule al aan het draaien was moeten we de referenties in de matches ongeldig maken door het serienummer op de hogen
			if (IsRunning || InOperativeChanged)
			{
				response.SerialUpdateMessages.Add(
					new SerialUpdate(
						SerialCache.Instance.IncreaseVersion(new SerialDefinition(SerialTypes.PouleMatches, request.PouleID)),
						et));

				response.SerialUpdateMessages.Add(
					new SerialUpdate(
						SerialCache.Instance.IncreaseVersion(new SerialDefinition(SerialTypes.PouleHistoryMatches, request.PouleID)),
						et));

				response.SerialUpdateMessages.Add(
					new SerialUpdate(
						SerialCache.Instance.IncreaseVersion(new SerialDefinition(SerialTypes.AllMatches, request.PouleID)),
						et));

				response.SerialUpdateMessages.Add(
					new SerialUpdate(
						SerialCache.Instance.IncreaseVersion(new SerialDefinition(SerialTypes.AllHistoryMatches, request.PouleID)),
						et));

				//Elke wedstrijd uit deze poule moet ook worden geupdate
				string query = string.Format("./Poules/Poule[@id='{0}']/Matches/Match", request.PouleID);
				var Matches = Document.Root.XPathSelectElements(query);
				foreach (var Match in Matches)
				{
					//Verhoog de versienummer van elke match in deze poule
					response.SerialUpdateMessages.Add(
						new SerialUpdate(
							SerialCache.Instance.IncreaseVersion(new SerialDefinition(SerialTypes.Match, Match.Attribute("id").Value.ToInt32())),
							SerialEventTypes.Changed));
				}
			}

			response.SerialUpdateMessages.Add(
				new SerialUpdate(
					SerialCache.Instance.IncreaseVersion(new SerialDefinition(SerialTypes.PouleTeams, request.PouleID)),
					et));

			response.SerialUpdateMessages.Add(
				new SerialUpdate(
					SerialCache.Instance.IncreaseVersion(new SerialDefinition(SerialTypes.PouleLadder, request.PouleID)),
					et));

			//Zorg ervoor dat we nu de Reponse sturen
			Communication.Send(ClientID, response);

			//Zeg de andere clients dat de data is veranderd
			Communication.SendAllExcept(ClientID, new SerialUpdateMessage(response.SerialUpdateMessages));

			//Sla de veranderingen op op de hardeschijf
			Database.Database.Instance.Save();
		}

		/// <summary>
		/// Vult een GetPouleLadder request in
		/// </summary>
		public static void GetPouleLadder(long ClientID, RequestGetPouleLadder request)
		{
			//Maak een nieuwe response aan
			ResponseGetPouleLadder response = new ResponseGetPouleLadder(request.MessageID);

			//Kijk eerst of de poule bestaat
			if (!Database.Database.Instance.PouleExists(request.PouleID))
			{
				//Als er geen poules zijn dan moeten we een fout teruggeven
				response.Exception = new DataDoesNotExistsException();

				Communication.Send(ClientID, response);
				return;
			}

			//Vul de ladderlijst
			string query = string.Format("./Poules/Poule[@id='{0}']/Teams/Team", request.PouleID);
			response.Ladder = (from t in Document.Root.XPathSelectElements(query)
							   select new LadderTeam
							   {
								   Xml = t,
								   PouleID = request.PouleID,
								   Team = new Team
								   {
									   Xml = t,
									   Player1 = Database.Database.Instance.GetDomainPlayer(
										   t.Element("Player1").Attribute("id").Value.ToInt32())
									   ,
									   Player2 = Database.Database.Instance.GetDomainPlayer(
										   t.Element("Player2").Attribute("id").Value.ToInt32())
								   }
							   }).ToList();

			//Sorteer nu op volgorde
			response.Ladder.Sort(new Shared.Sorters.LadderTeamSorter());

			//Nu de ranking invullen in de ladder
			int rank = 0;
			foreach (var team in response.Ladder)
			{
				rank++;
				team.Rank = rank;
			}

			//Geef de Serial mee van de Ladder
			response.NewSerialDefinitions.Add(
				SerialCache.Instance.GetOrAdd(
					new SerialDefinition(SerialTypes.PouleLadder, request.PouleID)));

			//Zorg ervoor dat we nu de Reponse sturen
			Communication.Send(ClientID, response);
		}

		/// <summary>
		/// Vult een GetPouleMatches request in
		/// </summary>
		public static void GetPouleMatches(long ClientID, RequestGetPouleMatches request)
		{
			//Maak een nieuwe response aan
			ResponseGetPouleMatches response = new ResponseGetPouleMatches(request.MessageID);

			//Kijk eerst of de poule bestaat
			if (!Database.Database.Instance.PouleExists(request.PouleID))
			{
				//Als er geen poules zijn dan moeten we een fout teruggeven
				response.Exception = new DataDoesNotExistsException();

				Communication.Send(ClientID, response);
				return;
			}


			//Vul de matchlijst
			string query = string.Format("./Poules/Poule[@id='{0}']/Matches/Match", request.PouleID);
			response.Matches = (from t in Document.Root.XPathSelectElements(query)
								where t.Attribute("cached") != null & !t.Attribute("cached").Value.ToBool()
								select new Match
								{
									Xml = t,
									TeamA = Database.Database.Instance.GetDomainTeam(t.Element("TeamA").Attribute("id").Value.ToInt32()),
									TeamB = Database.Database.Instance.GetDomainTeam(t.Element("TeamB").Attribute("id").Value.ToInt32()),
								}).ToList();

			//Geef de Serial mee van de Matches
			response.NewSerialDefinitions.Add(
				SerialCache.Instance.GetOrAdd(
					new SerialDefinition(SerialTypes.PouleMatches, request.PouleID)));

			foreach (Match p in response.Matches)
			{
				response.NewSerialDefinitions.Add(
					SerialCache.Instance.GetOrAdd(new SerialDefinition(SerialTypes.Match, p.MatchID)));
			}

			//Zorg ervoor dat we nu de Reponse sturen
			Communication.Send(ClientID, response);
		}

		/// <summary>
		/// Vult een SetPouleNextRound request in
		/// </summary>
		public static void SetPouleNextRound(long ClientID, RequestSetPouleNextRound request)
		{
			//Maak een response aan
			ResponseSetPouleNextRound response = new ResponseSetPouleNextRound(request.MessageID);

			//Eerst kijken of de poule wel bestaat
			XElement poule = Database.Database.Instance.GetPoule(request.PouleID);

			if (poule == null)
			{
				//Maak een error aan
				response.Exception = new DataDoesNotExistsException();
				Communication.Send(ClientID, response);
				return;
			}

			//Nu kijken of alle wedstrijden in de ronde zijn gespeeld
			int round = poule.Element("Round").Value.ToInt32();

			string query = "./Poules/Poule[@id='{0}']/Matches/Match[@round='{1}']";
			var oldmatches = Document.Root.XPathSelectElements(string.Format(query, request.PouleID, round));

			//Nu opzoeken of er 1 is die niet gespeeld is
			foreach (var item in oldmatches)
			{
				if (!item.TestAttribute("played",true.ToXMLString()) &&
					!item.TestAttribute("disabled",true.ToXMLString()))
				{
					//Maak een error aan
					response.Exception = new PouleMatchScoreNotCompleteException();
					Communication.Send(ClientID, response);
					return;
				}
			}

			//Als alles gespeeld is moeten we alle matches in die ronde archiveren
			foreach (var item in oldmatches)
			{
				//Archiveer hem
				item.SetAttributeValue("cached", true.ToXMLString());

				//Update de Serialnumber
				response.SerialUpdateMessages.Add(
					new SerialUpdate(
						SerialCache.Instance.IncreaseVersion(
							new SerialDefinition(SerialTypes.Match, item.Attribute("id").Value.ToInt32())),
						SerialEventTypes.Changed));
			}

			//Nu gaan we alle nieuwe matches invullen
			XElement newmatches = new XElement("temp");

			//De volgende ronde gaat in
			round++;

			//De teller van matchnumber
			short matchnumber = 0;

			foreach (var item in request.Matches)
			{
				//Verhoog de matchnumber teller
				matchnumber++;

				//Maak een nieuw match element
				XElement match = Match.XmlDummy;
				int matchid = Database.Database.Instance.NextMatchID.Increment();
				match.SetAttributeValue("id", matchid.ToString());
				match.SetAttributeValue("round", round.ToString());
				match.SetAttributeValue("matchnumber", matchnumber.ToString());
				match.Element("TeamA").SetAttributeValue("id", item.TeamA.TeamID);
				match.Element("TeamB").SetAttributeValue("id", item.TeamB.TeamID);

				//Nu moeten we controleren of een van de teams inoperatief is
				//Als dat namelijk zo is zetten we de disabled op true zodat de wedstrijd niet meetelt.
				if (item.TeamA.IsInOperative || item.TeamB.IsInOperative)
				{
					match.SetAttributeValue("disabled", true.ToXMLString());
				}


				//Update de serial van de nieuwe match
				response.SerialUpdateMessages.Add(
					new SerialUpdate(
						SerialCache.Instance.IncreaseVersion(
							new SerialDefinition(SerialTypes.Match, matchid)),
						SerialEventTypes.Added));

				//Voeg de nieuwe match toe
				newmatches.Add(match);
			}

			//Vul nu de nieuwe lijst met matches in
			poule.Element("Matches").Add(newmatches.Elements());

			//Nu kijken of we de poule ook moeten updaten,
			//Als de poule nog niet aan het draaien was, dan doet hij het nu wel
			bool IsRunning = poule.Element("IsRunning").Value.ToBool();
			if (!IsRunning)
			{
				poule.Element("IsRunning").Value = true.ToXMLString();
			}

			//Rondenummer invullen van de poule:
			poule.SetElementValue("Round", round.ToString());

			//Als alles klaar is zijn de wedstrijden geupdate dus verhoog de andere serials

			//Verander het Poule element (ronde is veranderd)
			response.SerialUpdateMessages.Add(
				new SerialUpdate(
					SerialCache.Instance.IncreaseVersion(
						new SerialDefinition(SerialTypes.Poule, request.PouleID)),
					SerialEventTypes.Changed));

			response.SerialUpdateMessages.Add(
				new SerialUpdate(
					SerialCache.Instance.IncreaseVersion(
						new SerialDefinition(SerialTypes.AllPoules)),
					SerialEventTypes.Changed));

			//Verhoog de poulematches
			response.SerialUpdateMessages.Add(
				new SerialUpdate(
					SerialCache.Instance.IncreaseVersion(
						new SerialDefinition(SerialTypes.PouleMatches, request.PouleID)),
				SerialEventTypes.Changed));

			//verhoog de poulehistorymatches
			response.SerialUpdateMessages.Add(
				new SerialUpdate(
					SerialCache.Instance.IncreaseVersion(
						new SerialDefinition(SerialTypes.PouleHistoryMatches, request.PouleID)),
					SerialEventTypes.Changed));

			//verhoog de allmatches
			response.SerialUpdateMessages.Add(
				new SerialUpdate(
					SerialCache.Instance.IncreaseVersion(
						new SerialDefinition(SerialTypes.AllMatches)),
					SerialEventTypes.Changed));

			//verhoog de allhistorymatches
			response.SerialUpdateMessages.Add(
				new SerialUpdate(
					SerialCache.Instance.IncreaseVersion(
						new SerialDefinition(SerialTypes.AllHistoryMatches)),
					SerialEventTypes.Changed));

			//Zorg ervoor dat we nu de Reponse sturen
			Communication.Send(ClientID, response);

			//Stuur de update naar de andere clients
			Communication.SendAllExcept(ClientID, new SerialUpdateMessage(response.SerialUpdateMessages));

			//Sla de veranderingen op op de hardeschijf
			Database.Database.Instance.Save();
		}

		/// <summary>
		/// Vult een GetAllMatches request in
		/// </summary>
		public static void GetAllMatches(long ClientID, RequestGetAllMatches request)
		{
			//Maak een nieuwe response aan
			ResponseGetAllMatches response = new ResponseGetAllMatches(request.MessageID);

			//Vul de matchlijst
			string query = "./Poules/Poule/Matches/Match";
			response.Matches = (from t in Document.Root.XPathSelectElements(query)
								where t.Attribute("cached") != null &&
								!t.Attribute("cached").Value.ToBool()
								select new Match
								{
									Xml = t,
									TeamA = Database.Database.Instance.GetDomainTeam(t.Element("TeamA").Attribute("id").Value.ToInt32()),
									TeamB = Database.Database.Instance.GetDomainTeam(t.Element("TeamB").Attribute("id").Value.ToInt32()),
								}).ToList();

			//Geef de Serial mee van de Matches
			response.NewSerialDefinitions.Add(
				SerialCache.Instance.GetOrAdd(
					new SerialDefinition(SerialTypes.AllMatches)));

			foreach (Match p in response.Matches)
			{
				response.NewSerialDefinitions.Add(
					SerialCache.Instance.GetOrAdd(new SerialDefinition(SerialTypes.Match, p.MatchID)));
			}

			//Zorg ervoor dat we nu de Reponse sturen
			Communication.Send(ClientID, response);
		}

		/// <summary>
		/// Vult een GetAllHistoryMatches request in
		/// </summary>
		public static void GetAllHistoryMatches(long ClientID, RequestGetAllHistoryMatches request)
		{
			//Maak een nieuwe response aan
			ResponseGetAllHistoryMatches response = new ResponseGetAllHistoryMatches(request.MessageID);

			//Vul de matchlijst
			string query = "./Poules/Poule/Matches/Match";
			response.Matches = (from t in Document.Root.XPathSelectElements(query)
								where t.Attribute("cached") != null &&
								t.Attribute("cached").Value.ToBool()
								select new Match
								{
									Xml = t,
									TeamA = Database.Database.Instance.GetDomainTeam(t.Element("TeamA").Attribute("id").Value.ToInt32()),
									TeamB = Database.Database.Instance.GetDomainTeam(t.Element("TeamB").Attribute("id").Value.ToInt32()),
								}).ToList();

			//Geef de Serial mee van de Matches
			response.NewSerialDefinitions.Add(
				SerialCache.Instance.GetOrAdd(
					new SerialDefinition(SerialTypes.AllHistoryMatches)));


			foreach (Match p in response.Matches)
			{
				response.NewSerialDefinitions.Add(
					SerialCache.Instance.GetOrAdd(new SerialDefinition(SerialTypes.Match, p.MatchID)));
			}

			//Zorg ervoor dat we nu de Reponse sturen
			Communication.Send(ClientID, response);
		}

		/// <summary>
		/// Vult een GetPouleHistoryMatches request in
		/// </summary>
		public static void GetPouleHistoryMatches(long ClientID, RequestGetPouleHistoryMatches request)
		{
			//Maak een nieuwe response aan
			ResponseGetPouleHistoryMatches response = new ResponseGetPouleHistoryMatches(request.MessageID);

			//Kijk eerst of de poule bestaat
			if (!Database.Database.Instance.PouleExists(request.PouleID))
			{
				//Als er geen poules zijn dan moeten we een fout teruggeven
				response.Exception = new DataDoesNotExistsException();

				Communication.Send(ClientID, response);
				return;
			}

			//Vul de matchlijst
			string query = string.Format("./Poules/Poule[@id='{0}']/Matches/Match", request.PouleID);
			response.Matches = (from t in Document.Root.XPathSelectElements(query)
								where t.Attribute("cached") != null  && t.Attribute("cached").Value.ToBool()
								select new Match
								{
									Xml = t,
									TeamA = Database.Database.Instance.GetDomainTeam(t.Element("TeamA").Attribute("id").Value.ToInt32()),
									TeamB = Database.Database.Instance.GetDomainTeam(t.Element("TeamB").Attribute("id").Value.ToInt32()),
								}).ToList();

			//Geef de Serial mee van de Matches
			response.NewSerialDefinitions.Add(
				SerialCache.Instance.GetOrAdd(
					new SerialDefinition(SerialTypes.PouleHistoryMatches, request.PouleID)));

			foreach (Match p in response.Matches)
			{
				response.NewSerialDefinitions.Add(
					SerialCache.Instance.GetOrAdd(new SerialDefinition(SerialTypes.Match, p.MatchID)));
			}

			//Zorg ervoor dat we nu de Reponse sturen
			Communication.Send(ClientID, response);
		}

		/// <summary>
		/// Vult een GetMatch request in
		/// </summary>
		public static void GetMatch(long ClientID, RequestGetMatch request)
		{
			//Maak een nieuwe response aan
			ResponseGetMatch response = new ResponseGetMatch(request.MessageID);

			//Controleer eerst of we een match hebben
			XElement match = Database.Database.Instance.GetMatch(request.MatchID);
			if (match == null)
			{
				//Geef een fout terug aan de client
				response.Exception = new DataDoesNotExistsException();
				Communication.Send(ClientID, response);
				return;
			}

			//Vul de match in
			response.Match = new Match
			{
				Xml = match,
				TeamA = Database.Database.Instance.GetDomainTeam(match.Element("TeamA").Attribute("id").Value.ToInt32()),
				TeamB = Database.Database.Instance.GetDomainTeam(match.Element("TeamB").Attribute("id").Value.ToInt32()),
			};

			response.NewSerialDefinitions.Add(
				SerialCache.Instance.GetOrAdd(new SerialDefinition(SerialTypes.Match, request.MatchID)));

			//Zorg ervoor dat we nu de Reponse sturen
			Communication.Send(ClientID, response);
		}

		/// <summary>
		/// Vult een SetMatch request in
		/// </summary>
		public static void SetMatch(long ClientID, RequestSetMatch request)
		{
			//Maak een nieuwe response aan
			ResponseSetMatch response = new ResponseSetMatch(request.MessageID);

			//Als het up to date is moeten we het verschil tussen de matches uitrekenen
			Match oldmatch = Database.Database.Instance.GetDomainMatch(request.match.MatchID);

			//Kijk eerst of de match wel bestaat
			if (oldmatch == null)
			{
				//Maak een error aan
				response.Exception = new DataDoesNotExistsException();
				Communication.Send(ClientID, response);
				return;
			}

			//Controleer eerst of het serienummer up to date is
			if (SerialCache.Instance.IsOutOfDate(request.match.SerialNumber))
			{
				//Maak een error aan
				response.Exception = new DataOutOfDateException();
				Communication.Send(ClientID, response);
				return;
			}

			//Als er niks is veranderd een leeg bericht meesturen
			if (!request.match.Changed)
			{
				//Stuur een leeg bericht
				Communication.Send(ClientID, response);
				return;
			}


			//Nu moeten we kijken of de score is veranderd, dan moeten we namelijk ook de ladder updaten
			if (request.match.ScoreChanged)
			{
				//Nu moeten we beide ladderteams ophalen
				LadderTeam teama = Database.Database.Instance.GetDomainLadderTeam(oldmatch.TeamA.TeamID);
				LadderTeam teamb = Database.Database.Instance.GetDomainLadderTeam(oldmatch.TeamB.TeamID);

				//Als een van de teams niet bestaat geef dan een error
				if (teama == null || teamb == null)
				{
					//Maak een error
					response.Exception = new DataDoesNotExistsException();
					Communication.Send(ClientID, response);
					return;
				}

				//Nu moeten we de huidige wedstrijd ongedaan maken zodat we de nieuwe scores kunnen invullen
				if (!oldmatch.UndoLadder(ref teama, ref teamb))
				{
					//Maak een error aan
					response.Exception = new DataReferenceException();
					Communication.Send(ClientID, response);
					return;
				}

				//Nu gaan we de nieuwe scores weer invullen
				if (!request.match.UpdateLadder(ref teama, ref teamb))
				{
					//Maak een error aan
					response.Exception = new DataReferenceException();
					Communication.Send(ClientID, response);
					return;
				}

				//Anders is de ladder wel geupdate en kunnen we de nieuwe gegevens wegschrijven
				XElement ta = Database.Database.Instance.GetLadderTeam(teama.Team.TeamID);
				XElement tb = Database.Database.Instance.GetLadderTeam(teamb.Team.TeamID);

				teama.ApplyXml(ref ta);
				teamb.ApplyXml(ref tb);

				XElement m = Database.Database.Instance.GetMatch(oldmatch.MatchID);
				request.match.ApplyXml(ref m);

				//Vul extra serienummers in
				response.SerialUpdateMessages.Add(
					new SerialUpdate(
						SerialCache.Instance.IncreaseVersion(
							new SerialDefinition(SerialTypes.PouleLadder, request.match.PouleID))
						, SerialEventTypes.Changed));
			}
			else
			{
				XElement m = Database.Database.Instance.GetMatch(request.match.MatchID);
				request.match.ApplyXml(ref m);
			}

			//Vul een nieuw serienummer in
			response.SerialUpdateMessages.Add(
				new SerialUpdate(
					SerialCache.Instance.IncreaseVersion(
						new SerialDefinition(SerialTypes.Match, request.match.MatchID))
					, SerialEventTypes.Changed));

			response.SerialUpdateMessages.Add(
				new SerialUpdate(
					SerialCache.Instance.IncreaseVersion(
						new SerialDefinition(SerialTypes.PouleMatches, request.match.PouleID))
					, SerialEventTypes.Changed));

			response.SerialUpdateMessages.Add(
				new SerialUpdate(
					SerialCache.Instance.IncreaseVersion(
						new SerialDefinition(SerialTypes.AllMatches))
					, SerialEventTypes.Changed));


			//Zorg ervoor dat we nu de Reponse sturen
			Communication.Send(ClientID, response);

			//Stuur nu ook een update bericht naar de andere clients
			Communication.SendAllExcept(ClientID, new SerialUpdateMessage(response.SerialUpdateMessages));

			//Sla de veranderingen op op de hardeschijf
			Database.Database.Instance.Save();
		}

		/// <summary>
		/// Vult een PouleRemoveRound request in
		/// </summary>
		public static void PouleRemoveRound(long ClientID, RequestPouleRemoveRound request)
		{
			//Maak een response aan
			ResponsePouleRemoveRound response = new ResponsePouleRemoveRound(request.MessageID);

			//Eerst kijken of de poule wel bestaat
			XElement poule = Database.Database.Instance.GetPoule(request.PouleID);

			if (poule == null)
			{
				//Maak een error aan
				response.Exception = new DataDoesNotExistsException();
				Communication.Send(ClientID, response);
				return;
			}

			//Hij bestaan,
			//nu kijken of er wedstrijden zijn die al zijn gespeeld
			string query = string.Format("./Poules/Poule[@id='{0}']/Matches/Match[@round='{1}']",
										request.PouleID, poule.Element("Round").Value.ToInt32());

			var matches = Document.Root.XPathSelectElements(query);

			//Als er geen wedstrijden zijn, geen nood aan de man
			if (matches.Count() == 0)
			{
				//Verstuur een leeg bericht
				Communication.Send(ClientID, response);
				return;
			}

			//Voor elke match kijken of er een is die gespeeld is
			foreach (var match in matches)
			{
				if (match.TestAttribute("played", true.ToXMLString()))
				{
					//Maak een fout aan
					response.SerialUpdateMessages.Clear();
					response.Exception = new PouleAlreadyRunningException();
					Communication.Send(ClientID, response);
					return;
				}

				//Zeg dat de wedstrijd is verwijderd
				response.SerialUpdateMessages.Add(
					new SerialUpdate(
						SerialCache.Instance.IncreaseVersion(
							new SerialDefinition(SerialTypes.Match, match.Attribute("id").Value.ToInt32())),
					SerialEventTypes.Removed));
			}

			//Als er daadwerkelijk wedstrijden zijn, moeten we het rondenummer verlagen
			if (matches.Count() > 0)
			{
				int round = poule.Element("Round").Value.ToInt32();
				round--;

				poule.SetElementValue("Round", round);

				//Als het rondenummer 0 is dan is de poule niet aan het spelen
				if (round <= 0)
				{
					round = 0;
					poule.SetElementValue("IsRunning", false.ToXMLString());
				}

				response.SerialUpdateMessages.Add(
					new SerialUpdate(
						SerialCache.Instance.IncreaseVersion(
							new SerialDefinition(SerialTypes.Poule, request.PouleID)),
						SerialEventTypes.Changed));

				response.SerialUpdateMessages.Add(
				new SerialUpdate(
						SerialCache.Instance.IncreaseVersion(
							new SerialDefinition(SerialTypes.AllPoules)),
						SerialEventTypes.Changed));

				response.SerialUpdateMessages.Add(
					new SerialUpdate(
						SerialCache.Instance.IncreaseVersion(
							new SerialDefinition(SerialTypes.PouleMatches, request.PouleID)),
						SerialEventTypes.Changed));

				response.SerialUpdateMessages.Add(
					new SerialUpdate(
						SerialCache.Instance.IncreaseVersion(
							new SerialDefinition(SerialTypes.AllMatches)),
						SerialEventTypes.Changed));
			}

			//Verwijder nu alle elementen
			matches.Remove();

			//Zorg ervoor dat we nu de Reponse sturen
			Communication.Send(ClientID, response);

			//Stuur de update naar de andere clients
			Communication.SendAllExcept(ClientID, new SerialUpdateMessage(response.SerialUpdateMessages));

			//Sla de veranderingen op op de hardeschijf
			Database.Database.Instance.Save();
		}

		/// <summary>
		/// Vult een PouleFinishRound request in
		/// </summary>
		public static void PouleFinishRound(long ClientID, RequestPouleFinishRound request)
		{
			//Maak een response aan
			ResponsePouleFinishRound response = new ResponsePouleFinishRound(request.MessageID);

			//Eerst kijken of de poule wel bestaat
			XElement poule = Database.Database.Instance.GetPoule(request.PouleID);

			if (poule == null)
			{
				//Maak een error aan
				response.Exception = new DataDoesNotExistsException();
				Communication.Send(ClientID, response);
				return;
			}

			//Nu kijken of alle wedstrijden in de ronde zijn gespeeld
			int round = poule.Element("Round").Value.ToInt32();

			string query = "./Poules/Poule[@id='{0}']/Matches/Match[@round='{1}']";
			var oldmatches = Document.Root.XPathSelectElements(string.Format(query, request.PouleID, round));

			//Nu opzoeken of er 1 is die niet gespeeld is
			foreach (var item in oldmatches)
			{
				//Als een van de wedstrijden in de huidige ronde niet gespeeld is of niet actiev is dan is het fout
				if (!item.TestAttribute("played", true.ToXMLString()) &&
					!item.TestAttribute("disabled", true.ToXMLString()))
				{
					//Maak een error aan
					response.Exception = new PouleMatchScoreNotCompleteException();
					Communication.Send(ClientID, response);
					return;
				}
			}

			//Als alles gespeeld is moeten we alle matches in die ronde archiveren
			foreach (var item in oldmatches)
			{
				//Archiveer hem
				item.SetAttributeValue("cached", true.ToXMLString());

				//Update de Serialnumber
				response.SerialUpdateMessages.Add(
					new SerialUpdate(
						SerialCache.Instance.IncreaseVersion(
							new SerialDefinition(SerialTypes.Match, item.Attribute("id").Value.ToInt32())),
						SerialEventTypes.Changed));
			}

			//Als alles klaar is zijn de wedstrijden geupdate dus verhoog de andere serials
			//Verhoog de poulematches
			response.SerialUpdateMessages.Add(
				new SerialUpdate(
					SerialCache.Instance.IncreaseVersion(
						new SerialDefinition(SerialTypes.PouleMatches, request.PouleID)),
				SerialEventTypes.Changed));

			response.SerialUpdateMessages.Add(
				new SerialUpdate(
					SerialCache.Instance.IncreaseVersion(
						new SerialDefinition(SerialTypes.AllPoules)),
					SerialEventTypes.Changed));

			//verhoog de poulehistorymatches
			response.SerialUpdateMessages.Add(
				new SerialUpdate(
					SerialCache.Instance.IncreaseVersion(
						new SerialDefinition(SerialTypes.PouleHistoryMatches, request.PouleID)),
					SerialEventTypes.Changed));

			//verhoog de allmatches
			response.SerialUpdateMessages.Add(
				new SerialUpdate(
					SerialCache.Instance.IncreaseVersion(
						new SerialDefinition(SerialTypes.AllMatches)),
					SerialEventTypes.Changed));

			//verhoog de allhistorymatches
			response.SerialUpdateMessages.Add(
				new SerialUpdate(
					SerialCache.Instance.IncreaseVersion(
						new SerialDefinition(SerialTypes.AllHistoryMatches)),
					SerialEventTypes.Changed));

			//Zorg ervoor dat we nu de Reponse sturen
			Communication.Send(ClientID, response);

			//Stuur de update naar de andere clients
			Communication.SendAllExcept(ClientID, new SerialUpdateMessage(response.SerialUpdateMessages));

			//Sla de veranderingen op op de hardeschijf
			Database.Database.Instance.Save();
		}

		/// <summary>
		/// Vult een GetSettings request in
		/// </summary>
		public static void GetSettings(long ClientID, RequestGetSettings request)
		{
			//Maak een nieuwe response aan
			ResponseGetSettings response = new ResponseGetSettings(request.MessageID);

			response.settings = new TournamentSettings
					{
						Xml = Document.Root.Element("Settings")
					};

			//Voeg het serienummer toe
			response.NewSerialDefinitions.Add(
				SerialCache.Instance.GetOrAdd(new SerialDefinition(SerialTypes.Settings)));

			//Zorg ervoor dat we nu de Reponse sturen
			Communication.Send(ClientID, response);
		}

		/// <summary>
		/// Vult een SetSettings request in
		/// </summary>
		public static void SetSettings(long ClientID, RequestSetSettings request)
		{
			//Maak een nieuwe response aan
			ResponseSetSettings response = new ResponseSetSettings(request.MessageID);

			//Eerst kijken we of de data up to date is
			if (SerialCache.Instance.IsOutOfDate(request.Settings.SerialNumber))
			{
				//Maak een error
				response.Exception = new DataOutOfDateException();
				Communication.Send(ClientID, response);
				return;
			}

			//Nu gaan we de gegevens in de gegevens in de database updaten
			XElement oldSettings = Document.Root.Element("Settings");
			if (oldSettings == null)
			{
				//Er zijn nog geen settings, dus nieuwe settings invoegen
				Document.Root.AddFirst(request.Settings.Xml);
			}
			else
			{
				//Als er wel al settings waren, moeten we het element updaten
				oldSettings.ReplaceWith(request.Settings.Xml);
			}

			//Nu even het serienummer updaten
			response.SerialUpdateMessages.Add(
				new SerialUpdate(
					SerialCache.Instance.IncreaseVersion(new SerialDefinition(SerialTypes.Settings)),
					SerialEventTypes.Changed
					));

			//Zorg ervoor dat we nu de Reponse sturen
			Communication.Send(ClientID, response);

			//Alle andere clients ook even een update sturen
			Communication.SendAllExcept(ClientID, new SerialUpdateMessage(response.SerialUpdateMessages));

			//Sla de veranderingen op op de hardeschijf
			Database.Database.Instance.Save();
		}

		#endregion
	}

}