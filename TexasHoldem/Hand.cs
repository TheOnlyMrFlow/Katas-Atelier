using System;
using System.Collections.Generic;


namespace TexasHoldem
{
    public class Hand : IComparable<Hand>
    {

        private List<CardSet> CardSets = new List<CardSet>();
        private HashSet<Card> AllCards = new HashSet<Card>();

        
        
        public Hand(IEnumerable<CardSet> sets) 
        {

            foreach(CardSet set in sets)
                AllCards.UnionWith(set.GetAllCards());

            if (AllCards.Count != 5)
                throw new Exception("A hand must be made of 5 cards");

            foreach (CardSet set in sets)
                CardSets.Add(set);


        }

        public int CompareTo(Hand other)
        {
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
