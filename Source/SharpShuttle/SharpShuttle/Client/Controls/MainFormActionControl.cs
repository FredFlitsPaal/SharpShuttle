using System;
using System.Collections.Generic;
using System.ComponentModel;
using Shared.Data;
using Shared.Domain;

namespace Client.Controls
{
	/// <summary>
	/// Deze klasse verteld welke functionaliteiten toegestaan zijn in de main form
	/// Niet alleen uitgaand van de gebruikersrollen, maar ook de state van het programma.
	/// </summary>
	class MainFormActionControl
	{
		/// <summary>
		/// De FunctionalityControl
		/// </summary>
		private FunctionalityControl funcCtrl;

		/// <summary>
		/// Lijst van alle bekende wedstrijden, wordt gebruikt om te bepalen
		/// of de initialisatie is afgerond
		/// </summary>
		private List<Match> matches;

		/// <summary>
		/// Default constructor
		/// </summary>
		public MainFormActionControl()
		{
			funcCtrl = FunctionalityControl.Instance;
			funcCtrl.FunctionalitiesChanged += processFunctionalitiesChanged;
		}

		/// <summary>
		/// Wordt uitgevoerd als er veranderingen zijn bij toegankelijke functionaliteiten
		/// </summary>
		public Action FunctionalitiesChanged;

		/// <summary>
		/// Verwerkt het FunctionalityControl.FunctionalitiesChanged event
		/// </summary>
		/// <param name="functionalities"></param>
		private void processFunctionalitiesChanged(FunctionalityControl.Functionalities functionalities)
		{
			//TODO scan functionalities for relevant items, and only then invoke FunctionalitiesChanged
			if (FunctionalitiesChanged != null)
				FunctionalitiesChanged.Invoke();
		}

		#region Login/Logout

		/// <summary>
		/// Kijk of de gebruiker de fucntionaliteit heeft om in te loggen
		/// </summary>
		/// <returns> Een bool die aangeeft mag inloggen </returns>
		public bool CheckLogin()
		{
			return (funcCtrl.CurrentFunctionalities & FunctionalityControl.Functionalities.Login) != 0;
		}

		/// <summary>
		/// Kijk of de gebruiker de fucntionaliteit heeft om uit te loggen
		/// </summary>
		/// <returns> Een bool die aangeeft mag uitloggen </returns>
		public bool CheckLogout()
		{
			return (funcCtrl.CurrentFunctionalities & FunctionalityControl.Functionalities.Logout) != 0;
		}

		# endregion

		#region Print Match Paper

		/// <summary>
		/// Kijk of de gebruiker toestemming heeft om te printen
		/// </summary>
		/// <returns> Een bool die aangeeft of de gebruiker mag printen </returns>
		public bool CheckPrintMatchPapers()
		{
			return (funcCtrl.CurrentFunctionalities & FunctionalityControl.Functionalities.PrintMatchPaper) != 0;
		}

		#endregion

		#region Poule Toevoegen

		/// <summary>
		/// Kijk of de gebruiker toestemming heeft om een nieuwe poule toe te voegen
		/// </summary>
		/// <returns>Een bool die aangeeft of de gebruiker een poule mag toevoegen</returns>
		public bool CheckAddPoule()
		{
			FunctionalityControl.Functionalities cond =
				FunctionalityControl.Functionalities.PouleWrite |
				FunctionalityControl.Functionalities.PlayerTeamWrite |
				FunctionalityControl.Functionalities.NiveauWrite |
				FunctionalityControl.Functionalities.PlayerReadBasic |
				FunctionalityControl.Functionalities.StartRound |
				FunctionalityControl.Functionalities.PlayerPouleWrite;
			return (funcCtrl.CurrentFunctionalities & cond) == cond;
		}

		#endregion

		#region Spelers Beheren

		/// <summary>
		/// Kijk of de gebruiker toestemming heeft om spelers te beheren
		/// </summary>
		/// <returns>Een bool die aangeeft of de gebruiker spelers mag beheren</returns>
		public bool CheckManagePlayers()
		{
			return (funcCtrl.CurrentFunctionalities & FunctionalityControl.Functionalities.PlayerWrite) != 0;
		}
        
		#endregion

		#region Score Input

		/// <summary>
		/// Kijk of de gebruiker toestemming heeft om scores in te voeren
		/// </summary>
		/// <returns> Een bool die aangeeft of de gebruiker scores in mag voeren </returns>
		public bool CheckScoreInput()
		{
			return (funcCtrl.CurrentFunctionalities & FunctionalityControl.Functionalities.ScoreWrite) != 0;
		}

		#endregion

		#region Poule Information

