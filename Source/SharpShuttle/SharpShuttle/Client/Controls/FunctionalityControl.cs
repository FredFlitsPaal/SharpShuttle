using System;
using Shared.Domain;

namespace Client.Controls
{
	/// <summary>
	/// Houdt bij welke functionaliteit er beschikbaar is.
	/// Daarvoor is een vaste set aan mogelijke functionaliteiten
	/// (gebruikersrollen zijn gedeeltelijk verantwoordelijk voor de beschikbare functionaliteit)
	/// </summary>
	class FunctionalityControl
	{

		/// <summary>
		/// Een enum voor alle mogelijke functionaliteiten
		/// </summary>
		[Flags]
		public enum Functionalities
		{
			//Ongeclassificeerd
			/// <summary>
			/// Login
			/// </summary>
			Login = 1, 
			/// <summary>
			/// Logout
			/// </summary>
			Logout = 1 << 1,

			//Poules
			/// <summary>
			/// Lezen van een niveau
			/// </summary>
			NiveauRead = 1 << 2, 
			/// <summary>
			/// Schrijven van een niveau
			/// </summary>
			NiveauWrite = 1 << 4,
			/// <summary>
			/// Lezen van een discipline
			/// </summary>
			DisciplineRead = 1 << 5,
			/// <summary>
			/// Lezen van een Poule
			/// </summary>
			PouleRead = 1 << 6, 
			/// <summary>
			/// Schrijven van een Poule
			/// </summary>
			PouleWrite = 1 << 7,

			//Spelers
			/// <summary>
			/// Lezen van de basisonderdelen van een speler
			/// </summary>
			PlayerReadBasic = 1 << 8, 
			/// <summary>
			/// Lezen van een complete speler
			/// </summary>
			PlayerReadComplete = 1 << 9, 
			/// <summary>
			/// Schrijven van een speler
			/// </summary>
			PlayerWrite = 1 << 10,

			//Spelers indelen
			/// <summary>
			/// Speler in een poule plaatsen
			/// </summary>
			PlayerPouleWrite = 1 << 11,
			/// <summary>
			/// Lezen welke spelers er in een poule zitten
			/// </summary>
			PlayerPouleRead = 1 << 12,
			/// <summary>
			/// Lezen welke spelers er in een team zitten
			/// </summary>
			PlayerTeamRead = 1 << 13,
			/// <summary>
			/// Een team indelen
			/// </summary>
			PlayerTeamWrite = 1 << 14,

			//Ladder
			/// <summary>
			/// Een ronde sluiten 
			/// </summary>
			CloseRound = 1 << 15, 
			/// <summary>
			/// Een ronde starten
			/// </summary>
			StartRound = 1 << 16, 
			/// <summary>
			/// Een ronde herstarten
			/// </summary>
			RestartRound = 1 << 17,
			/// <summary>
			/// Een team uit laten vallen
			/// </summary>
			DisableTeam = 1 << 18,
			/// <summary>
			/// Wedstrijden lezen
			/// </summary>
			MatchRead = 1 << 19,
			/// <summary>
			/// Wedstrijden schrijven.
			/// </summary>
			MatchWrite = 1 << 20,

			//Scores
			/// <summary>
			/// Scores lezen
			/// </summary>
			ScoreRead = 1 << 21, 
			/// <summary>
			/// Scores invullen
			/// </summary>
			ScoreWrite = 1 << 22,

			//Velden
			/// <summary>
			/// Velden lezen
			/// </summary>
			CourtRead = 1 << 23, 
			/// <summary>
			/// Velden schrijven en indelen
			/// </summary>
			CourtWrite = 1 << 24,

			//Overige
			/// <summary>
			/// Rankings en overviews printen
			/// </summary>
			PrintRanking = 1 << 25, 
			/// <summary> 
			/// MatchPaper printen
			/// </summary>
			PrintMatchPaper = 1 << 26,
			/// <summary>
			/// Sets lezen
			/// </summary>
			SetsRead = 1 << 27, 
			/// <summary>
			/// Sets schrijven
			/// </summary>
			SetsWrite = 1 << 28
		}

		/// <summary>
		/// ProcessFunctionalities
		/// </summary>
		/// <param name="functionalities">De functionaliteiten</param>
		public delegate void ProcessFunctionalities(Functionalities functionalities);

