//using System.Collections.Generic;
//using System.Linq;
//using Shared.Communication.Exceptions;
//using Shared.Communication.Serials;
//using Shared.Datastructures;
//using Shared.Domain;
//using Shared.Data;
//using System.IO;
//using System.IO.Compression;

//namespace Shared.Data
//{
//    /// <summary>
//    /// Tijdelijke implementatie van datacache
//    /// </summary>
//    public class IntermediateDataCache : IDataCache
//    {
//        List<User> users;
//        Dictionary<int, Poule> poules;
//        Dictionary<int, Player> players;
//        Dictionary<int, PouleData> pouledata;
//        List<Match> historicMatches;

//        //ID generatoren voor alle domeinelementen
//        //AtomicCounter32 nextMatchID = new AtomicCounter32(1);
//        AtomicCounter32 nextPouleID = new AtomicCounter32(0);
//        AtomicCounter32 nextTeamID = new AtomicCounter32(0);
//        AtomicCounter32 nextPlayerID = new AtomicCounter32(0);

//        System.Threading.ReaderWriterLock rwlock = new System.Threading.ReaderWriterLock();
//        const int lockTimeout = -1;

//        static readonly IntermediateDataCache instance = new IntermediateDataCache();

//        public static IDataCache Instance
//        {
//            get { return instance; }
//        }

//        private IntermediateDataCache()
//        {
//            users = new List<User>();
//            users.Add(new User(0, "Administrator", "Hoi dit is de info van de admin"));
//            users.Add(new User(1, "ScoreInvuller", "Deze persoon vult de scores in"));

//            poules = new Dictionary<int, Poule>();
//            pouledata = new Dictionary<int, PouleData>();

//            players = new Dictionary<int, Player>();
			
//            historicMatches = new List<Match>();
            
//        }

//        class PouleData
//        {
//            public Dictionary<int, Player> planning;
//            public Dictionary<int, Team> indeling;
//            public List<Match> matches;
//            public Dictionary<int, LadderTeam> ladder;

//            public PouleData()
//            {
//                planning = new Dictionary<int, Player>();
//                indeling = new Dictionary<int, Team>();
//                matches = new List<Match>();
//                ladder = new Dictionary<int, LadderTeam>();
//            }
//        }

//        public static void Reset()
//        {
//            instance.rwlock.AcquireWriterLock(lockTimeout);

//            instance.nextPouleID = new AtomicCounter32(0);
//            instance.nextTeamID = new AtomicCounter32(0);
//            instance.nextPlayerID = new AtomicCounter32(0);

//            instance.users = new List<User>();
//            instance.users.Add(new User(0, "Administrator", "Hoi dit is de info van de admin"));
//            instance.users.Add(new User(1, "ScoreInvuller", "Deze persoon vult de scores in"));

//            instance.poules = new Dictionary<int, Poule>();
//            instance.pouledata = new Dictionary<int, PouleData>();


//            instance.players = new Dictionary<int, Player>();

//            instance.historicMatches = new List<Match>();

//            instance.rwlock.ReleaseWriterLock();
//        }

//        public static void Save(string fileName)
//        {
//            MemoryStream ms = new MemoryStream();
//            BinaryWriter writer = new BinaryWriter(ms);
//            IDataCache cache = Instance;

//            Logging.Logger.Write("Save", "Serializing data");

//            instance.rwlock.AcquireReaderLock(lockTimeout);
			
//            //ID counters wegschrijven
//            writer.Write(0); //backwards compat
//            writer.Write(instance.nextPlayerID.Value);
//            writer.Write(instance.nextPouleID.Value);
//            writer.Write(instance.nextTeamID.Value);

//            //Spelers wegschrijven
//            ChangeTrackingList<Player> players = cache.GetAllPlayers();
//            writer.Write(players.Count);

//            foreach (Player player in players)
//            {
//                player.Write(writer);
//            }

