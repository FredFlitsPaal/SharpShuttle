using System;
using System.Collections.Generic;
using System.Threading;
using Shared.Communication;

namespace Server.Communication
{
	/// <summary>
	/// MessagePool is de queue met berichten van de clients 
	/// </summary>
	public class MessagePool
	{
		/// <summary>
		/// De interne lijst van QueuedMessages
		/// </summary>
		private Queue<QueuedMessage> pool = new Queue<QueuedMessage>();
		/// <summary>
		/// Semafoor voor het beheren van de queue
		/// </summary>
		private Semaphore MessageSemaphore = new Semaphore(0, int.MaxValue);

		/// <summary>
		/// Default constructor
		/// </summary>
		public MessagePool() { }

		/// <summary>
		/// Stop een berichtje in de Queue
		/// </summary>
		/// <param name="ClientID"> De Client ID</param>
		/// <param name="message">Het bericht</param>
		public void Enqueue(long ClientID, Message message)
		{
			lock (pool)
			{
				pool.Enqueue(new QueuedMessage(ClientID, message));
			}
				
			MessageSemaphore.Release();
		}

		/// <summary>
		/// Haal een bericht op uit de Queue
		/// </summary>
		/// <returns></returns>
		public QueuedMessage Dequeue()
		{
			//Zolang we geen element uit de lijst hebben gewoon nog een keer ophalen
			while (true)
			{
				//Voor elk element in de Pool word de semafoor opgehoogd.
				MessageSemaphore.WaitOne();

				lock (pool)
				{
					//Maar het kan zijn dat hij is opgehoogd en daarna is geleegd
					if (pool.Count > 0)
						return pool.Dequeue();
				}
			}
		}

		/// <summary>
		/// Maak de Pool helemaal leeg zodat Dequeue blijft blokkeren
		/// </summary>
		public void Clear()
		{
			lock (pool)
			{
				pool.Clear();
				
			}
		}
	}

	/// <summary>
	/// Een Message die in een Queue staat
	/// </summary>
	public class QueuedMessage
	{
		/// <summary>
		/// De messge
		/// </summary>
		public Message Message;
		/// <summary>
		/// De client id
		/// </summary>
		public long ClientID;

		/// <summary>
		/// Maakt een QueuedMessage
		/// </summary>
		/// <param name="ClientID"> De Client ID </param>
		/// <param name="Message"> De Message </param>
		public QueuedMessage(long ClientID, Message Message)
		{
			this.ClientID = ClientID;
			this.Message = Message;
		}
	}
}
