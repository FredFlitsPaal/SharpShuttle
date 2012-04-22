using System;

namespace Shared.Communication.Messages.Requests
{
	/// <summary>
	/// Request voor GetMatch
	/// </summary>
	[Serializable]
	public class RequestGetMatch : RequestMessage
	{
		#region Fields

		/// <summary>
		/// ID van de match om op te vragen
		/// </summary>
		public readonly int MatchID;

		#endregion

		#region Constructors

		/// <summary>
		/// Maak een Request voor een Match met een bepaald MatchID
		/// </summary>
		/// <param name="MatchID">ID van de Match om op te vragen</param>
		public RequestGetMatch(int MatchID)
		{
			this.MatchID = MatchID;
		}

		#endregion

		/// <summary>
		/// RequestType van deze message
		/// </summary>
		public override RequestTypes RequestType
		{
			get { return RequestTypes.GetMatch; }
		}
	}
}
