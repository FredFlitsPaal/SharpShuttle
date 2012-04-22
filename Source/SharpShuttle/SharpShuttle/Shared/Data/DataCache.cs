using System.Collections.Generic;
using System.Threading;
using Shared.Communication;
using Shared.Communication.Exceptions;
using Shared.Communication.Messages.Requests;
using Shared.Communication.Messages.Responses;
using Shared.Communication.Serials;
using Shared.Domain;

namespace Shared.Data
{
	/// <summary>
	/// Een IDataCache implementatie die over een netwerk communiceert
	/// </summary>
	public class DataCache : IDataCache
	{
		#region Fields

		/// <summary>
		/// De instance van de DataCache
		/// </summary>
		static IDataCache instance = new DataCache();
		/// <summary>
		/// De toernooiinstellingen
		/// </summary>
		private TournamentSettings CachedSettings = new TournamentSettings();
		/// <summary>
		/// De lijst van alle gebruikers
		/// </summary>
		private List<User> AllUsers = new List<User>();
		/// <summary>
		/// De lijst van alle poules
		/// </summary>
		private List<Poule> AllPoules = new List<Poule>();
		/// <summary>
		/// De lijst van alle spelers
		/// </summary>
		private List<Player> AllPlayers = new List<Player>();
		/// <summary>
		/// De lijst van alle al gespeelde wedstrijden
		/// </summary>
		private List<Match> AllHistoryMatches = new List<Match>();
		/// <summary>
		/// De lijst van alle actieve wedstrijden
		/// </summary>
		private List<Match> AllMatches = new List<Match>();
		/// <summary>
		/// De indeling van spelers in teams
		/// </summary>
		private Dictionary<int, List<Player>> PoulePlanning = new Dictionary<int, List<Player>>();
		/// <summary>
		/// De teamindeling per poule
		/// </summary>
		private Dictionary<int, List<Team>> PouleTeams = new Dictionary<int, List<Team>>();
		/// <summary>
		/// De ranking per poule
		/// </summary>
		private Dictionary<int, List<LadderTeam>> PouleLadders = new Dictionary<int, List<LadderTeam>>();
		/// <summary>
		/// Alle actieve wedstrijden per poule
		/// </summary>
		private Dictionary<int, List<Match>> PouleMatches = new Dictionary<int, List<Match>>();
		/// <summary>
		/// Alle al gespeelde wedstrijden per poule
		/// </summary>
		private Dictionary<int, List<Match>> PouleHistoryMatches = new Dictionary<int, List<Match>>();
		/// <summary>
		/// Alle matches per poule
		/// </summary>
		private Dictionary<int, Match> PouleMatch = new Dictionary<int, Match>();

		#endregion

		#region Properties

		/// <summary>
		/// Vraag de instance van de DataCache op
		/// </summary>
		public static IDataCache Instance { get { return instance; } }

		#endregion

		#region Constructors

		// DO NOT REMOVE!!
		// google BeforeFieldInit+singleton
		static DataCache() { }

		#endregion

		#region IDataCache Members

		/// <summary>
		/// Mutex om GetAllUsers te synchronizeren
		/// </summary>
		Mutex _allusersmutex = new Mutex();
		List<User> IDataCache.GetAllUsers()
		{
			//Als er geen verbinding is met de server, dan disconnecten
			if (!Communication.Communication.Instance.Connected)
				throw new NotConnectedException();

			//Mutex pakken, buiten de IF anders komt 
			//hij 2 keer naarbinnen terwijl het niet hoeft
			_allusersmutex.WaitOne();

			try
			{
				if (SerialCache.Instance.IsOutOfDate(new SerialDefinition(SerialTypes.AllUsers)))
				{
					//We zijn out of date, updaten en dan returnen

					RequestMessage m = new RequestGetAllUsers();

					ResponseGetAllUsers answer = ResponseCaster.CastAnswer<ResponseGetAllUsers>(Communication.Communication.Instance.SendRequest(m));

					AllUsers = answer.Users;
					//SerialTracker.Instance.HandleSerialUpdateMessage(answer.SerialUpdateMessages);

					SerialCache.Instance.HandleNewSerialDefinitions(answer.NewSerialDefinitions);
				}
				return new List<User>(AllUsers);
			}
			finally
			{
				_allusersmutex.ReleaseMutex();
			}
		}

