using System;

namespace Shared.Communication.Messages.Requests
{
	/// <summary>
	/// Request voor GetPoule
	/// </summary>
	[Serializable]
	public class RequestGetPoule : RequestMessage
	{
		#region Fields

		/// <summary>
		/// ID van de Poule om op te vragen
		/// </summary>
		public readonly int PouleID;

		#endregion

		#region Constructors

		/// <summary>
		/// Maakt een Request aan voor een Poule met een bepaald PouleID
		/// </summary>
		/// <param name="PouleID">ID van de Poule om op te vragen</param>
		public RequestGetPoule(int PouleID)
		{
			this.PouleID = PouleID;
		}

		#endregion

		/// <summary>
		/// RequestType van deze message
		/// </summary>
		public override RequestTypes RequestType
		{
			get { return RequestTypes.GetPoule; }
		}
	}
}
