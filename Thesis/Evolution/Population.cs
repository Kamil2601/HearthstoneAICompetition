using System.Collections.Generic;
using System.Linq;
using SabberStoneCore.Enums;
using SabberStoneCore.Model;
using Thesis.Evaluation;

namespace Thesis.Evolution
{
    public class Population
    {
        public List<Card> MinionsAndWeapons { get; private set; }
        public List<Card> Spells { get; private set; }
        public int ChromosomeLength { get; private set; }
        public List<Chromosome> Individuals { get; set; }

        public Population(List<Player> players)
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

            MinionsAndWeapons = minions.ToList();
            Spells = spells.ToList();
            ChromosomeLength = MinionsAndWeapons.Count * 3 + Spells.Count;
        }

        public void Apply(Chromosome chromosome)
        {
            for (int i=0; i < MinionsAndWeapons.Count; i++)
            {
                var costChange = chromosome.Genes[3*i];
                var healthChange = chromosome.Genes[3*i+1];
                var atkChange = chromosome.Genes[3*i+2];
                MinionsAndWeapons[i].ChangeAttributes(costChange, healthChange, atkChange);
            }

            for (int i=0; i < Spells.Count; i++)
            {
                var costChange = chromosome.Genes[3*MinionsAndWeapons.Count + i];
                Spells[i].ChangeAttributes(costChange);
            }
        }


    }
}
