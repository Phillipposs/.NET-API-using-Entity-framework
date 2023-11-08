using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zadatak1.DTOs;
using Zadatak1.Entities;
using Zadatak1.Models;

namespace Zadatak1.Services
{
    public class GroupService : IGroupService
    {
        private readonly ILeagueRepository _leagueRepository;
        public GroupService(ILeagueRepository leagueRepository)
        {
            _leagueRepository = leagueRepository;
        }

        public bool DeleteGroup(int id)
        {
            throw new NotImplementedException();
        }

        public Group GetGroup(int id)
        {
            return _leagueRepository.GetGroup(id);
        }

        public IEnumerable<Group> GetGroups(int id)
        {
            return _leagueRepository.GetGroups(id);

        }

        public int InsertGroup(string groupTitle, int leagueId)
        {
            return _leagueRepository.InsertGroup(groupTitle, leagueId);
        }

        public void CreateGroupsAndTeams(IList<GroupDTO> groupDTOs, int leagueId)
        {
            _leagueRepository.CreateGroupsAndTeams( groupDTOs, leagueId);
        }
    }
}
