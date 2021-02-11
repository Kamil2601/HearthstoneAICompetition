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

        public int Length => length;

        public int Minions => minions;

        public int Spells => spells;

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

        public Chromosome(string str)
        {
            var split = str.Split(";");

            var size = split[0];

            var cards = size.Remove(size.Length-1, 1).Remove(0, 1).Split(", ");

            minions = Convert.ToInt32(cards[0]);
            spells = Convert.ToInt32(cards[1]);

            maxMagnitude = 2*3*(minions+spells)+2*3*minions;

            var geneStr = split[1];

            var genes = geneStr.Remove(geneStr.Length-1, 1).Remove(0, 1).Split(",");

            Genes = genes.Select(g => Convert.ToInt32(g)).ToArray();
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
            var nm = NormalizedMagnitude;

            string genes = String.Join(",",Genes.Select(g => g.ToString()));
            return $"({minions}, {spells});[{genes}];{Balance};{m};{nm}";
        }
    }
}
