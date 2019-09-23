using System;
using System.Collections.Generic;
using System.Linq;

namespace TexasHoldem
{
    public class Triple : CardCombination, IComparable<Triple>
    {

        public override string Label => "Three of a kind";
        public Triple(ISet<Card> cards) : base(cards)
        {
            if (cards.Count != 3)
                throw new CardCombinationValidationException("A triple is made of 3 cards");

            Face f = cards.ElementAt(0).Face;
            if (cards.ElementAt(1).Face != f || cards.ElementAt(2).Face != f)
                throw new CardCombinationValidationException("A triple is made of 3 cards that have the same face");
        }

        public int CompareTo(Triple other)
        {
            return this._cards.First().CompareTo(other._cards.First());
        }

        public override int CompareTo(IValuable other)
        {
            var asTriple = other as Triple;

            if (asTriple != null)
                return CompareTo(asTriple);

            return GlobVars.TypeToValue[this.GetType()] - GlobVars.TypeToValue[other.GetType()];
        }

    }
}
