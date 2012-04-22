using System.Collections.Generic;
using Shared.Domain;
using Shared.Communication.Exceptions;

namespace Shared.Data
{
	/// <summary>
	/// De IDataCache interface beschrijft alle datauitwisselingen die bestaan tussen server en client
	/// </summary>
	public interface IDataCache
	{
		/// <summary>
		/// Vraag een lijst van alle mogelijke gebruikers in dit systeem
		/// </summary>
		/// <returns>Lijst van alle user objecten</returns>
		/// <exception cref="NotConnectedException">Als er geen verbinding is met de server</exception>
		/// <exception cref="CommunicationTimeOutException">Als de server er te lang over deed om een antwoord te formuleren</exception>
		List<User> GetAllUsers();

		/// <summary>
		/// Vraag een lijst van alle poules die je vervolgens kunt aanpassen / toevoegen / verwijderen
		/// </summary>
		/// <returns>
		/// Een lijst met alle poules die in dit toernooi worden gespeeld
		/// </returns>
		/// <exception cref="NotConnectedException">Als er geen verbinding is met de server</exception>
		/// <exception cref="CommunicationTimeOutException">Als de server er te lang over deed om een antwoord te formuleren</exception>
		ChangeTrackingList<Poule> GetAllPoules();

		/// <summary>
		/// Geef de server een lijstje met alle poules die er moeten zijn
		/// </summary>
		/// <param name="Poules">De lijst met poules die in dit toernooi worden gespeeld</param>
		/// <param name="exception">De fout die optreed bij het verwerken van de operatie</param>
		/// <returns>
		/// true als de methode succesvol is geeindigd, anders false
		/// </returns>
		/// <exception cref="NotConnectedException">Als er geen verbinding is met de server</exception>
		/// <exception cref="CommunicationTimeOutException">Als de server er te lang over deed om een antwoord te formuleren</exception>
		/// <exception cref="DataOutOfDateException">Als de lijst met poules verouderd is</exception>
		/// <remarks>
		/// Het verwijderen van een poule zal er ook voor zorgen dat alle gegevens die bij die poule horen worden verwijderd
		/// </remarks>
		bool SetAllPoules(ChangeTrackingList<Poule> Poules, out CommunicationException exception);

		/// <summary>
		/// Vraag een lijstje met alle spelers die in het systeem zijn gedefinieerd
		/// </summary>
		/// <returns>
		/// Een lijst met spelers die in dit toernooi spelen
		/// </returns>
		/// <exception cref="NotConnectedException">Als er geen verbinding is met de server</exception>
		/// <exception cref="CommunicationTimeOutException">Als de server er te lang over deed om een antwoord te formuleren</exception>
		ChangeTrackingList<Player> GetAllPlayers();

		/// <summary>
		/// Geef de server een lijstje met alle spelers van dit toernooi
		/// </summary>
		/// <param name="Players">De lijst van spelers die men wilt updaten</param>
		/// <param name="exception">De fout die optreed bij het verwerken van de operatie</param>
		/// <returns>
		/// true als de methode succesvol is geeindigd, anders false
		/// </returns>
		/// <exception cref="NotConnectedException">Als er geen verbinding is met de server</exception>
		/// <exception cref="CommunicationTimeOutException">Als de server er te lang over deed om een antwoord te formuleren</exception>
		/// <exception cref="DataReferenceException">Als men een speler probeert te verwijderen die ingedeeld is in een poule.</exception>
		/// <exception cref="DataOutOfDateException">Als de lijst met spelers verouderd is</exception>
		/// <remarks>
		/// Indien men een speler probeert te verwijderen die al bij een poule is ingepland wordt deze actie geblokkeerd.
		/// Men dient eerst de teams aan te passen waarin deze speler staat gedefinieerd.
		/// </remarks>
		bool SetAllPlayers(ChangeTrackingList<Player> Players, out CommunicationException exception);

