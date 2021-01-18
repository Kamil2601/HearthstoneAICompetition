using System.Collections.Generic;

namespace Thesis.Evolution.Selections
{
    public interface Selection
    {
        List<Chromosome> Select(List<Chromosome> individuals);
    }
}
