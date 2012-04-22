using System;

namespace Shared.Communication.Messages.Responses
{
	/// <summary>
	/// Response voor SetMatch
	/// </summary>
	[Serializable]
	public class ResponseSetMatch : ResponseMessage
	{
		/// <summary>
		/// Maakt een nieuwe Response voor SetMatch
		/// </summary>
		/// <param name="MessageID">ID van de Request waarop dit het antwoord is</param>
		public ResponseSetMatch(long MessageID)
			: base(MessageID)
		{

		}
	}
}
