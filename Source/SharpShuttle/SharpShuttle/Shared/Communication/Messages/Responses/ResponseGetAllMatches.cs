using System;
using System.Collections.Generic;
using Shared.Domain;

namespace Shared.Communication.Messages.Responses
{
	/// <summary>
	/// Response voor GetAllMatches
	/// </summary>
	[Serializable]
	public class ResponseGetAllMatches : ResponseMessage
	{
		#region Fields

		/// <summary>
		/// De lijst met alle matches
		/// </summary>
		public List<Match> Matches;

		#endregion

		#region Constructors

		/// <summary>
		/// Maakt een Response voor GetAllMatches
		/// </summary>
		/// <param name="MessageID">ID van de Request waarop dit het antwoord is</param>
		public ResponseGetAllMatches(long MessageID)
			: base(MessageID)
		{
		}

		#endregion
	}
}