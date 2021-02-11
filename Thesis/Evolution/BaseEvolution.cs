using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SabberStoneCore.Enums;
using SabberStoneCore.Model;
using Thesis.Evolution.Evaluation;
using Thesis.Evolution.Helpers;
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

        public PopulationExport Export { get; protected set; }

        public BaseEvolution(EvolutionConfig config)
        {
            Players = config.Players;
            Evaluation = config.Evaluation;
            Offspring = config.Offspring;
            Export = config.Export;
            Population = config.Population;

            Generation = 0;

            InitializeCards();

            Console.WriteLine($"Generation {Generation}");
            Evaluate();
            // Export.Export(Population, Generation);


            populationSize = Population.Size;
        }

        public void InitializeCards()
        {
            EvolutionCardSet cardSet = new EvolutionCardSet(Players);

            Minions = cardSet.Minions;
            Spells = cardSet.Spells;
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

        public void Evaluate()
        {
            Evaluate(Population);
        }

        public void Evaluate(Population population)
        {
            foreach (var chromosome in population)
            {
                if (chromosome.Balance == -1)
                {
                    Apply(chromosome);
                    chromosome.Balance = Evaluation.Evaluate(Players);
                }

                Console.WriteLine(chromosome);

                using (StreamWriter file = new StreamWriter("nsga2-eval.csv", true))
                {
                    file.WriteLine(chromosome);
                }
            }
        }

        public void Apply(Chromosome chromosome)
        {
            EvolutionHelper.Apply(chromosome, Minions, Spells);
        }
    }
}
