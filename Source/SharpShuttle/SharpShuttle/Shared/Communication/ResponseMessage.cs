using System;
using Shared.Communication.Exceptions;
using Shared.Communication.Serials;
using Shared.Communication.Messages;
using System.Collections.Generic;

namespace Shared.Communication
{
	/// <summary>
	/// Lege reponse message (zonder data)
	/// </summary>
	[Serializable]
	public class ResponseMessage : Message
	{
		#region Fields

		/// <summary>
		/// ID van de message waarop hij het antwoord is
		/// </summary>
		public long MessageID;
		/// <summary>
		/// De exception die de bijbehorende Request opleverde (null als er geen exception was)
		/// </summary>
		public CommunicationException Exception = null;
		/// <summary>
		/// Een lijst met SerialUpdates, voor ieder ding dat veranderd is
		/// </summary>
		public List<SerialUpdate> SerialUpdateMessages = new List<SerialUpdate>();
		/// <summary>
		/// De nieuwe SerialDefinitions
		/// </summary>
		public List<SerialDefinition> NewSerialDefinitions = new List<SerialDefinition>();

		#endregion

		#region Constructors

		private ResponseMessage() { this.Type = MessageTypes.RESPONSE; }

		/// <summary>
		/// Maak een nieuwe Response aan
		/// </summary>
		/// <param name="MessageID">ID van de request waarop dit het antwoord is</param>
		public ResponseMessage(long MessageID)
			: this()
		{
			this.MessageID = MessageID;
		}

		/// <summary>
		/// Maak een nieuwe Response aan met een exception er in
		/// </summary>
		/// <param name="Exception">De exception</param>
		/// <param name="MessageID">ID van de request waarop dit het antwoord is</param>
		public ResponseMessage(CommunicationException Exception, long MessageID)
			: this()
		{
			this.Exception = Exception;
			this.MessageID = MessageID;
		}

		#endregion
	}
}