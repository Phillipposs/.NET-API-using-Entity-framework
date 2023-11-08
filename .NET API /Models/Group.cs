using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zadatak1.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int LeagueId { get; set; }
        public League League { get; set; }
        public List<Match>  Matches { get; set; }
        public List<GroupTeam>  GroupTeams { get; set; }
    }
}
