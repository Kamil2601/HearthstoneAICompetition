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
        Random random = new Random();


        public PopulationExport Export { get; protected set; }

        public BaseEvolution(List<Player> players, IEvaluation evaluation,
            IOffspring offspring, Population population = null)
        {
            Players = players;
            Evaluation = evaluation;
            Offspring = offspring;
            Generation = 0;
            Export = new PopulationExport("./results/score", "./results/populations");
            Population = population;


            InitializeCards();

            if (Players != null && population == null)
                InitializePopulation();

            populationSize = Population.Size;
        }

        public void InitializeCards()
        {
            EvolutionCardSet cardSet = new EvolutionCardSet(Players);

            Minions = cardSet.Minions;
            Spells = cardSet.Spells;
        }

        private void InitializePopulation()
        {
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

        public void Evaluate()
        {
            Evaluate(Population);
        }

        public void Evaluate(Population population)
        {
            var path = "./results/BaseEvolution/best-chromosomes.csv";

            foreach (var chromosome in population)
            {
                Console.WriteLine(chromosome);

                if (chromosome.Balance == -1)
                {
                    Apply(chromosome);
                    chromosome.Balance = Evaluation.Evaluate(Players);

                    using (StreamWriter file = new StreamWriter(path, true))
                    {
                        file.WriteLine(chromosome);
                    }
                }
            }
        }

        public void Apply(Chromosome chromosome)
        {
            EvolutionHelper.Apply(chromosome, Minions, Spells);
        }
    }
}