		/// <summary>
		/// Mutex om GetAllPoules te synchronizeren
		/// </summary>
		Mutex _allpoulesmutex = new Mutex();
		ChangeTrackingList<Poule> IDataCache.GetAllPoules()
		{
			//Als er geen verbinding is met de server, dan disconnecten
			if (!Communication.Communication.Instance.Connected)
				throw new NotConnectedException();

			_allpoulesmutex.WaitOne();
			try
			{
				lock (AllPoules)
				{
					if (SerialCache.Instance.IsOutOfDate(new SerialDefinition(SerialTypes.AllPoules)))
					{
						RequestMessage m = new RequestGetAllPoules();
						var answer = ResponseCaster.CastAnswer<ResponseGetAllPoules>(Communication.Communication.Instance.SendRequest(m));
						AllPoules = answer.Poules;
						SerialCache.Instance.HandleNewSerialDefinitions(answer.NewSerialDefinitions);
					}
					return new ChangeTrackingList<Poule>(AllPoules, SerialCache.Instance.GetOrAdd(new SerialDefinition(SerialTypes.AllPoules)));
				}
			}
			finally
			{
				_allpoulesmutex.ReleaseMutex();
			}
		}

		/// <summary>
		/// Mutex om SetAllUsers te synchronizeren
		/// </summary>
		Mutex _setallpoulesmutex = new Mutex();
		bool IDataCache.SetAllPoules(ChangeTrackingList<Poule> changelist, out Shared.Communication.Exceptions.CommunicationException exception)
		{
			//Als er geen verbinding is met de server, dan disconnecten
			if (!Communication.Communication.Instance.Connected)
			{
				exception = new NotConnectedException();
				return false;
			}


			exception = null;
			_setallpoulesmutex.WaitOne();

			if (SerialCache.Instance.IsOutOfDate(changelist.SerialNumber))
			{
				_setallpoulesmutex.ReleaseMutex();
				exception = new DataOutOfDateException();
				return false;
			}

			RequestSetAllPoules m = new RequestSetAllPoules(changelist);
			try
			{
				var ans = ResponseCaster.CastAnswer<ResponseSetAllPoules>(Communication.Communication.Instance.SendRequest(m));
				SerialTracker.Instance.HandleSerialUpdateMessage(ans.SerialUpdateMessages);
			}
			catch (CommunicationException e)
			{
				exception = e;
			}

			_setallpoulesmutex.ReleaseMutex();
			return exception == null;
		}

		/// <summary>
		/// Mutex om GetAllPlayers te synchronizeren
		/// </summary>
		Mutex _getallplayersmutex = new Mutex();
		ChangeTrackingList<Player> IDataCache.GetAllPlayers()
		{
			//Als er geen verbinding is met de server, dan disconnecten
			if (!Communication.Communication.Instance.Connected)
				throw new NotConnectedException();

			_getallplayersmutex.WaitOne();
			try
			{
				lock (AllPlayers)
				{
					if (SerialCache.Instance.IsOutOfDate(new SerialDefinition(SerialTypes.AllPlayers)))
					{
						RequestMessage m = new RequestGetAllPlayers();
						var answer = ResponseCaster.CastAnswer<ResponseGetAllPlayers>(Communication.Communication.Instance.SendRequest(m));
						AllPlayers = answer.Players;
						SerialCache.Instance.HandleNewSerialDefinitions(answer.NewSerialDefinitions);
					}
					return new ChangeTrackingList<Player>(AllPlayers, SerialCache.Instance.GetOrAdd(new SerialDefinition(SerialTypes.AllPlayers)));
				}
			}
			finally
			{
				_getallplayersmutex.ReleaseMutex();
			}
		}

		/// <summary>
		/// Mutex om SetAllPlayers te synchronizeren
		/// </summary>
		Mutex _setallplayersmutex = new Mutex();
		bool IDataCache.SetAllPlayers(ChangeTrackingList<Player> changelist, out Shared.Communication.Exceptions.CommunicationException exception)
		{
			//Als er geen verbinding is met de server, dan een error
			if (!Communication.Communication.Instance.Connected)
			{
				exception = new NotConnectedException();
				return false;
			}

			exception = null;
			_setallplayersmutex.WaitOne();

			if (SerialCache.Instance.IsOutOfDate(changelist.SerialNumber))
			{
				_setallplayersmutex.ReleaseMutex();
				exception = new DataOutOfDateException();
				return false;
			}

			RequestSetAllPlayers m = new RequestSetAllPlayers(changelist);

			try
			{
				var ans = ResponseCaster.CastAnswer<ResponseSetAllPlayers>(Communication.Communication.Instance.SendRequest(m));
				SerialTracker.Instance.HandleSerialUpdateMessage(ans.SerialUpdateMessages);
			}
			catch (CommunicationException e)
			{
				exception = e;
			}
			_setallplayersmutex.ReleaseMutex();
			return exception == null;
		}

