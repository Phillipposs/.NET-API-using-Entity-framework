using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zadatak1.DTOs;
using Zadatak1.Models;

namespace Zadatak1.Services
{
    public interface ILeaguesService
    {
        List<LeagueInfoDTO> GetLeagues();
        int CreateLeague(string leagueTitle);
    }
}
