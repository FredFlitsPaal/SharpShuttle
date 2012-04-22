using System;
using Shared.Domain;

namespace Shared.Communication.Messages.Requests
{
	/// <summary>
	/// Request voor SetPoulePlanning
	/// </summary>
	[Serializable]
	public class RequestSetPoulePlanning : RequestMessage
	{
		#region Fields

		/// <summary>
		/// ID van de Poule waarvan de Planning geset wordt
		/// </summary>
		public readonly int PouleID;
		/// <summary>
		/// De nieuwe Planning voor de Poule
		/// </summary>
		public readonly ChangeTrackingList<Player> Players;

		#endregion

		#region Constructors

		/// <summary>
		/// Maakt een Request voor SetPoulePlanning
		/// </summary>
		/// <param name="PouleID">ID van de Poule waarvan de Planning geset wordt</param>
		/// <param name="Players">De nieuwe Planning voor de Poule</param>
		public RequestSetPoulePlanning(int PouleID, ChangeTrackingList<Player> Players)
		{
			this.PouleID = PouleID;
			this.Players = Players;
		}

		#endregion

		/// <summary>
		/// RequestType van deze message
		/// </summary>
		public override RequestTypes RequestType
		{
			get { return RequestTypes.SetPoulePlanning; }
		}
	}
}