		/// <summary>
		/// Mutex om GetPoule te synchronizeren
		/// </summary>
		Mutex _getpoulemutex = new Mutex();
		Poule IDataCache.GetPoule(int PouleID)
		{
			//Als er geen verbinding is met de server, dan een error
			if (!Communication.Communication.Instance.Connected)
				throw new NotConnectedException();

			_getpoulemutex.WaitOne();
			try
			{
				lock (AllPoules)
				{
					if (SerialCache.Instance.IsOutOfDate(new SerialDefinition(SerialTypes.Poule, PouleID)))
					{
						RequestMessage m = new RequestGetPoule(PouleID);
						var answer = ResponseCaster.CastAnswer<ResponseGetPoule>(Communication.Communication.Instance.SendRequest(m));
						AllPoules.RemoveAll(p => p.PouleID == answer.Poule.PouleID);
						AllPoules.Add(answer.Poule);
						SerialCache.Instance.HandleNewSerialDefinitions(answer.NewSerialDefinitions);
					}
					return AllPoules.Find(p => p.PouleID == PouleID);
				}
			}
			finally
			{
				_getpoulemutex.ReleaseMutex();
			}
		}

		/// <summary>
		/// Mutex om SetPoule te synchronizeren
		/// </summary>
		Mutex _setpoulemutex = new Mutex();
		bool IDataCache.SetPoule(int PouleID, Poule Poule, out Shared.Communication.Exceptions.CommunicationException exception)
		{
			//Als er geen verbinding is met de server, dan een error
			if (!Communication.Communication.Instance.Connected)
			{
				exception = new NotConnectedException();
				return false;
			}

			exception = null;
			_setpoulemutex.WaitOne();

			if (SerialCache.Instance.IsOutOfDate(Poule.SerialNumber))
			{
				_setpoulemutex.ReleaseMutex();
				exception = new DataOutOfDateException();
				return false;
			}

			RequestMessage m = new RequestSetPoule(PouleID, Poule);

			try
			{
				var ans = ResponseCaster.CastAnswer<ResponseSetPoule>(Communication.Communication.Instance.SendRequest(m));
				SerialTracker.Instance.HandleSerialUpdateMessage(ans.SerialUpdateMessages);
			}
			catch (CommunicationException e)
			{
				exception = e;
			}
			_setpoulemutex.ReleaseMutex();
			return exception == null;
		}

		/// <summary>
		/// Mutex om GetPoulePlanning te synchronizeren
		/// </summary>
		Mutex _getpouleplanningmutex = new Mutex();
		ChangeTrackingList<Player> IDataCache.GetPoulePlanning(int PouleID)
		{
			//Als er geen verbinding is met de server, dan een error
			if (!Communication.Communication.Instance.Connected)
				throw new NotConnectedException();

			_getpouleplanningmutex.WaitOne();
			try
			{
				if (SerialCache.Instance.IsOutOfDate(new SerialDefinition(SerialTypes.PoulePlanning, PouleID)))
				{
					RequestMessage m = new RequestGetPoulePlanning(PouleID);
					var answer = ResponseCaster.CastAnswer<ResponseGetPoulePlanning>(Communication.Communication.Instance.SendRequest(m));
					if (PoulePlanning.ContainsKey(PouleID))
						PoulePlanning[PouleID] = answer.Players;
					else
						PoulePlanning.Add(PouleID, answer.Players);
					SerialCache.Instance.HandleNewSerialDefinitions(answer.NewSerialDefinitions);
				}
				return new ChangeTrackingList<Player>(PoulePlanning[PouleID], SerialCache.Instance.GetOrAdd(new SerialDefinition(SerialTypes.PoulePlanning, PouleID)));
			}
			finally
			{
				_getpouleplanningmutex.ReleaseMutex();
			}
		}

