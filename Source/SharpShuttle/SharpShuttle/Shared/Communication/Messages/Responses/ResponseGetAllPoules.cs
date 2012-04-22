using System;
using System.Collections.Generic;
using Shared.Domain;

namespace Shared.Communication.Messages.Responses
{
	/// <summary>
	/// Response voor GetAllPoules
	/// </summary>
	[Serializable]
	public class ResponseGetAllPoules : ResponseMessage
	{
		#region Fields

		/// <summary>
		/// De lijst met alle poules
		/// </summary>
		public List<Poule> Poules;

		#endregion

		#region Constructors

		/// <summary>
		/// Maakt een Response voor GetAllPoules
		/// </summary>
		/// <param name="MessageID">ID van de Request waarop dit het antwoord is</param>
		public ResponseGetAllPoules(long MessageID) : base(MessageID) { }

		#endregion
	}
}
