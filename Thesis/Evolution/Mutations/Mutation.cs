using System;
using System.Linq;
using Thesis.Evolution.Models;

namespace Thesis.Evolution.Mutations
{
    public class Mutation : IMutation
    {
        Random random = new Random();

        public void Mutate(Chromosome chromosome)
        {
            for (int i=0; i<chromosome.Genes.Length; i++)
            {
                if (random.NextDouble() < 0.05)
                {
                    chromosome.Genes[i] = random.Next(-3, 4);
                    chromosome.Balance = -1;
                }
            }
        }

        public void Mutate(Population population)
        {
            int mutationCount = (int)Math.Round(population.Count * (0.2));

            var toMutate = population.OrderBy(_ => random.NextDouble()).Take(mutationCount);

            foreach (var chromosome in toMutate)
                Mutate(chromosome);
        }
    }
}