		/// <summary>
		/// Mutex om SetPoulePlanning te synchronizeren
		/// </summary>
		Mutex _setpouleplanningmutex = new Mutex();
		bool IDataCache.SetPoulePlanning(int PouleID, ChangeTrackingList<Player> changelist, out Shared.Communication.Exceptions.CommunicationException exception)
		{
			//Als er geen verbinding is met de server, dan een error
			if (!Communication.Communication.Instance.Connected)
			{
				exception = new NotConnectedException();
				return false;
			}

			exception = null;
			_setpouleplanningmutex.WaitOne();

			if (SerialCache.Instance.IsOutOfDate(changelist.SerialNumber))
			{
				_setpouleplanningmutex.ReleaseMutex();
				exception = new DataOutOfDateException();
				return false;
			}

			RequestMessage m = new RequestSetPoulePlanning(PouleID, changelist);

			try
			{
				var ans = ResponseCaster.CastAnswer<ResponseSetPoulePlanning>(Communication.Communication.Instance.SendRequest(m));
				SerialTracker.Instance.HandleSerialUpdateMessage(ans.SerialUpdateMessages);
			}
			catch (CommunicationException e)
			{
				exception = e;
			}
			_setpouleplanningmutex.ReleaseMutex();
			return exception == null;
		}

		/// <summary>
		/// Mutex om GetPouleTeams te synchronizeren
		/// </summary>
		Mutex _getpouleteamsmutex = new Mutex();
		ChangeTrackingList<Team> IDataCache.GetPouleTeams(int PouleID)
		{
			//Als er geen verbinding is met de server, dan een error
			if (!Communication.Communication.Instance.Connected)
				throw new NotConnectedException();

			_getpouleteamsmutex.WaitOne();
			try
			{
				if (SerialCache.Instance.IsOutOfDate(new SerialDefinition(SerialTypes.PouleTeams, PouleID)))
				{
					RequestMessage m = new RequestGetPouleTeams(PouleID);
					var answer = ResponseCaster.CastAnswer<ResponseGetPouleTeams>(Communication.Communication.Instance.SendRequest(m));
					if (PouleTeams.ContainsKey(PouleID))
						PouleTeams[PouleID] = answer.Teams;
					else
						PouleTeams.Add(PouleID, answer.Teams);
					SerialCache.Instance.HandleNewSerialDefinitions(answer.NewSerialDefinitions);
				}
				return new ChangeTrackingList<Team>(PouleTeams[PouleID],
					SerialCache.Instance.GetOrAdd(new SerialDefinition(SerialTypes.PouleTeams, PouleID)));
			}
			finally
			{
				_getpouleteamsmutex.ReleaseMutex();
			}
		}

		/// <summary>
		/// Mutex om SetPouleTeams te synchronizeren
		/// </summary>
		Mutex _setpouleteamsmutex = new Mutex();
		bool IDataCache.SetPouleTeams(int PouleID, ChangeTrackingList<Team> changelist, out Shared.Communication.Exceptions.CommunicationException exception)
		{
			//Als er geen verbinding is met de server, dan een error
			if (!Communication.Communication.Instance.Connected)
			{
				exception = new NotConnectedException();
				return false;
			}

			exception = null;
			_setpouleteamsmutex.WaitOne();

			if (SerialCache.Instance.IsOutOfDate(changelist.SerialNumber))
			{
				_setpouleteamsmutex.ReleaseMutex();
				exception = new DataOutOfDateException();
				return false;
			}

			RequestMessage m = new RequestSetPouleTeams(PouleID, changelist);

			try
			{
				var ans = ResponseCaster.CastAnswer<ResponseSetPouleTeams>(Communication.Communication.Instance.SendRequest(m));
				SerialTracker.Instance.HandleSerialUpdateMessage(ans.SerialUpdateMessages);
			}
			catch (CommunicationException e)
			{
				exception = e;
			}
			_setpouleteamsmutex.ReleaseMutex();
			return exception == null;
		}

