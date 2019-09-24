using System;
using System.Collections.Generic;
using System.Linq;

namespace TexasHoldem
{
    public class Flush : CardCombination, IComparable<Flush>
    {
        public override string Label => "Flush";

        public Flush(ISet<Card> cards) : base(cards)
        {
            if (cards.Count != 5)
                throw new CardCombinationValidationException("A flush is made of 5 cards");

            Suit s = cards.ElementAt(0).Suit;
            foreach (Card c in cards)
                if (c.Suit != s)
                    throw new CardCombinationValidationException("All cards of a flush should have the same suit");
        }

        public int CompareTo(Flush other)
        {
            ISet<Card> selfChildrenCopy = AllCards;
            ISet<Card> otherChildrenCopy = other.AllCards;

            // Tie breaking two flushes is done by comparing their highest card.
            // If they have the smame value then we compare their 2nd highest, etc ...

            while (selfChildrenCopy.Count() > 0 && otherChildrenCopy.Count() > 0)
            {
                var selfCurrent = selfChildrenCopy.Max();
                var otherCurrent = otherChildrenCopy.Max();
                selfChildrenCopy.Remove(selfCurrent);
                otherChildrenCopy.Remove(otherCurrent);

                int comparison = selfCurrent.CompareTo(otherCurrent);

                if (comparison != 0)
                    return comparison;
            }


            return 0;
        }

        public override int CompareTo(IValuable other)
        {
            var asFlush = other as Flush;

            if (asFlush != null)
                return CompareTo(asFlush);

            return GlobVars.TypeToValue[this.GetType()] - GlobVars.TypeToValue[other.GetType()];
        }
    }
}
