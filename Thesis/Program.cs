using System;
using SabberStoneBasicAI.Score;
using SabberStoneCore.Enums;
using Thesis.Evolution.Evaluation;
using Thesis.Evolution.Models;
using Thesis.MyDecks;

namespace Thesis
{
    class Program
    {
        static void Main(string[] args)
        {
            Population population = new Population(10, 10, 5);

            Console.WriteLine(population.Count);
            
            foreach (var chromosome in population)
            {
                Console.WriteLine(chromosome);
            }
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
