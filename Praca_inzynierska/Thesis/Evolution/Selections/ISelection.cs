using System.Collections.Generic;
using Thesis.Evolution.Models;

namespace Thesis.Evolution.Selections
{
    public interface ISelection
    {
        Population Select(Population population);
        Chromosome SelectOne(Population population);
    }
}
