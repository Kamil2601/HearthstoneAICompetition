using System;
using System.Collections.Generic;
using System.IO;
using SabberStoneBasicAI.Score;
using Thesis.Data;
using Thesis.Evolution.Evaluation;
using Thesis.Evolution.Models;

namespace Thesis.CompareDecks
{
    public class DeckTournament
    {
        List<Player> Players { get; set; }
        public double [,] WinRates { get; set; }


        public DeckTournament()
        {
            InitializePlayers();

            WinRates = new double [Players.Count, Players.Count];          
        }

        public void Play()
        {
            for (int i=0; i<Players.Count; i++)
            {
                for (int j=i+1; j<Players.Count; j++)
                {
                    Matchup matchup = new Matchup(Players[i], Players[j])
                    {
                        GamesForMatchup = 50,
                        MaxDepth = 10,
                        MaxWidth = 40
                    };

                    matchup.PlayMatchup();

                    WinRates[i, j] = matchup.P1WinRate;
                    WinRates[j, i] = matchup.P2WinRate;
                }
            }   
        }

        public void Export(string fileName)
        {
            using (StreamWriter file = new StreamWriter(fileName))
            {
                for (int i=0; i<Players.Count; i++)
                {
                    string line = "";

                    for (int j=0; j<Players.Count; j++)
                    {
                        line += WinRates[i,j].ToString() + ";";
                    }
                    file.WriteLine(line);
                }
            }
        }

        public override string ToString()
        {
            var result = "";

            for (int i=0; i<Players.Count; i++)
            {
                for (int j=0; j<Players.Count; j++)
                {
                    result +=  WinRates[i,j] + " ";
                }

                result += "\n";
            }

            return result;
        }

        private void InitializePlayers()
        {
            Players = new List<Player>()
            {
                new Player()
                {
                    Name = "Druid-Aggro",
                    Deck = BasicDecks.Druid,
                    HeroClass = SabberStoneCore.Enums.CardClass.DRUID,
                    AI = new AggroScore()
                },
                new Player()
                {
                    Name = "Druid-Control",
                    Deck = BasicDecks.Druid,
                    HeroClass = SabberStoneCore.Enums.CardClass.DRUID,
                    AI = new ControlScore()
                },
                new Player()
                {
                    Name = "Druid-MidRange",
                    Deck = BasicDecks.Druid,
                    HeroClass = SabberStoneCore.Enums.CardClass.DRUID,
                    AI = new MidRangeScore()
                },
                new Player()
                {
                    Name = "Mage-Aggro",
                    Deck = BasicDecks.Mage,
                    HeroClass = SabberStoneCore.Enums.CardClass.MAGE,
                    AI = new AggroScore()
                },
                new Player()
                {
                    Name = "Mage-Control",
                    Deck = BasicDecks.Mage,
                    HeroClass = SabberStoneCore.Enums.CardClass.MAGE,
                    AI = new ControlScore()
                },
                new Player()
                {
                    Name = "Mage-MidRange",
                    Deck = BasicDecks.Mage,
                    HeroClass = SabberStoneCore.Enums.CardClass.MAGE,
                    AI = new MidRangeScore()
                },
                new Player()
                {
                    Name = "Hunter-Aggro",
                    Deck = BasicDecks.Hunter,
                    HeroClass = SabberStoneCore.Enums.CardClass.HUNTER,
                    AI = new AggroScore()
                },
                new Player()
                {
                    Name = "Hunter-Control",
                    Deck = BasicDecks.Hunter,
                    HeroClass = SabberStoneCore.Enums.CardClass.HUNTER,
                    AI = new ControlScore()
                },
                new Player()
                {
                    Name = "Hunter-MidRange",
                    Deck = BasicDecks.Hunter,
                    HeroClass = SabberStoneCore.Enums.CardClass.HUNTER,
                    AI = new MidRangeScore()
                },
            };
        }
    }
}