//            //Poules wegschrijven
//            ChangeTrackingList<Poule> poules = cache.GetAllPoules();
//            writer.Write(poules.Count);

//            foreach (Poule poule in poules)
//            {
//                poule.Write(writer);

//                //Poule planning wegschrijven
//                ChangeTrackingList<Player> planning = cache.GetPoulePlanning(poule.PouleID);
//                writer.Write(planning.Count);

//                foreach (Player player in planning)
//                {
//                    writer.Write(player.PlayerID);
//                }

//                //Poule teams wegschrijven
//                ChangeTrackingList<Team> pouleTeams = cache.GetPouleTeams(poule.PouleID);
//                writer.Write(pouleTeams.Count);

//                foreach (Team team in pouleTeams)
//                {
//                    team.Write(writer);
//                }

//                //Poule ladder wegschrijven
//                List<LadderTeam> ladderTeams = cache.GetPouleLadder(poule.PouleID);
//                writer.Write(ladderTeams.Count);

//                foreach (LadderTeam ladderTeam in ladderTeams)
//                {
//                    ladderTeam.Write(writer);
//                }

//                //Poule matches wegschrijven
//                List<Match> pouleMatches = cache.GetPouleMatches(poule.PouleID);
//                writer.Write(pouleMatches.Count);

//                foreach (Match match in pouleMatches)
//                {
//                    match.Write(writer);
//                }
//            }

//            //History matches wegschrijven
//            List<Match> historyMatches = cache.GetAllHistoryMatches();
//            writer.Write(historyMatches.Count);

//            foreach (Match match in historyMatches)
//            {
//                writer.Write(match.PouleID);
//                match.Write(writer);
//            }

//            writer.Flush();

//            byte[] data = ms.ToArray();

//            FileStream fs = null;

//            try { fs = File.OpenWrite(fileName); }
//            catch (System.Exception e)
//            {
//                Logging.Logger.Write("Save Failed", e.ToString());
//                return;
//            }

//            using (DeflateStream deflater = 
//                new DeflateStream(fs, CompressionMode.Compress))
//            {
//                Logging.Logger.Write("Save", "Writing data to file");
//                deflater.Write(data, 0, data.Length);
//                deflater.Flush();
//            }

//            instance.rwlock.ReleaseReaderLock();
			
//            Logging.Logger.Write("Save", "Save completed");
//        }

//        public static void Load(string fileName)
//        {
//            BinaryReader reader = new BinaryReader(
//                new DeflateStream(File.OpenRead(fileName), CompressionMode.Decompress));

//            Reset();

//            instance.rwlock.AcquireWriterLock(lockTimeout);

//            //ID counters laden
//            reader.ReadInt32(); //backwards compat
//            instance.nextPlayerID = new AtomicCounter32(reader.ReadInt32());
//            instance.nextPouleID = new AtomicCounter32(reader.ReadInt32());
//            instance.nextTeamID = new AtomicCounter32(reader.ReadInt32());

//            //Spelers inlezen
//            int nPlayers = reader.ReadInt32();
//            List<Player> players = new List<Player>(nPlayers);

//            for (int i = 0; i < nPlayers; i++)
//            {
//                Player player = new Player(reader);
//                players.Add(player);
//            }

//            //Spelers in de cache stoppen
//            foreach (Player p in players)
//                instance.players.Add(p.PlayerID, p);

//            //Poules inlezen
//            int nPoules = reader.ReadInt32();
//            List<Team> teams = new List<Team>();
//            List<Match> matches = new List<Match>();

//            #region Poule laden

//            for (int i = 0; i < nPoules; i++)
//            {
//                Poule poule = new Poule(reader);

//                //Voeg poule toe aan cache
//                instance.pouledata.Add(poule.PouleID, new PouleData());
//                instance.poules.Add(poule.PouleID, poule);

//                //Poule planning inlezen
//                int nPlanning = reader.ReadInt32();
//                List<int> planning = new List<int>(nPlanning);

