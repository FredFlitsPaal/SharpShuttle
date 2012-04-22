using System;

namespace Shared.Communication.Messages.Requests
{
	/// <summary>
	/// Request voor GetAllMatches
	/// </summary>
	[Serializable]
	public class RequestGetAllMatches : RequestMessage
	{
		/// <summary>
		/// RequestType van deze message
		/// </summary>
		public override RequestTypes RequestType
		{
			get { return RequestTypes.GetAllMatches; }
		}
	}
}
