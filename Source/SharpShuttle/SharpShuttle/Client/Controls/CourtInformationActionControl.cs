namespace Client.Controls
{
	/// <summary>
	/// Deze klasse verteld welke functionaliteiten toegestaan zijn in het velden informatie scherm.
	/// Gaat alleen uit van de gebruikersrollen bij creatie.
	/// </summary>
	class CourtInformationActionControl
	{
		/// <summary>
		/// De FunctionalityControl
		/// </summary>
		private FunctionalityControl funcCtrl;

		/// <summary>
		/// Default constructor
		/// </summary>
		public CourtInformationActionControl()
		{
			funcCtrl = FunctionalityControl.Instance;
		}

		#region Wedstrijden starten/stoppen

		/// <summary>
		/// Kijkt of een wedstrijd gestart mag worden.
		/// </summary>
		/// <returns>Een bool die aangeeft of een wedstrijd gestart mag worden.</returns>
		public bool CheckStartMatch()
		{
			return (funcCtrl.CurrentFunctionalities & FunctionalityControl.Functionalities.MatchWrite) != 0;
		}

		/// <summary>
		/// Kijkt of een wedstrijd beëindigd mag worden.
		/// </summary>
		/// <returns>Een bool die aangeeft of een wedstrijd beëindigd mag worden.</returns>
		public bool CheckEndMatch()
		{
			return (funcCtrl.CurrentFunctionalities & FunctionalityControl.Functionalities.MatchWrite) != 0;
		}

		#endregion

		#region Wedstrijden indelen/terugzetten

		/// <summary>
		/// Kijkt of de wedstrijden verschoven mogen worden.
		/// </summary>
		/// <returns>Een bool die aangeeft of de wedstrijden verschoven mogen worden.</returns>
		public bool CheckSortMatches()
		{
			return (funcCtrl.CurrentFunctionalities & FunctionalityControl.Functionalities.MatchWrite) != 0;
		}

		/// <summary>
		/// Kijkt of een wedstrijd op een veld geplaatst mag worden.
		/// </summary>
		/// <returns>Een bool die aangeeft of een wedstrijd op een veld geplaatst mag worden.</returns>
		public bool CheckPlaceMatch()
		{
			FunctionalityControl.Functionalities conds =
				FunctionalityControl.Functionalities.CourtWrite |
				FunctionalityControl.Functionalities.MatchWrite;
			return (funcCtrl.CurrentFunctionalities & conds) != 0;
		}

		/// <summary>
		/// Kijkt of een wedstrijd van een veld gehaald mag worden.
		/// </summary>
		/// <returns>Een bool die aangeeft of een wedstrijd van een veld gehaald mag worden.</returns>
		public bool CheckUnplaceMatch()
		{
			FunctionalityControl.Functionalities conds =
				FunctionalityControl.Functionalities.CourtWrite |
				FunctionalityControl.Functionalities.MatchWrite;
			return (funcCtrl.CurrentFunctionalities & conds) != 0;
		}

		#endregion
	}
}
