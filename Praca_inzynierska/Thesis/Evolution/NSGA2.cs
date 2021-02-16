using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Thesis.Evolution.Evaluation;
using Thesis.Evolution.Helpers;
using Thesis.Evolution.Models;
using Thesis.Evolution.Offsprings;

namespace Thesis.Evolution
{

    public class NSGA2 : BaseEvolution
    {

        public NSGA2(EvolutionConfig config): base(config)
        {
            
        }

        public override void Evolve(int generations = 1)
        {
            for (int i = 0; i < generations; i++)
            {
                Console.WriteLine($"Generation {Generation}");
                var offspring = Offspring.Evolve(Population);
                Population.AddRange(offspring);
                Evaluate(offspring);
                var sorted = NonDominatedSort(Population);
                Population = NextGeneration(sorted);

                Generation++;
                Export.Export(Population, Generation);
            }
        }

        private Population NextGeneration(List<List<Chromosome>> sorted)
        {
            PopulationConfig config= new PopulationConfig()
            {
                Size = Population.Size,
                Minions = Minions.Count,
                Spells = Spells.Count,
                Init = false
            };

            var result = new Population(config);

            foreach (var front in sorted)
            {
                if (result.Count + front.Count <= result.Size)
                {
                    result.AddRange(front);
                }                
                else if (result.Count == result.Size)
                {
                    break;
                }
                else
                {
                    CalculateCrowdingDistance(front);
                    var takenChromosomes = front.OrderByDescending(c => c.Rank)
                        .Take(result.Size - result.Count);
                    result.AddRange(takenChromosomes);
                }
            }

            return result;
        }

        public List<List<Chromosome>> NonDominatedSort(List<Chromosome> population)
        {
            var result = new List<List<Chromosome>>();

            Domination[] dominations = new Domination[population.Count];

            for (int i = 0; i < dominations.Length; i++)
                dominations[i] = new Domination();

            for (int i = 0; i < population.Count; i++)
            {
                for (int j = 0; j < population.Count; j++)
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

                for (int i = 0; i < population.Count; i++)
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
                    population[index].Rank = result.Count; 

                    foreach (int dominated in dominations[index].Dominates)
                    {
                        dominations[dominated].DominationCount--;
                        
                    }
                }
            }

            return result;
        }

        public void CalculateCrowdingDistance(List<Chromosome> front)
        {
            var orderedIndividuals = front.OrderBy(c => c.Balance).ToList();

            for (int i=0; i<orderedIndividuals.Count; i++)
            {
                if (i == 0 || i == orderedIndividuals.Count - 1)
                {
                    orderedIndividuals[i].CrowdingDistance = double.PositiveInfinity;
                }
                else
                {
                    // Grab a reference to each individual to make the next section a bit cleaner.
                    var current = orderedIndividuals[i];
                    var left = orderedIndividuals[i - 1];
                    var right = orderedIndividuals[i + 1];

                    // Get the positions on the 2D fitness graph, where time is our X axis and distance is our Y.
                    var currentPosition = new Vector2((float)current.Balance, (float)current.NormalizedMagnitude);
                    var leftPosition = new Vector2((float)left.Balance, (float)left.NormalizedMagnitude);
                    var rightPosition = new Vector2((float)right.Balance, (float)right.NormalizedMagnitude);

                    // Calculate the distance to the neighbourn on each side
                    var distanceLeft = Vector2.Distance(currentPosition, leftPosition);
                    var distanceRight = Vector2.Distance(currentPosition, rightPosition);

                    // Set the crowding distance for the current individual
                    orderedIndividuals[i].CrowdingDistance = distanceLeft + distanceRight;
                }
            }
        }

        private bool Dominates(Chromosome dominator, Chromosome dominated)
        {
            return dominator.Balance <= dominated.Balance && dominator.Magnitude <= dominated.Magnitude
                && (dominator.Balance < dominated.Balance || dominator.Magnitude < dominated.Magnitude);
        }
    }
}
