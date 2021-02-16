using Thesis.Evolution.Models;

namespace Thesis.Evolution.Offsprings
{
    public interface IOffspring
    {
        Population Evolve(Population population);
    }
}
