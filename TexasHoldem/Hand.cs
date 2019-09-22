using System;
using System.Collections.Generic;


namespace TexasHoldem
{
    public class Hand : IComparable<Hand>
    {

        public static Hand FoldedHand()
        {
            Hand h = new Hand();
            h.folded = true;
            return h;
        }

        public readonly List<CardSet> CardSets = new List<CardSet>();
        public readonly HashSet<Card> AllCards = new HashSet<Card>();

        public bool folded { get; private set; }

        private Hand() { }
        
        public Hand(IEnumerable<CardSet> sets) 
        {
            folded = false;

            foreach(CardSet set in sets)
                AllCards.UnionWith(set.GetAllCards());

            if (AllCards.Count != 5)
                throw new Exception("A hand must be made of 5 cards");

            foreach (CardSet set in sets)
                CardSets.Add(set);


        }

        public int CompareTo(Hand other)
        {
            if (folded)
                return other.folded ? 0 : -1;

            else if (other.folded)
                return 1;

            CardSets.Sort();
            other.CardSets.Sort();
            int comparison;
            int i = 0;
            do
            {
                comparison = CardSets[i].CompareTo(other.CardSets[i]);
                i++;
            } while (comparison == 0 && i < CardSets.Count && i < other.CardSets.Count);

            return comparison;
        }

    }

}
