using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Zadatak1.Models
{
  

    public class Standing
    {
        [Key]
        public int id { get; set; }
        public int Rank { get; set; }
        public int TeamId { get; set; }
        public GroupTeam Team { get; set; }
        public int PlayedGames { get; set; }
        public int Win { get; set; }
        public int Lose { get; set; }
        public int Draw { get; set; }
        public int Goals { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalDifference { get; set; }
        public int Points { get; set; }
    }
}
