using System;
using System.Runtime.Serialization;

namespace Shared.Communication.Exceptions
{
	/// <summary>
	/// Standaard CommunicationException
	/// </summary>
	[Serializable]
	public class CommunicationException : Exception, ISerializable
	{
		#region Constructors

		/// <summary>
		/// Standaard constructor
		/// </summary>
		public CommunicationException() { }
		/// <summary>
		/// Maak een CommunicationException met een message
		/// </summary>
		public CommunicationException(string Message) : base(Message) { }
		/// <summary>
		/// Voor serialization
		/// </summary>
		public CommunicationException(SerializationInfo info, StreamingContext context) : base(info, context) { }

		#endregion

		#region ISerializable Members

		/// <summary>
		/// Voor serialization
		/// </summary>
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		#endregion
	}

	/// <summary>
	/// Exception die gegooid wordt bij het proberen te casten van een antwoord naar een onjuist type
	/// </summary>
	[Serializable]
	public class WrongTypedAnswerException : CommunicationException, ISerializable
	{
		#region Constructors

		/// <summary>
		/// Standaard constructor
		/// </summary>
		public WrongTypedAnswerException() { }
		/// <summary>
		/// Voor serialization
		/// </summary>
		public WrongTypedAnswerException(SerializationInfo info, StreamingContext context) : base(info, context) { }

		#endregion

		#region ISerializable Members

		/// <summary>
		/// Voor serialization
		/// </summary>
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		#endregion
	}

	/// <summary>
	/// Exception die gegooid wordt als er een Set operatie gedaan wordt op data die out of date is
	/// </summary>
	[Serializable]
	public class DataOutOfDateException : CommunicationException, ISerializable
	{
		#region Constructors

		/// <summary>
		/// Standaard constructor
		/// </summary>
		public DataOutOfDateException() { }
		/// <summary>
		/// Voor serialization
		/// </summary>
		public DataOutOfDateException(SerializationInfo info, StreamingContext context) : base(info, context) { }

		#endregion

		#region ISerializable Members

		/// <summary>
		/// Voor serialization
		/// </summary>
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		#endregion
	}

	/// <summary>
	/// Exception die gegooid wordt als een object uit de database verwijderd wordt dat op andere plekken in de database nog gebruikt wordt
	/// </summary>
	[Serializable]
	public class DataReferenceException : CommunicationException, ISerializable
	{
		#region Constructors

		/// <summary>
		/// Standaard constructor
		/// </summary>
		public DataReferenceException() { }
		/// <summary>
		/// Maak een DataReferenceException met een message
		/// </summary>
		public DataReferenceException(string AdditionalMessage) : base(AdditionalMessage) { }
		/// <summary>
		/// Voor serialization
		/// </summary>
		public DataReferenceException(SerializationInfo info, StreamingContext context) : base(info, context) { }

		#endregion

		#region ISerializable Members

		/// <summary>
		/// Voor serialization
		/// </summary>
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		#endregion
	}

	/// <summary>
	/// Exception die gegooid wordt als de opgevraagde data niet bestaat
	/// </summary>
	[Serializable]
	public class DataDoesNotExistsException : CommunicationException, ISerializable
	{
		#region Constructors

		/// <summary>
		/// Standaard constructor
		/// </summary>
		public DataDoesNotExistsException() { }
		/// <summary>
		/// Voor serialization
		/// </summary>
		public DataDoesNotExistsException(SerializationInfo info, StreamingContext context) : base(info, context) { }

		#endregion

		#region ISerializable Members

		/// <summary>
		/// Voor serialization
		/// </summary>
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		#endregion
	}

	/// <summary>
	/// Exception die gegooid wordt als je probeert om een poule te veranderen als hij al bezig is
	/// </summary>
	[Serializable]
	public class PouleAlreadyRunningException : CommunicationException, ISerializable
	{
		#region Constructors

		/// <summary>
		/// Standaard constructor
		/// </summary>
		public PouleAlreadyRunningException() { }
		/// <summary>
		/// Voor serialization
		/// </summary>
		public PouleAlreadyRunningException(SerializationInfo info, StreamingContext context) : base(info, context) { }

		#endregion

		#region ISerializable Members

		/// <summary>
		/// Voor serialization
		/// </summary>
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		#endregion
	}

	/// <summary>
	/// Exception die optreedt als er nog matches in een poule zijn zonder score
	/// </summary>
	[Serializable]
	public class PouleMatchScoreNotCompleteException : CommunicationException, ISerializable
	{
		#region Constructors

		/// <summary>
		/// Standaard constructor
		/// </summary>
		public PouleMatchScoreNotCompleteException() { }
		/// <summary>
		/// Voor serialization
		/// </summary>
		public PouleMatchScoreNotCompleteException(SerializationInfo info, StreamingContext context) : base(info, context) { }

		#endregion

		#region ISerializable Members

		/// <summary>
		/// Voor serialization
		/// </summary>
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		#endregion
	}

	/// <summary>
	/// Exception die gegooid wordt als de server niet optijd antwoort
	/// </summary>
	[Serializable]
	public class CommunicationTimeOutException : CommunicationException, ISerializable
	{
		#region Constructors

		/// <summary>
		/// Standaard constructor
		/// </summary>
		public CommunicationTimeOutException() : base("Communication timeout detected") { }
		/// <summary>
		/// Voor serialization
		/// </summary>
		public CommunicationTimeOutException(SerializationInfo info, StreamingContext context) : base(info, context) { }

		#endregion

		#region ISerializable Members

		/// <summary>
		/// Voor serialization
		/// </summary>
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		#endregion
	}

	/// <summary>
	/// Exception die gegooid wordt als er operaties op de datacache gedaan worden maar er is geen connectie naar de server
	/// </summary>
	[Serializable]
	public class NotConnectedException : CommunicationException, ISerializable
	{
		#region Constructors

		/// <summary>
		/// Standaard constructor
		/// </summary>
		public NotConnectedException() { }
		/// <summary>
		/// Voor serialization
		/// </summary>
		public NotConnectedException(SerializationInfo info, StreamingContext context) : base(info, context) { }

		#endregion

		#region ISerializable Members

		/// <summary>
		/// Voor serialization
		/// </summary>
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		#endregion
	}
}