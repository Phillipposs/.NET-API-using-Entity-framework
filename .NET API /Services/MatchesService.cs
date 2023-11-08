using System.Collections.Generic;
using Zadatak1.DTOs;
using Zadatak1.Models;

namespace Zadatak1.Services
{
    public class MatchesService : IMatchesService
    {
        private readonly ILeagueRepository _leagueRepository;
        public MatchesService(ILeagueRepository leagueRepository)
        {
            _leagueRepository = leagueRepository;
        }

        public  void CreateMatchesForLeague(int leagueId)
        {
            _leagueRepository.CreateMatchesForLeague(leagueId);
        }

        public List<MatchDTO> GetMatches(int leagueId)
        {
           return _leagueRepository.GetMatches(leagueId);
        }

        public List<MatchDTO> GetMatchesByDay(int leagueId, int matchDay)
        {

            return _leagueRepository.GetMatchesByDay(leagueId, matchDay);
        }

        public List<MatchDTO> GetMatchesByDayAndGroup(int leagueId, int matchDay, int groupId)
        {
            return _leagueRepository.GetMatchesByDayAndGroup(leagueId, matchDay, groupId);
        }

        public List<MatchDTO> GetMatchesByGroup(int leagueId, int groupId)
        {
            return _leagueRepository.GetMatchesByGroup(leagueId, groupId);
        }

        public bool UpdateMatches(List<MatchDTO> updateMatchDTOs)
        {
           return _leagueRepository.UpdateMatches(updateMatchDTOs);
        }
    }
}
