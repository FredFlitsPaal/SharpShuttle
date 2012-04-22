using System;

namespace Shared.Communication.Messages.Requests
{
	/// <summary>
	/// Request voor GetAllUsers
	/// </summary>
	[Serializable]
	public class RequestGetAllUsers : RequestMessage
	{
		/// <summary>
		/// RequestType van deze message
		/// </summary>
		public override RequestTypes RequestType
		{
			get { return RequestTypes.GetAllUsers; }
		}
	}
}