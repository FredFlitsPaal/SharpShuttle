using System;
using Shared.Domain;

namespace Shared.Communication.Messages.Requests
{
	/// <summary>
	/// Request voor SetAllPlayers
	/// </summary>
	[Serializable]
	public class RequestSetAllPlayers : RequestMessage
	{
		#region Fields

		/// <summary>
		/// De nieuwe lijst met spelers
		/// </summary>
		public readonly ChangeTrackingList<Player> Players;

		#endregion

		#region Constructors

		/// <summary>
		/// Maakt een Request voor SetAllPlayers
		/// </summary>
		/// <param name="Players">De nieuwe lijst met spelers</param>
		public RequestSetAllPlayers(ChangeTrackingList<Player> Players)
		{
			this.Players = Players;
		}

		#endregion

		/// <summary>
		/// RequestType van deze message
		/// </summary>
		public override RequestTypes RequestType
		{
			get { return RequestTypes.SetAllPlayers; }
		}
	}
}
