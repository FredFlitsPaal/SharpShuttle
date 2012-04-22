using System;
using System.Collections.Generic;
using Shared.Domain;

namespace Shared.Views
{
	/// <summary>
	/// Een lijst van ServerViews
	/// </summary>
	[Serializable]
    public class ServerViews : AbstractViews<ServerView, Server>
    {
		/// <summary>
		/// Default constructor
		/// </summary>
		public ServerViews ()
		{
			SetViews();
		}

		/// <summary>
		/// Maakt een ServerViews van een lijst van Server domeinobjecten
		/// </summary>
		/// <param name="servers"></param>
		public ServerViews(IList<Server> servers)
		{
			SetDomainList(servers);

			foreach (Server s in servers)
				viewList.Add(new ServerView(s));
		}

    }

}
