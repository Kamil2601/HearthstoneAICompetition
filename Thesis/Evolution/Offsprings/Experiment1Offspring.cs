using System;
using System.Collections.Generic;
using Thesis.Evolution.Crossovers;
using Thesis.Evolution.Models;
using Thesis.Evolution.Mutations;
using Thesis.Evolution.Selections;

namespace Thesis.Evolution.Offsprings
{
    public class Experiment1Offspring : IOffspring
    {
        public double CrossoverRate { get; set; } = 0.35;
        public double MutationRate { get; set; } = 0.2;
        public double MutationProbability { get; set; } = 0.05;
        public ISelection Selection { get; set; } = new TournamentSelection(3);
        public ICrossover Crossover { get; set; } = new TwoPointCrossover();
        public IMutation Mutation { get; set; } = new MutationExperiment1();
        public Population Evolve(Population population)
        {
            var crossoverSize = Math.Round(population.Size*CrossoverRate);

            var result = new Population(population);

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

            Mutation.Mutate(result);

            return result;
        }
    }
}
