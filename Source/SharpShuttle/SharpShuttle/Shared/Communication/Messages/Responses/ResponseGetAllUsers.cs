using System;
using System.Collections.Generic;
using Shared.Domain;

namespace Shared.Communication.Messages.Responses
{
	/// <summary>
	/// Response voor GetAllUsers
	/// </summary>
	[Serializable]
	public class ResponseGetAllUsers : ResponseMessage
	{
		#region Fields

		/// <summary>
		/// De lijst met alle Users
		/// </summary>
		public List<User> Users;

		#endregion

		#region Constructors

		/// <summary>
		/// Maakt een nieuwe Response voor GetAllUsers
		/// </summary>
		/// <param name="MessageID">ID van de Request waarop dit het antwoord is</param>
		public ResponseGetAllUsers(long MessageID) : base(MessageID) { }

		#endregion
	}
}
