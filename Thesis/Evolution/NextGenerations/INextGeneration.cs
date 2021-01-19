using Thesis.Evolution.Models;

namespace Thesis.Evolution.NextGenerations
{
    public interface INextGeneration
    {
        Population Evolve(Population population);
    }
}
