using System;
using System.Collections.Generic;
using System.Linq;
using SabberStoneCore.Enums;
using SabberStoneCore.Model;
using Thesis.Evolution.Evaluation;
using Thesis.Evolution.Export;
using Thesis.Evolution.Models;
using Thesis.Evolution.NextGenerations;

namespace Thesis.Evolution
{
    public class Algorithm
    {
        public List<Player> Players { get; private set; }
        public IEvaluation Evaluation { get; set; }
        public INextGeneration NextGeneration { get; set; }
        public Population Population { get; private set; }
        public List<Card> Minions { get; private set; }
        public List<Card> Spells { get; private set; }
        public int Generation { get; private set; }
        private int populationSize = 5;
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


        public PopulationExport Export { get; private set; }

        public Algorithm(List<Player> players, IEvaluation evaluation,
            INextGeneration nextGeneration)
        {
            Players = players;
            Evaluation = evaluation;
            NextGeneration = nextGeneration;
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

        }

        public void Evolve(int generations = 1)
        {
            for (int i = 0; i < generations; i++)
            {
                Population = NextGeneration.Evolve(Population);
                Evaluate();
                Generation++;
            }
        }

        private void Evaluate()
        {
            foreach (var chromosome in Population)
            {
                Console.WriteLine(chromosome);

                if (chromosome.Score == -1)
                {
                    Apply(chromosome);
                    chromosome.Score = Evaluation.Evaluate(Players);
                    // chromosome.Score = random.NextDouble();
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
