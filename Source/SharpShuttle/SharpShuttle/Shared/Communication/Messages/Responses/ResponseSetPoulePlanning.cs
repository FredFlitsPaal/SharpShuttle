using System;

namespace Shared.Communication.Messages.Responses
{
	/// <summary>
	/// Response voor SetPoulePlanning
	/// </summary>
	[Serializable]
	public class ResponseSetPoulePlanning : ResponseMessage
	{
		/// <summary>
		/// Maakt een nieuwe Response voor SetPoulePlanning
		/// </summary>
		/// <param name="MessageID">ID van de Request waarop dit het antwoord is</param>
		public ResponseSetPoulePlanning(long MessageID) : base(MessageID) { }
	}
}
