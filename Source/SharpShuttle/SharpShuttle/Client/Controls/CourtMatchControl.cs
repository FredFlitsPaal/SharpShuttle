using System;
using Client.Forms.PopUpWindows;
using Shared.Communication.Exceptions;
using Shared.Data;
using Shared.Views;

namespace Client.Controls
{

	/// <summary>
	/// Business logica voor het binden van velden aan wedstrijden
	/// </summary>
	class CourtMatchControl
	{
		/// <summary>
		/// De datacache
		/// </summary>
		private IDataCache cache;

		/// <summary>
		/// Een lijst van wedstrijden
		/// </summary>
		private MatchViews matches;

		/// <summary>
		/// Wedstrijden die nog gespeeld moeten worden
		/// </summary>
		MatchViews matchesToPlay;

		/// <summary>
		/// Default constructor, haalt de datacache op
		/// </summary>
		public CourtMatchControl()
		{
			cache = DataCache.Instance;
		}

		#region Ophalen van data (velden en wedstrijden)

		/// <summary>
		/// Geeft alle matches terug op velden die al wel gestart zijn maar nog niet geeindigd
		/// </summary>
		/// <returns> Alle matches die op dit moment gespeeld worden </returns>
		public MatchViews getCourtMatches()
		{
			MatchViews courtMatches = new MatchViews();

			getMatches();

			foreach (MatchView mv in matches)
			{
				if ((mv.StartTime != "" && mv.EndTime == "" && !mv.Domain.Disabled) ||
						(mv.StartTime == "" && mv.EndTime == "" && mv.Court != 0 && !mv.Domain.Disabled))
					courtMatches.Add(mv);
			}

			return courtMatches;
		}

		/// <summary>
		/// Returnt alle wedstrijden die nog gespeeld moeten worden
		/// </summary>
		/// <returns> Alle wedstrijden die nog gespeeld moeten worden </returns>
		public MatchViews getMatchesToPlay()
		{
			matchesToPlay = new MatchViews();

			getMatches();

			foreach (MatchView mv in matches)
			{
				if (mv.StartTime == "" && mv.EndTime == "" && mv.Court == 0 && !mv.Domain.Played && !mv.Domain.Disabled)
					matchesToPlay.Add(mv);
			}

			return matchesToPlay;
		}

		/// <summary>
		/// Het aantal velden
		/// </summary>
		public int NumberOfCourts
		{
			get { return Configurations.NumberOfCourts; }
		}

		/// <summary>
		/// Haal alle wedstrijden op en sla ze op in matches
		/// </summary>
		private void getMatches()
		{
			matches = new MatchViews();

			try
			{
				matches = new MatchViews(cache.GetAllMatches());
			}

			catch (CommunicationException exc)
			{
				CatchCommunicationExceptions.Show(exc);
			}
		}

		/// <summary>
		/// Haalt een wedstrijd op via het wedstrijd ID
		/// </summary>
		/// <param name="MatchID">het wedstrijdId</param>
		/// <returns></returns>
		public MatchView GetMatch(int MatchID)
		{
			MatchView mv = null;

			try
			{
				mv = new MatchView(cache.GetMatch(MatchID));
			}
			catch (DataDoesNotExistsException)
			{
				return null;
			}
			catch (CommunicationException exc)
			{
				CatchCommunicationExceptions.Show(exc);
			}

			return mv;
		}

		#endregion

		#region Wedstrijd bewerkings methodes

		/// <summary>
		/// Start een wedstrijd op een gegeven tijd
		/// </summary>
		/// <param name="mv"> De wedstrijd </param>
		/// <param name="time"> De tijd waarop de wedstrijd start </param>
		public void startMatch(MatchView mv, DateTime time)
		{
			CommunicationException exc;

			mv.StartTime = time.ToString("dd/MM/yyyy HH:mm:ss");

			if (!cache.SetMatch(mv.Domain, out exc))
			{
				CatchCommunicationExceptions.Show(exc);
			}
		}

		/// <summary>
		/// Reset een wedstrijd
		/// </summary>
		/// <param name="mv"> De wedstrijd </param>
		public void resetMatch(MatchView mv)
		{
			CommunicationException exc;

			mv.Court = 0;
			mv.StartTime = "";
			mv.EndTime = "";

			if (!cache.SetMatch(mv.Domain, out exc))
			{
				CatchCommunicationExceptions.Show(exc);
			}
		}

		/// <summary>
		/// Eindigt een wedstrijd op een gegeven tijd
		/// </summary>
		/// <param name="mv"> De wedstrijd </param>
		/// <param name="time"> Het tijdstip </param>
		public void endMatch(MatchView mv, DateTime time)
		{
			CommunicationException exc;

			mv.EndTime = time.ToString("dd/MM/yyyy HH:mm:ss");

			if (!cache.SetMatch(mv.Domain, out exc))
			{
				CatchCommunicationExceptions.Show(exc);
			}
		}

		#endregion

		#region Veld bewerkings methode
		
		/// <summary>
		/// Ken een wedstrijd aan een veld toe
		/// </summary>
		/// <param name="mv"> De wedstrijd </param>
		/// <param name="court"> Het veldnummer </param>
		public void assignToCourt(MatchView mv, int court)
		{
			CommunicationException exc;

			mv.Court = court;

			if (!cache.SetMatch(mv.Domain, out exc))
			{
				CatchCommunicationExceptions.Show(exc);	
			}
		}

		#endregion
	}
}
