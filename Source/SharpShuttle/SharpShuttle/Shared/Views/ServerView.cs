using System;
using Shared.Domain;

namespace Shared.Views
{

	/// <summary>
	/// View die een server beschrijft
	/// </summary>
	[Serializable]
	public class ServerView : AbstractView <Server>
	{
		#region Constructors

		/// <summary>
		/// Default constructor
		/// </summary>
		public ServerView()
		{
			data = new Server("");
		}

		/// <summary>
		/// Maakt een ServerView van een Server domeinobject
		/// </summary>
		/// <param name="server"></param>
		public ServerView(Server server)
		{
			data = server;
		}

		#endregion

		/// <summary>
		/// Het Server domeinobject
		/// </summary>
		public override Server Domain
		{
			get { return data; }
		}

		#region Eigenschappen ServerView

		/// <summary>
		/// Het adres van de server
		/// </summary>
		public string Address
		{
			get { return data.Address; }
			set { data.Address = value; }
		}

		#endregion

	}
}
