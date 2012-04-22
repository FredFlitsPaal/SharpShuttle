using System;
using System.Collections.Generic;
using Shared.Domain;
using Shared.Datastructures;

namespace Shared.Algorithms
{
	/// <summary>
	/// Klasse die verantwoordelijk is voor het matching algoritme dat spelers aan
	/// elkaar koppelt voor een team in een gemengde poule
	/// </summary>
	internal class MixedMatcher
	{
        /// <summary>
        /// Koppelt een lijst van spelers aan elkaar, waarbij spelers wanneer
        /// mogelijk aan spelers uit een andere vereniging worden gekoppeld, met als extra
        /// constraint dat spelers alleen aan spelers van het andere geslacht gekoppeld worden.
        /// </summary>
        /// <param name="players"> 
        /// Een lijst met spelers, gegroepeerd op geslacht, daarbinnen
        /// gegroepeerd op vereniging, gesorteerd op vereniging grootte</param>
        /// <returns> Een lijst van gekoppelde teams, bestaande uit 1 man en 1 vrouw</returns>
		internal static List<Team> TeamDoubles(List<Player> players)
		{

			#region initialisatie

			// De uiteindelijke output van het algoritme
			List<Team> teams = new List<Team>();

			// Lijsten en priority queues voor database input.
			LinkedList<ClubPlaceHolder> maleClubList = new LinkedList<ClubPlaceHolder>();
			LinkedList<ClubPlaceHolder> femaleClubList = new LinkedList<ClubPlaceHolder>();
			Heap<ClubPlaceHolder> maleClubs = new Heap<ClubPlaceHolder>();
			Heap<ClubPlaceHolder> femaleClubs = new Heap<ClubPlaceHolder>();

			// Tijdelijke variabelen voor controleren van de lijst
			string currentClub = null;
			string currentGender = "M";

			// De 'placeholder' waar de spelers in komen
			List<Player> club = null;

			// Deel de spelers in op basis van geslacht en vereniging
			foreach (Player player in players)
			{
				// Als de volgende speler uit een nieuwe vereniging is of van een
				// ander geslacht
				// maken we een nieuwe lijst aan en voegen we de speler toe
				if (!player.Club.Equals(currentClub)
						|| player.Gender != currentGender)
				{
					// Stel het nieuwe geslacht in
					currentGender = player.Gender;

					// Stel de vereningin in
					currentClub = player.Club;

					// Maak een nieuwe vereniging aan
					club = new List<Player>();

					// Kijk wat het geslacht is en voeg de vereniging toe
					if (player.Gender == "V")
						femaleClubList.AddLast(new ClubPlaceHolder(player.Club, club));
					else
						maleClubList.AddLast(new ClubPlaceHolder(
								player.Club, club));

				}

				// Voeg nu de speler daadwerkelijk toe aan de vereniging
				club.Add(player);
			}

			// Zet de lijsten om naar priorityqueues.
			foreach (ClubPlaceHolder femalePlaceHolder in femaleClubList)
				femaleClubs.Add(femalePlaceHolder);
			foreach (ClubPlaceHolder malePlaceHolder in maleClubList)
				maleClubs.Add(malePlaceHolder);

			#endregion

			// Ga door tot alle spelers ingedeeld zijn.
			while (femaleClubs.Count != 0)
			{
				int maleMaxSize = maleClubs[0].players.Count;
				int femaleMaxSize = femaleClubs[0].players.Count;
				Heap<ClubPlaceHolder> takeFrom;
				Heap<ClubPlaceHolder> teamWith;

				// Pak van de grootste stapel, koppel met grootste stapel aan de
				// andere kant.
				if (maleMaxSize > femaleMaxSize)
				{
					takeFrom = maleClubs;
					teamWith = femaleClubs;
				}
				else
				{
					takeFrom = femaleClubs;
					teamWith = maleClubs;
				}

				// Bepaal welke verenigingen de grootste zijn bij mannen en vrouwen.
				ClubPlaceHolder largestClub = takeFrom.RemoveMin();
				ClubPlaceHolder largestMatchingClub = teamWith.RemoveMin();

				// Kies eerste speler.
				Player player1 = largestClub.players[0];
				largestClub.players.RemoveAt(0);

				// Als een vereniging aan beide kanten de grootste is, wissel om met
				// de een na grootste.
				if (largestClub.club == largestMatchingClub.club)
				{
					// Wissel alleen om als er een andere vereniging is om uit te
					// kiezen.
					if (teamWith.Count != 0)
					{
						ClubPlaceHolder swap = teamWith.RemoveMin();
						teamWith.Add(largestMatchingClub);
						largestMatchingClub = swap;
					}
				}

				// Kies tweede speler.
				Player player2 = largestMatchingClub.players[0];
				largestMatchingClub.players.RemoveAt(0);

				// Stop niet-lege verenigingen terug in de queues.
				if (largestClub.players.Count != 0)
					takeFrom.Add(largestClub);
				if (largestMatchingClub.players.Count != 0)
					teamWith.Add(largestMatchingClub);

                //Voeg het nieuwe team toe aan de lijst van teams
				if (player1.Gender == "M")
					teams.Add(new Team(player1.Name + " + " + player2.Name, player1, player2));
				else
					teams.Add(new Team(player1.Name + " + " + player2.Name, player2, player1));
			}

			// Bij oneven aantal verenigingen is er de kans dat het laatste team van
			// dezelfde club word.t
			// Ook al is dat niet nodig.
			// Dit stuk husselt de laatste 2 teams als dat kan.
			if (teams.Count > 1)
			{
				Team last = teams[teams.Count - 1];
				// Als het laatste team van dezelfde vereniging is.
				if (last.Player1.Club.Equals(last.Player2.Club))
				{
					Player swap = last.Player2;
					Team subLast = teams[teams.Count - 2];
					// Wissel alleen als de voorlaatste ook verschillend is.
					if (subLast.Player1.Club != subLast.Player2.Club)
					{
						last.Player2 = subLast.Player2;
						subLast.Player2 = swap;
					}
				}
			}

			// Geef nu het lijstje met teams terug
			return teams;
		}

		/// <summary>
		/// Placeholder klasse voor een vereniging voor het algoritme
		/// </summary>
		internal class ClubPlaceHolder : IComparable<ClubPlaceHolder>
		{
			public string club;
			public List<Player> players;

			/// <summary>
			/// Constructor
			/// </summary>
			/// <param name="club"> De vereniging </param>
			/// <param name="players"> De spelers </param>
			internal ClubPlaceHolder(string club, List<Player> players)
			{
				// vul de vereniging in:
				this.club = club;

				// maak een link naar de spelers
				this.players = players;
			}

			/// <summary>
			/// Standaard compare methode. Kijkt alleen naar de hoeveelheid spelers.
			/// </summary>
			/// <param name="compareWith"></param>
			/// <returns> een int die aangeeft welke vereniging de meeste spelers heeft</returns>
			public int CompareTo(ClubPlaceHolder compareWith)
			{
				if (compareWith.players.Count == players.Count)
					return 0;
				if(compareWith.players.Count < players.Count)
					return -1;
				return 1;
			}
		}
	}
}
