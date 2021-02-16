using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SabberStoneBasicAI.Nodes;
using SabberStoneBasicAI.Score;
using SabberStoneCore.Config;
using SabberStoneCore.Enums;
using SabberStoneCore.Model;
using SabberStoneCore.Tasks.PlayerTasks;
using Thesis.Data;
using Thesis.Evolution.Evaluation;
using Thesis.Evolution.Models;

namespace Thesis.CardNerf
{
    public class WinRates
    {
        public Player Player { get; set; } = new Player()
        {
            Name = "Hunter-MidRange",
            HeroClass = CardClass.HUNTER,
            AI = new MidRangeScore(),
            Deck = BasicDecks.Hunter
        };
        public List<Player> Opponents { get; set; } = PlayersList.Experiment3;
        public EvolutionCardSet CardSet { get; set; }
        public List<Card> Cards { get; set; }
        public List<CardStats> CardStats { get; set; }
        public int GamesForMatchup { get; set; } = 1000;
        public int MaxDepth { get; set; } = 10;
        public int MaxWidth { get; set; } = 40;
        public int Games { get; set; }
        public int Wins { get; set; }
        public double WinRate => (double)Wins/(double)Games;
        public string FileName { get; set; } = "CardStats.csv";

        public WinRates()
        {
            CardSet = new EvolutionCardSet(new List<Player>() {Player});
            Cards = new List<Card>();
            Cards.AddRange(CardSet.Minions);
            Cards.AddRange(CardSet.Spells);
            CardStats = Cards.Select(card => new CardStats(card)).ToList();
        }


        public void CalculateStats()
        {
            foreach (var opponent in Opponents)
            {
                Console.WriteLine($"vs {opponent.Name}");
                for (int i=0; i<GamesForMatchup; i++)
                {
                    var startPlayer = i%2 + 1;

                    Game game = Play(Player, opponent, startPlayer);

                    Update(game);
                }
            }

            Export();
        }

        public void CalculateNerfStats()
        {
            Write("After Nerf");

            foreach (var stats in CardStats)
            {
                stats.Card.ChangeAttributes(1);

                foreach (var opponent in Opponents)
                {
                    Console.WriteLine($"{Player.Name} vs {opponent.Name}");
                    for (int i=0; i<GamesForMatchup; i++)
                    {
                        var startPlayer = i%2 + 1;

                        Game game = Play(Player, opponent, startPlayer);

                        int result = 0;

                        if (game.Player1.PlayState == PlayState.WON)
                        {
                            result = 1;
                        }

                        stats.GamesAfterNerf++;
                        stats.WinsAfterNerf += result;
                    }
                }

                Write(stats.FullPrint());

                stats.Card.ResetAttributes();
            }

        }

        private void Export()
        {
            Write("Before Nerf");

            Write($"{Games};{Wins};{WinRate}");

            foreach (CardStats stats in CardStats)
            {
                Write(stats.ToString());
            }
        }

        private void Write(string str)
        {
            Console.WriteLine(str);

            using (StreamWriter file = new StreamWriter(FileName, true))
            {
                file.WriteLine(str);
            }
        }

        public void Update(Game game)
        {
            int result = 0;

            if (game.Player1.PlayState == PlayState.WON)
            {
                result = 1;
                // Console.WriteLine($"Winner: {Player1.Name}");
            }

            foreach (CardStats stats in CardStats)
            {
                if (game.Player1.Played(stats.Card))
                {
                    stats.GamesWhenPlayed++;
                    stats.WinsWhenPlayed += result;
                }
                if (game.Player1.Drew(stats.Card))
                {
                    stats.GamesWhenDrawn++;
                    stats.WinsWhenDrawn += result;
                }
            }

            Games++;
            Wins += result;
        }

        public Game Play(Player player1, Player player2, int startPlayer)
        {
            var game = new Game(new GameConfig()
            {
                StartPlayer = startPlayer,
                Player1Name = player1.Name,
                Player1HeroClass = player1.HeroClass,
                Player1Deck = player1.Deck,
                Player2Name = player2.Name,
                Player2HeroClass = player2.HeroClass,
                Player2Deck = player2.Deck,
                FillDecks = false,
                Shuffle = true,
                SkipMulligan = false,
                History = false
            });

            game.StartGame();

            List<int> mulligan1 = player1.AI.MulliganRule().Invoke(
                game.Player1.Choice.Choices.Select(p => game.IdEntityDic[p]).ToList());
            List<int> mulligan2 = player2.AI.MulliganRule().Invoke(
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
                        game, game.Player1.Id, player1.AI, MaxDepth, MaxWidth);
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
                        game, game.Player2.Id, player2.AI, MaxDepth, MaxWidth);
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

            return game;                
        }
    }
}
