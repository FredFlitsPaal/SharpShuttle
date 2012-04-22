using Client.Forms.PopUpWindows;
using Shared.Views;
using Shared.Communication.Exceptions;
using Shared.Data;

namespace Client.Controls
{

	/// <summary>
	/// Businesslogica voor het beheren van wedstrijdscores
	/// </summary>
    public class ManageScoreControl
    {
		/// <summary>
		/// De DataCache
		/// </summary>
        private IDataCache cache;

		/// <summary>
		/// Alle matches
		/// </summary>
		private MatchViews matches;

		/// <summary>
		/// Event die aangeeft dat het score invoeren klaar is
		/// </summary>
		/// <param name="match"></param>
		public delegate void ScoreEditDoneEvent(MatchView match);

		/// <summary>
		/// Default constructor, haalt de datacache op
		/// </summary>
        public ManageScoreControl()
        {
            cache = DataCache.Instance;
        }

		/// <summary>
		/// Haalt matches op uit de cache
		/// </summary>
		public void UpdateMatches()
		{
			matches = new MatchViews();

			try
			{
				MatchViews matchViews = new MatchViews(cache.GetAllMatches());

				foreach (MatchView view in matchViews)
					if (!view.Domain.Disabled)
						matches.Add(view);
			}
			catch (CommunicationException exc)
			{
				CatchCommunicationExceptions.Show(exc);
			}
		}

		/// <summary>
		/// Alle matches
		/// </summary>
		public MatchViews Matches
		{
			get { return matches; }
		}

		/// <summary>
		/// Voert de score van de gegeven wedstrijd in
		/// </summary>
		/// <param name="match"> De wedstrijd </param>
        public void SetScore(MatchView match)
        {
			CommunicationException exc;

			if (!cache.SetMatch(match.Domain, out exc))
			{
				Shared.Logging.Logger.Write("Fout tijdens het opslaan van een score.", exc.ToString());
				CatchCommunicationExceptions.Show(exc);
			}
        }
    }
}
