using System;

namespace Shared.Communication.Messages.Responses
{
	/// <summary>
	/// Response voor SetPouleNextRound
	/// </summary>
	[Serializable]
	public class ResponseSetPouleNextRound : ResponseMessage
	{
		/// <summary>
		/// Maakt een nieuwe Response voor SetPouleNextRound
		/// </summary>
		/// <param name="MessageID">ID van de Request waarop dit het antwoord is</param>
		public ResponseSetPouleNextRound(long MessageID)
			: base(MessageID)
		{

		}
	}
}
