using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zadatak1.DTOs;
using Zadatak1.Models;

namespace Zadatak1.Services
{
    public interface IMatchesService
    {
        void CreateMatchesForLeague(int leagueId);
        List<MatchDTO> GetMatches(int leagueId);
        List<MatchDTO> GetMatchesByDay(int leagueId,int matchDay);
        bool UpdateMatches(List<MatchDTO> updateMatchDTOs);
        List<MatchDTO> GetMatchesByDayAndGroup(int leagueId, int matchDay, int groupId);
        List<MatchDTO> GetMatchesByGroup(int id, int groupId);
    }
}
