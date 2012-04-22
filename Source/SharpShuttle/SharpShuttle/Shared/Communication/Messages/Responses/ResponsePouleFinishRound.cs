using System;

namespace Shared.Communication.Messages.Responses
{
	/// <summary>
	/// Response voor PouleFinishRound
	/// </summary>
	[Serializable]
	public class ResponsePouleFinishRound : ResponseMessage
	{
		/// <summary>
		/// Maakt een nieuwe Response voor PouleFinishRound
		/// </summary>
		/// <param name="MessageID">ID van de Request waarop dit het antwoord is</param>
		public ResponsePouleFinishRound(long MessageID)
			: base(MessageID)
		{

		}
	}
}