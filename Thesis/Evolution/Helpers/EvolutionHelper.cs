using System.Collections.Generic;
using SabberStoneCore.Model;
using Thesis.Evolution.Evaluation;
using Thesis.Evolution.Models;

namespace Thesis.Evolution.Helpers
{
    public static class EvolutionHelper
    {
        public static void Apply(Chromosome chromosome, List<Card> minions, List<Card> spells)
        {
            for (int i = 0; i < minions.Count; i++)
            {
                var costChange = chromosome.Genes[3 * i];
                var healthChange = chromosome.Genes[3 * i + 1];
                var atkChange = chromosome.Genes[3 * i + 2];
                minions[i].ChangeAttributes(costChange, healthChange, atkChange);
            }

            for (int i = 0; i < spells.Count; i++)
            {
                var costChange = chromosome.Genes[3 * minions.Count + i];
                spells[i].ChangeAttributes(costChange);
            }
        }

        public static void Evaluate(Chromosome chromosome, List<Player> players,
            EvolutionCardSet cardSet, IEvaluation evalutation)
        {
            Apply(chromosome, cardSet.Minions, cardSet.Spells);
            chromosome.Balance = evalutation.Evaluate(players);
        }

        public static void Evaluate(List<Chromosome> chromosomes, List<Player> players,
            EvolutionCardSet cardSet, IEvaluation evalutation)
        {
            foreach (var c in chromosomes)
            {
                Evaluate(c, players, cardSet, evalutation);
            }
        }
    }
}