//                for (int j = 0; j < nPlanning; j++)
//                {
//                    int playerID = reader.ReadInt32();
//                    planning.Add(playerID);
//                }

//                //Poule planning in de cache stoppen
//                foreach (int playerID in planning)
//                    instance.pouledata[poule.PouleID].planning.Add(playerID, instance.players[playerID]);

//                //Poule teams inlezen

//                int nTeams = reader.ReadInt32();
//                List<Team> pouleTeams = new List<Team>(nTeams);

//                for (int j = 0; j < nTeams; j++)
//                {
//                    Team team = new Team(reader, instance.pouledata[poule.PouleID].planning);
//                    pouleTeams.Add(team);
//                    teams.Add(team);
//                }

//                //Poule Teams in de cache stoppen
//                foreach (Team t in pouleTeams)
//                    instance.pouledata[poule.PouleID].indeling.Add(t.TeamID, t);


//                //Poule ladder inlezen
//                int nLadder = reader.ReadInt32();
				
//                for (int j = 0; j < nLadder; j++)
//                {
//                    LadderTeam ladder = new LadderTeam(reader, instance.pouledata[poule.PouleID].indeling);
//                    //Poule ladder in de cache stoppen
//                    instance.pouledata[poule.PouleID].ladder.Add(ladder.TeamID, ladder);
//                }

//                //Poule matches inlezen
//                int nPouleMatches = reader.ReadInt32();

//                for (int j = 0; j < nPouleMatches; j++)
//                {
//                    Match match = new Match(reader, instance.pouledata[poule.PouleID].indeling);
//                    //Poule matche in de cache stoppen
//                    instance.pouledata[poule.PouleID].matches.Add(match);
//                    matches.Add(match);
//                }

//            }

//            #endregion
		

//            //History matches inlezen
//            int nHistoryMatches = reader.ReadInt32();

//            for (int i = 0; i < nHistoryMatches; i++)
//            {
//                int pouleID = reader.ReadInt32();
//                Match match = new Match(reader, instance.pouledata[pouleID].indeling);

//                //HistoryMatch in de cache stoppen
//                instance.historicMatches.Add(match);
//            }

//            reader.Close();

//            instance.rwlock.ReleaseWriterLock();
//        }

//        static void Delay()
//        {
//#if DEBUG
//            System.Threading.Thread.Sleep(0);
//#endif
//        }


//        #region CREATORS

//        public int CreateTeam(string TeamName, int PlayerID1, int PlayerID2, int PouleID)
//        {
//            Team team = new Team(TeamName, players[PlayerID1], players[PlayerID2]);
//            team.TeamID = nextTeamID.Increment();
//            pouledata[PouleID].indeling.Add(team.TeamID, team);
//            return team.TeamID;
//        }

//        public Match CreateMatch(int PouleID, int Round, int MatchNumber, int TeamID1, int TeamID2)
//        {
//            Match match = new Match(PouleID, Round, MatchNumber, (from t in pouledata[PouleID].indeling.Values where t.TeamID == TeamID1 select t).First(),
//                (from t in pouledata[PouleID].indeling.Values where t.TeamID == TeamID2 select t).First(), "");
//            pouledata[PouleID].matches.Add(match);
//            return match;
//        }

//        public int CreateTeam(int PlayerID, int PouleID)
//        {
//            Team team = new Team(players[PlayerID]);
//            team.TeamID = nextTeamID.Increment();
//            pouledata[PouleID].indeling.Add(team.TeamID, team);
//            return team.TeamID;
//        }

//        #endregion

//        #region GETTERS
		
//        List<User> IDataCache.GetAllUsers()
//        {
//            Delay();
//            return new List<User>(users);
//        }

//        ChangeTrackingList<Poule> IDataCache.GetAllPoules()
//        {
//            Delay();

//            rwlock.AcquireReaderLock(lockTimeout);

//            SerialDefinition serial = SerialCache.Instance.GetOrAdd(new SerialDefinition(SerialTypes.AllPoules));

