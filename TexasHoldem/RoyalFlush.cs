using System;
using System.Collections.Generic;
using System.Linq;

namespace TexasHoldem
{
    public class RoyalFlush : StraightFlush, IComparable<RoyalFlush>
    {

        public override string Label => "Royal Flush";
        public RoyalFlush(ISet<Card> cards) : base(cards)
        {
            if (cards.Max().Face != Face.Ace)
                throw new CardCombinationValidationException("The highest card of a royal flush should be an ace");
        }

        public int CompareTo(RoyalFlush other)
        {
            return 0;
        }

        public override int CompareTo(IValuable other)
        {
            var asRoyalFlush = other as RoyalFlush;

            if (asRoyalFlush != null)
                return CompareTo(asRoyalFlush);

            return GlobVars.TypeToValue[this.GetType()] - GlobVars.TypeToValue[other.GetType()];
        }

    }
}
