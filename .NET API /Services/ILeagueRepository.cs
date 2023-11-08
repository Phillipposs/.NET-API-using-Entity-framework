using System;
using System.Collections.Generic;
using Zadatak1.DTOs;
using Zadatak1.Models;

namespace Zadatak1.Services
{
    public interface ILeagueRepository
    {
        #region League
        List<LeagueInfoDTO> GetLeagues();    
        int InsertLeague(string leagueId);
        #endregion League

        #region Group
        IEnumerable<Group> GetGroups(int id);
        Group GetGroup(int id);
        int InsertGroup(string groupTitle, int leagueId);
       
        void CreateGroupFromMatches(ICollection<Match> matches);
   
        void CreateGroupsAndTeams(IList<GroupDTO> groupDTOs, int leagueId);
        #endregion Group
        #region Match
        List<MatchDTO> GetMatches(int leagueId);
        List<MatchDTO> GetMatchesByDayAndGroup(int leagueId, int matchDay, int groupId);
        List<MatchDTO> GetMatchesByDay(int leagueId, int matchDay);
        void CreateMatchesForLeague(int leagueId);
        void InsertMatch(Match match);
        bool UpdateMatch(MatchDTO match);
        bool UpdateMatches(List<MatchDTO> updateMatchDTOs);
        #endregion Match
        #region GroupTeam
        void InsertGroupTeam(GroupTeam groupTeam);
        List<MatchDTO> GetMatchesByGroup(int leagueId, int groupId);
        List<GroupTeam> GetGroupTeams(int groupId);
        #endregion GroupTeam
        #region GroupStanding
        List<GroupStandingsDTO> GetGroupStandings(int leagueId);
        void InsertStranding(Standing standing);
        #endregion GroupStanding
    }
}
