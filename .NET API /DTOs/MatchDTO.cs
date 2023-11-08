using System;

using System.Linq;
using System.Threading.Tasks;

namespace Zadatak1.DTOs
{
    public class MatchDTO
    {

        public string LeagueTitle { get; set; }
        public int MatchDay { get; set; }
        public string Group { get; set; }
        public int GroupId { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string KickOffAt { get; set; }
        public string Score { get; set; }
    }
}
