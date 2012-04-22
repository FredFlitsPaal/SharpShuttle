using System;

namespace Shared.Communication
{
	/// <summary>
	/// MessageTypes behoudt een lijst met alle berichttypes die tussen server en client kunnen
	/// worden gecommuniceerd
	/// </summary>
	public enum MessageTypes
	{ 
		/// <summary>
		/// Message type van een placeholder voor een niet geimplementeerd bericht
		/// </summary>
		placeholder = 0,
		/// <summary>
		/// Message type van het welkomst bericht van de client aan de server
		/// </summary>
		PING = 1,
		/// <summary>
		/// Message type van het welkomst bericht dat de server stuurt na een PING
		/// </summary>
		PONG = 2,
		/// <summary>
		/// Message type van een container voor een verzoek aan de server
		/// </summary>
		REQUEST = 3,
		/// <summary>
		/// Message type van een antwoord van de server
		/// </summary>
		RESPONSE = 4,
		/// <summary>
		/// Message type van een serial update message
		/// </summary>
		SERIALUPDATE = 5,
		/// <summary>
		/// Message type van de afsluitende message
		/// </summary>
		GOODBYE = -int.MaxValue
	}

	/// <summary>
	/// Message is het meest basale bericht dat geserialiseerd kan worden
	/// </summary>
	[Serializable]
	public class Message
	{
		#region Properties

		/// <summary>
		/// De MessageType van deze Message
		/// </summary>
		public MessageTypes Type
		{
			get;
			set;
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Een standaard placeholder message
		/// </summary>
		public Message()
		{
			Type = MessageTypes.placeholder;
		}

		/// <summary>
		/// Een message met een bepaald type
		/// </summary>
		/// <param name="Type">MessageType van deze message</param>
		public Message(MessageTypes Type)
		{
			this.Type = Type;
		}

		#endregion
	}
}
