using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zadatak1.Services;

namespace Zadatak1.Controllers
{
    [Route("api/standings")]
    public class StandingsController : Controller
    {
        private readonly IStandingsService _standingsService;
        public StandingsController( IStandingsService standingsService)
        {
            _standingsService = standingsService;
        }
        [HttpGet("/standings/getteamstandings/{leagueId}")]
        public IActionResult GetTeamStandings(int leagueId)
        {
            var standings = _standingsService.GetGroupStandings(leagueId);
            if (standings.Count>0)
                return Ok(standings);
            else
                return NotFound();
        }
    }
}
