using Microsoft.AspNetCore.Mvc;
using Zadatak1.DTOs;
using Zadatak1.Services;

namespace Zadatak1.Controllers
{
    [Route("api/league")]
    public class LeaguesController : Controller
    {
        private readonly ILeaguesService _leagueService;
        private readonly IMatchesService _matchesService;
        private readonly IGroupService _groupService;
        public LeaguesController(ILeaguesService leaguesService, IMatchesService matchesService, IGroupService groupService)
        {
            _leagueService = leaguesService;
            _matchesService = matchesService;
            _groupService = groupService;

        }


        [HttpPost("/league/createnewleague")]
        public IActionResult CreateNewLeague([FromBody] LeagueDTO leagueDTO)
        {
            if (leagueDTO == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int leagueId = _leagueService.CreateLeague(leagueDTO.LeagueTitle);
            _groupService.CreateGroupsAndTeams(leagueDTO.Groups, leagueId);
            _matchesService.CreateMatchesForLeague(leagueId);
            return Ok();
        }


        [HttpGet("/league/getallleagues")]
        public IActionResult GetAllLeagues()
        {
            var leagues = _leagueService.GetLeagues();
            if (leagues.Count > 0)
                return Ok(leagues);
            else
                return NotFound();
        }


    }
}
