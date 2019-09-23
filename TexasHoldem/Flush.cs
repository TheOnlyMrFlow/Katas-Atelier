using System;
using System.Collections.Generic;
using System.Linq;
using TexasHoldem.Utils;

namespace TexasHoldem
{
    public class Flush : CardCombination, IComparable<Flush>
    {
        public override string Label => "Flush";

        public Flush(ISet<Card> cards) : base(cards)
        {
            if (cards.Count != 5)
                throw new CardCombinationValidationException("A flush is made of 5 cards");

            Suit s = cards.ElementAt(0).Suit;
            foreach (Card c in cards)
                if (c.Suit != s)
                    throw new CardCombinationValidationException("All cards of a flush should have the same suit");
        }

        public int CompareTo(Flush other)
        {
            PriorityQueue<Card> selfToVisit = new PriorityQueue<Card>();
            PriorityQueue<Card> otherToVisit = new PriorityQueue<Card>();

            foreach (Card c in _cards.OrderByDescending(c => c))
                selfToVisit.Enqueue(c);

            foreach (Card c in other._cards.OrderByDescending(c => c))
                otherToVisit.Enqueue(c);

            while(selfToVisit.Count() > 0 && otherToVisit.Count() > 0)
            {
                int comparison = selfToVisit.Dequeue().CompareTo(otherToVisit.Dequeue());
                if (comparison != 0)
                    return comparison;
            }

            return 0;
        }

        public override int CompareTo(IValuable other)
        {
            var asFlush = other as Flush;

            if (asFlush != null)
                return CompareTo(asFlush);

            return GlobVars.TypeToValue[this.GetType()] - GlobVars.TypeToValue[other.GetType()];
        }
    }
}
