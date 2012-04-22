using System;
using Shared.Domain;

namespace Shared.Communication.Messages.Requests
{
	/// <summary>
	/// Request voor SetMatch
	/// </summary>
	[Serializable]
	public class RequestSetMatch : RequestMessage
	{
		#region Fields

		/// <summary>
		/// De Match die geset wordt
		/// </summary>
		public readonly Match match;

		#endregion

		#region Constructors

		/// <summary>
		/// Maakt een Request voor SetMatch
		/// </summary>
		/// <param name="match">De Match dei geset wordt</param>
		public RequestSetMatch(Match match)
		{
			this.match = match;
		}

		#endregion

		/// <summary>
		/// RequestType van deze message
		/// </summary>
		public override RequestTypes RequestType
		{
			get { return RequestTypes.SetMatch; }
		}
	}
}
