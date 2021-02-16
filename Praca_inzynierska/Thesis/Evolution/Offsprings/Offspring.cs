using System;
using Thesis.Evolution.Crossovers;
using Thesis.Evolution.Models;
using Thesis.Evolution.Mutations;
using Thesis.Evolution.Selections;

namespace Thesis.Evolution.Offsprings
{
    public class Offspring : IOffspring
    {
        public double CrossoverRate { get; set; } = 0.35;
        public double MutationRate { get; set; } = 0.2;
        public double MutationProbability { get; set; } = 0.05;
        public ISelection Selection { get; set; } = new TournamentSelection(3);
        public ICrossover Crossover { get; set; } = new TwoPointCrossover();
        public IMutation Mutation { get; set; } = new Mutation();
        public Population Evolve(Population population)
        {
            var crossoverSize = Math.Round(population.Size*CrossoverRate);

            PopulationConfig config = new PopulationConfig()
            {
                Size = population.Size,
                Minions = population.Minions,
                Spells = population.Spells,
                Init = false,
            };

            var result = new Population(config);

            while (result.Count < crossoverSize)
            {
                var p1 = Selection.SelectOne(population);
                var p2 = Selection.SelectOne(population);

                while (p1 == p2)
                    p2 = Selection.SelectOne(population);

                var (c1, c2) = Crossover.Crossover(p1, p2);

                result.Add(c1);
                result.Add(c2);
            }

            while (result.Count < result.Size)
            {
                var parent = Selection.SelectOne(population);

                result.Add(parent);
            }

            while (result.Count > result.Size)
            {
                result.RemoveAt(result.Count-1);
            }

            Mutation.Mutate(result);

            return result;
        }
    }
}
