using System;

namespace Shared.Communication.Messages.Responses
{
	/// <summary>
	/// Response voor SetPouleTeams
	/// </summary>
	[Serializable]
	public class ResponseSetPouleTeams : ResponseMessage
	{
		/// <summary>
		/// Maakt een nieuwe Response voor SetPouleTeams
		/// </summary>
		/// <param name="MessageID">ID van de Request waarop dit het antwoord is</param>
		public ResponseSetPouleTeams(long MessageID)
			: base(MessageID)
		{

		}
	}
}
