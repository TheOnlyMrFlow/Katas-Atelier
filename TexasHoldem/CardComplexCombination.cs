using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TexasHoldem
{
    /// <summary>
    /// Represents a combination of Valuables
    /// </summary>
    public abstract class CardComplexCombination : IValuable, ICardCollection
    {

        public CardComplexCombination() { }

        public abstract ICollection<IValuable> Children { get;  }
        public abstract ISet<Card> AllCards { get; }

        public abstract string Label { get; }

        //public abstract int Value { get; }

        public abstract int CompareTo(IValuable other);
    }

}
