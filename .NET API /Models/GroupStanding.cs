using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zadatak1.Models
{
    public class GroupStanding
    {
        public string LeagueTitle { get; set; }
        public int Matchday { get; set; }
        public string Group { get; set; }
        public List<Standing> Standings { get; set; }
    }
}
