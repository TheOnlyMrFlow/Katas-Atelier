using System.Collections.Generic;

namespace TexasHoldem
{

    public abstract class CardCombination : IValuable, ICardCollection
    {

        protected HashSet<Card> _cards;

        public ISet<Card> AllCards { get => new HashSet<Card> (_cards);  }

        public abstract string Label { get; }

        public CardCombination(ISet<Card> cards)
        {
            _cards = new HashSet<Card>(cards);
        } 

        public abstract int CompareTo(IValuable other);
    }

}
