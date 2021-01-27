using System;
using System.Collections.Generic;
using System.Linq;

namespace Thesis.Evolution.Models
{
    public class Population : List<Chromosome>
    {
        public int Size { get; }
        public int Minions { get; }
        public int Spells { get; }
        public Population(int size, int minions, int spells, bool init = true): base()
        {
            this.Spells = spells;
            this.Minions = minions;
            this.Size = size;

            if (init)
                RandomInitialize();
        }

        public Population(Population population): 
            this(population.Size, population.Minions, population.Spells, false)
        {

        }

        private void RandomInitialize()
        {
            for (int i=0; i<Size; i++)
                Add(new Chromosome(Minions, Spells, true));
        }

        public double MaxScore => this.Select(chromosome => chromosome.Balance)
            .OrderByDescending(score => score).First();

        public double MinScore => this.Select(chromosome => chromosome.Balance)
            .OrderBy(score => score).First();

        public double AvgScore => this.Average(chromosome => chromosome.Balance);

        public double BestChromosomeMagnitude
        {
            get
            {
                var best = this.OrderBy(chrom => chrom.Balance).First();

                return best.Magnitude;
            }
        }
    }
}
