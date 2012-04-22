using System;
using System.Collections.Generic;
using Shared.Domain;

namespace Shared.Communication.Messages.Responses
{
	/// <summary>
	/// Response voor GetPouleHistoryMatches
	/// </summary>
	[Serializable]
	public class ResponseGetPouleHistoryMatches : ResponseMessage
	{
		#region Fields

		/// <summary>
		/// De HistoryMatches van de Poule
		/// </summary>
		public List<Match> Matches;

		#endregion

		#region Constructors

		/// <summary>
		/// Maakt een nieuwe Response voor GetPouleHistoryMatches
		/// </summary>
		/// <param name="MessageID">ID van de Request waarop dit het antwoord is</param>
		public ResponseGetPouleHistoryMatches(long MessageID)
			: base(MessageID)
		{

		}

		#endregion
	}
}