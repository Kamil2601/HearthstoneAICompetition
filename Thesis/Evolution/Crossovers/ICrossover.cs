using Thesis.Evolution.Models;

namespace Thesis.Evolution.Crossovers
{
    public interface ICrossover
    {
        (Chromosome, Chromosome) Crossover(Chromosome parent1, Chromosome parent2);
    }
}
