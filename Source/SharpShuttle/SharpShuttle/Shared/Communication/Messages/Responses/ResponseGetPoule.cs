using System;
using Shared.Domain;

namespace Shared.Communication.Messages.Responses
{
	/// <summary>
	/// Response voor GetPoule
	/// </summary>
	[Serializable]
	public class ResponseGetPoule : ResponseMessage
	{
		#region Fields

		/// <summary>
		/// De Poule
		/// </summary>
		public Poule Poule;

		#endregion

		#region Constructors

		/// <summary>
		/// Maakt een nieuwe Response voor GetPoule
		/// </summary>
		/// <param name="MessageID">ID van de Request waarop dit het antwoord is</param>
		public ResponseGetPoule(long MessageID)
			: base(MessageID)
		{

		}

		#endregion
	}
}