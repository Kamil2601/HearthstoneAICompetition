using System.Collections.Generic;
using SabberStoneBasicAI.Score;
using SabberStoneCore.Enums;
using Thesis.Evolution.Models;

namespace Thesis.Data
{
    public static class PlayersList
    {
        public static List<Player> Experiments => new List<Player>()
        {
            new Player()
            {
                Name = "Hunter-Aggro",
                HeroClass = CardClass.HUNTER,
                AI = new AggroScore(),
                Deck = BasicDecks.Hunter
            },
            new Player()
            {
                Name = "Mage-Midrange",
                HeroClass = CardClass.MAGE,
                AI = new MidRangeScore(),
                Deck = BasicDecks.Mage
            },
            new Player()
            {
                Name = "Druid-Control",
                HeroClass = CardClass.DRUID,
                AI = new ControlScore(),
                Deck = BasicDecks.Druid
            }
        };

        public static List<Player> Experiment3 => new List<Player>()
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
        };
    }
}