//            foreach(Poule poule in poules.Values)
//            {
//                poule.SerialNumber = SerialCache.Instance.GetOrAdd(new SerialDefinition(SerialTypes.Poule, poule.PouleID));
//            }
			
//            var result = new ChangeTrackingList<Poule>(poules.Values, serial);
//            rwlock.ReleaseReaderLock();
//            return result;
//        }

//        ChangeTrackingList<Player> IDataCache.GetAllPlayers()
//        {
//            Delay();
//            rwlock.AcquireReaderLock(lockTimeout);

//            SerialDefinition serial = SerialCache.Instance.GetOrAdd(new SerialDefinition(SerialTypes.AllPlayers));
//            var result = new ChangeTrackingList<Player>(players.Values, serial);

//            rwlock.ReleaseReaderLock();

//            return result;
//        }

//        Poule IDataCache.GetPoule(int PouleID)
//        {
//            Delay();
//            rwlock.AcquireReaderLock(lockTimeout);

//            Poule poule = poules[PouleID];
//            poule.SerialNumber = SerialCache.Instance.GetOrAdd(new SerialDefinition(SerialTypes.Poule, PouleID));
//            Poule result = (Poule)poule.Clone();

//            rwlock.ReleaseReaderLock();
//            return result;
//        }

//        ChangeTrackingList<Player> IDataCache.GetPoulePlanning(int PouleID)
//        {
//            Delay();
//            rwlock.AcquireReaderLock(lockTimeout);

//            SerialDefinition serial = SerialCache.Instance.GetOrAdd(new SerialDefinition(SerialTypes.PoulePlanning, PouleID));
//            var result = new ChangeTrackingList<Player>(pouledata[PouleID].planning.Values, serial);

//            rwlock.ReleaseReaderLock();
//            return result;
//        }

//        ChangeTrackingList<Team> IDataCache.GetPouleTeams(int PouleID)
//        {
//            Delay();
//            rwlock.AcquireReaderLock(lockTimeout);

//            SerialDefinition serial = SerialCache.Instance.GetOrAdd(new SerialDefinition(SerialTypes.PouleTeams, PouleID));
//            var result = new ChangeTrackingList<Team>(pouledata[PouleID].indeling.Values, serial);

//            rwlock.ReleaseReaderLock();
//            return result;
//        }

//        List<LadderTeam> IDataCache.GetPouleLadder(int PouleID)
//        {
//            Delay();
//            rwlock.AcquireReaderLock(lockTimeout);
//            List<LadderTeam> teams = new List<LadderTeam>();

//            foreach (LadderTeam team in pouledata[PouleID].ladder.Values)
//            {
//                teams.Add((LadderTeam)team.Clone());
//            }

//            rwlock.ReleaseReaderLock();
//            return teams;
//        }

//        List<Match> IDataCache.GetPouleMatches(int PouleID)
//        {
//            Delay();
//            rwlock.AcquireReaderLock(lockTimeout);
//            List<Match> matches = new List<Match>();

//            foreach (Match match in pouledata[PouleID].matches)
//            {
//                match.SerialNumber = SerialCache.Instance.GetOrAdd(new SerialDefinition(SerialTypes.Match, PouleID, match.MatchNumber));
//                matches.Add((Match)match.Clone());
//            }

//            rwlock.ReleaseReaderLock();
//            return matches;
//        }

//        List<Match> IDataCache.GetAllMatches()
//        {
//            List<Match> result = new List<Match>();
//            List<Match> result2 = new List<Match>();

//            rwlock.AcquireReaderLock(lockTimeout);

//            foreach (List<Match> match in (from p in pouledata.Values where p.matches != null select p.matches))
//            {
//                result.AddRange(match);
//            }

//            foreach (Match match in result)
//            {
//                match.SerialNumber = SerialCache.Instance.GetOrAdd(new SerialDefinition(SerialTypes.Match, match.PouleID, match.MatchNumber));
//                result2.Add((Match)match.Clone());
//            }

