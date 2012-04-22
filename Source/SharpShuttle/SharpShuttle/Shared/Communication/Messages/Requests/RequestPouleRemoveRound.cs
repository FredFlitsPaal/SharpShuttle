using System;

namespace Shared.Communication.Messages.Requests
{
	/// <summary>
	/// Request voor PouleRemoveRound
	/// </summary>
	[Serializable]
	public class RequestPouleRemoveRound : RequestMessage
	{
		#region Fields

		/// <summary>
		/// ID van de Poule waarop RemoveRound wordt gedaan
		/// </summary>
		public readonly int PouleID;

		#endregion

		#region Constructors

		/// <summary>
		/// Maakt een request voor RemoveRound voor een bepaalde Poule
		/// </summary>
		/// <param name="PouleID">ID van de Poule waarop RemoveRound wordt gedaan</param>
		public RequestPouleRemoveRound(int PouleID)
		{
			this.PouleID = PouleID;
		}

		#endregion

		/// <summary>
		/// RequestType van deze message
		/// </summary>
		public override RequestTypes RequestType
		{
			get { return RequestTypes.PouleRemoveRound; }
		}
	}
}
