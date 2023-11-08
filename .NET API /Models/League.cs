using System.Collections.Generic;

namespace Zadatak1.Models
{
    public class League
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<Group> Groups { get; set; }
    }
}
