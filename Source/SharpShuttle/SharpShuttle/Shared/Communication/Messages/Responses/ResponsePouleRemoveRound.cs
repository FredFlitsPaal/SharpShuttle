using System;

namespace Shared.Communication.Messages.Responses
{
	/// <summary>
	/// Response voor PouleRemoveRound
	/// </summary>
	[Serializable]
	public class ResponsePouleRemoveRound : ResponseMessage
	{
		/// <summary>
		/// Maakt een nieuwe Response voor PouleRemoveRound
		/// </summary>
		/// <param name="MessageID">ID van de Request waarop dit het antwoord is</param>
		public ResponsePouleRemoveRound(long MessageID) : base(MessageID) { }
	}
}
