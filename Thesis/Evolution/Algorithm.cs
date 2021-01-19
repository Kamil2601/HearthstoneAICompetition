using System;
using System.Collections.Generic;
using SabberStoneCore.Model.Helpers;
using Thesis.Evolution.Crossovers;
using Thesis.Evolution.Evaluation;
using Thesis.Evolution.Mutations;
using Thesis.Evolution.Selections;

namespace Thesis.Evolution
{
    public class Algorithm
    {
        public List<Player> Players { get; private set; }
        public List<AttributesChange> Changes { get; private set; }
        public Population Population { get; private set; }
        public ISelection Selection { get; set; }
        public ICrossover Crossover { get; set; }
        public IMutation Mutation { get; set; }

        public Algorithm(List<Player> players)
        {
            Players = players;
        }

        public void NextGeneration()
        {
            Evaluate();
            Select();
            Cross();
            Mutate();
        }

        private void Mutate()
        {
            throw new NotImplementedException();
        }

        private void Cross()
        {
            throw new NotImplementedException();
        }

        private void Select()
        {
            throw new NotImplementedException();
        }

        private void Evaluate()
        {
            for (int i=0; i<Population.Count; i++)
            {
                // Population.Apply()
            }
        }
    }
}
