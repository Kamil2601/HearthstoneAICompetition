using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Thesis.Evolution.Models
{
    public class Chromosome
    {
        //     MINIONS / WEAPONS      |    SPELLS
        // [cost, health, attack, ... , cost, cost, ...]
        public int[] Genes { get; set; }
        public double Balance { get; set; } = -1;
        private readonly int length;
        private readonly int minions;
        private readonly int spells;

        public int Magnitude
        {
            get
            {
                int result = 0;

                for (int i = 0; i < minions; i++)
                {
                    result += 2 * Math.Abs(Genes[3 * i]) + Math.Abs(Genes[3 * i + 1]) + Math.Abs(Genes[3 * i + 2]);
                }

                for (int i = 0; i < spells; i++)
                {
                    result += 2 * Math.Abs(Genes[3 * minions + i]);
                }

                return result;
            }
        }

        public double NormalizedMagnitude => (double)Magnitude/(double)maxMagnitude;

        public double CrowdingDistance { get; set; }
        public int Rank { get; set; } = -1;

        private int maxMagnitude;
        

        public Chromosome(int minions, int spells, bool randomInit = false)
        {
            this.spells = spells;
            this.minions = minions;
            length = 3 * minions + spells;
            Genes = new int[length];

            maxMagnitude = 2*3*(minions+spells)+2*3*minions;

            if (randomInit)
                RandomInitialize();
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

        public override string ToString()
        {
            var m = Magnitude;

            string genes = String.Join(",",Genes.Select(g => g.ToString()));
            return $"({minions}, {spells});[{genes}];{Balance};{m}";
        }
    }
}
