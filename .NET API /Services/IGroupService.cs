using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zadatak1.DTOs;
using Zadatak1.Entities;
using Zadatak1.Models;

namespace Zadatak1.Services
{
    public interface IGroupService
    {
        IEnumerable<Group> GetGroups(int id);
        Group GetGroup(int id);
        int InsertGroup(string groupTitle, int leagueId);
        void CreateGroupsAndTeams(IList<GroupDTO> groupDTOs, int leagueId);
        bool DeleteGroup(int id);
    }
}
