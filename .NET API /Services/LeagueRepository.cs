using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zadatak1.DTOs;
using Zadatak1.Entities;
using Zadatak1.Models;

namespace Zadatak1.Services
{
    public class LeagueRepository : ILeagueRepository
    {
        private LeagueContext LeagueContext { get; }
        public LeagueRepository(LeagueContext leagueContext)
        {
            LeagueContext = leagueContext;
        }
        #region Group

        public Group GetGroup(int id)
        {
            return LeagueContext.Groups
                     .Where(g => g.Id == id).FirstOrDefault();
        }

        public IEnumerable<Group> GetGroups(int id)
        {
            return LeagueContext.Groups.Where(g => g.LeagueId == id).ToList();


        }

        public int InsertGroup(string groupTitle, int leagueId)
        {
            var group = new Group { Title = groupTitle, LeagueId = leagueId };
            LeagueContext.Groups.Add(group);
            LeagueContext.SaveChanges();
            return group.Id;

        }

        public void CreateGroupFromMatches(ICollection<Match> matches)
        {
            throw new NotImplementedException();
        }
        #endregion Group
        #region League
        public int InsertLeague(string leagueTitle)
        {
            var league = new League { Title = leagueTitle };
            LeagueContext.Leagues.Add(league);
            LeagueContext.SaveChanges();
            return league.Id;


        }

        public List<LeagueInfoDTO> GetLeagues()
        {
            List<LeagueInfoDTO> leagueInfoDTOs = new List<LeagueInfoDTO>();
            var leagues = LeagueContext.Leagues.ToList();
            foreach (var league in leagues)
            {
                leagueInfoDTOs.Add(new LeagueInfoDTO { Id = league.Id, Title = league.Title });
            }
            return leagueInfoDTOs;
        }


        #endregion League
        #region Team
       
