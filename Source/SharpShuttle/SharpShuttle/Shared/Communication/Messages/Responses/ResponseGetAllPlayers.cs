using System;
using System.Collections.Generic;
using Shared.Domain;

namespace Shared.Communication.Messages.Responses
{
	/// <summary>
	/// Response voor GetAllPlayers
	/// </summary>
	[Serializable]
	public class ResponseGetAllPlayers : ResponseMessage
	{
		#region Fields

		/// <summary>
		/// De lijst met alle spelers
		/// </summary>
		public List<Player> Players;

		#endregion

		#region Constructors

		/// <summary>
		/// Maakt een nieuwe Response voor GetAllPlayers
		/// </summary>
		/// <param name="MessageID">ID van de Request waarop dit het antwoord is</param>
		public ResponseGetAllPlayers(long MessageID) : base(MessageID) { }

		#endregion
	}
}
