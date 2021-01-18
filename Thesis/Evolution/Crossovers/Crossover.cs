namespace Thesis.Evolution.Crossovers
{
    public interface Crossover
    {
        (Chromosome, Chromosome) Crossover(Chromosome parent1, Chromosome parent2);
    }
}