		/// <summary>
		/// Vraag de server om gegevens van een bepaalde poule
		/// </summary>
		/// <param name="PouleID">De id van de poule waarvan met de gegevens wilt hebben</param>
		/// <returns>
		/// Een poule object met daarin de gegevens die kunnen worden veranderd
		/// </returns>
		/// <exception cref="NotConnectedException">Als er geen verbinding is met de server</exception>
		/// <exception cref="CommunicationTimeOutException">Als de server er te lang over deed om een antwoord te formuleren</exception>
		/// <exception cref="DataDoesNotExistsException">Als de gevraagde PouleID niet bestaat</exception>
		Poule GetPoule(int PouleID);

		/// <summary>
		/// Stuur de server nieuwe gegevens van een poule
		/// </summary>
		/// <param name="PouleID">De id van de poule die men wilt veranderen</param>
		/// <param name="Poule">Het poule object met de veranderingen</param>
		/// <param name="exception">De fout die optreed bij het verwerken van de operatie</param>
		/// <returns>
		/// true als de methode succesvol is geeindigd, anders false
		/// </returns>
		/// <exception cref="NotConnectedException">Als er geen verbinding is met de server</exception>
		/// <exception cref="CommunicationTimeOutException">Als de server er te lang over deed om een antwoord te formuleren</exception>
		/// <exception cref="DataDoesNotExistsException">Als de gevraagde PouleID niet bestaat</exception>
		/// <exception cref="DataOutOfDateException">Als het poule object verouderd is</exception>
		bool SetPoule(int PouleID, Poule Poule, out CommunicationException exception);

		/// <summary>
		/// Vraag de server een lijstje van spelers die ingepland zijn voor een bepaalde poule
		/// </summary>
		/// <param name="PouleID">De id van de poule waarvan men de planning wilt weten</param>
		/// <returns>
		/// Een lijst met spelers die in deze poule zijn ingepland
		/// </returns>
		/// <exception cref="NotConnectedException">Als er geen verbinding is met de server</exception>
		/// <exception cref="CommunicationTimeOutException">Als de server er te lang over deed om een antwoord te formuleren</exception>
		/// <exception cref="DataDoesNotExistsException">Als de gevraagde PouleID niet bestaat</exception>
		ChangeTrackingList<Player> GetPoulePlanning(int PouleID);

		/// <summary>
		/// Stuur de server een lijstje met de nieuwe planning die bij een poule hoort
		/// </summary>
		/// <param name="PouleID">De id van de pule waarvoor de planning wordt veranderd</param>
		/// <param name="Players">De lijst met spelers die voor deze poule zijn ingepland</param>
		/// <param name="exception">De fout die optreed bij het verwerken van de operatie</param>
		/// <returns>
		/// true als de methode succesvol is geeindigd, anders false
		/// </returns>
		/// <exception cref="NotConnectedException">Als er geen verbinding is met de server</exception>
		/// <exception cref="CommunicationTimeOutException">Als de server er te lang over deed om een antwoord te formuleren</exception>
		/// <exception cref="DataDoesNotExistsException">Als de gevraagde PouleID niet bestaat</exception>
		/// <exception cref="DataOutOfDateException">Als de lijst met spelers verouderd is</exception>
		/// <exception cref="DataReferenceException">Als een speler in de planning lijst niet bestaat</exception>
		bool SetPoulePlanning(int PouleID, ChangeTrackingList<Player> Players, out CommunicationException exception);

		/// <summary>
		/// Vraag de server een lijstje met alle teams die in een poule spelen
		/// </summary>
		/// <param name="PouleID">De id van de poule waarvan men de teams wilt weten</param>
		/// <returns>
		/// Een lijst met daarin alle teams die in de betreffende poule spelen
		/// </returns>
		/// <exception cref="NotConnectedException">Als er geen verbinding is met de server</exception>
		/// <exception cref="CommunicationTimeOutException">Als de server er te lang over deed om een antwoord te formuleren</exception>
		/// <exception cref="DataDoesNotExistsException">Als de gevraagde PouleID niet bestaat</exception>
		ChangeTrackingList<Team> GetPouleTeams(int PouleID);

