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
        public Population(PopulationConfig config): base()
        {
            this.Spells = config.Spells;
            this.Minions = config.Minions;
            this.Size = config.Size;

            if (config.Chromosomes != null)
            {
                AddRange(config.Chromosomes);
            }

            if (config.Init)
                RandomInitialize();
        }

        // public Population(Population population, bool copy = false): 
        //     this(population.Size, population.Minions, population.Spells, false)
        // {
        //     if (copy)
        //         AddRange(population);
        // }

        private void RandomInitialize()
        {
            for (int i=Count; i<Size; i++)
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
