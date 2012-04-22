using System;

namespace Shared.Communication.Messages.Responses
{
	/// <summary>
	/// Response voor SetAllPoules
	/// </summary>
	[Serializable]
	public class ResponseSetAllPoules : ResponseMessage
	{
		/// <summary>
		/// Maakt een nieuwe Response voor SetAllPoules
		/// </summary>
		/// <param name="MessageID">ID van de Request waarop dit het antwoord is</param>
		public ResponseSetAllPoules(long MessageID) : base(MessageID) { }
	}
}
