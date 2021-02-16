using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using SabberStoneBasicAI.Score;
using SabberStoneCore.Enums;
using SabberStoneCore.Model;
using Thesis.CardNerf;
using Thesis.CompareDecks;
using Thesis.Data;
using Thesis.Evolution;
using Thesis.Evolution.Evaluation;
using Thesis.Evolution.Helpers;
using Thesis.Evolution.Models;
using Thesis.Evolution.Offsprings;

namespace Thesis
{
    class Program
    {
        static void Main(string[] args)
        {
            // RunBaseEvolution();
            // RunNSGA2();
            // RunCardNerf();
        }

        private static void RunCardNerf()
        {
            Console.WriteLine("Card nerf");

            WinRates winRates = new WinRates();

            winRates.CalculateStats();
            winRates.CalculateNerfStats();
        }

        private static void DeckTournament()
        {
            var tournament = new DeckTournament();

            tournament.Play();

            tournament.Export("./results/decks");

            Console.WriteLine(tournament);
        }

        static void RunNSGA2()
        {
            IOffspring offspring = new Offspring()
            {
                CrossoverRate = 1
            };

            var baseChromosome = new Chromosome(22, 13);
            var bestChromosome = new Chromosome("(22, 13);[-1,0,2,-3,2,1,-2,-2,-3,-3,-3,2,-2,3,-1,2,1,1,-2,-3,-3,2,0,-1,-2,-2,2,2,-1,0,1,-3,2,-1,1,-1,-2,2,-2,1,0,-2,2,1,0,0,-3,0,2,1,-3,1,0,0,2,-1,-3,-1,-1,0,-3,1,1,-2,-2,1,-3,1,2,-3,1,1,-1,0,0,1,2,-3,2]");

            PopulationConfig populationConfig = new PopulationConfig()
            {
                Size = 50,
                Minions = 22,
                Spells = 13,
                Chromosomes = new List<Chromosome>() { baseChromosome, bestChromosome },
                Init = true
            };

            Population population = new Population(populationConfig);

            Console.WriteLine(population.Count);

            var config = new EvolutionConfig()
            {
                Players = PlayersList.Experiments,
                Evaluation = new Tournament()
                {
                    GamesForMatchup = 50
                },
                Offspring = offspring,
                Export = new PopulationExport("nsga2-score.csv", "nsga2-population.csv"),
                Population = population
            };

            NSGA2 nSGA2 = new NSGA2(config);

            nSGA2.Evolve(40);
        }

        static void RunBaseEvolution()
        {
            var config = new EvolutionConfig()
            {
                Players = PlayersList.Experiments,
                Evaluation = new Tournament(),
                Offspring = new Offspring(),
                Export = new PopulationExport("base-score.csv", "base-population.csv")
            };

            BaseEvolution evolution = new BaseEvolution(config);

            evolution.Evolve(30);
        }

        static void EvaluateBestChromosomes()
        {

            var players = PlayersList.Experiments;

            HashSet<string> strs = new HashSet<string>();

            using (StreamReader file = new StreamReader("results/nsga2-best.csv"))
            {
                while (true)
                {
                    var str = file.ReadLine();

                    if (str == null)
                        break;

                    strs.Add(str);
                }
            }

            List<Chromosome> chromosomes = new List<Chromosome>();

            chromosomes.AddRange(strs.Select(c => new Chromosome(c)));

            int size = chromosomes.Count;

            foreach (var c in chromosomes)
            {
                Console.WriteLine(c);
            }

            var tournament = new Tournament()
            {
                GamesForMatchup = 500
            };

            Console.WriteLine(tournament.GamesForMatchup);
            
            PopulationConfig config = new PopulationConfig()
            {
                Size = size,
                Minions = 22,
                Spells = 13
            };

            Population population = new Population(config);

            population.AddRange(chromosomes);
            Offspring offspring = new Offspring();

            var eConfig = new EvolutionConfig()
            {
                Population = population,
                Offspring = new Offspring(),
                Evaluation = tournament,
                Players = PlayersList.Experiments
            };

            BaseEvolution evolution = new BaseEvolution(eConfig);

            evolution.Evaluate();
        }

        static void MatchupTest()
        {
            Player player1 = new Player()
            {
                Name = "Druid-Control",
                HeroClass = CardClass.DRUID,
                AI = new ControlScore(),
                Deck = BasicDecks.Druid
            };

            Player player2 = new Player()
            {
                Name = "Paladin-Control",
                HeroClass = CardClass.PALADIN,
                AI = new ControlScore(),
                Deck = BasicDecks.Paladin
            };

            Matchup matchup = new Matchup(player1, player2);

            matchup.MaxDepth = 10;
            matchup.MaxWidth = 20;
            matchup.GamesForMatchup = 13;

            // matchup.GamesForMatchup = 20;

            // matchup.PlayGame();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            matchup.PlayMatchup();
            // matchup.PlayGame();

            stopwatch.Stop();

            Console.WriteLine(stopwatch.Elapsed);

            matchup.Print();
            Console.WriteLine($"{matchup.P1WinRate} {matchup.P2WinRate}");
        }
    }
}
