using System;

namespace Shared.Communication.Messages.Requests
{
	/// <summary>
	/// Request voor GetPoulePlanning
	/// </summary>
	[Serializable]
	public class RequestGetPoulePlanning : RequestMessage
	{
		#region Fields

		/// <summary>
		/// ID van de Poule om de Planning van op te vragen
		/// </summary>
		public readonly int PouleID;

		#endregion

		#region Constructors

		/// <summary>
		/// Maakt een Request voor de Planning van een bepaalde Poule
		/// </summary>
		/// <param name="PouleID">ID van de Poule om de Planning van op te vragen</param>
		public RequestGetPoulePlanning(int PouleID)
		{
			this.PouleID = PouleID;
		}

		#endregion

		/// <summary>
		/// RequestType van deze message
		/// </summary>
		public override RequestTypes RequestType
		{
			get { return RequestTypes.GetPoulePlanning; }
		}
	}
}
