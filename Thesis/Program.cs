﻿using System;
using System.Collections.Generic;
using SabberStoneBasicAI.Score;
using SabberStoneCore.Enums;
using SabberStoneCore.Model;
using Thesis.Evolution;
using Thesis.Evolution.Evaluation;
using Thesis.Evolution.Models;
using Thesis.Evolution.NextGenerations;
using Thesis.MyDecks;

namespace Thesis
{
    class Program
    {
        static void Main(string[] args)
        {
            // RunExperiment1();
            MatchupTest();

            // var card = Cards.FromName("Fiery War Axe");

            // card.ChangeAttributes(1, 1, 1);

            // card.ChangeAttributes(0,0,0);
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
                Name = "Mage-Control",
                HeroClass = CardClass.MAGE,
                AI = new ControlScore(),
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

            IEvaluation evaluation = new Tournament();
            INextGeneration nextGeneration = new Experiment1();

            Algorithm algorithm = new Algorithm(players, evaluation, nextGeneration);

            // algorithm.Evolve(1);
        }

        static void MatchupTest()
        {
            Player player1 = new Player()
            {
                Name = "Druid-Control",
                HeroClass = CardClass.DRUID,
                AI = new AggroScore(),
                Deck = BasicDecks.Druid
            };

            Player player2 = new Player()
            {
                Name = "Mage-Control",
                HeroClass = CardClass.MAGE,
                AI = new ControlScore(),
                Deck = BasicDecks.Mage
            };

            Matchup matchup = new Matchup(player1, player2);

            matchup.MaxDepth = 2;
            matchup.MaxWidth = 50;
            matchup.GamesForMatchup = 10;

            // matchup.GamesForMatchup = 20;

            // matchup.PlayGame();

            matchup.PlayMatchup();

            matchup.Print();
        }
    }
}
