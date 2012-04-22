using System;

namespace Shared.Communication.Messages.Requests
{
	/// <summary>
	/// Request voor GetPouleMatches
	/// </summary>
	[Serializable]
	public class RequestGetPouleMatches : RequestMessage
	{
		#region Fields

		/// <summary>
		/// ID van de Poule waarvan de Matches opgevraagd worden
		/// </summary>
		public readonly int PouleID;

		#endregion

		#region Constructors

		/// <summary>
		/// Maakt een Request voor de Matches van een bepaalde Poule
		/// </summary>
		/// <param name="PouleID">ID van de Poule om de Matches van op te vragen</param>
		public RequestGetPouleMatches(int PouleID)
		{
			this.PouleID = PouleID;
		}

		#endregion

		/// <summary>
		/// RequestType van deze message
		/// </summary>
		public override RequestTypes RequestType
		{
			get { return RequestTypes.GetPouleMatches; }
		}
	}
}