//            rwlock.ReleaseReaderLock();
//            return result2;
//        }

//        List<Match> IDataCache.GetAllHistoryMatches()
//        {
//            List<Match> result = new List<Match>();
//            rwlock.AcquireReaderLock(lockTimeout);

//            foreach (Match match in historicMatches)
//            {
//                result.Add((Match)match.Clone());
//            }

//            rwlock.ReleaseReaderLock();
//            return result;
//        }

//        Match IDataCache.GetMatch(int MatchID)
//        {
//            rwlock.AcquireReaderLock(lockTimeout);

//            List<Match> l = new List<Match>();

//            foreach (List<Match> match in (from p in pouledata.Values where p.matches != null select p.matches))
//            {
//                l.AddRange(match);
//            }

//            Match ma = (from m in l
//                           where m.MatchID == MatchID
//                           select m).First();

//            ma.SerialNumber = SerialCache.Instance.GetOrAdd(new SerialDefinition(SerialTypes.Match, MatchID));
//            Match result = (Match)ma.Clone();
//            rwlock.ReleaseReaderLock();
//            return result;
//        }

//        #endregion

//        #region SETTERS

//        bool IDataCache.SetAllPoules(ChangeTrackingList<Poule> changelist, out CommunicationException exception)
//        {
//            Delay();
//            exception = null;
						
//            //Eerst kijken of je een up to date versie hebt van alle poules
//            if (SerialCache.Instance.IsOutOfDate(changelist.SerialNumber))
//            {
//                exception = new DataOutOfDateException();
//                return false;
//            }

//            //Als er niks veranderd is kunnen we gelijk doorgaan
//            if (!changelist.Changed)
//                return true;

//            rwlock.AcquireWriterLock(lockTimeout);

//            //Houd ook bij welke acties er allemaal gebeuren: add/update/delete
//            SerialEventTypes changes = SerialEventTypes.None;

//            foreach (ChangeTrackingList<Poule>.Change poule in changelist.Changes)
//            {
//                switch (poule.type)
//                {
//                    case ChangeTrackingList<Poule>.ChangeType.Removed:
//                        //Verwijder de poule definitie
//                        poules.Remove(poule.item.PouleID);

//                        //Verwijder alle data uit de poules
//                        pouledata.Remove(poule.item.PouleID);

//                        //Verwijder alle historicMatches die met deze poule hebben te maken
//                        historicMatches.RemoveAll(a1 => a1.PouleID == poule.item.PouleID);
						
//                        //We hebben een removed poule:
//                        changes |= SerialEventTypes.Removed;
//                        break;

//                    case ChangeTrackingList<Poule>.ChangeType.Added:
//                        //PouleID invullen
//                        poule.item.PouleID = nextPouleID.Increment();

//                        //Poule toevoegen
//                        poules.Add(poule.item.PouleID, poule.item);

//                        //Wrapper maken voor alle pouledata
//                        pouledata.Add(poule.item.PouleID, new PouleData());

//                        //Voeg een Poule serienummer toe
//                        SerialCache.Instance.AddOrUpdate(new SerialDefinition(SerialTypes.Poule, poule.item.PouleID));
						
//                        //We hebben een add
//                        changes |= SerialEventTypes.Added;
//                        break;
//                }
//            }

//            //Nu willen we alle veranderde objecten updaten
//            foreach (Poule poule in changelist)
//            {
//                if (poule.Changed)
//                {
//                    //We hebben een change
//                    changes |= SerialEventTypes.Changed;

//                    poules[poule.PouleID].UpdateData(poule);
//                    //Update het serienummer van de poules
//                    SerialCache.Instance.IncreaseVersion(new SerialDefinition(SerialTypes.Poule, poule.PouleID));
//                }
//            }

//            rwlock.ReleaseWriterLock();

//            return true;
//        }

