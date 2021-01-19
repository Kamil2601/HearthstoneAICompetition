using System.Collections.Generic;
using SabberStoneBasicAI.Score;
using SabberStoneCore.Enums;
using SabberStoneCore.Model;

namespace Thesis.Evolution.Models
{
    public class Player
    {
        public string Name { get; set; }
        public List<Card> Deck { get; set; }
        public CardClass HeroClass { get; set; }
        public IScore AI { get; set; }
    }
}
