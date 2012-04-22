using System.Collections.Generic;
using Shared.Domain;

namespace Shared.Views
{
	/// <summary>
	/// Een lijst van ladderteams
	/// </summary>
	public class LadderTeamViews : AbstractViews<LadderTeamView, LadderTeam>
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public LadderTeamViews()
		{
			SetViews();
		}

		/// <summary>
		/// Maakt een LadderTeamViews met een lijst van LadderTeam domeinobjecten
		/// </summary>
		/// <param name="teams"></param>
		public LadderTeamViews(IList<LadderTeam> teams)
		{
			SetDomainList(teams);
			
			foreach (LadderTeam t in teams)
				viewList.Add(new LadderTeamView(t));
		}

	}

}
