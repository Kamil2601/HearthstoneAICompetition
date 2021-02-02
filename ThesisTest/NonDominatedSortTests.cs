using Thesis.Evolution;
using Thesis.Evolution.Models;
using Xunit;

namespace ThesisTest
{
    public class NonDominatedSortTests
    {
        [Fact]
        public void SortTest1()
        {
            var c1 = new Chromosome(0, 1)
            {
                Genes = new int[1] { 1 },
                Balance = 0.1
            };

            var c2 = new Chromosome(0, 1)
            {
                Genes = new int[1] { 2 },
                Balance = 0.2
            };

            var c3 = new Chromosome(0, 1)
            {
                Genes = new int[1] { 3 },
                Balance = 0.3
            };

            var populationConfig = new PopulationConfig()
            {
                Size = 3,
                Minions = 0,
                Spells = 1,
            };

            Population population = new Population(populationConfig) { c1, c2, c3 };

            var config = new EvolutionConfig()
            {
                Population = population
            };

            var nsga2 = new NSGA2(config);

            var sorted = nsga2.NonDominatedSort(population);

            Assert.Equal(3, sorted.Count);

            int i=0;

            foreach (var list in sorted)
            {
                Assert.Equal(1, list.Count);
                Assert.Equal(population[i], list[0]);
                i++;
                Assert.Equal(i, list[0].Rank);
            }
        }
    }
}
