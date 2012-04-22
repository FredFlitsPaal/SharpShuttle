using System.Collections.Generic;
using Shared.Domain;

namespace Shared.Algorithms
{

	/// <summary>
	/// Klasse die verantwoordelijk is voor het matching algoritme dat spelers aan
	/// elkaar koppelt voor een team in een niet-gemengde poule
	/// </summary>
    internal static class Matcher
    {
        /// <summary>
        /// Koppelt een lijst van spelers aan elkaar, waarbij spelers wanneer
        /// mogelijk aan spelers uit een andere vereniging worden gekoppeld
        /// </summary>
        /// <param name="players"> 
        /// Een lijst van spelers, gegroepeerd op vereniging, gesorteerd
        /// op verenigingsgrootte
        /// </param>
        /// <returns> Een lijst van gekoppelde teams</returns>
        /// 
        internal static List<Team> TeamDoubles(List<Player> players)
        {
            List<Team> teams = new List<Team>();
            List<List<Player>> clubs = new List<List<Player>>();
 
            string currentClub = null;
            List<Player> club = null;

            // Deel de spelers in in verenigingen
            foreach (Player player in players)
            {
                if (player.Club != currentClub)
                {
                    club = new List<Player>();
                    clubs.Add(club);
                    currentClub = player.Club;
                }

                club.Add(player);
            }

            RoundRobin roundRobin = new RoundRobin(1, clubs.Count - 1);

            // Ga door totdat de grootste vereniging leeg is
            while (clubs[0].Count != 0)
            {
                //kies de eerste vereniging en speler om te koppelen
                List<Player> club1 = clubs[0];
                Player player1 = club1[club1.Count - 1];
                club1.RemoveAt(club1.Count - 1);

                //kies de tweede vereniging om te koppelen
                int next = roundRobin.Next();
                List<Player> club2;

                // Als alle andere verenigingen leeg zijn, koppel je aan iemand
                // uit dezelfde vereniging
                if (clubs.Count == 1) club2 = club1;
                else club2 = clubs[next];

                //kies een speler uit de tweede vereniging
                Player player2 = club2[club2.Count - 1];
                club2.RemoveAt(club2.Count - 1);

                //vorm een nieuw team van deze spelers en voeg toe aan de lijst
                Team team = new Team(player1.Name + " + " + player2.Name, player1, player2);
                teams.Add(team);

                // Als de tweede vereniging leeg is, verwijder hem uit de lijst
                // en verlaag roundrobin
                if (club2.Count == 0 && clubs.Count > 1)
                {
                    clubs.RemoveAt(next);
                    roundRobin.DecreaseMax();
                }

                //Zelfde voor de eerste vereniging.
                if (club1.Count == 0 && clubs.Count > 1)
                {
                    clubs.RemoveAt(0);
                    roundRobin.DecreaseMax();
                }

                //Zoek de vereniging met de hoogste waarde.
                int max = clubs[0].Count;
                int maxIndex = 0;

                for (int i = 1; i < clubs.Count; i++)
                {
                    if (clubs[i].Count > max)
                    {
                        max = clubs[i].Count;
                        maxIndex = i;
                    }
                }

                //Verplaats, als dat al niet zo is, de vereniging met de hoogste waarde naar positie 0.
                if (maxIndex != 0) 
                {
                    //swap de vereniging met de hoogste waarde met de vereniging op positie 0
                    List<Player> tempClub = clubs[0];
                    clubs[0] = clubs[maxIndex];
                    clubs[maxIndex] = tempClub;
                }
            }

            return teams;
        }
    }
}
