using System;
using System.Collections.Generic;
using Shared.Domain;

namespace Shared.Communication.Messages.Responses
{
	/// <summary>
	/// Response voor GetPoulePlanning
	/// </summary>
	[Serializable]
	public class ResponseGetPoulePlanning : ResponseMessage
	{
		#region Fields

		/// <summary>
		/// De Planning van de Poule
		/// </summary>
		public List<Player> Players;

		#endregion

		#region Constructors

		/// <summary>
		/// Maakt een nieuwe Response voor GetPoulePlanning
		/// </summary>
		/// <param name="MessageID">ID van de Request waarop dit het antwoord is</param>
		public ResponseGetPoulePlanning(long MessageID)
			: base(MessageID)
		{
		}

		#endregion
	}
}