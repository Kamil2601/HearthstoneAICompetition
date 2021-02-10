using System;
using System.Collections.Generic;
using System.Linq;
using SabberStoneBasicAI.Nodes;
using SabberStoneCore.Config;
using SabberStoneCore.Enums;
using SabberStoneCore.Model;
using SabberStoneCore.Tasks.PlayerTasks;
using Thesis.Evolution.Models;

namespace Thesis.Evolution.Evaluation
{
    public class Matchup
    {
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        GameConfig gameConfig;
        public int GamesPlayed { get; private set; }
        public int P1Wins { get; private set; }
        public int P2Wins { get; private set; }
        public int MaxDepth { get; set; } = 10;
        public int MaxWidth { get; set; } = 40;
        public int GamesForMatchup { get; set; } = 50;
        public int ExceptionsThrown { get; set; }

        public double P1WinRate => (double)P1Wins/(double)GamesPlayed;
        public double P2WinRate => (double)P2Wins/(double)GamesPlayed;

        public Matchup(Player player1, Player player2)
        {
            Player1 = player1;
            Player2 = player2;

            gameConfig = new GameConfig()
            {
                StartPlayer = 1,
                Player1Name = Player1.Name,
                Player1HeroClass = Player1.HeroClass,
                Player1Deck = Player1.Deck,
                Player2Name = Player2.Name,
                Player2HeroClass = Player2.HeroClass,
                Player2Deck = Player2.Deck,
                FillDecks = false,
                Shuffle = true,
                SkipMulligan = false,
                History = false
            };
        }


        public Game Play()
        {
            var game = new Game(gameConfig);

            game.StartGame();

            List<int> mulligan1 = Player1.AI.MulliganRule().Invoke(
                game.Player1.Choice.Choices.Select(p => game.IdEntityDic[p]).ToList());
            List<int> mulligan2 = Player2.AI.MulliganRule().Invoke(
                game.Player2.Choice.Choices.Select(p => game.IdEntityDic[p]).ToList());

            game.Process(ChooseTask.Mulligan(game.Player1, mulligan1));
            game.Process(ChooseTask.Mulligan(game.Player2, mulligan2));

            game.MainReady();

            // Console.WriteLine($"First player: {game.CurrentPlayer.Name}");

            while (game.State != State.COMPLETE)
			{
				while (game.State == State.RUNNING && game.CurrentPlayer == game.Player1)
				{
					List<OptionNode> solutions = OptionNode.GetSolutions(
                        game, game.Player1.Id, Player1.AI, MaxDepth, MaxWidth);
					var solution = new List<PlayerTask>();
					solutions.OrderByDescending(p => p.Score).First().PlayerTasks(ref solution);

					foreach (PlayerTask task in solution)
					{
						game.Process(task);
						if (game.CurrentPlayer.Choice != null)
						{
							break;
						}
					}
				}

				while (game.State == State.RUNNING && game.CurrentPlayer == game.Player2)
				{
					List<OptionNode> solutions = OptionNode.GetSolutions(
                        game, game.Player2.Id, Player2.AI, MaxDepth, MaxWidth);
					var solution = new List<PlayerTask>();
					solutions.OrderByDescending(p => p.Score).First().PlayerTasks(ref solution);
					foreach (PlayerTask task in solution)
					{
						game.Process(task);
						if (game.CurrentPlayer.Choice != null)
						{
							break;
						}
					}
				}
			}

            GamesPlayed++;

            return game;                
        }

        public void PlayGame()
        {
            var game = Play();

            if (game.Player1.PlayState == PlayState.WON)
            {
                P1Wins++;
                // Console.WriteLine($"Winner: {Player1.Name}");
            }
            else if (game.Player2.PlayState == PlayState.WON)
            {
                P2Wins++;
                // Console.WriteLine($"Winner: {Player2.Name}");
            }
        }

        public void PlayMatchup()
        {
            Reset();

            Console.WriteLine($"{Player1.Name} vs {Player2.Name}");

            for (int i=0; i<GamesForMatchup; i++)
            {
                // Console.WriteLine($"Game {i}");

                gameConfig.StartPlayer = i%2 + 1;

                try
                {
                    PlayGame();
                    // Console.WriteLine("OK");
                }
                catch (Exception)
                {
                    ExceptionsThrown++;
                    // Console.WriteLine("Exception");
                }
                    
            }
        }

        public void Print()
        {
            Console.WriteLine($"P1: {Player1.Name}, Wins: {P1Wins}");
            Console.WriteLine($"P2: {Player2.Name}, Wins: {P2Wins}");
        }

        private void Reset()
        {
            GamesPlayed = 0;
            P1Wins = 0;
            P2Wins = 0;
            ExceptionsThrown = 0;
        }
    }
}
