using System;
using System.Collections.Generic;

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
    }
}
