using SabberStoneCore.Model;

namespace Thesis.CardNerf
{
    public class CardStats
    {
        public Card Card { get; set; }
        public int GamesWhenPlayed { get; set; }
        public int WinsWhenPlayed { get; set; }
        public int GamesWhenDrawn { get; set; }
        public int WinsWhenDrawn { get; set; }
        public int GamesAfterNerf { get; set; }
        public int WinsAfterNerf { get; set; }
        public double WRP => (double)WinsWhenPlayed/(double)GamesWhenPlayed;
        public double WRD => (double)WinsWhenDrawn/(double)(GamesWhenDrawn);
        public double WRN => (double)WinsAfterNerf/(double)(GamesAfterNerf);

        public CardStats(Card card)
        {
            Card = card;
        }

        public override string ToString()
        {
            return $"{Card.Name};{WinsWhenPlayed};{GamesWhenPlayed};{WRP};{WinsWhenDrawn};{GamesWhenDrawn};{WRD}";
        }

        public string FullPrint()
        {
            return $"{Card.Name};{WinsWhenPlayed};{GamesWhenPlayed};{WRP};{WinsWhenDrawn};{GamesWhenDrawn};{WRD};{WinsAfterNerf};{GamesAfterNerf};{WRN}";
        }
    }
}
