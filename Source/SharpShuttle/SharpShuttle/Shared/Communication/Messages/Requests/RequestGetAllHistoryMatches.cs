using System;

namespace Shared.Communication.Messages.Requests
{
	/// <summary>
	/// Request voor GetAllHistoryMatches
	/// </summary>
	[Serializable]
	public class RequestGetAllHistoryMatches : RequestMessage
	{
		/// <summary>
		/// RequestType van deze message
		/// </summary>
		public override RequestTypes RequestType
		{
			get { return RequestTypes.GetAllHistoryMatches; }
		}
	}
}
