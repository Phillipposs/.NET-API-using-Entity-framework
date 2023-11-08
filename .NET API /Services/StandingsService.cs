using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zadatak1.DTOs;
using Zadatak1.Entities;
using Zadatak1.Models;

namespace Zadatak1.Services
{
    public class StandingsService : IStandingsService
    {
        private readonly ILeagueRepository _leagueRepository;
        public StandingsService(ILeagueRepository leagueRepository)
        {
            _leagueRepository = leagueRepository;
        }

        public List<GroupStandingsDTO> GetGroupStandings(int leagueId)
        {
            return _leagueRepository.GetGroupStandings(leagueId);
        }
    }
}
