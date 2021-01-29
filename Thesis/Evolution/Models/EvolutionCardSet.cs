using System.Collections.Generic;
using System.Linq;
using SabberStoneCore.Enums;
using SabberStoneCore.Model;

namespace Thesis.Evolution.Models
{
    public class EvolutionCardSet
    {
        public List<Card> Minions { get; set; }
        public List<Card> Spells { get; set; }

        public EvolutionCardSet(List<Player> players)
        {
            HashSet<Card> minions = new HashSet<Card>();
            HashSet<Card> spells = new HashSet<Card>();

            foreach (var player in players)
            {
                foreach (var card in player.Deck)
                {
                    if (card.Type == CardType.MINION || card.Type == CardType.WEAPON)
                    {
                        minions.Add(card);
                    }
                    else if (card.Type == CardType.SPELL)
                    {
                        spells.Add(card);
                    }
                }
            }

            Minions = minions.ToList();
            Spells = spells.ToList();
        }
    }
}
