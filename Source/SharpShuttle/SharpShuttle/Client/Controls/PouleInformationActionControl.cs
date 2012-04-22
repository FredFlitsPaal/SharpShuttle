using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.Controls
{
	/// <summary>
	/// Deze klasse verteld welke functionaliteiten toegestaan zijn in het poule informatie scherm.
	/// Gaat alleen uit van de gebruikersrollen bij creatie.
	/// </summary>
	class PouleInformationActionControl
	{
		/// <summary>
		/// De FunctionalityControl
		/// </summary>
		private FunctionalityControl funcCtrl;

				/// <summary>
		/// Default constructor
		/// </summary>
		public PouleInformationActionControl()
		{
			funcCtrl = FunctionalityControl.Instance;
		}

		#region printen

		/// <summary>
		/// Kijkt of wedstrijdbriefjes uitgeprint mogen worden.
		/// </summary>
		/// <returns>Een bool die aangeeft of wedstrijdbriefjes uitgeprint mogen worden.</returns>
		public bool CheckAddMatchPapers()
		{
			return (funcCtrl.CurrentFunctionalities & FunctionalityControl.Functionalities.PrintMatchPaper) != 0;
		}

		/// <summary>
		/// Kijkt of ranglijsten en wedstrijden geprint mogen worden.
		/// </summary>
		/// <returns>Een bool die aangeeft of ranglijsten en wedstrijden geprint mogen worden.</returns>
		public bool CheckPrintRankings()
		{
			return (funcCtrl.CurrentFunctionalities & FunctionalityControl.Functionalities.PrintRanking) != 0;
		}

		#endregion

		#region scores

		/// <summary>
		/// Kijkt of de scores ingevuld en gewijzigd mogen worden.
		/// </summary>
		/// <returns>Een bool die aangeeft of de scores ingevuld en gewijzigd mogen worden.</returns>
		public bool CheckEditScore()
		{
			return (funcCtrl.CurrentFunctionalities & FunctionalityControl.Functionalities.ScoreWrite) != 0;
		}

		#endregion

		#region teamwijziging

		/// <summary>
		/// Kijkt of een team gewijzigd mag worden.
		/// </summary>
		/// <returns>Een bool die aangeeft of een team gewijzigd mag worden.</returns>
		public bool CheckEditTeam()
		{
			FunctionalityControl.Functionalities conds =
				FunctionalityControl.Functionalities.DisableTeam |
				FunctionalityControl.Functionalities.PlayerTeamWrite |
				FunctionalityControl.Functionalities.MatchWrite |
				FunctionalityControl.Functionalities.PlayerPouleWrite;
			return (funcCtrl.CurrentFunctionalities & conds) != 0;
		}

		#endregion

		#region rondes

		/// <summary>
		/// Kijkt of een nieuwe ronde gestart mag worden.
		/// </summary>
		/// <returns>Een bool die aangeeft of een nieuwe ronde gestart mag worden.</returns>
		public bool CheckNextRound()
		{
			FunctionalityControl.Functionalities conds =
				FunctionalityControl.Functionalities.CloseRound |
				FunctionalityControl.Functionalities.StartRound |
				FunctionalityControl.Functionalities.PrintMatchPaper;
			return (funcCtrl.CurrentFunctionalities & conds) != 0;
		}

		/// <summary>
		/// Kijkt of de huidige ronde teruggedraait mag worden.
		/// </summary>
		/// <returns>Een bool die aangeeft of de huidige ronde teruggedraait mag worden.</returns>
		public bool CheckUndoRound()
		{
			return (funcCtrl.CurrentFunctionalities & FunctionalityControl.Functionalities.RestartRound) != 0;
		}

		#endregion

	}
}
