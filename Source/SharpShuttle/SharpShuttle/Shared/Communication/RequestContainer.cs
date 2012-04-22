using System;
using System.Threading;
using Shared.Communication.Exceptions;
using Shared.Logging;

namespace Shared.Communication
{
	/// <summary>
	/// Een wrapper class om een Request heen, wacht automatisch op het antwoord
	/// </summary>
	public class RequestContainer
	{
		#region Fields

		/// <summary>
		/// De timeout voor het ontvangen van een response (15 seconden)
		/// </summary>
		private const int WAITTIMEOUT = 15000;
		/// <summary>
		/// Het antwoord, als het ontvangen is
		/// </summary>
		internal ResponseMessage answer;
		/// <summary>
		/// De Request waarmee deze RequestContainer gemaakt is
		/// </summary>
		internal RequestMessage request;
		/// <summary>
		/// De waithandle waarmee gewacht wordt op een Response (wordt geset in de RequestPool)
		/// </summary>
		internal ManualResetEvent Handle = new ManualResetEvent(false);
		/// <summary>
		/// Geeft aan of er een antwoord ontvangen is
		/// </summary>
		private bool answerReceived = false;
		/// <summary>
		/// Geeft aan of er een timeout was
		/// </summary>
		private bool error = false;

		#endregion

		#region Properties

		/// <summary>
		/// Het antwoord op de Request <para/>
		/// Wacht tot 15 seconden, als er dan geen antwoord is CommunicationTimeOutException
		/// </summary>
		public object Answer
		{
			get
			{
				if (answerReceived)
				{
					return FormulateAnswer();
				}

				try
				{
					//stel dat we het antwoord nog niet hebben gehad,
					//dan moeten we wachten op het antwoord:
					if (Handle.WaitOne(WAITTIMEOUT))
					{
						//We hebben een set op de handle gekregen, dus hebben we antwoord
						answerReceived = true;
						return FormulateAnswer();
					}
				}

				catch (Exception e)
				{
					Logger.Write("RequestContainer.Answer", e.ToString());
				}

				//Als de uitkomst van de WaitOne false is (of een fout), dan is er iets fout:
				answerReceived = true;
				error = true;

				Communication.Instance.RemoveRequest(request.MessageID);
				throw new CommunicationTimeOutException();
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Maak een nieuwe RequestContainer voor een RequestMessage
		/// </summary>
		public RequestContainer(RequestMessage OriginalRequest)
		{
			request = OriginalRequest;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Returnt answer als er geen error is <para/>
		/// </summary>
		/// <exception cref="CommunicationTimeOutException">Als er een time out was bij het ontvangen van het antwoord</exception>
		private object FormulateAnswer()
		{
			if (error)
			{
				//Als het antwoord een timeout is geweest dan geven we een error terug
				throw new CommunicationTimeOutException();
			}

			//Het type klopt, dus kunnen we het resultaat teruggeven
			return answer;
		}

		#endregion
	}
}
