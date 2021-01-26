using System.Collections.Generic;
using Thesis.Evolution.Models;
using Thesis.Evolution.Offsprings;

namespace Thesis.Evolution
{

    public class NSGA2
    {
        public Population Population { get; set; }
        public IOffspring Offspring { get; set; }

        public List<List<Chromosome>> NonDominatedSort(Population population)
        {
            var result = new List<List<Chromosome>>();

            Domination[] dominations = new Domination[population.Count];

            for (int i=0; i<dominations.Length; i++)
                dominations[i] = new Domination();

            for (int i=0; i<population.Count; i++)
            {
                for (int j=0; j<population.Count; j++)
                {
                    if (Dominates(population[i], population[j]))
                    {
                        dominations[i].Dominates.Add(j);
                        dominations[j].DominationCount++;
                    }
                }
            }

            int sortCount = 0;

            while (sortCount < population.Count)
            {
                List<Chromosome> currentFront = new List<Chromosome>();
                List<int> frontIndexes = new List<int>();

                for (int i=0; i<population.Count; i++)
                {
                    if (dominations[i].DominationCount == 0)
                    {
                        currentFront.Add(population[i]);
                        frontIndexes.Add(i);
                        dominations[i].DominationCount = -1;
                        sortCount++;
                    }
                }

                result.Add(currentFront);

                foreach (int index in frontIndexes)
                {
                    foreach (int dominated in dominations[index].Dominates)
                    {
                        dominations[dominated].DominationCount--;
                    }
                }   
            }

            return result;
        }

        private bool Dominates(Chromosome dominator, Chromosome dominated)
        {
            return dominator.Score <= dominated.Score && dominator.Magnitude <= dominated.Magnitude
                && (dominator.Score < dominated.Score || dominator.Magnitude < dominated.Magnitude);
        }
    }
}
