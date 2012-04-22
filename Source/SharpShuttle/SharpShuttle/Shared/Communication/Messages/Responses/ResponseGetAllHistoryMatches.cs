using System;
using System.Collections.Generic;
using Shared.Domain;

namespace Shared.Communication.Messages.Responses
{
	/// <summary>
	/// Response voor GetAllHistoryMatches
	/// </summary>
	[Serializable]
	public class ResponseGetAllHistoryMatches : ResponseMessage
	{
		#region Fields

		/// <summary>
		/// De lijst met HistoryMatches
		/// </summary>
		public List<Match> Matches;

		#endregion

		#region Constructors

		/// <summary>
		/// Maakt een Response voor GetAllHistoryMatches
		/// </summary>
		/// <param name="MessageID">ID van de Request waarop dit het antwoord is</param>
		public ResponseGetAllHistoryMatches(long MessageID)
			: base(MessageID)
		{

		}

		#endregion
	}
}
