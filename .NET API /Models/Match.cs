using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zadatak1.Models
{
    public class Match
    {
        public int Id { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }

        public int MatchDay { get; set; }

        public int HomeTeamId { get; set; }
        public GroupTeam HomeTeam { get; set; }

        public int AwayTeamId { get; set; }
        public GroupTeam AwayTeam { get; set; }

        public DateTime? KickOffAt { get; set; }

        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }
    }
}
