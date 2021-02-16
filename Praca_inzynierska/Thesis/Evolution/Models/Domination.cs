using System.Collections.Generic;

namespace Thesis.Evolution.Models
{
    public class Domination
    {
        public int DominationCount { get; set; } = 0;
        public List<int> Dominates { get; set; } = new List<int>();
    }
}
