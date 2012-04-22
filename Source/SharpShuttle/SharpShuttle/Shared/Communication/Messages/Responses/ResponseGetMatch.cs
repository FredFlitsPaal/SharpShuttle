using System;
using Shared.Domain;

namespace Shared.Communication.Messages.Responses
{
	/// <summary>
	/// Response voor GetMatch
	/// </summary>
	[Serializable]
	public class ResponseGetMatch : ResponseMessage
	{
		#region Fields

		/// <summary>
		/// De Match
		/// </summary>
		public Match Match;

		#endregion

		#region Constructors

		/// <summary>
		/// Maakt een nieuwe Response voor GetMatch
		/// </summary>
		/// <param name="MessageID">ID van de Request waarop dit het antwoord is</param>
		public ResponseGetMatch(long MessageID)
			: base(MessageID)
		{

		}

		#endregion
	}
}