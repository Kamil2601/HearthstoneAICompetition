using System;
using System.Collections.Generic;
using SabberStoneCore.Model;
using Thesis.Data;
using Xunit;

namespace ThesisTest
{
    public class DecksComplete
    {
        [Fact]
        public void BasicDruid()
        {
            CompleteDeck(BasicDecks.Druid);    
        }

        [Fact]
        public void BasicHunter()
        {
            CompleteDeck(BasicDecks.Hunter);    
        }

        [Fact]
        public void BasicMage()
        {
            CompleteDeck(BasicDecks.Mage);    
        }


        private void CompleteDeck(List<Card> deck)
        {
            Assert.Equal(deck.Count, 30);

            foreach (Card card in deck)
            {
                Assert.NotNull(card);
            }               
        }
    }
}
