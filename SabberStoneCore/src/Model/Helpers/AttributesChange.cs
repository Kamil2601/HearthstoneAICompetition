namespace SabberStoneCore.Model.Helpers
{
    public class AttributesChange
    {
        public Card Card { get; set; }
        public int ATK { get; set; }
        public int Health { get; set; }
        public int Cost { get; set; }
        // public int Durability { get; set; }

        public AttributesChange(Card card = null)
        {
            if (card != null)
            {
                Card = card;
                Card.AttributesChange = this;
            }
        }

        public AttributesChange Copy()
        {
            var result = new AttributesChange();

            result.ATK = ATK;
            result.Health = Health;
            result.Cost = Cost;
            result.Card = Card;
            // result.Durability = Durability;

            return result;
        }

        public void Reset()
        {
            ATK = 0;
            Health = 0;
            Cost = 0;
            // Durability = 0;
        }
    }
}
