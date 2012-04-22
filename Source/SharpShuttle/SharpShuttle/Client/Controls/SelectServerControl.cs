using System;
using System.Collections.Generic;
using Shared.Communication;
using Shared.Domain;
using Shared.Views;

namespace Client.Controls
{
	/// <summary>
	/// Businesslogica voor het verbinden met een server
	/// </summary>
	public class SelectServerControl
    {
		private ServerViews items;

		/// <summary>
		/// Sluit mogelijk open verbindingen
		/// </summary>
		public void Dispose()
		{
			Communication.Instance.Disconnect();
		}

		/// <summary>
		/// Maakt verbinding met server.
		/// Gooit exception bij mislukken.
		/// </summary>
		/// <param name="address"></param>
		public bool Connect(string address)
		{
			return Communication.Instance.Connect(address);
		}

		/// <summary>
		/// Maximum aantal servers
		/// </summary>
		protected static int MaxCount
		{
			get { return 5; }
		}

		/// <summary>
		/// Default constructor 
		/// </summary>
		public SelectServerControl()
		{
			items = null;
		}

        /// <summary>
        /// Haalt de laatst gebruikte servers op
        /// </summary>
        /// <returns> </returns>
        public ServerViews GetServers()
        {
			if (items != null)
				return items;

			string[] prevServerNames = Configurations.PreviousServers;
			List<Server> servers = new List<Server>();
			
			foreach (string serverName in prevServerNames)
			{
				servers.Add(new Server(serverName));
			}

			if (servers.Count == 0)
				servers.Add(new Server("127.0.0.1"));

			items = new ServerViews(servers);
			return items;
        }

		/// <summary>
		/// schrijft een server weg naar de configuratie
		/// </summary>
		/// <param name="newview">server die bij het laden bovenaan de lijst komt</param>
		public void StoreServer(ServerView newview)
		{
			if (items == null)
				GetServers();

			////Nieuwe lijst van servers bouwen.
			int count = MaxCount-1;
			ServerViews newItems = new ServerViews();
			
			if (newview.Address != "")
				newItems.Add(newview);

				foreach (ServerView oldview in items)
				{
					if (newview.Address != oldview.Address && count > 0)
					{
						newItems.Add(oldview);
						count--;
					}
				}
			
			//newItems wegschrijven
			items = newItems;
			string[] newServerNames = new string[newItems.Count];

			for (int i = 0; i < newServerNames.Length; i++)
			{
			        newServerNames[i] = newItems[i].Address;
			}

			Configurations.PreviousServers = newServerNames;
		}
    }
}
