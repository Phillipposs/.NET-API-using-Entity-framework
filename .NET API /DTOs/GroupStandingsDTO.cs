using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zadatak1.DTOs
{
    public class GroupStandingsDTO
    {
        public string GroupTitle { get; set; }
        public string LeagueTitle { get; set; }
        public ICollection<StandingDTO> Standings { get; set; }
    }
}
