using System;
using System.Collections.Generic;

namespace TexasHoldem
{
    public static class GlobVars
    {
        public static readonly Dictionary<Type, int> TypeToValue
           = new Dictionary<Type, int>
           {
                { typeof (Card), 0 },
                { typeof (Pair), 1 },
                { typeof (TwoPairs), 2 },
                { typeof (Triple), 3 },
                { typeof (Straight), 4 },
                { typeof (Flush), 5 },
                { typeof (FullHouse), 6 },
                { typeof (Quadruple), 7 },
                { typeof (StraightFlush), 8 },
                { typeof (RoyalFlush), 9 }
           };
    }

}
