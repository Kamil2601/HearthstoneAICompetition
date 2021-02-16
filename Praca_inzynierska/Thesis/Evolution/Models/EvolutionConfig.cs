using System.Collections.Generic;
using Thesis.Evolution.Evaluation;
using Thesis.Evolution.Helpers;
using Thesis.Evolution.Offsprings;

namespace Thesis.Evolution.Models
{
    public class EvolutionConfig
    {
        public int PopulationSize { get; set; }
        public IOffspring Offspring { get; set; }
        public IEvaluation Evaluation { get; set; }
        public Population Population { get; set; }
        public PopulationExport Export { get; set; }
        public List<Player> Players { get; internal set; }
    }
}
