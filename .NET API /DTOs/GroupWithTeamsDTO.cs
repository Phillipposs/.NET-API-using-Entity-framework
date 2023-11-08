using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zadatak1.Models;

namespace Zadatak1.DTOs
{
    public class GroupWithTeamsDTO
    {
        public string Title { get; set; }
        public List<string> Teams { get; set; }
    }
}
