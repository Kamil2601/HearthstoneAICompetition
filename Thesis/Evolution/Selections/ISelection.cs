using System.Collections.Generic;
using Thesis.Evolution.Models;

namespace Thesis.Evolution.Selections
{
    public interface ISelection
    {
        List<Chromosome> Select(List<Chromosome> individuals);
    }
}
