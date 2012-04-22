using System;

namespace Shared.Communication.Messages.Requests
{
	/// <summary>
	/// Request voor GetPouleLadder
	/// </summary>
	[Serializable]
	public class RequestGetPouleLadder : RequestMessage
	{
		#region Fields

		/// <summary>
		/// ID van de Poule waarvan de Ladder opgevraagd wordt
		/// </summary>
		public readonly int PouleID;

		#endregion

		#region Constructors

		/// <summary>
		/// Maak een Request aan voor de Ladder van een bepaalde Poule
		/// </summary>
		/// <param name="PouleID">ID van de Poule om de Ladder van op te vragen</param>
		public RequestGetPouleLadder(int PouleID)
		{
			this.PouleID = PouleID;
		}

		#endregion

		/// <summary>
		/// RequestType van deze message
		/// </summary>
		public override RequestTypes RequestType
		{
			get { return RequestTypes.GetPouleLadder; }
		}
	}
}