		/// <summary>
		/// Stuur de server een lijstje teams die in een poule moeten spelen
		/// </summary>
		/// <param name="PouleID">De id van de poule waarvan de teams moeten worden aangepast</param>
		/// <param name="Teams">Het lijstje met de teamveranderingen</param>
		/// <param name="exception">De fout die optreed bij het verwerken van de operatie</param>
		/// <returns>
		/// true als de methode succesvol is geeindigd, anders false
		/// </returns>
		/// <exception cref="NotConnectedException">Als er geen verbinding is met de server</exception>
		/// <exception cref="CommunicationTimeOutException">Als de server er te lang over deed om een antwoord te formuleren</exception>
		/// <exception cref="DataDoesNotExistsException">Als de gevraagde PouleID niet bestaat</exception>
		/// <exception cref="DataOutOfDateException">Als de gegevens van de teams verouderd zijn</exception>
		/// <exception cref="PouleAlreadyRunningException">Als de poule al actief is, d.w.z. er zijn al wedstrijden ingepland. Op dit moment mogen de teams niet meer veranderen</exception>
		/// <remarks>Als men teams wilt toevoegen of verwijderen is dit alleen toegestaan als er nog geen wedstrijden zijn ingepland.
		/// Als dit wel het geval is zal de eerste ronde moeten worden ongedaan gemaakt. Dit kan alleen als er nog geen scores zijn ingevuld.
		/// Verder is het wel altijd mogelijk om de teamsamenstelling en de naam te veranderen zolang er maar geen teams worden
		/// toegevoegd of verwijderd.
		/// </remarks>
		bool SetPouleTeams(int PouleID, ChangeTrackingList<Team> Teams, out CommunicationException exception);

		/// <summary>
		/// Vraag de server om een gesorteerde ladderoverzicht
		/// </summary>
		/// <param name="PouleID">De id van de poule waar je de ladder van wilt weten</param>
		/// <returns>
		/// Een lijst met daarin per team de score statistieken
		/// </returns>
		/// <remarks>
		/// Deze lijst is readonly en wordt alleen aan de server kant veranderd
		/// </remarks>
		/// <exception cref="NotConnectedException">Als er geen verbinding is met de server</exception>
		/// <exception cref="CommunicationTimeOutException">Als de server er te lang over deed om een antwoord te formuleren</exception>
		/// <exception cref="DataDoesNotExistsException">Als de gevraagde PouleID niet bestaat</exception>
		List<LadderTeam> GetPouleLadder(int PouleID);

		/// <summary>
		/// Vraag de server om een lijst van wedstrijden die momenteel in een poule worden gespeeld
		/// </summary>
		/// <param name="PouleID">De id van de poule waar je de huidige wedstrijden van wilt weten</param>
		/// <returns>
		/// De lijst met wedstrijden die momenteel actief zijn in de gevraagde poule
		/// </returns>
		/// <remarks>
		/// De lijst houdt geen veranderingen bij maar de matches an sich kunnen wel worden geupdate door het aanroepen van SetMatch
		/// </remarks>
		/// <see cref="SetMatch"/>
		/// <exception cref="NotConnectedException">Als er geen verbinding is met de server</exception>
		/// <exception cref="CommunicationTimeOutException">Als de server er te lang over deed om een antwoord te formuleren</exception>
		/// <exception cref="DataDoesNotExistsException">Als de gevraagde PouleID niet bestaat</exception>
		List<Match> GetPouleMatches(int PouleID);

