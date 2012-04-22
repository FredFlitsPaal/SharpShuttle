using System;

namespace Shared.Communication.Messages.Requests
{
	/// <summary>
	/// Request voor GetAllPlayers
	/// </summary>
	[Serializable]
	public class RequestGetAllPlayers : RequestMessage
	{
		/// <summary>
		/// RequestType van deze message
		/// </summary>
		public override RequestTypes RequestType
		{
			get { return RequestTypes.GetAllPlayers; }
		}
	}
}
