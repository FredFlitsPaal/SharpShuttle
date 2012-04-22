using System;
using Shared.Domain;
using System.Collections.Generic;

namespace Shared.Communication.Messages.Responses
{
	/// <summary>
	/// Response voor GetPouleTeams
	/// </summary>
	[Serializable]
	public class ResponseGetPouleTeams : ResponseMessage
	{
		#region Fields

		/// <summary>
		/// De Teams van de Poule
		/// </summary>
		public List<Team> Teams;

		#endregion

		#region Constructors

		/// <summary>
		/// Maakt een nieuwe Response voor GetPouleTeams
		/// </summary>
		/// <param name="MessageID">ID van de Request waarop dit het antwoord is</param>
		public ResponseGetPouleTeams(long MessageID)
			: base(MessageID)
		{

		}

		#endregion
	}
}