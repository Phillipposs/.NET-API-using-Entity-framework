using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zadatak1.DTOs
{
    public class UpdateMatchDTO
    {
        public string LeagueTitle { get; set; }
        public string Group { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string KickOffAt { get; set; }
        public string Score { get; set; }
    }
}
