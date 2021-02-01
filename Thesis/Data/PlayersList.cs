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
    }
}
