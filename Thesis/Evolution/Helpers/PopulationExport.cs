using System.Collections.Generic;
using System.IO;
using Thesis.Evolution.Models;

namespace Thesis.Evolution.Helpers
{
    public class PopulationExport
    {
        public string ScoreFile { get; set; }
        public string PopulationsFile { get; set; }

        public PopulationExport(string scoreFile, string populationsFile)
        {
            ScoreFile = scoreFile;
            PopulationsFile = populationsFile;
        }

        public void Export(Population population, int generation)
        {
            using (StreamWriter file = new StreamWriter(ScoreFile, true))
            {
                file.WriteLine($"{generation};{population.MinScore};{population.MaxScore};" +
                    $"{population.AvgScore};{population.BestChromosomeMagnitude}");
            }

            using (StreamWriter file = new StreamWriter(PopulationsFile, true))
            {
                file.WriteLine($"Generation {generation}");
                foreach (Chromosome chromosome in population)
                {
                    file.WriteLine($"{chromosome}");
                }
            }
        }
    }
}