		/// <summary>
		/// Geef de server een lijst met nieuwe wedstrijden die in de volgende ronde van de poule moeten worden gespeeld
		/// </summary>
		/// <param name="PouleID">De id van de poule waarvan de nieuwe ronde moet worden ingevld</param>
		/// <param name="matches">De lijst met wedstrijden die in de volgende ronde gespeeld moeten worden</param>
		/// <param name="exception">De fout die optreed bij het verwerken van de operatie</param>
		/// <returns>
		/// true als de methode succesvol is geeindigd, anders false
		/// </returns>
		/// <exception cref="NotConnectedException">Als er geen verbinding is met de server</exception>
		/// <exception cref="CommunicationTimeOutException">Als de server er te lang over deed om een antwoord te formuleren</exception>
		/// <exception cref="DataDoesNotExistsException">Als de gevraagde PouleID niet bestaat</exception>
		/// <exception cref="PouleMatchScoreNotCompleteException">Als er nog wedstrijden in deze poule actief zijn waarvan de score nog niet is ingevuld</exception>
		bool SetPouleNextRound(int PouleID, List<Match> matches, out CommunicationException exception);

		/// <summary>
		/// Vraag de server om een lijst met alle matches die op dit moment actief zijn in de poules
		/// </summary>
		/// <returns></returns>
		/// <exception cref="NotConnectedException">Als er geen verbinding is met de server</exception>
		/// <exception cref="CommunicationTimeOutException">Als de server er te lang over deed om een antwoord te formuleren</exception>
		/// <remarks>
		/// De lijst houdt geen veranderingen bij maar de matches an sich kunnen wel worden geupdate door het aanroepen van SetMatch
		/// </remarks>
		/// <see cref="SetMatch"/>
		List<Match> GetAllMatches();

		/// <summary>
		/// Vraag de server om een lijst met wedstrijden uit het verleden
		/// </summary>
		/// <returns>
		/// Een lijst met eerder gespeelde wedstrijden uit alle poules
		/// </returns>
		/// <exception cref="NotConnectedException">Als er geen verbinding is met de server</exception>
		/// <exception cref="CommunicationTimeOutException">Als de server er te lang over deed om een antwoord te formuleren</exception>
		/// <remarks>
		/// De lijst houdt geen veranderingen bij maar de matches an sich kunnen wel worden geupdate door het aanroepen van SetMatch
		/// </remarks>
		/// <see cref="SetMatch"/>
		List<Match> GetAllHistoryMatches();

		/// <summary>
		/// Vraag de server om een lijst met wedstrijden uit het verleden die gespeeld waren in deze poule
		/// </summary>
		/// <param name="PouleID">De id van de poule waar de history matches van worden gevraagd</param>
		/// <returns>
		/// Een lijst met matches die al gespeeld zijn in eerdere rondes
		/// </returns>
		/// <exception cref="NotConnectedException">Als er geen verbinding is met de server</exception>
		/// <exception cref="CommunicationTimeOutException">Als de server er te lang over deed om een antwoord te formuleren</exception>
		/// <exception cref="DataDoesNotExistsException">Als de gevraagde PouleID niet bestaat</exception>
		/// <remarks>
		/// De lijst houdt geen veranderingen bij maar de matches an sich kunnen wel worden geupdate door het aanroepen van SetMatch
		/// </remarks>
		/// <see cref="SetMatch"/>
		List<Match> GetPouleHistoryMatches(int PouleID);

		/// <summary>
		/// Vraag de server een bepaalde match op te sturen
		/// </summary>
		/// <param name="MatchID">De id van de match waarnaar gevraagd wordt</param>
		/// <returns>Het Match object waar om gevraagd is</returns>
		/// <exception cref="NotConnectedException">Als er geen verbinding is met de server</exception>
		/// <exception cref="CommunicationTimeOutException">Als de server er te lang over deed om een antwoord te formuleren</exception>
		/// <exception cref="DataDoesNotExistsException">Als de gevraagde MatchID niet bestaat</exception>
		Match GetMatch(int MatchID);

