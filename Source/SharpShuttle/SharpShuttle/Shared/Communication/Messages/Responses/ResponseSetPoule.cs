using System;

namespace Shared.Communication.Messages.Responses
{
	/// <summary>
	/// Response voor SetPoule
	/// </summary>
	[Serializable]
	public class ResponseSetPoule : ResponseMessage
	{
		/// <summary>
		/// Maakt een nieuwe Response voor SetPoule
		/// </summary>
		/// <param name="MessageID">ID van de Request waarop dit het antwoord is</param>
		public ResponseSetPoule(long MessageID) : base(MessageID) { }
	}
}