//        bool IDataCache.SetAllPlayers(ChangeTrackingList<Player> changelist, out CommunicationException exception)
//        {
//            Delay();
//            exception = null;
//            if (!changelist.Changed)
//                return true;

//            SerialEventTypes changeType = SerialEventTypes.None;

//            rwlock.AcquireWriterLock(lockTimeout);

//            foreach (ChangeTrackingList<Player>.Change item in changelist.Changes)
//            {
//                switch (item.type)
//                {
//                    case ChangeTrackingList<Player>.ChangeType.Added:
//                        changeType |= SerialEventTypes.Added;
//                        item.item.PlayerID = nextPlayerID.Increment();
//                        players.Add(item.item.PlayerID, item.item);
//                        break;

//                    case ChangeTrackingList<Player>.ChangeType.Removed:
//                        changeType |= SerialEventTypes.Removed;
//                        players.Remove(item.item.PlayerID);
//                        break;

//                    default:
//                        break;
//                }
//            }

//            foreach (Player player in changelist)
//            {
//                if (player.Changed)
//                {
//                    players[player.PlayerID].UpdateData(player); //zo moet het, right?
//                    changeType |= SerialEventTypes.Changed; //updated of changed??
//                }
//            }

//            rwlock.ReleaseWriterLock();

//            return true;
//        }

//        bool IDataCache.SetPoule(int PouleID, Poule poule, out CommunicationException exception)
//        {
//            Delay();
//            exception = null;

//            //Eerst kijken of je een up to date versie hebt van de poule
//            if (SerialCache.Instance.IsOutOfDate(poule.SerialNumber))
//            {
//                exception = new DataOutOfDateException();
//                return false;
//            }

//            //Als er niks veranderd is kunnen we gelijk doorgaan
//            if (!poule.Changed)
//                return true;

//            rwlock.AcquireWriterLock(lockTimeout);

//            //Nu gaan we de poule gegevens bijwerken van poule
//            Poule data = poules[poule.PouleID];
//            data.UpdateData(poule);

//            SerialCache.Instance.IncreaseVersion(poule.SerialNumber);

//            rwlock.ReleaseWriterLock();

//            return true;
//        }

//        bool IDataCache.SetPoulePlanning(int PouleID, ChangeTrackingList<Player> changelist, out CommunicationException exception)
//        {
//            Delay();
//            exception = null;

//            //Eerst kijken of je een up to date versie hebt van de planninglijst
//            if (SerialCache.Instance.IsOutOfDate(changelist.SerialNumber))
//            {
//                exception = new DataOutOfDateException();
//                return false;
//            }

//            if (!changelist.Changed)
//                return true;

//            SerialEventTypes changeType = SerialEventTypes.None;

//            rwlock.AcquireWriterLock(lockTimeout);

//            foreach (ChangeTrackingList<Player>.Change item in changelist.Changes)
//            {
//                switch (item.type)
//                {
//                    case ChangeTrackingList<Player>.ChangeType.Added:
//                        changeType |= SerialEventTypes.Added;
//                        pouledata[PouleID].planning.Add(item.item.PlayerID, item.item);
//                        break;

//                    case ChangeTrackingList<Player>.ChangeType.Removed:
//                        changeType |= SerialEventTypes.Removed;
//                        pouledata[PouleID].planning.Remove(item.item.PlayerID);
//                        break;

//                    default:
//                        break;
//                }
//            }

//            foreach (Player player in changelist)
//            {
//                if (player.Changed)
//                {
//                    changeType |= SerialEventTypes.Changed;
//                    pouledata[PouleID].planning[player.PlayerID].UpdateData(player);
//                }
//            }

//            SerialCache.Instance.IncreaseVersion(new SerialDefinition(SerialTypes.PoulePlanning, PouleID));

//            rwlock.ReleaseWriterLock();

//            return true;
//        }

