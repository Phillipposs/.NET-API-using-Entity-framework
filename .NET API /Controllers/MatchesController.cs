using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zadatak1.DTOs;
using Zadatak1.Services;

namespace Zadatak1.Controllers
{
    [Route("api/matches")]
    public class MatchesController : Controller
    {
        private readonly IMatchesService _matchesService;
        public MatchesController(IMatchesService matchesService)
        {
            _matchesService = matchesService;
        }
        [HttpPost("/matches/updatematchscores")]
        public IActionResult UpdateMatchScores([FromBody]List<MatchDTO> updateMatchDTOs)
        {
            if (updateMatchDTOs == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _matchesService.UpdateMatches(updateMatchDTOs);
            return Ok();
        }
        [HttpGet("/matches/getmatchesfromleague/{id}")]
        public IActionResult GetMatchesFromLeague(int id, bool filterByDay ,bool filterByGroup,int matchDay, int groupId)
        {
            var matches = new List<MatchDTO>();
            if(filterByDay && !filterByGroup)
                matches = _matchesService.GetMatchesByDay(id,matchDay);
            else if (filterByDay && filterByGroup)
                    matches = _matchesService.GetMatchesByDayAndGroup(id, matchDay, groupId);
                else if(filterByGroup && !filterByDay)
                    matches = _matchesService.GetMatchesByGroup(id, groupId);
                else
                matches = _matchesService.GetMatches(id);
            
           
            if (matches.Count > 0)
                return Ok(matches);
            else
                return NotFound();
        }
    }
}
