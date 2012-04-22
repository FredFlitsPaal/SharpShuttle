using System.Collections.Generic;
using Shared.Domain;

namespace Shared.Views
{

	/// <summary>
	/// Een lijst van PlayerPoulesViews
	/// </summary>
	public class PlayerPoulesViews : AbstractViews<PlayerPoulesView, Player>
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public PlayerPoulesViews ()
		{
			SetViews();
		}

		/// <summary>
		/// Maakt een PlayerPoulesViews van een lijst van speler domeinobjecten
		/// </summary>
		/// <param name="plaPos"></param>
		public PlayerPoulesViews(ICollection<Player> plaPos)
		{
			SetDomainList(plaPos);

			foreach (Player p in plaPos)
				viewList.Add(new PlayerPoulesView(p));
		}

		/// <summary>
		/// Haalt de poule-informatie van een speler op
		/// </summary>
		/// <param name="playerID"> Het ID van de speler </param>
		/// <returns> De poule-informatie van de speler </returns>
		public PlayerPoulesView getPlayer(int playerID)
		{
			foreach (PlayerPoulesView player in this)
				if (player.Id == playerID)
					return player;

			return null;
		}

	}

}
