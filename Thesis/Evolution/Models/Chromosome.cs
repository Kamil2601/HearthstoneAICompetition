using System;
using System.Collections;
using System.Collections.Generic;

namespace Thesis.Evolution.Models
{
    public class Chromosome
    {
        //     MINIONS / WEAPONS      |    SPELLS
        // [cost, health, attack, ... , cost, cost, ...]
        public int[] Genes { get; set; }
        public double Score { get; set; } = -1;
        private readonly int length;
        private readonly int minions;
        private readonly int spells;
        

        public Chromosome(int minions, int spells)
        {
            this.spells = spells;
            this.minions = minions;
            length = 3 * minions + spells;
            Genes = new int[length];
        }

        public Chromosome(Chromosome other): this(other.minions, other.spells)
        {

        }

        public Chromosome Copy()
        {
            var result = new Chromosome(this);

            Array.Copy(Genes, result.Genes, length);

            return result;
        }

        public void RandomInitialize()
        {
            Random random = new Random();

            for (int i=0; i<length; i++)
            {
                Genes[i] = random.Next(-3, 3);
            }
        }
    }
}