//        bool IDataCache.SetPouleTeams(int PouleID, ChangeTrackingList<Team> changelist, out CommunicationException exception)
//        {
//            Delay();
//            exception = null;

//            //Eerst kijken of je een up to date versie hebt van de teamlijst
//            if (SerialCache.Instance.IsOutOfDate(changelist.SerialNumber))
//            {
//                exception = new DataOutOfDateException();
//                return false;
//            }

//            //Bij geen veranderingen geen nood aan de man
//            if (!changelist.Changed)
//                return true;

//            //Als het zo is dat er iets is toegevoegd / verwijderd terwijl de pouleladder al aan de gang is, mag dat niet
//            if (changelist.Changes.Count > 0 && poules[PouleID].IsRunning)
//            {
//                exception = new PouleAlreadyRunningException();
//                return false;
//            }

//            SerialEventTypes changeType = SerialEventTypes.None;

//            foreach (ChangeTrackingList<Team>.Change item in changelist.Changes)
//            {
//                switch (item.type)
//                {
//                    case ChangeTrackingList<Team>.ChangeType.Added:
//                        changeType |= SerialEventTypes.Added;
//                        item.item.TeamID = nextTeamID.Increment();
//                        pouledata[PouleID].indeling.Add(item.item.TeamID, item.item);
//                        break;

//                    case ChangeTrackingList<Team>.ChangeType.Removed:
//                        changeType |= SerialEventTypes.Removed;
//                        pouledata[PouleID].indeling.Remove(item.item.TeamID);
//                        break;

//                    default:
//                        break;
//                }
//            }

//            foreach (Team team in changelist)
//            {
//                if (team.Changed)
//                {
//                    changeType |= SerialEventTypes.Changed;
//                    pouledata[PouleID].indeling[team.TeamID].UpdateData(team);
//                }
//            }

//            //Nu gaan we de LadderTeams maken:
//            pouledata[PouleID].ladder.Clear();

//            int rank = 0;

//            foreach (Team team in changelist)
//            {
//                //Maak nu een nieuwe LadderTeamWrapper aan
//                rank++;
//                pouledata[PouleID].ladder.Add(team.TeamID, new LadderTeam(PouleID, rank, team));
//            }

//            //Wel natuurlijk even het serienummer updaten
//            SerialCache.Instance.IncreaseVersion(new SerialDefinition(SerialTypes.PouleTeams, PouleID));
//            SerialCache.Instance.IncreaseVersion(new SerialDefinition(SerialTypes.PouleLadder, PouleID));

//            return true;
//        }

//        List<Match> IDataCache.GetPouleHistoryMatches(int PouleID)
//        {
//            Delay();

//            rwlock.AcquireReaderLock(lockTimeout);

//            List<Match> result = new List<Match>(from match in historicMatches
//                                                 where match.PouleID == PouleID
//                                                 select match);

//            rwlock.ReleaseReaderLock();

//            return result;
//        }

//        bool IDataCache.PouleFinishRound(int PouleID, out CommunicationException exception)
//        {
//            Delay();
//            exception = null;

//            rwlock.AcquireWriterLock(lockTimeout);

//            //We gaan er van uit dat alle teams gebruikt zijn
//            //dus gaan we nu alle huidige matches omzetten naar history matches
//            foreach (Match match in pouledata[PouleID].matches)
//            {
//                match.Cached = true;
//                historicMatches.Add(match);
//            }

//            //Maak de lijst met huidige matches leeg
//            pouledata[PouleID].matches.Clear();

//            poules[PouleID].IsRunning = false;

//            SerialCache.Instance.IncreaseVersion(new SerialDefinition(SerialTypes.PouleMatches, PouleID));
//            SerialCache.Instance.IncreaseVersion(new SerialDefinition(SerialTypes.Poule, PouleID));

//            rwlock.ReleaseWriterLock();

//            return true;
//        }

//        bool IDataCache.SetPouleNextRound(int PouleID, List<Match> matches, out CommunicationException exception)
//        {
//            Delay();
//            exception = null;

