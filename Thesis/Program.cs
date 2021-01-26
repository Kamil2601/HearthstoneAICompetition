using System;
using System.Collections.Generic;
using System.Diagnostics;
using SabberStoneBasicAI.Score;
using SabberStoneCore.Enums;
using SabberStoneCore.Model;
using Thesis.CompareDecks;
using Thesis.Evolution;
using Thesis.Evolution.Evaluation;
using Thesis.Evolution.Models;
using Thesis.Evolution.Offsprings;
using Thesis.MyDecks;

namespace Thesis
{
    class Program
    {
        static void Main(string[] args)
        {
            RunExperiment1();
            // MatchupTest();
            // DeckTournament();
        }

        private static void DeckTournament()
        {
            var tournament = new DeckTournament();

            tournament.Play();

            tournament.Export("./results/decks");

            Console.WriteLine(tournament);
        }

        static void RunExperiment1()
        {
            Player player1 = new Player()
            {
                Name = "Hunter-Aggro",
                HeroClass = CardClass.HUNTER,
                AI = new AggroScore(),
                Deck = BasicDecks.Hunter
            };

            Player player2 = new Player()
            {
                Name = "Mage-Midrange",
                HeroClass = CardClass.MAGE,
                AI = new MidRangeScore(),
                Deck = BasicDecks.Mage
            };

            Player player3 = new Player()
            {
                Name = "Druid-Control",
                HeroClass = CardClass.DRUID,
                AI = new ControlScore(),
                Deck = BasicDecks.Druid
            };

            var players = new List<Player>() {player1, player2, player3};

            Tournament evaluation = new Tournament();
            Experiment1 nextGeneration = new Experiment1();

            BaseEvolution evolution = new BaseEvolution(players, evaluation, nextGeneration);

            evolution.Evolve(30);
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