		/// <summary>
		/// De singleton instance van deze class
		/// </summary>
		private static FunctionalityControl instance;
		
		/// <summary>
		/// De huidige rol of null
		/// </summary>
		private User.Roles? current_role;
		/// <summary>
		/// De huidige functionaliteiten of null
		/// </summary>
		private Functionalities? functionalities;

		/// <summary>
		/// De functionaliteiten zijn veranderd
		/// </summary>
		public event ProcessFunctionalities FunctionalitiesChanged;

		/// <summary>
		/// Vraag de singleton instance op
		/// </summary>
		public static FunctionalityControl Instance
		{
			get
			{
				if (instance == null)
					instance = new FunctionalityControl();
				return instance;
			}
		}

		/// <summary>
		/// Default constructor waar alleen maar basisrechten worden geinitializeerd
		/// </summary>
		protected  FunctionalityControl()
		{
			//Initialisatie met alleen maar basisrechten
			current_role = null;
		}

		/// <summary>
		/// De huidige rol
		/// </summary>
		public User.Roles? CurrentRole
		{
			get
			{
				return current_role;
			}
			set
			{
				current_role = value;
				functionalities = null;
				FireFunctionalitiesChanged();
			}
		}

		/// <summary>
		/// De functionaliteiten die voor de huidige rol beschikbaar zijn
		/// </summary>
		public Functionalities CurrentFunctionalities
		{
			get
			{
				if (functionalities == null)
				{
					functionalities = getFunctionalities(current_role);
				}
				return (Functionalities) functionalities;
			}
		}

		/// <summary>
		/// Vraag de functionaliteiten die bij een bepaalde rol horen op
		/// </summary>
		/// <param name="roles"> De rol </param>
		/// <returns> De bijbehorende functionaliteiten </returns>
		protected static Functionalities getFunctionalities(User.Roles? roles)
		{
			Functionalities result = new Functionalities();

			if (roles == null)
			{
				result |= Functionalities.Login;
			}
			else
			{
				//wat iedereen mag
				result |= Functionalities.Logout;
				result |= Functionalities.NiveauRead;
				result |= Functionalities.DisciplineRead;
				result |= Functionalities.PouleRead;
				result |= Functionalities.PlayerReadBasic;
				result |= Functionalities.PlayerPouleRead;
				result |= Functionalities.PlayerTeamRead;
				result |= Functionalities.MatchRead;
				result |= Functionalities.ScoreRead;
				result |= Functionalities.CourtRead;
				result |= Functionalities.SetsRead;
				
				//rol-specefieke items
				if ((roles & User.Roles.Poules) != 0)
				{
					result |= Functionalities.NiveauWrite;
					result |= Functionalities.PouleWrite;
				}

				if ((roles & User.Roles.Players) != 0)
				{
					result |= Functionalities.PlayerReadComplete;
					result |= Functionalities.PlayerWrite;
				}

				if ((roles & User.Roles.Grouping) != 0)
				{
					result |= Functionalities.PlayerPouleWrite;
					result |= Functionalities.PlayerTeamWrite;
					result |= Functionalities.DisableTeam;
				}

				if ((roles & User.Roles.Ladder) != 0)
				{
					result |= Functionalities.CloseRound;
					result |= Functionalities.StartRound;
					result |= Functionalities.RestartRound;
					result |= Functionalities.PrintRanking;
					result |= Functionalities.PrintMatchPaper;
					result |= Functionalities.SetsWrite;
					result |= Functionalities.MatchWrite;
				}

				if ((roles & User.Roles.Courts) != 0)
				{
					result |= Functionalities.CourtWrite;
				}

				if ((roles & User.Roles.Scores) != 0)
				{
					result |= Functionalities.ScoreWrite;
				}

				if ((roles & User.Roles.Reader) != 0)
				{
					
				}
			}
			
			return result;
		}

		/// <summary>
		/// Roep de FunctionalitiesChanged methode aan als functionaliteiten 
		/// veranderd zijn
		/// </summary>
		public void FireFunctionalitiesChanged()
		{
			if (FunctionalitiesChanged != null)
				FunctionalitiesChanged(CurrentFunctionalities);
		}
	}
}
