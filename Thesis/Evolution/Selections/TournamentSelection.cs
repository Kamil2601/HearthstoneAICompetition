using System;
using System.Collections.Generic;
using System.Linq;

namespace Thesis.Evolution.Selections
{
    public class TournamentSelection: Selection
    {
        private int tournamentSize;
        private Random random = new Random();

        public TournamentSelection(int size)
        {
            tournamentSize = size;
        }

        public List<Chromosome> Select(List<Chromosome> individuals)
        {
            var result = new List<Chromosome>();

            var count = individuals.Count;

            while (result.Count < count)
            {
                var tournament = new List<Chromosome>();

                for (int i=0; i<tournamentSize; i++)
                {
                    var index = random.Next(count);
                    tournament.Add(individuals[index]);
                }

                var bestScore = tournament.Select(ind => ind.Score).Max();

                var bestIndividuals = tournament.Where(ind => ind.Score == bestScore).ToList();

                var winner = bestIndividuals[random.Next(bestIndividuals.Count)];

                result.Add(winner.Copy());
            }

            return result;
        }
    }
}