		/// <summary>
		/// Mutex om GetPouleLadder te synchronizeren
		/// </summary>
		Mutex _getpouleladdermutex = new Mutex();
		List<LadderTeam> IDataCache.GetPouleLadder(int PouleID)
		{
			//Als er geen verbinding is met de server, dan een error
			if (!Communication.Communication.Instance.Connected)
				throw new NotConnectedException();

			_getpouleladdermutex.WaitOne();
			try
			{
				if (SerialCache.Instance.IsOutOfDate(new SerialDefinition(SerialTypes.PouleLadder, PouleID)))
				{
					RequestMessage m = new RequestGetPouleLadder(PouleID);
					var answer = ResponseCaster.CastAnswer<ResponseGetPouleLadder>(Communication.Communication.Instance.SendRequest(m));
					if (PouleLadders.ContainsKey(PouleID))
						PouleLadders[PouleID] = answer.Ladder;
					else
						PouleLadders.Add(PouleID, answer.Ladder);
					SerialCache.Instance.HandleNewSerialDefinitions(answer.NewSerialDefinitions);
				}
				return new List<LadderTeam>(PouleLadders[PouleID]);
			}
			finally
			{
				_getpouleladdermutex.ReleaseMutex();
			}
		}

		/// <summary>
		/// Mutex om GetPouleMatches te synchronizeren
		/// </summary>
		Mutex _getpoulematchesmutex = new Mutex();
		List<Match> IDataCache.GetPouleMatches(int PouleID)
		{
			//Als er geen verbinding is met de server, dan een error
			if (!Communication.Communication.Instance.Connected)
				throw new NotConnectedException();

			_getpoulematchesmutex.WaitOne();
			try
			{
				if (SerialCache.Instance.IsOutOfDate(new SerialDefinition(SerialTypes.PouleMatches, PouleID)))
				{
					RequestMessage m = new RequestGetPouleMatches(PouleID);
					var answer = ResponseCaster.CastAnswer<ResponseGetPouleMatches>(Communication.Communication.Instance.SendRequest(m));
					if (PouleMatches.ContainsKey(PouleID))
						PouleMatches[PouleID] = answer.Matches;
					else
						PouleMatches.Add(PouleID, answer.Matches);
					SerialCache.Instance.HandleNewSerialDefinitions(answer.NewSerialDefinitions);
				}
				return new List<Match>(PouleMatches[PouleID]);
			}
			finally
			{
				_getpoulematchesmutex.ReleaseMutex();
			}
		}

		/// <summary>
		/// Mutex om SetPouleNextRound te synchronizeren
		/// </summary>
		Mutex _setpoulenextroundmutex = new Mutex();
		bool IDataCache.SetPouleNextRound(int PouleID, List<Match> matches, out Shared.Communication.Exceptions.CommunicationException exception)
		{
			//Als er geen verbinding is met de server, dan een error
			if (!Communication.Communication.Instance.Connected)
			{
				exception = new NotConnectedException();
				return false;
			}

			exception = null;
			_setpoulenextroundmutex.WaitOne();

			RequestMessage m = new RequestSetPouleNextRound(PouleID, matches);

			try
			{
				var ans = ResponseCaster.CastAnswer<ResponseSetPouleNextRound>(Communication.Communication.Instance.SendRequest(m));
				SerialTracker.Instance.HandleSerialUpdateMessage(ans.SerialUpdateMessages);
			}
			catch (CommunicationException e)
			{
				exception = e;
			}
			_setpoulenextroundmutex.ReleaseMutex();
			return exception == null;
		}

		/// <summary>
		/// Mutex om GetAllMatches te synchronizeren
		/// </summary>
		Mutex _getallmatchesmutex = new Mutex();
		List<Match> IDataCache.GetAllMatches()
		{
			//Als er geen verbinding is met de server, dan een error
			if (!Communication.Communication.Instance.Connected)
				throw new NotConnectedException();

			_getallmatchesmutex.WaitOne();
			try
			{
				if (SerialCache.Instance.IsOutOfDate(new SerialDefinition(SerialTypes.AllMatches)))
				{
					RequestMessage m = new RequestGetAllMatches();
					var answer = ResponseCaster.CastAnswer<ResponseGetAllMatches>(Communication.Communication.Instance.SendRequest(m));
					AllMatches = answer.Matches;
					SerialCache.Instance.HandleNewSerialDefinitions(answer.NewSerialDefinitions);
				}
				return new List<Match>(AllMatches);
			}
			finally
			{
				_getallmatchesmutex.ReleaseMutex();
			}
		}

