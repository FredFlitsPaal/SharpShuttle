using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared.Communication.Messages.Responses
{
	/// <summary>
	/// Response voor SetSettings
	/// </summary>
	[Serializable]
	public class ResponseSetSettings : ResponseMessage
	{
		/// <summary>
		/// Maakt een nieuwe Response voor SetSettings
		/// </summary>
		/// <param name="MessageID">ID van de Request waarop dit het antwoord is</param>
		public ResponseSetSettings(long MessageID) : base(MessageID) { }
	}
}
