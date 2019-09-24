using System;
using System.Collections.Generic;
using System.Linq;

namespace TexasHoldem
{
    public class StraightFlush : Flush, IComparable<StraightFlush>
    {

        public override string Label => "Straight Flush";
        public StraightFlush(ISet<Card> cards) : base(cards)
        {
            // the base constructor already checks if it is a flush
            // we call straight constructor to check if it is a straight,
            // it will throw error if not
            new Straight(cards);
        }

        public int CompareTo(StraightFlush other)
        {
            return _cards.Max().CompareTo(other._cards.Max());
        }

        public override int CompareTo(IValuable other)
        {
            var asStraightFlush = other as Straight;

            if (asStraightFlush != null)
                return CompareTo(asStraightFlush);

            return GlobVars.TypeToValue[this.GetType()] - GlobVars.TypeToValue[other.GetType()];
        }

    }
}
