using System.Collections.Generic;
using Shared.Domain;

namespace Shared.Views
{
	/// <summary>
	/// Een lijst van wedstrijden
	/// </summary>
	public class MatchViews : AbstractViews<MatchView, Match>
	{
		/// <summary>
		/// Default constructor
		/// </summary>
        public MatchViews()
		{
        	SetViews();
		}

		/// <summary>
		/// Maakt een MatchViews met een lijst van wedstrijd domeinobjecten
		/// </summary>
		/// <param name="matches"></param>
		public MatchViews(IList<Match> matches)
		{
			SetDomainList(matches);

			foreach (Match m in matches)
				viewList.Add(new MatchView(m));
		}

	}

}
