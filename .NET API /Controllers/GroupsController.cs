using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zadatak1.DTOs;
using Zadatak1.Models;
using Zadatak1.Services;

namespace Zadatak1.Controllers
{
    [Route("api/groups")]
    public class GroupsController : Controller
    {
        private readonly IGroupService _groupService;
        private readonly IGroupTeamsService _groupTeamsService;
        public GroupsController(IGroupService groupService, IGroupTeamsService groupTeamsService)
        {
            _groupService = groupService;
            _groupTeamsService = groupTeamsService;
        }
        [HttpGet("/groups/getgroupswithteams/{id}")]
        public IActionResult GetGroupsWithTeams(int id)
        {

            var groupsWithTeams = new List<GroupWithTeamsDTO>();
            var groups = _groupService.GetGroups(id);

            foreach (Group group in groups)
            {
                List<string> teamNames = new List<string>();
                List<GroupTeam> groupTeams = _groupTeamsService.GetGroupTeams(group.Id);
                foreach (var groupTeam in groupTeams)
                {
                    teamNames.Add(groupTeam.Name);
                }
                groupsWithTeams.Add(new GroupWithTeamsDTO { Title = group.Title, Teams = teamNames });
            }
            return Ok(groupsWithTeams);
        }
    }
}
