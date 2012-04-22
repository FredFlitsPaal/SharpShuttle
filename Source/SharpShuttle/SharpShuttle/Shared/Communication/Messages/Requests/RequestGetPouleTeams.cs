using System;

namespace Shared.Communication.Messages.Requests
{
	/// <summary>
	/// Request voor GetPouleTeams
	/// </summary>
	[Serializable]
	public class RequestGetPouleTeams : RequestMessage
	{
		#region Fields

		/// <summary>
		/// ID van de Poule om de Teams van op te vragen
		/// </summary>
		public readonly int PouleID;

		#endregion

		#region Constructors

		/// <summary>
		/// Maakt een Request voor de Teams van een bepaalde Poule
		/// </summary>
		/// <param name="PouleID">ID van de Poule om de Teams van op te vragen</param>
		public RequestGetPouleTeams(int PouleID)
		{
			this.PouleID = PouleID;
		}

		#endregion

		/// <summary>
		/// RequestType van deze message
		/// </summary>
		public override RequestTypes RequestType
		{
			get { return RequestTypes.GetPouleTeams; }
		}
	}
}