		/// <summary>
		/// Kijk of de gebruiker toestemming heeft om poule informatie te bekijken
		/// </summary>
		/// <returns> Een bool die aangeeft of de gebruiker poule informatie mag bekijken </returns>
		public bool CheckPouleInformation()
		{
			FunctionalityControl.Functionalities cond =
				FunctionalityControl.Functionalities.MatchRead |
				FunctionalityControl.Functionalities.PouleRead;
			return (funcCtrl.CurrentFunctionalities & cond) == cond;
		}

		#endregion

		#region Court Information

		/// <summary>
		/// Kijk of de gebruiker toestemming heeft om veld informatie te bekijken
		/// </summary>
		/// <returns> Een bool die aangeeft of de gebruiker veld informatie mag bekijken </returns>
		public bool CheckCourtInformation()
		{
			FunctionalityControl.Functionalities cond =
				FunctionalityControl.Functionalities.MatchRead |
				FunctionalityControl.Functionalities.CourtRead;
			return (funcCtrl.CurrentFunctionalities & cond) == cond;
		}

		#endregion

		# region Tournament Initialisation

		/// <summary>
		/// 
		/// </summary>
		/// <param name="finished">Actie die wordt uitgevoerd als de initialisatie afgelopen is.</param>
		/// <param name="not_finished">Actie die wordt uitgevoerd als de initialisatie nog niet afgelopen is.</param>
		public void CheckInitDone(Action finished, Action not_finished)
		{
			BackgroundWorker bgw = new BackgroundWorker();
			bgw.RunWorkerCompleted += checkInitDone;
			bgw.DoWork += getAllMatches;
			bgw.RunWorkerAsync(new Action[] { finished, not_finished });
		}

		/// <summary>
		/// Kijkt of de initialisatiewizard mag worden uitgevoerd.
		/// </summary>
		/// <returns>Een bool die aangeeft of de initialisatiewizard mag worden uitgevoerd.</returns>
		public bool CheckTournamentInit()
		{
			FunctionalityControl.Functionalities conditions =
				FunctionalityControl.Functionalities.NiveauRead |
				FunctionalityControl.Functionalities.NiveauWrite |
				FunctionalityControl.Functionalities.DisciplineRead |
				FunctionalityControl.Functionalities.PouleRead |
				FunctionalityControl.Functionalities.PouleWrite |
				FunctionalityControl.Functionalities.PlayerReadComplete |
				FunctionalityControl.Functionalities.PlayerWrite |
				FunctionalityControl.Functionalities.PlayerPouleRead |
				FunctionalityControl.Functionalities.PlayerPouleWrite |
				FunctionalityControl.Functionalities.PlayerTeamRead |
				FunctionalityControl.Functionalities.PlayerTeamWrite |
				FunctionalityControl.Functionalities.CourtRead |
				FunctionalityControl.Functionalities.CourtWrite |
				FunctionalityControl.Functionalities.StartRound;
			FunctionalityControl.Functionalities func = funcCtrl.CurrentFunctionalities;
			//kijken of de gebruiker wel aan de juiste functionaliteiten mag
			return (func & conditions) != 0;
		}

		/// <summary>
		/// Haalt alle wedstrijden op en bewaart ze in een instance variable
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void getAllMatches(object sender, DoWorkEventArgs e)
		{
			try
			{
				IDataCache c = DataCache.Instance;
				List<Match> currentmatches = c.GetAllMatches();
				List<Match> historymatches = c.GetAllHistoryMatches();
				matches = new List<Match>(currentmatches);
				matches.AddRange(historymatches);
			}
			catch
			{
				matches = null;
			}
			e.Result = e.Argument;
		}

		/// <summary>
		/// Beoordeelt of de initialisatie al is afgerond op grond van hoeveel wedstrijden in het systeem bekend zijn.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void checkInitDone(object sender, RunWorkerCompletedEventArgs e)
		{
			Action[] result = e.Result as Action[];
			if (matches != null)
			{
				if (matches.Count <= 0)
				{
					if (result[1] != null)
						result[1].Invoke();
				}
				else 
				{
					if (result[0] != null)
						result[0].Invoke();
				}
			}
		}

		# endregion

		#region AmountFields

		/// <summary>
		/// Kijk of de gebruiker toestemming heeft om het aantal velden te veranderen
		/// </summary>
		/// <returns> Een bool die aangeeft of de gebruiker het aantal velden mag veranderen </returns>
		public bool CheckAmountFields()
		{
			return (funcCtrl.CurrentFunctionalities & FunctionalityControl.Functionalities.CourtWrite) != 0;
		}

		#endregion

	}

}
