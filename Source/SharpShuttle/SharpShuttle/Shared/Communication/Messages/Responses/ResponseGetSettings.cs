using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shared.Domain;

namespace Shared.Communication.Messages.Responses
{
	/// <summary>
	/// Response voor GetSettings
	/// </summary>
	[Serializable]
	public class ResponseGetSettings : ResponseMessage
	{
		#region Fields

		/// <summary>
		/// De Settings
		/// </summary>
		public TournamentSettings settings;

		#endregion

		#region Constructors

		/// <summary>
		/// Maakt een nieuwe Response voor GetPouleTeams
		/// </summary>
		/// <param name="MessageID">ID van de Request waarop dit het antwoord is</param>
		public ResponseGetSettings(long MessageID)
			: base(MessageID)
		{

		}

		#endregion
	}
}