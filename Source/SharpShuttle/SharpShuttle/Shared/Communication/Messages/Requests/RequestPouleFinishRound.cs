using System;

namespace Shared.Communication.Messages.Requests
{
	/// <summary>
	/// Request voor PouleFinishRound
	/// </summary>
	[Serializable]
	public class RequestPouleFinishRound : RequestMessage
	{
		#region Fields

		/// <summary>
		/// ID van de Poule waarop FinishRounds wordt gedaan
		/// </summary>
		public readonly int PouleID;

		#endregion

		#region Constructors

		/// <summary>
		/// Maak een Request voor FinishRound voor een bepaalde Poule
		/// </summary>
		/// <param name="PouleID">ID van de Poule waarop FinishRounds wordt gedaan</param>
		public RequestPouleFinishRound(int PouleID)
		{
			this.PouleID = PouleID;
		}

		#endregion

		/// <summary>
		/// RequestType van deze message
		/// </summary>
		public override RequestTypes RequestType
		{
			get { return RequestTypes.PouleFinishRound; }
		}
	}
}
