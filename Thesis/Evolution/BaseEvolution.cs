using System;
using System.Collections.Generic;
using System.Linq;
using SabberStoneCore.Enums;
using SabberStoneCore.Model;
using Thesis.Evolution.Evaluation;
using Thesis.Evolution.Export;
using Thesis.Evolution.Models;
using Thesis.Evolution.Offsprings;

namespace Thesis.Evolution
{
    public class BaseEvolution
    {
        public List<Player> Players { get; protected set; }
        public IEvaluation Evaluation { get; set; }
        public IOffspring Offspring { get; set; }
        public Population Population { get; protected set; }
        public List<Card> Minions { get; protected set; }
        public List<Card> Spells { get; protected set; }
        public int Generation { get; protected set; }
        private int populationSize = 50;
        public int PopulationSize
        {
            get => populationSize;
            set
            {
                if (Generation == 0)
                    populationSize = value;
            }
        }
        Random random = new Random();


        public PopulationExport Export { get; protected set; }

        public BaseEvolution(List<Player> players, IEvaluation evaluation,
            IOffspring offspring)
        {
            Players = players;
            Evaluation = evaluation;
            Offspring = offspring;
            Generation = 0;
            Export = new PopulationExport("./results/score", "./results/populations");

            InitializePopulation();
        }

        private void InitializePopulation()
        {
            HashSet<Card> minions = new HashSet<Card>();
            HashSet<Card> spells = new HashSet<Card>();

            foreach (var player in Players)
            {
                foreach (var card in player.Deck)
                {
                    if (card.Type == CardType.MINION || card.Type == CardType.WEAPON)
                    {
                        minions.Add(card);
                    }
                    else if (card.Type == CardType.SPELL)
                    {
                        spells.Add(card);
                    }
                }
            }

            Minions = minions.ToList();
            Spells = spells.ToList();

            Population = new Population(populationSize, Minions.Count, Spells.Count);
            Evaluate();
            Export.Export(Population, Generation);

        }

        public virtual void Evolve(int generations = 1)
        {
            for (int i = 0; i < generations; i++)
            {
                Console.WriteLine($"Generation {Generation}");
                Population = Offspring.Evolve(Population);
                Evaluate();
                Generation++;
                Export.Export(Population, Generation);
            }
        }

        protected void Evaluate()
        {
            Evaluate(Population);
        }

        protected void Evaluate(Population population)
        {
            foreach (var chromosome in population)
            {
                Console.WriteLine(chromosome);

                if (chromosome.Balance == -1)
                {
                    Apply(chromosome);
                    chromosome.Balance = Evaluation.Evaluate(Players);
                    Console.WriteLine(chromosome.Balance);
                    // chromosome.Balance = random.NextDouble();
                }
            }
        }

        public void Apply(Chromosome chromosome)
        {
            for (int i = 0; i < Minions.Count; i++)
            {
                var costChange = chromosome.Genes[3 * i];
                var healthChange = chromosome.Genes[3 * i + 1];
                var atkChange = chromosome.Genes[3 * i + 2];
                Minions[i].ChangeAttributes(costChange, healthChange, atkChange);
            }

            for (int i = 0; i < Spells.Count; i++)
            {
                var costChange = chromosome.Genes[3 * Minions.Count + i];
                Spells[i].ChangeAttributes(costChange);
            }
        }
    }
}
