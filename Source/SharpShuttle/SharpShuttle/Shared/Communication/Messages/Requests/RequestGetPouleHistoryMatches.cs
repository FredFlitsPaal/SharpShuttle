using System;

namespace Shared.Communication.Messages.Requests
{
	/// <summary>
	/// Request voor GetPouleHistoryMatches
	/// </summary>
	[Serializable]
	public class RequestGetPouleHistoryMatches : RequestMessage
	{
		#region Fields

		/// <summary>
		/// PouleID van de Poule waarvan de HistoryMatches opgevraagd worden
		/// </summary>
		public readonly int PouleID;

		#endregion

		#region Constructors

		/// <summary>
		/// Maakt een Request aan voor de HistoryMatches van een bepaalde Poule
		/// </summary>
		/// <param name="PouleID">ID van de Poule om de HistoryMatches van op te vragen</param>
		public RequestGetPouleHistoryMatches(int PouleID)
		{
			this.PouleID = PouleID;
		}

		#endregion

		/// <summary>
		/// RequestType van deze message
		/// </summary>
		public override RequestTypes RequestType
		{
			get { return RequestTypes.GetPouleHistoryMatches; }
		}
	}
}
