using System;

namespace Shared.Communication.Messages.Responses
{
	/// <summary>
	/// Response voor SetAllPlayers
	/// </summary>
	[Serializable]
	public class ResponseSetAllPlayers : ResponseMessage
	{
		/// <summary>
		/// Maakt een nieuwe Response voor SetAllPlayers
		/// </summary>
		/// <param name="MessageID">ID van de Request waarop dit het antwoord is</param>
		public ResponseSetAllPlayers(long MessageID) : base(MessageID) { }
	}
}
