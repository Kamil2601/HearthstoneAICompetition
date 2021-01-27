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

            Population population = new Population(3, 0, 1, false) { c1, c2, c3 };

            var nsga2 = new NSGA2();

            var sorted = nsga2.NonDominatedSort(population);

            Assert.Equal(3, sorted.Count);

            int i=0;

            foreach (var list in sorted)
            {
                Assert.Equal(list.Count, 1);
                Assert.Equal(list[0], population[i]);
                i++;
            }
        }
    }
}
