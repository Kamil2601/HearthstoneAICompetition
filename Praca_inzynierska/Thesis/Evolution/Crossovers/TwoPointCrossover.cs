using System;
using Thesis.Evolution.Models;

namespace Thesis.Evolution.Crossovers
{
    public class TwoPointCrossover : ICrossover
    {
        Random random = new Random();

        public (Chromosome, Chromosome) Crossover(Chromosome parent1, Chromosome parent2)
        {
            var length = parent1.Genes.Length;

            var child1 = parent1.Copy();
            var child2 = parent2.Copy();
            
            var p1 = random.Next(length);
            var p2 = random.Next(length);

            while (p2 == p1)
                p1 = random.Next(length);

            var from = Math.Min(p1, p2);
            var to = Math.Max(p1, p2);

            for (int i=from; i<to; i++)
            {
                (child1.Genes[i], child2.Genes[i]) = (child2.Genes[i], child1.Genes[i]);
            }

            return (child1, child2);
        }

        public Population Crossover(Population population)
        {
            throw new NotImplementedException();
        }
    }
}
