using System.Collections.Generic;
using Shared.Domain;

namespace Shared.Views
{

	/// <summary>
	/// Een lijst van TeamViews
	/// </summary>
	public class TeamViews : AbstractViews<TeamView, Team>
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public TeamViews()
		{
			SetViews();
		}

		/// <summary>
		/// Maakt een TeamViews met een lijst van Team domeinobjecten
		/// </summary>
		/// <param name="teams"></param>
		public TeamViews(ICollection<Team> teams)
		{
			SetDomainList(teams);
			
			foreach (Team t in teams)
				viewList.Add(new TeamView(t));
		}

	}

}
