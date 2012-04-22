using System;
using System.Collections.Generic;
using Shared.Domain;

namespace Shared.Communication.Messages.Responses
{
	/// <summary>
	/// Response voor GetPouleLadder
	/// </summary>
	[Serializable]
	public class ResponseGetPouleLadder : ResponseMessage
	{
		#region Fields

		/// <summary>
		/// De Ladder van de Poule
		/// </summary>
		public List<LadderTeam> Ladder;

		#endregion

		#region Constructors

		/// <summary>
		/// Maakt een nieuwe Response voor GetPouleHistoryMatches
		/// </summary>
		/// <param name="MessageID">ID van de Request waarop dit het antwoord is</param>
		public ResponseGetPouleLadder(long MessageID) : base(MessageID) { }

		#endregion
	}
}