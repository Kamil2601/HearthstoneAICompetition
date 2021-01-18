using System;
using System.Collections.Generic;
using SabberStoneBasicAI.Meta;
using SabberStoneBasicAI.Score;
using SabberStoneCore.Enums;
using SabberStoneCore.Model;
using Thesis.Evaluation;
using Thesis.MyDecks;

namespace Thesis
{
    class Program
    {
        static void Main(string[] args)
        {
            int x = 1, y = 2;
            (x, y) = (y, x);
            Console.WriteLine((x, y));
        }



        static void MatchupTest()
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

            Matchup matchup = new Matchup(player1, player2);

            // matchup.GamesForMatchup = 20;

            // matchup.PlayGame();

            matchup.PlayMatchup();

            matchup.Print();
        }
    }
}
