using System;
using System.Collections.Generic;
using System.Linq;

namespace TexasHoldem
{
    public class Quadruple : CardCombination, IComparable<Quadruple>
    {
        public override string Label => "Four of a kind";
        public Quadruple(ISet<Card> cards) : base(cards)
        {
            if (cards.Count != 4)
                throw new CardCombinationValidationException("A quadruple is made of 4 cards");

            Face f = cards.ElementAt(0).Face;
            if (cards.ElementAt(1).Face != f || cards.ElementAt(2).Face != f || cards.ElementAt(3).Face != f)
                throw new CardCombinationValidationException("A triple is made of 4 cards that have the same face");
        }
       

        public int CompareTo(Quadruple other)
        {
            return this._cards.First().CompareTo(other._cards.First());
        }

        public override int CompareTo(IValuable other)
        {
            var asQuadruple = other as Quadruple;

            if (asQuadruple != null)
                return CompareTo(asQuadruple);

            return GlobVars.TypeToValue[this.GetType()] - GlobVars.TypeToValue[other.GetType()];
        }

    }
}
