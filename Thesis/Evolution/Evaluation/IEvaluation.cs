using System.Collections.Generic;
using Thesis.Evolution.Models;

namespace Thesis.Evolution.Evaluation
{
    public interface IEvaluation
    {
        double Evaluate(List<Player> players);
    }
}
