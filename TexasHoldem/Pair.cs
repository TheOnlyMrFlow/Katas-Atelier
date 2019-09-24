using System;
using System.Collections.Generic;
using System.Linq;

namespace TexasHoldem
{
    public class Pair : CardCombination, IComparable<Pair>
    {
        public override string Label => "Pair";
        public Pair(ISet<Card> cards) : base(cards)
        {
            if (cards.Count != 2)
                throw new CardCombinationValidationException("A pair is made of 2 cards");

            if (cards.ElementAt(0).CompareTo(cards.ElementAt(1)) != 0)
                throw new CardCombinationValidationException("A pair is made of 2 cards that have the same face");
        }


        public int CompareTo(Pair other)
        {
            return this._cards.First().CompareTo(other._cards.First());
        }

        public override int CompareTo(IValuable other)
        {
            var asPair = other as Pair;

            if (asPair != null)
                return CompareTo(asPair);

            return GlobVars.TypeToValue[this.GetType()] - GlobVars.TypeToValue[other.GetType()];
        }
    }
}
