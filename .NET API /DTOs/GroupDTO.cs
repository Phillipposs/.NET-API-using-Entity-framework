using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zadatak1.Models;

namespace Zadatak1.DTOs
{
    public class GroupDTO
    {
        public string Title { get; set; }
        public IList<string> Teams { get; set; }
    }
}
