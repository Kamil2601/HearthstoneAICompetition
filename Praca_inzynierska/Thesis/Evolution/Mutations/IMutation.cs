using Thesis.Evolution.Models;

namespace Thesis.Evolution.Mutations
{
    public interface IMutation
    {
        // void Mutate(Chromosome chromosome);

        void Mutate(Population population);
    }
}