		/// <summary>
		/// Mutex om GetAllHistoryMatches te synchronizeren
		/// </summary>
		Mutex _getallhistorymatchesmutex = new Mutex();
		List<Match> IDataCache.GetAllHistoryMatches()
		{
			//Als er geen verbinding is met de server, dan een error
			if (!Communication.Communication.Instance.Connected)
				throw new NotConnectedException();

			_getallhistorymatchesmutex.WaitOne();
			try
			{
				List<Match> result;
				lock (AllHistoryMatches)
				{
					if (SerialCache.Instance.IsUpToDate(new SerialDefinition(SerialTypes.AllHistoryMatches)))
					{
						RequestMessage m = new RequestGetAllHistoryMatches();
						var answer = ResponseCaster.CastAnswer<ResponseGetAllHistoryMatches>(Communication.Communication.Instance.SendRequest(m));
						AllHistoryMatches = answer.Matches;
						SerialCache.Instance.HandleNewSerialDefinitions(answer.NewSerialDefinitions);
					}
					result = new List<Match>(AllHistoryMatches);
				}
				return result;
			}
			finally
			{
				_getallhistorymatchesmutex.ReleaseMutex();
			}
		}

		/// <summary>
		/// Mutex om GetPouleHistoryMatches te synchronizeren
		/// </summary>
		Mutex _getpoulehistorymatchesmutex = new Mutex();
		List<Match> IDataCache.GetPouleHistoryMatches(int PouleID)
		{
			//Als er geen verbinding is met de server, dan een error
			if (!Communication.Communication.Instance.Connected)
				throw new NotConnectedException();

			_getpoulehistorymatchesmutex.WaitOne();
			try
			{
				if (SerialCache.Instance.IsOutOfDate(new SerialDefinition(SerialTypes.PouleHistoryMatches, PouleID)))
				{
					RequestMessage m = new RequestGetPouleHistoryMatches(PouleID);
					var answer = ResponseCaster.CastAnswer<ResponseGetPouleHistoryMatches>(Communication.Communication.Instance.SendRequest(m));
					if (PouleHistoryMatches.ContainsKey(PouleID))
						PouleHistoryMatches[PouleID] = answer.Matches;
					else
						PouleHistoryMatches.Add(PouleID, answer.Matches);
					SerialCache.Instance.HandleNewSerialDefinitions(answer.NewSerialDefinitions);
				}
				return new List<Match>(PouleHistoryMatches[PouleID]);
			}
			finally
			{
				_getpoulehistorymatchesmutex.ReleaseMutex();
			}
		}

		/// <summary>
		/// Mutex om GetMatch te synchronizeren
		/// </summary>
		Mutex _getmatchmutex = new Mutex();
		Match IDataCache.GetMatch(int MatchID)
		{
			//Als er geen verbinding is met de server, dan een error
			if (!Communication.Communication.Instance.Connected)
				throw new NotConnectedException();

			_getmatchmutex.WaitOne();
			try
			{
				RequestMessage m = new RequestGetMatch(MatchID);
				var answer = ResponseCaster.CastAnswer<ResponseGetMatch>(Communication.Communication.Instance.SendRequest(m));
				SerialCache.Instance.HandleNewSerialDefinitions(answer.NewSerialDefinitions);

				return answer.Match;
			}
			finally
			{
				_getmatchmutex.ReleaseMutex();
			}
		}

		/// <summary>
		/// Mutex om SetMatch te synchronizeren
		/// </summary>
		Mutex _setmatchmutex = new Mutex();
		bool IDataCache.SetMatch(Match match, out Shared.Communication.Exceptions.CommunicationException exception)
		{
			//Als er geen verbinding is met de server, dan een error
			if (!Communication.Communication.Instance.Connected)
			{
				exception = new NotConnectedException();
				return false;
			}

			exception = null;
			_setmatchmutex.WaitOne();

			if (SerialCache.Instance.IsOutOfDate(match.SerialNumber))
			{
				_setmatchmutex.ReleaseMutex();
				exception = new DataOutOfDateException();
				return false;
			}

			RequestMessage m = new RequestSetMatch(match);

			try
			{
				var ans = ResponseCaster.CastAnswer<ResponseSetMatch>(Communication.Communication.Instance.SendRequest(m));
				SerialTracker.Instance.HandleSerialUpdateMessage(ans.SerialUpdateMessages);
			}
			catch (CommunicationException e)
			{
				exception = e;
			}
			_setmatchmutex.ReleaseMutex();
			return exception == null;
		}

