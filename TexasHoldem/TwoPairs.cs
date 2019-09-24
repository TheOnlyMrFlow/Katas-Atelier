using System;
using System.Collections.Generic;
using System.Linq;

namespace TexasHoldem
{
    public class TwoPairs : CardComplexCombination, IComparable<TwoPairs>
    {
        public override string Label => "Two pairs";

        Pair _highPair;
        Pair _lowPair;

        public TwoPairs(Pair a, Pair b) : base()
        {
            if (a.CompareTo(b) > 0)
            {
                _highPair = a;
                _lowPair = b;
            }
            else
            {
                _highPair = b;
                _lowPair = a;
            }
        }

        public override ISet<Card> AllCards { get => new HashSet<Card>(_highPair.AllCards.Union(_lowPair.AllCards)); }

        public override ICollection<IValuable> Children { get => new Pair[] { _highPair, _lowPair };  }

        public int CompareTo(TwoPairs other)
        {
            int comp = _highPair.CompareTo(other._highPair);
            if (comp != 0)
                return comp;
            return _lowPair.CompareTo(other._lowPair);
        }

        public override int CompareTo(IValuable other)
        {
            var asTwoPairs = other as TwoPairs;

            if (asTwoPairs != null)
                return CompareTo(asTwoPairs);

            return GlobVars.TypeToValue[this.GetType()] - GlobVars.TypeToValue[other.GetType()];
        }

    }

}