		/// <summary>
		/// Verander de uitslag/gegevens van een bepaalde wedstrijd.
		/// </summary>
		/// <param name="match">Match object dat de nieuwe gegevens bevat</param>
		/// <param name="exception">De fout die optreed bij het verwerken van de operatie</param>
		/// <returns>
		/// true als de methode succesvol is geeindigd, anders false
		/// </returns>
		/// <exception cref="NotConnectedException">Als er geen verbinding is met de server</exception>
		/// <exception cref="CommunicationTimeOutException">Als de server er te lang over deed om een antwoord te formuleren</exception>
		/// <exception cref="DataDoesNotExistsException">Als de gevraagde PouleID niet bestaat</exception>
		/// <exception cref="DataOutOfDateException">Als de gegevens van match verouderd zijn</exception>
		/// <exception cref="DataReferenceException">Als de gegevens in de match naar elementen verwijzen die niet bestaan of ongeldig zijn</exception>
		bool SetMatch(Match match, out CommunicationException exception);

		/// <summary>
		/// Sluit van PouleID de hidige ronde met wedstrijden af zodat er geen actieve wedstrijden meer zijn
		/// </summary>
		/// <param name="PouleID">De id van de poule waarvan de ronde moet worden beeidigd</param>
		/// <param name="exception">De fout die optreed bij het verwerken van de operatie</param>
		/// <returns>
		/// true als de methode succesvol is geindigd, anders false
		/// </returns>
		/// <exception cref="NotConnectedException">Als er geen verbinding is met de server</exception>
		/// <exception cref="CommunicationTimeOutException">Als de server er te lang over deed om een antwoord te formuleren</exception>
		/// <exception cref="DataDoesNotExistsException">Als de gevraagde PouleID niet bestaat</exception>
		/// <exception cref="PouleMatchScoreNotCompleteException">Als er nog wedstrijden open staan waarvan een score is ingevuld</exception>
		bool PouleFinishRound(int PouleID, out CommunicationException exception);

		/// <summary>
		/// Verwijderen alle wedstrijden in de huidige ronde zodat de wedstrijden opnieuw kunnen worden gegenereerd
		/// </summary>
		/// <param name="PouleID">De id van de poule waar de huidige ronde van moet worden verwijderd.</param>
		/// <param name="exception">De fout die optreed bij het verwerken van de operatie</param>
		/// <returns>
		/// true als de methode succesvol is geindigd, anders false
		/// </returns>
		/// <exception cref="NotConnectedException">Als er geen verbinding is met de server</exception>
		/// <exception cref="CommunicationTimeOutException">Als de server er te lang over deed om een antwoord te formuleren</exception>
		/// <exception cref="DataDoesNotExistsException">Als de gevraagde PouleID niet bestaat</exception>
		bool PouleRemoveRound(int PouleID, out CommunicationException exception);

		/// <summary>
		/// Vraag de server om de toernooi-instellingen.
		/// </summary>
		/// <returns>
		/// Een tournamentsettings object met daarin de bekende settings.
		/// </returns>
		/// <exception cref="NotConnectedException">Als er geen verbinding is met de server</exception>
		/// <exception cref="CommunicationTimeOutException">Als de server er te lang over deed om een antwoord te formuleren</exception>
		TournamentSettings GetSettings();

		/// <summary>
		/// Stuur de server veranderde settings.
		/// </summary>
		/// <param name="settings"> De nieuwe Toernooinstellingen</param>
		/// <param name="exception">De fout die optreed bij het verwerken van de operatie</param>
		/// <returns>
		/// true als de methode succesvol is geeindigd, anders false
		/// </returns>
		/// <exception cref="NotConnectedException">Als er geen verbinding is met de server</exception>
		/// <exception cref="CommunicationTimeOutException">Als de server er te lang over deed om een antwoord te formuleren</exception>
		/// <exception cref="DataOutOfDateException">Als het settings object verouderd is</exception>
		bool SetSettings(TournamentSettings settings, out CommunicationException exception);
	}
}
