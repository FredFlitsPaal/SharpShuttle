using System.Collections.Generic;
using Shared.Domain;

namespace Shared.Views
{
	/// <summary>
	/// Een lijst van Playerviews
	/// </summary>
	public class PlayerViews : AbstractViews<PlayerView, Player>
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public PlayerViews()
		{
			SetViews();
		}

		/// <summary>
		/// Maakt een PlayerViews van een lijst van speler domeinobjecten
		/// </summary>
		/// <param name="players"></param>
		public PlayerViews(ICollection<Player> players)
		{
			SetDomainList(players);

			foreach (Player p in players)
				viewList.Add(new PlayerView(p));
		}
	}
}
