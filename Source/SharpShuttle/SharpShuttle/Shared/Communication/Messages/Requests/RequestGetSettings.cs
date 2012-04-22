using System;

namespace Shared.Communication.Messages.Requests
{
	/// <summary>
	/// Request voor GetSettings
	/// </summary>
	[Serializable]
	public class RequestGetSettings : RequestMessage
	{
		/// <summary>
		/// RequestType van deze message
		/// </summary>
		public override RequestTypes RequestType
		{
			get { return RequestTypes.GetSettings; }
		}
	}
}
