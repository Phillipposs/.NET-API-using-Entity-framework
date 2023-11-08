using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zadatak1.DTOs;
using Zadatak1.Models;

namespace Zadatak1.Services
{
    public class LeagueService : ILeaguesService
    {
        private readonly ILeagueRepository _leagueRepository;
        public LeagueService(ILeagueRepository leagueRepository)
        {
            _leagueRepository = leagueRepository;
        }
        public int CreateLeague(string leagueTitle)
        {
            League league = new League()
            {
                Title = leagueTitle
            };
           var leagueId= _leagueRepository.InsertLeague(league.Title);
            return leagueId;
        }
        public List<LeagueInfoDTO> GetLeagues()
        {
            return _leagueRepository.GetLeagues();
        }
    }
}
