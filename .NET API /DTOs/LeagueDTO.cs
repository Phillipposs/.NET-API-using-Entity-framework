using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zadatak1.Models;

namespace Zadatak1.DTOs
{
    public class LeagueDTO
    {
        public string LeagueTitle { get; set; }
        public IList<GroupDTO> Groups { get; set; }
    }
}
