using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using SabberStoneBasicAI.Score;
using SabberStoneCore.Enums;
using SabberStoneCore.Model;
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
            RunNSGA2();

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

            var config = new EvolutionConfig()
            {
                Players = PlayersList.Experiments,
                Evaluation = new Tournament(),
                Offspring = offspring,
                Export = new PopulationExport("nsga2-score.csv", "nsga2-population.csv")
            };

            NSGA2 nSGA2 = new NSGA2(config);

            nSGA2.Evolve(40);
        }

        static void RunExperiment1()
        {
            var config = new EvolutionConfig()
            {
                Players = PlayersList.Experiments,
                Evaluation = new Tournament(),
                Offspring = new Offspring()
            };

            BaseEvolution evolution = new BaseEvolution(config);

            evolution.Evolve(30);
        }

        static void EvaluateBestChromosomes()
        {

            var players = PlayersList.Experiments;

            HashSet<string> strs = new HashSet<string>();

            using (StreamReader file = new StreamReader("results/BaseEvolution/Chromosomes-sorted.csv"))
            {
                while (strs.Count < 10)
                {
                    var str = file.ReadLine();

                    strs.Add(str);
                }
            }

            List<Chromosome> chromosomes = new List<Chromosome>
            {
                new Chromosome(22, 13, false)
            };

            chromosomes.AddRange(strs.Select(c => new Chromosome(c)));

            var tournament = new Tournament()
            {
                GamesForMatchup = 500
            };

            Population population = new Population(11, 22, 13, false);

            population.AddRange(chromosomes);
            Offspring offspring = new Offspring();

            var config = new EvolutionConfig()
            {
                Population = population,
                Offspring = new Offspring(),
                Evaluation = tournament
            };

            BaseEvolution evolution = new BaseEvolution(config);

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
