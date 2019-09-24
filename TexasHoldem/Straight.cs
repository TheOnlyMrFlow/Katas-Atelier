using System;
using System.Collections.Generic;
using System.Linq;

namespace TexasHoldem
{
    public class Straight : CardCombination, IComparable<Straight>
    {
        public override string Label => "Straight";
        public Straight(ISet<Card> cards) : base(cards)
        {
            if (cards.Count != 5)
                throw new CardCombinationValidationException("A straight is made of 5 cards");

            ISet<Card> ordered = new HashSet<Card>(cards.OrderByDescending(c => c.Face));
            for (int i = 0; i < 4; i++)
                if (ordered.ElementAt(i).Face != ordered.ElementAt(i + 1).Face + 1)
                    throw new CardCombinationValidationException("A straight is made of a sequence of 5 cards");
        }

        public int CompareTo(Straight other)
        {
            return _cards.Max().CompareTo(other._cards.Max());
        }

        public override int CompareTo(IValuable other)
        {
            var asStraight = other as Straight;

            if (asStraight != null)
                return CompareTo(asStraight);

            return GlobVars.TypeToValue[this.GetType()] - GlobVars.TypeToValue[other.GetType()];
        }
    }
}
