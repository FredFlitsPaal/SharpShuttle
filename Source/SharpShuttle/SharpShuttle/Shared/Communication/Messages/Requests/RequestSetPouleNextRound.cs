using System;
using System.Collections.Generic;
using Shared.Domain;

namespace Shared.Communication.Messages.Requests
{
	/// <summary>
	/// Request voor SetPouleNextRound
	/// </summary>
	[Serializable]
	public class RequestSetPouleNextRound : RequestMessage
	{
		#region Fields

		/// <summary>
		/// ID van de Poule waarvan de volgende ronde geset wordt
		/// </summary>
		public readonly int PouleID;
		/// <summary>
		/// De lijst met Matches voor de volgende ronde
		/// </summary>
		public readonly List<Match> Matches;

		#endregion

		#region Constructors

		/// <summary>
		/// Maakt een Request voor SetPouleNextRound
		/// </summary>
		/// <param name="PouleID">ID van de Poule waarvan de volgende ronde geset wordt</param>
		/// <param name="Matches">De lijst met Matches voor de volgende ronde</param>
		public RequestSetPouleNextRound(int PouleID, List<Match> Matches)
		{
			this.PouleID = PouleID;
			this.Matches = Matches;
		}

		#endregion

		/// <summary>
		/// RequestType van deze message
		/// </summary>
		public override RequestTypes RequestType
		{
			get { return RequestTypes.SetPouleNextRound; }
		}
	}
}