        public void CreateMatchesForLeague(int leagueId)
        {
            int indexOne = 0;
            int indexTwo = 0;
            int matchDay = 1;

            Random a = new Random();
            List<int> randomList = new List<int>();
            var groups = LeagueContext.Groups.Where(g => g.LeagueId == leagueId).ToList();
            foreach (var group in groups)
            {
                var groupTeams = LeagueContext.GroupTeams.Where(gt => gt.GroupId == group.Id).ToList();
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        indexOne = a.Next(0, 4);
                        indexTwo = a.Next(0, 4);
                        while (LeagueContext.Matches.Where(m => (m.HomeTeam.Id == groupTeams.ElementAt(indexOne).Id && m.AwayTeamId == groupTeams.ElementAt(indexTwo).Id) && m.GroupId == group.Id).SingleOrDefault() != null || indexOne == indexTwo || randomList.Contains(indexOne) || randomList.Contains(indexTwo))
                        {
                            indexOne = a.Next(0, 4);
                            indexTwo = a.Next(0, 4);
                        }

                        randomList.Add(indexOne);
                        randomList.Add(indexTwo);


                        Match match = new Match()
                        {
                            GroupId = group.Id,
                            HomeTeam = groupTeams.ElementAt(indexOne),
                            AwayTeam = groupTeams.ElementAt(indexTwo),
                            MatchDay = matchDay
                        };
                        InsertMatch(match);
                    }
                    randomList.Clear();
                    matchDay++;
                    if (matchDay > 6)
                        matchDay = 1;
                }
            }

        }

        public void CreateGroupsAndTeams(IList<GroupDTO> groupDTOs, int leagueId)
        {
            foreach (var group in groupDTOs)
            {
                int groupId = InsertGroup(group.Title, leagueId);
                foreach (var team in group.Teams)
                {
                    GroupTeam groupTeam = new GroupTeam()
                    {
                        GroupId = groupId,
                        Name = team

                    };
                    InsertGroupTeam(groupTeam);
                }

            }
        }
        #endregion Team



        public void InsertMatch(Match match)
        {

            LeagueContext.Matches.Add(match);
            LeagueContext.SaveChanges();

        }

        public bool UpdateMatch(MatchDTO match)
        {
            var dbMatch = LeagueContext.Matches.FirstOrDefault(m => m.HomeTeam.Name == match.HomeTeam && m.AwayTeam.Name == match.AwayTeam && m.GroupId==match.GroupId);
            if (dbMatch == null)
                return false;
            string[] scores = match.Score.Split(':')!=null ? match.Score.Split(':') : new string[2] { "","" };
            dbMatch.HomeGoals = scores.ElementAt(0)!="" ? Convert.ToInt32(scores.ElementAt(0)) : 0;
            dbMatch.AwayGoals = (scores.Count() > 1 && scores.ElementAt(1) != "" ) ? Convert.ToInt32(scores.ElementAt(1)) : 0;
            if(!match.KickOffAt.Equals(""))
            dbMatch.KickOffAt =  DateTime.Parse(match.KickOffAt);
            LeagueContext.SaveChanges();

            return true;
        }


        public void InsertGroupTeam(GroupTeam groupTeam)
        {

            LeagueContext.GroupTeams.Add(groupTeam);
            LeagueContext.SaveChanges();

        }

        public List<GroupTeam> GetGroupTeams(int groupId)
        {
            return LeagueContext.GroupTeams.Where(gt => gt.GroupId == groupId).ToList();
        }

        public List<MatchDTO> GetMatches(int leagueId)
        {
            if (LeagueContext.Leagues.Where(l => l.Id == leagueId).SingleOrDefault() == null)
                return new List<MatchDTO>();
            var leagueTitle = LeagueContext.Leagues.Where(l => l.Id == leagueId).SingleOrDefault().Title;
            List<MatchDTO> matches = new List<MatchDTO>();
            IEnumerable<Group> groups = GetGroups(leagueId);
            foreach (var group in groups)
            {
                foreach (var match in LeagueContext.Matches.Where(m => m.GroupId == group.Id).ToList())
                {
                    string score = "";
                    var homeTeamName = LeagueContext.GroupTeams.Where(gt => gt.Id == match.HomeTeamId).SingleOrDefault().Name;
                    var awayTeamName = LeagueContext.GroupTeams.Where(gt => gt.Id == match.AwayTeamId).SingleOrDefault().Name;
                    if (match.KickOffAt != null)
                        score = match.HomeGoals + ":" + match.AwayGoals;
                    matches.Add(new MatchDTO { LeagueTitle = leagueTitle, Group = group.Title,GroupId=group.Id, HomeTeam = homeTeamName, AwayTeam = awayTeamName, KickOffAt = match.KickOffAt.ToString(), Score = score, MatchDay = match.MatchDay });
                }

            }
            return matches;
        }

        public bool UpdateMatches(List<MatchDTO> updateMatchDTOs)
        {
            bool isUpdated = false;
            foreach (var matchDTO in updateMatchDTOs)
            {
                isUpdated = UpdateMatch(new MatchDTO { AwayTeam = matchDTO.AwayTeam, Group = matchDTO.Group, GroupId= matchDTO.GroupId, HomeTeam = matchDTO.HomeTeam, KickOffAt = matchDTO.KickOffAt, LeagueTitle = matchDTO.LeagueTitle, Score = matchDTO.Score });
            }
            return isUpdated;
        }

        public List<GroupStandingsDTO> GetGroupStandings(int leagueId)
        {
            List<GroupStandingsDTO> groupStandingsDTOs = new List<GroupStandingsDTO>();
            
            
            var groups = LeagueContext.Groups.Where(g => g.LeagueId == leagueId).ToList();
            foreach( var group in groups)
            {
                List<StandingDTO> standingDTOs = new List<StandingDTO>();
                foreach (var team in LeagueContext.GroupTeams.Where(gt => gt.GroupId == group.Id))
                {
                    int homegoals = LeagueContext.Matches.Where(m => m.HomeTeamId == team.Id).ToList().Sum(x => x.HomeGoals);
                    int homegoalsAgainst = LeagueContext.Matches.Where(m => m.HomeTeamId == team.Id).ToList().Sum(x => x.AwayGoals);
                    int awaygoals = LeagueContext.Matches.Where(m => m.AwayTeamId == team.Id).ToList().Sum(x => x.AwayGoals);
                    int awaygoalsAgainst = LeagueContext.Matches.Where(m => m.AwayTeamId == team.Id).ToList().Sum(x => x.HomeGoals);
                    int homeWins = LeagueContext.Matches.Where(m => m.HomeTeamId == team.Id && m.HomeGoals > m.AwayGoals).ToList().Count();
                    int awayWins = LeagueContext.Matches.Where(m => m.AwayTeamId == team.Id && m.AwayGoals > m.HomeGoals).ToList().Count();
                    int homeLoses = LeagueContext.Matches.Where(m => m.HomeTeamId == team.Id && m.HomeGoals < m.AwayGoals).ToList().Count();
                    int awayLoses = LeagueContext.Matches.Where(m => m.AwayTeamId == team.Id && m.AwayGoals < m.HomeGoals).ToList().Count();
                    int homeDraws = LeagueContext.Matches.Where(m => m.HomeTeamId == team.Id && m.HomeGoals == m.AwayGoals && m.KickOffAt!=null).ToList().Count();
                    int awayDraws = LeagueContext.Matches.Where(m => m.AwayTeamId == team.Id && m.AwayGoals == m.HomeGoals && m.KickOffAt != null).ToList().Count();

                    Standing standing = new Standing()
                    {
                        TeamId = team.Id,
                        Goals = homegoals + awaygoals,
                        GoalsAgainst = homegoalsAgainst + awaygoalsAgainst,
                        Win = homeWins + awayWins,
                        Lose = homeLoses + awayLoses,
                        Draw = homeDraws + awayDraws,
                        GoalDifference = Math.Abs((homegoals + awaygoals) - (homegoalsAgainst + awaygoalsAgainst)),
                        Points = 3 * (homeWins + awayWins) + (homeDraws + awayDraws)

                    };
                    InsertStranding(standing);
                }
                int rank = 1;
                List<Standing> sortedStandings = LeagueContext.Standings.Where(s=>s.Team.GroupId==group.Id).OrderBy(s => s.Points).ThenBy(p => p.GoalDifference).ThenBy(p => p.Goals).ToList();
                sortedStandings.Reverse();
                foreach (var sortedStanding in sortedStandings)
                {

                    StandingDTO standingDTO = new StandingDTO()
                    {
                        Rank = rank,
                        Team = LeagueContext.GroupTeams.Where(gt => gt.Id == sortedStanding.TeamId).SingleOrDefault().Name,
                        Goals = sortedStanding.Goals,
                        GoalsAgainst = sortedStanding.GoalsAgainst,
                        Win = sortedStanding.Win,
                        Lose = sortedStanding.Lose,
                        Draw = sortedStanding.Draw,
                        GoalDifference = sortedStanding.GoalDifference,
                        Points = sortedStanding.Points

                    };
                    sortedStanding.Rank = rank;
                    LeagueContext.SaveChanges();
                    rank++;
                    if (rank > 4)
                        rank = 1;
                    standingDTOs.Add(standingDTO);
                }
                GroupStandingsDTO groupStandingsDTO = new GroupStandingsDTO()
                {
                    LeagueTitle = LeagueContext.Leagues.Where(l => l.Id == leagueId).SingleOrDefault().Title,
                    GroupTitle = group.Title,
                    Standings = standingDTOs

                };
                groupStandingsDTOs.Add(groupStandingsDTO);
            }

            return groupStandingsDTOs;
        }

        public void InsertStranding(Standing standing)
        {
            if(LeagueContext.Standings.Where(s => s.TeamId==standing.TeamId).SingleOrDefault()==null)
            {
                LeagueContext.Standings.Add(standing);
                LeagueContext.SaveChanges();
            }
            else
            {
                var s = LeagueContext.Standings.Where(st => st.TeamId == standing.TeamId).SingleOrDefault();
                s.GoalDifference = standing.GoalDifference;
                s.Goals = standing.Goals;
                s.GoalsAgainst = standing.GoalsAgainst;
                s.Lose = standing.Lose;
                s.Draw = standing.Draw;
                s.Win = standing.Win;
                s.Rank = standing.Rank;
                s.Points = standing.Points;
                LeagueContext.SaveChanges();
            }

        }

        public List<MatchDTO> GetMatchesByDayAndGroup(int leagueId, int matchDay, int groupId)
        {
            if (LeagueContext.Leagues.Where(l => l.Id == leagueId).SingleOrDefault() == null)
                return new List<MatchDTO>();
            var leagueTitle = LeagueContext.Leagues.Where(l => l.Id == leagueId).SingleOrDefault().Title;
            List<MatchDTO> matches = new List<MatchDTO>();
            IEnumerable<Group> groups = GetGroups(leagueId);
            var group = LeagueContext.Groups.Where(g => g.Id == groupId).SingleOrDefault();
                foreach (var match in LeagueContext.Matches.Where(m => m.GroupId == groupId && m.MatchDay==matchDay).ToList())
                {
                    string score = "";
                    var homeTeamName = LeagueContext.GroupTeams.Where(gt => gt.Id == match.HomeTeamId).SingleOrDefault().Name;
                    var awayTeamName = LeagueContext.GroupTeams.Where(gt => gt.Id == match.AwayTeamId).SingleOrDefault().Name;
                    if (match.KickOffAt != null)
                        score = match.HomeGoals + ":" + match.AwayGoals;
                    matches.Add(new MatchDTO { LeagueTitle = leagueTitle, Group = group.Title, GroupId = group.Id, HomeTeam = homeTeamName, AwayTeam = awayTeamName, KickOffAt = match.KickOffAt.ToString(), Score = score, MatchDay = match.MatchDay });
                }

            
            return matches;
        }

        public List<MatchDTO> GetMatchesByDay(int leagueId, int matchDay)
        {
            if (LeagueContext.Leagues.Where(l => l.Id == leagueId).SingleOrDefault() == null)
                return new List<MatchDTO>();
            var leagueTitle = LeagueContext.Leagues.Where(l => l.Id == leagueId).SingleOrDefault().Title;
            List<MatchDTO> matches = new List<MatchDTO>();
            IEnumerable<Group> groups = GetGroups(leagueId);
            foreach (var group in groups)
            {
                foreach (var match in LeagueContext.Matches.Where(m => m.GroupId == group.Id && m.MatchDay==matchDay).ToList())
                {
                    string score = "";
                    var homeTeamName = LeagueContext.GroupTeams.Where(gt => gt.Id == match.HomeTeamId).SingleOrDefault().Name;
                    var awayTeamName = LeagueContext.GroupTeams.Where(gt => gt.Id == match.AwayTeamId).SingleOrDefault().Name;
                    if (match.KickOffAt != null)
                        score = match.HomeGoals + ":" + match.AwayGoals;
                    matches.Add(new MatchDTO { LeagueTitle = leagueTitle, Group = group.Title, GroupId = group.Id, HomeTeam = homeTeamName, AwayTeam = awayTeamName, KickOffAt = match.KickOffAt.ToString(), Score = score, MatchDay = match.MatchDay });
                }

            }
            return matches;
        }

        public List<MatchDTO> GetMatchesByGroup(int leagueId, int groupId)
        {
            if (LeagueContext.Leagues.Where(l => l.Id == leagueId).SingleOrDefault() == null)
                return new List<MatchDTO>();
            var leagueTitle = LeagueContext.Leagues.Where(l => l.Id == leagueId).SingleOrDefault().Title;
            List<MatchDTO> matches = new List<MatchDTO>();
            IEnumerable<Group> groups = GetGroups(leagueId);
            var group = LeagueContext.Groups.Where(g => g.Id == groupId).SingleOrDefault();
            foreach (var match in LeagueContext.Matches.Where(m => m.GroupId == groupId ).ToList())
            {
                string score = "";
                var homeTeamName = LeagueContext.GroupTeams.Where(gt => gt.Id == match.HomeTeamId).SingleOrDefault().Name;
                var awayTeamName = LeagueContext.GroupTeams.Where(gt => gt.Id == match.AwayTeamId).SingleOrDefault().Name;
                if (match.KickOffAt != null)
                    score = match.HomeGoals + ":" + match.AwayGoals;
                matches.Add(new MatchDTO { LeagueTitle = leagueTitle, Group = group.Title, GroupId = group.Id, HomeTeam = homeTeamName, AwayTeam = awayTeamName, KickOffAt = match.KickOffAt.ToString(), Score = score, MatchDay = match.MatchDay });
            }


            return matches;
        }
    }
}