		/// <summary>
		/// Mutex om PouleFinishRound te synchronizeren
		/// </summary>
		Mutex _poulefinishroundmutex = new Mutex();
		bool IDataCache.PouleFinishRound(int PouleID, out CommunicationException exception)
		{
			//Als er geen verbinding is met de server, dan een error
			if (!Communication.Communication.Instance.Connected)
			{
				exception = new NotConnectedException();
				return false;
			}

			exception = null;
			_poulefinishroundmutex.WaitOne();

			RequestMessage m = new RequestPouleFinishRound(PouleID);

			try
			{
				var ans = ResponseCaster.CastAnswer<ResponsePouleFinishRound>(Communication.Communication.Instance.SendRequest(m));
				SerialTracker.Instance.HandleSerialUpdateMessage(ans.SerialUpdateMessages);
			}
			catch (CommunicationException e)
			{
				exception = e;
			}
			_poulefinishroundmutex.ReleaseMutex();
			return exception == null;
		}

		/// <summary>
		/// Mutex om PouleRemoveRound te synchronizeren
		/// </summary>
		Mutex _pouleremoveround = new Mutex();
		bool IDataCache.PouleRemoveRound(int PouleID, out CommunicationException exception)
		{
			//Als er geen verbinding is met de server, dan een error
			if (!Communication.Communication.Instance.Connected)
			{
				exception = new NotConnectedException();
				return false;
			}

			exception = null;
			_pouleremoveround.WaitOne();

			RequestMessage m = new RequestPouleRemoveRound(PouleID);

			try
			{
				var ans = ResponseCaster.CastAnswer<ResponsePouleRemoveRound>(Communication.Communication.Instance.SendRequest(m));
				SerialTracker.Instance.HandleSerialUpdateMessage(ans.SerialUpdateMessages);
			}
			catch (CommunicationException e)
			{
				exception = e;
			}
			_pouleremoveround.ReleaseMutex();
			return exception == null;
		}

		/// <summary>
		/// Mutex om SetSettings te synchronizeren
		/// </summary>
		Mutex _setsettingsmutex = new Mutex();
		bool IDataCache.SetSettings(TournamentSettings settings, out CommunicationException exception)
		{
			//Als er geen verbinding is met de server, dan een error
			if (!Communication.Communication.Instance.Connected)
			{
				exception = new NotConnectedException();
				return false;
			}

			exception = null;
			_setsettingsmutex.WaitOne();

			if (SerialCache.Instance.IsOutOfDate(settings.SerialNumber))
			{
				_setsettingsmutex.ReleaseMutex();
				exception = new DataOutOfDateException();
				return false;
			}

			RequestMessage m = new RequestSetSettings(settings);

			try
			{
				var ans = ResponseCaster.CastAnswer<ResponseSetSettings>(Communication.Communication.Instance.SendRequest(m));
				SerialTracker.Instance.HandleSerialUpdateMessage(ans.SerialUpdateMessages);
			}
			catch (CommunicationException e)
			{
				exception = e;
			}
			_setsettingsmutex.ReleaseMutex();
			return exception == null;
		}

		/// <summary>
		/// Mutex om GetSettings te synchronizeren
		/// </summary>
		Mutex _getsettingsmutex = new Mutex();
		TournamentSettings IDataCache.GetSettings()
		{
			//Als er geen verbinding is met de server, dan een error
			if (!Communication.Communication.Instance.Connected)
				throw new NotConnectedException();

			_getsettingsmutex.WaitOne();
			try
			{
				if (SerialCache.Instance.IsOutOfDate(new SerialDefinition(SerialTypes.Settings)))
				{
					RequestMessage m = new RequestGetSettings();
					var answer = ResponseCaster.CastAnswer<ResponseGetSettings>(Communication.Communication.Instance.SendRequest(m));
					CachedSettings = answer.settings;
					SerialCache.Instance.HandleNewSerialDefinitions(answer.NewSerialDefinitions);
				}

				TournamentSettings settings = (TournamentSettings)CachedSettings.Clone();
				settings.SerialNumber = SerialCache.Instance.GetOrAdd(new SerialDefinition(SerialTypes.Settings));

				return settings;
			}
			finally
			{
				_getsettingsmutex.ReleaseMutex();
			}
		}

		#endregion
	}
}