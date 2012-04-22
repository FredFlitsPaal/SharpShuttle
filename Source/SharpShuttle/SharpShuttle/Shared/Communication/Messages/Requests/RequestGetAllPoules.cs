using System;

namespace Shared.Communication.Messages.Requests
{
	/// <summary>
	/// Request voor GetAllPoules
	/// </summary>
	[Serializable]
	public class RequestGetAllPoules : RequestMessage
	{
		/// <summary>
		/// RequestType van deze message
		/// </summary>
		public override RequestTypes RequestType
		{
			get { return RequestTypes.GetAllPoules; }
		}
	}
}