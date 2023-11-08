using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zadatak1.Models;

namespace Zadatak1.Services
{
    public class GroupTeamsService : IGroupTeamsService
    {
        private readonly ILeagueRepository _leagueRepository;
        public GroupTeamsService(ILeagueRepository leagueRepository)
        {
            _leagueRepository = leagueRepository;
        }
        public List<GroupTeam> GetGroupTeams(int groupId)
        {
           return _leagueRepository.GetGroupTeams(groupId);
        }
    }
}
