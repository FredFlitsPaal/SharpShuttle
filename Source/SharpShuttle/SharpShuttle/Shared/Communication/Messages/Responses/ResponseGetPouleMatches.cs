using System;
using Shared.Domain;
using System.Collections.Generic;

namespace Shared.Communication.Messages.Responses
{
	/// <summary>
	/// Response voor GetPouleMatches
	/// </summary>
	[Serializable]
	public class ResponseGetPouleMatches : ResponseMessage
	{
		#region Fields

		/// <summary>
		/// De Matches van de Poule
		/// </summary>
		public List<Match> Matches;

		#endregion

		#region Constructors

		/// <summary>
		/// Maakt een nieuwe Response voor GetPouleMatches
		/// </summary>
		/// <param name="MessageID">ID van de Request waarop dit het antwoord is</param>
		public ResponseGetPouleMatches(long MessageID)
			: base(MessageID)
		{

		}

		#endregion
	}
}
