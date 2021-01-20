using System;
using System.Collections.Generic;
using System.Linq;
using Thesis.Evolution.Models;

namespace Thesis.Evolution.Selections
{
    public class TournamentSelection : ISelection
    {
        private int tournamentSize;
        private Random random = new Random();

        public TournamentSelection(int size)
        {
            tournamentSize = size;
        }

        public Population Select(Population population)
        {
            var result = new Population(population.Size,
                population.Minions, population.Spells, false);

            var count = population.Count;

            while (result.Count < count)
            {
                var tournament = new List<Chromosome>();

                for (int i = 0; i < tournamentSize; i++)
                {
                    var index = random.Next(count);
                    tournament.Add(population[index]);
                }

                var bestScore = tournament.Select(ind => ind.Score).Max();

                var bestIndividuals = tournament.Where(ind => ind.Score == bestScore).ToList();

                var winner = bestIndividuals[random.Next(bestIndividuals.Count)];

                result.Add(winner.Copy());
            }

            return result;
        }

        public Chromosome SelectOne(Population population)
        {
            var count = population.Count;

            var tournament = new List<Chromosome>();

            for (int i = 0; i < tournamentSize; i++)
            {
                var index = random.Next(count);
                tournament.Add(population[index]);
            }

            var bestScore = tournament.Select(ind => ind.Score).Max();

            var bestIndividuals = tournament.Where(ind => ind.Score == bestScore).ToList();

            var winner = bestIndividuals[random.Next(bestIndividuals.Count)];

            return winner.Copy();
        }
    }
}
