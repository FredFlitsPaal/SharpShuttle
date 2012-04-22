using System;
using Shared.Domain;

namespace Shared.Communication.Messages.Requests
{
	/// <summary>
	/// Request voor SetAllPoules
	/// </summary>
	[Serializable]
	public class RequestSetAllPoules : RequestMessage
	{
		#region Fields

		/// <summary>
		/// De nieuwe lijst met Poules
		/// </summary>
		public readonly ChangeTrackingList<Poule> Poules;

		#endregion

		#region Constructors

		/// <summary>
		/// Maakt een Request voor SetAllPoules
		/// </summary>
		/// <param name="Poules">De nieuwe lijst met Poules</param>
		public RequestSetAllPoules(ChangeTrackingList<Poule> Poules)
		{
			this.Poules = Poules;
		}

		#endregion

		/// <summary>
		/// RequestType van deze message
		/// </summary>
		public override RequestTypes RequestType
		{
			get { return RequestTypes.SetAllPoules; }
		}
	}
}