//            rwlock.AcquireWriterLock(lockTimeout);

//            //We gaan er van uit dat alle teams gebruikt zijn
//            //dus gaan we nu alle huidige matches omzetten naar history matches
//            foreach (Match match in pouledata[PouleID].matches)
//            {
//                match.Cached = true;
//                historicMatches.Add(match);
//            }
			
//            //Maak de lijst met huidige matches leeg
//            pouledata[PouleID].matches.Clear();
			
//            //Volgende ronde in de Poule
//            poules[PouleID].Round ++;
//            int round = poules[PouleID].Round;
//            short matchnumber = 0;

//            //Nu elke Match langslopen en zorgen dat de gegevens goed zijn ingevuld
//            if (matches != null)
//            {
//                foreach (Match match in matches)
//                {
//                    matchnumber++;
//                    match.PouleID = PouleID;
//                    match.Round = round;
//                    match.MatchNumber = matchnumber;

//                    //Natuurlijk ook even de versie van de Match updaten
//                    SerialCache.Instance.IncreaseVersion(new SerialDefinition(SerialTypes.Match, PouleID, matchnumber));
//                }
//            }
			
//            //Sla de Matches op in de de pouledata
//            pouledata[PouleID].matches = matches;

//            //Nu zijn we aan het spelen, dus Poule is running = true
//            poules[PouleID].IsRunning = true;

//            SerialCache.Instance.IncreaseVersion(new SerialDefinition(SerialTypes.PouleMatches, PouleID));
//            SerialCache.Instance.IncreaseVersion(new SerialDefinition(SerialTypes.Poule, PouleID));

//            rwlock.ReleaseWriterLock();

//            return true;
//        }

//        bool IDataCache.SetMatch(Match match, out CommunicationException exception)
//        {
//            Delay();
//            exception = null;

//            //Kijk eerst of de data up-to-date is
//            if (SerialCache.Instance.IsOutOfDate(match.SerialNumber))
//            {
//                exception = new DataOutOfDateException();
//                return false;
//            }

//            //Als er niks is veranderd, hoeven we ook niet te updaten
//            if (!match.Changed)
//                return true;

//            rwlock.AcquireWriterLock(lockTimeout);

//            Match m = (from mtch in pouledata[match.PouleID].matches
//                      where mtch.Round == match.Round & mtch.MatchNumber == match.MatchNumber
//                      select mtch).First();

//            //Nu gaan we de ladder updaten
//            LadderTeam TeamA = pouledata[match.PouleID].ladder[match.TeamA.TeamID];
//            LadderTeam TeamB = pouledata[match.PouleID].ladder[match.TeamB.TeamID];

//            m.UpdateLadder(ref TeamA, ref TeamB, match);
//            m.UpdateData(match);

//            SerialCache.Instance.IncreaseVersion(new SerialDefinition(SerialTypes.Match, m.PouleID, m.MatchNumber));
//            SerialCache.Instance.IncreaseVersion(new SerialDefinition(SerialTypes.PouleLadder, m.PouleID));
//            SerialCache.Instance.IncreaseVersion(new SerialDefinition(SerialTypes.PouleMatches, m.PouleID));
			
//            //Nu moeten we de ladder weer sorteren:
//            IEnumerable<LadderTeam> newladder = from t in pouledata[m.PouleID].ladder.Values
//                                                orderby ((float)t.SetsWon / t.MatchesPlayed) descending, ((t.PointsWon - t.PointsLost) / (float)t.MatchesPlayed) descending
//                                                select t;

//            //Maak een tellertje aan voor de Ranking
//            int position = 0;

//            //Loop nu over de gesorteerde lijst en vul de rank in
//            foreach (LadderTeam t in newladder)
//            {
//                position++;
//                t.Rank = position;
//            }

//            rwlock.ReleaseWriterLock();
	
//            return true;
//        }

//        #endregion
//    }
//}
