using System.Collections.Generic;

namespace Thesis.Evolution.Models
{
    public class PopulationConfig
    {
        public int Size { get; set; }
        public int Minions { get; set; }
        public int Spells { get; set; }
        public bool Init { get; set; }
        public List<Chromosome> Chromosomes { get; set; }
    }
}
