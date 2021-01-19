using Thesis.Evolution.Models;

namespace Thesis.Evolution.Mutations
{
    public class Mutation1 : IMutation
    {
        public void Mutate(Chromosome chromosome)
        {
            for (int i=0; i<chromosome.Genes.Length; i++)
            {
                
            }
        }

        public void Mutate(Population population)
        {
            throw new System.NotImplementedException();
        }
    }
}
