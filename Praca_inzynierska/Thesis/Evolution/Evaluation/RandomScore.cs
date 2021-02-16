using System;
using System.Collections.Generic;
using Thesis.Evolution.Models;

namespace Thesis.Evolution.Evaluation
{
    public class RandomScore : IEvaluation
    {
        Random random = new Random();
        public double Evaluate(List<Player> players)
        {
            return random.NextDouble();
        }
    }
}
