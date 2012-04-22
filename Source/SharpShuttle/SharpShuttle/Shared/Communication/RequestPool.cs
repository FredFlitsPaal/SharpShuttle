using System;
using System.Collections.Generic;
using System.Threading;
using Shared.Datastructures;
using Shared.Communication;

namespace Shared.Communication
{
	/// <summary>
	/// De RequestPool houdt een lijst met actieve requests naar de servers bij.
	/// Deze pool zorgt er ook voor dat respectievelijke antwoorden van de server ingevuld worden
	/// bij de desbetreffende RequestContainer
	/// </summary>
	internal class RequestPool
	{
		#region Fields

		/// <summary>
		/// Mutex om operaties op activeRequests te synchronizeren
		/// </summary>
		private Mutex activeRequestsMutex = new Mutex(false);
		/// <summary>
		/// Een dictionary met alle Requests die nog niet beantwoord zijn
		/// </summary>
		private Dictionary<long, RequestContainer> activeRequests = new Dictionary<long, RequestContainer>(64);
		/// <summary>
		/// AtomicCounter om MessageID's te genereren
		/// </summary>
		private AtomicCounter currentMessageID = new AtomicCounter();

		#endregion

		#region Methods

		/// <summary>
		/// AddRequest Maakt een RequestContainer aan met daarin een WaitHandle voor het antwoord van de server
		/// </summary>
		/// <remarks>
		/// Een hogere abstractielaag moet ervoor zorgen dat het originele verzoek in de RequestContainer
		/// daadwerkelijk naar de server wordt verzonden.
		/// </remarks>
		/// <param name="message">Het bericht met daarin de vraag naar de server</param>
		/// <returns>Een RequestContainer met daarin het antwoord veld</returns>
		public RequestContainer AddRequest(RequestMessage message)
		{
			//Kijk wat het volgende berichtid wordt.
			long MessageID = currentMessageID.IncreaseAndGetValue();

			//Vul het MessageID in bij de request
			message.MessageID = MessageID;

			//Maak nu een nieuwe RequestContainer aan
			RequestContainer container = new RequestContainer(message);

			//Nu moeten we de RequestContainer gaan toevoegen aan onze lokale poule van requests
			//daarvoor moeten we eerst totale exclusiviteit van de lijst hebben
			activeRequestsMutex.WaitOne();

			//Voeg het bericht toe aan de lijst met lopende requests
			activeRequests.Add(MessageID, container);

			//Nu geven we de lijst weer vrij
			activeRequestsMutex.ReleaseMutex();

			return container;

			//Let op, een hogere implementatie moet er nu voor zorgen dat het bericht verzonden wordt
		}

		/// <summary>
		/// HandleRequest handelt de bericht geving van het antrwoord af.
		/// Daartoe wordt eerst gecontroleerd of het berichtid in de response wel bestaat
		/// </summary>
		/// <param name="response">Een ResponseMessage van de server met daarin het antwoord</param>
		internal void HandleRequest(ResponseMessage response)
		{
			RequestContainer request = null;
			bool hasRequest = false;

			//Ga eerst onderzoeken of het request met het MessageID bestaat in de poule
			//daarvoor hebben we totale exclusiviteit nodig
			activeRequestsMutex.WaitOne();

			if (activeRequests.TryGetValue(response.MessageID, out request))
			{
				hasRequest = true;
				activeRequests.Remove(response.MessageID);
			}

			//Geef de lijst nu weer vrij
			activeRequestsMutex.ReleaseMutex();

			if (hasRequest)
			{
				//Maak het antwoord toegangkelijk.
				request.answer = response;
				request.Handle.Set();
			}
		}

		/// <summary>
		/// Verwijder een Request
		/// </summary>
		/// <param name="MessageID">ID van de Request om te verwijderen</param>
		internal void RemoveRequest(long MessageID)
		{
			//omdat we toch wat in de lijst willen veranderen,
			//moeten we toegang krijgen
			activeRequestsMutex.WaitOne();

			//Verwijder het element
			activeRequests.Remove(MessageID);

			//En we geven de lijst weer vrij
			activeRequestsMutex.ReleaseMutex();
		}

		#endregion
	}
}
