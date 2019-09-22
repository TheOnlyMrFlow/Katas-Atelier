using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TexasHoldem.Utils;

namespace TexasHoldem
{
    public class CardSet : IComparable<CardSet>, IComparable<Card>
    {
        public enum CardSetLabel
        {
            SingleCard = 0,
            Pair = 1,
            TwoPairs = 2,
            ThreeOfAkind = 3,
            Straight = 4, // Five cards in numerical order, but not of the same suit.
            Flush = 5, // Five cards, all in one suit, but not in numerical order.
            FullHouse = 6, // A pair plus three of a kind in the same hand.
            FourOfAkind = 7,
            StraightFlush = 8, // Five cards in a row, all in the same suit.
            RoyalFlush = 9 // Ten, Jack, Queen, King, Ace all in the same suit.
        }

        public static CardSet BuildRoyalFlush(Card[] cards)
        {
            if (cards.Length != 5)
                throw new Exception("A royal flush is made of 5 cards");

            return new CardSet(CardSetLabel.RoyalFlush, cards);
        }

        public static CardSet BuildStraightFlush(Card[] cards)
        {
            if (cards.Length != 5)
                throw new Exception("A royal flush is made of 5 cards");

            return new CardSet(CardSetLabel.StraightFlush, cards);
        }

        public static CardSet BuildFourOfAKind(Card[] cards)
        {
            if (cards.Length != 4)
                throw new Exception("A four of a kind is made of 4 cards");

            return new CardSet(CardSetLabel.FourOfAkind, cards);
        }

        public static CardSet BuildFullHouse(CardSet threeOfAKind, CardSet pair)
        {

            CardSet[] subSets = new CardSet[] { threeOfAKind, pair };

            return new CardSet(CardSetLabel.FullHouse, subSets);
        }

        public static CardSet BuildFlush(Card[] cards)
        {
            if (cards.Length != 5)
                throw new Exception("A flush is made of 5 cards");

            return new CardSet(CardSetLabel.Flush, cards);
        }

        public static CardSet BuildStraight(Card[] cards)
        {
            if (cards.Length != 5)
                throw new Exception("A straight is made of 5 cards");

            return new CardSet(CardSetLabel.Straight, cards);
        }

        public static CardSet BuildThreeOfAKind(Card[] cards)
        {
            if (cards.Length != 3)
                throw new Exception("A three of a kind is made of 3 cards");

            return new CardSet(CardSetLabel.ThreeOfAkind, cards);
        }

        public static CardSet BuildTwoPairs(CardSet[] pairs)
        {
            if (pairs.Length != 2)
                throw new Exception("A two-pair set is made of 2 pairs");

            return new CardSet(CardSetLabel.TwoPairs, pairs);
        }

        public static CardSet BuildPair(Card[] cards)
        {
            if (cards.Length != 2)
                throw new Exception("A pair is made of 2 cards");

            return new CardSet(CardSetLabel.Pair, cards);
        }


        public CardSetLabel Label;

        public CardSet[] SubSets;

        protected CardSet(CardSetLabel label, CardSet[] subSets)
        {
            Label = label;
            SubSets = subSets;
        }


        public HashSet<Card> GetAllCards()
        {
            // recursive

            Card asCard = this as Card;
            if (asCard != null)
                return new HashSet<Card> { asCard };

            HashSet<Card> cards = new HashSet<Card>();

            foreach (CardSet subSet in SubSets)
                cards.UnionWith(subSet.GetAllCards());

            return cards;

        }

        public int CompareTo(Card other)
        {
            var asCard = this as Card;

            if (asCard != null)
                return asCard.Label - other.Label;
            else
                return 1;

        }

        // recursive comparison
        public int CompareTo(CardSet other)
        {

            Card asCard = this as Card;
            Card otherAsCard = other as Card;

            if (asCard != null)
            {
                if (otherAsCard != null)
                    return asCard.CompareTo(otherAsCard);

                else
                    return -1;
            }

            else if (otherAsCard != null)
                return this.CompareTo(otherAsCard);


            if (Label > other.Label)
                return 1;
            else if (Label < other.Label)
                return -1;

            // if same label we gotta check the subsets:

            PriorityQueue<CardSet> toVisit = new PriorityQueue<CardSet>();
            PriorityQueue<CardSet> otherToVisit = new PriorityQueue<CardSet>();


            List<CardSet> branches = new List<CardSet>(this.SubSets);
            List<CardSet> otherBranches = new List<CardSet>(other.SubSets);


            //branches.OrderByDescending(e => e);
            //otherBranches.OrderByDescending(e => e);

            foreach (CardSet branch in branches)
                toVisit.Enqueue(branch);

            foreach (CardSet branch in otherBranches)
                otherToVisit.Enqueue(branch);

            // double depth first search with priority queue
            while (toVisit.Count() > 0 && otherToVisit.Count() > 0)
            {
                CardSet current = toVisit.Dequeue();
                CardSet otherCurrent = otherToVisit.Dequeue();

                int comparison = current.CompareTo(otherCurrent);

                if (comparison != 0)
                    return comparison;

                branches = new List<CardSet>(current.SubSets);
                otherBranches = new List<CardSet>(otherCurrent.SubSets);

                //branches.OrderByDescending(e => e);
                //otherBranches.OrderByDescending(e => e);

                foreach (CardSet branch in branches)
                    toVisit.Enqueue(branch);

                foreach (CardSet branch in otherBranches)
                    otherToVisit.Enqueue(branch);

            }

            // reaching this point means that every single subsets were equal so this is a tie
            return 0;
        }
    }
}
