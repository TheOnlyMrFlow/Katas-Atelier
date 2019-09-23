using System;
using System.Collections.Generic;
using System.Linq;

namespace TexasHoldem
{
    public class FullHouse : CardComplexCombination, IComparable<FullHouse>
    {
        public override string Label => "Full house";

        Triple _triple;
        Pair _pair;
        public FullHouse(Triple triple, Pair pair)
        {
            _triple = triple;
            _pair = pair;
        }

        public override ICollection<IValuable> Children { get => new CardCombination[] { _triple, _pair }; }

        public override ISet<Card> AllCards { get => new HashSet<Card>(_pair.AllCards.Union(_triple.AllCards)); }

        public override int CompareTo(IValuable other)
        {
            var asFullHouse = other as FullHouse;

            if (asFullHouse != null)
                return CompareTo(asFullHouse);

            return GlobVars.TypeToValue[this.GetType()] - GlobVars.TypeToValue[other.GetType()];
        }

        public int CompareTo(FullHouse other)
        {
            int comp = _triple.CompareTo(other._triple);
            if (comp != 0)
                return comp;
            return _pair.CompareTo(other._pair);
        }

    }
}
