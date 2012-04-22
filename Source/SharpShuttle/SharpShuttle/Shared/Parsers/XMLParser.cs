using System;
using System.Xml;
using Shared.Domain;
using Shared.Views;

namespace Shared.Parsers
{
	/// <summary>
	/// XML parser voor de database van Helios
	/// </summary>
	public static class XMLParser
	{
		/// <summary>
		/// Parse een XML bestand
		/// </summary>
		/// <param name="fileName">Naam van xml bestand</param>
		/// <param name="poules"> </param>
		/// <returns>Lijst van spelers</returns>
		public static PlayerViews readFile(string fileName, out PouleViews poules)
		{
			poules = new PouleViews();
			XmlTextReader reader = new XmlTextReader(fileName);
			PlayerViews players = new PlayerViews();
			string name = "";
			string surName = "";
			string gender = "";
			string club = "";
			int singlePoule = 0;
			int doublePoule = 0;
			int mixedPoule = 0;
			string singleN = "";
			string doubleN = "";
			string mixedN = "";

			try
			{
				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.Element)
						switch (reader.Name)
						{
							case "voornaam":
								reader.Read();
								name = reader.Value;
								break;
							case "achternaam":
								reader.Read();
								surName = reader.Value;
								break;
							case "geslacht":
								reader.Read();
								gender = reader.Value;
								break;
							case "club":
								reader.Read();
								club = reader.Value;
								break;
							case "enkel":
								reader.Read();
								singlePoule = reader.ReadContentAsInt();
								break;
							case "dubbel":
								reader.Read();
								doublePoule = reader.ReadContentAsInt();
								break;
							case "mix":
								reader.Read();
								mixedPoule = reader.ReadContentAsInt();
								break;
							case "enkel_niv":
								reader.Read();
								singleN = reader.Value;
								break;
							case "dubbel_niv":
								reader.Read();
								doubleN = reader.Value;
								break;
							case "mix_niv":
								reader.Read();
								mixedN = reader.Value;
								break;
						}
					else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "srsb_inschrijvingen")
					{
						// Controleer eerst of bepaalde gegevens niet leeg zijn
						if (gender != "" && (name != "" || surName != ""))
						{
							// Maak spelers object aan
							if (gender == "F")
								gender = "V";
							string preferences = "";
							string comment = "";
							PlayerView player = new PlayerView(new Player(name + " " + surName, gender, club, preferences, comment));
							players.Add(player);

							string discipline;
							if (singlePoule == 1 && singleN != "")
							{

								if (gender == "M")
									discipline = PouleView.DisciplineToString(Poule.Disciplines.MaleSingle);
								else
									discipline = PouleView.DisciplineToString(Poule.Disciplines.FemaleSingle);
								player.AddPreference(PlayerView.CreatePreference(discipline, singleN));
								
								if (!poules.ContainsPoule(discipline, singleN))
									poules.Add(new PouleView(discipline + " " + singleN, discipline, singleN, ""));
							}
							if (doublePoule == 1 && doubleN != "")
							{
								if (gender == "M")
									discipline = PouleView.DisciplineToString(Poule.Disciplines.MaleDouble);
								else
									discipline = PouleView.DisciplineToString(Poule.Disciplines.FemaleDouble);

								player.AddPreference(PlayerView.CreatePreference(discipline, doubleN));
								if (!poules.ContainsPoule(discipline, doubleN))
									poules.Add(new PouleView(discipline + " " + doubleN, discipline, doubleN, ""));
							}
							if (mixedPoule == 1 && mixedN != "")
							{
								discipline = PouleView.DisciplineToString(Poule.Disciplines.Mixed);

								player.AddPreference(PlayerView.CreatePreference(discipline, mixedN));
								if (!poules.ContainsPoule(discipline, mixedN))
									poules.Add(new PouleView(discipline + " " + mixedN, discipline, mixedN, ""));
							}

						}

						// Reset de gebruikte variabelen
						name = "";
						surName = "";
						gender = "";
						club = "";
						singlePoule = 0;
						doublePoule = 0;
						mixedPoule = 0;
						singleN = "";
						doubleN = "";
						mixedN = "";
					}
				}
			}
			catch (Exception)
			{
				throw new Exception("Gegeven bestand is een onjuist XML bestand.");
			}
			reader.Close();
			return players;
		}

	}
}
