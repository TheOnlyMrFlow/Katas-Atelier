using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TexasHoldem
{
    public class Deal
    {

        public List<Card> Cards { get; private set; } = new List<Card>();

        private Dictionary<Face, int> facesOccurences = new Dictionary<Face, int>();
        private Dictionary<Suit, int> suitsOccurences = new Dictionary<Suit, int>();

        public Deal(string[] cardsAsStrings)
        {
            int n = 0;
            foreach (String cardString in cardsAsStrings)
            {
                n++;

                if (n > 6)
                    throw new Exception("Cannot have more than 7 cards in texas holdem");

                Card c = new Card(cardString);
                this.Cards.Add(c);

                int currentFaceOccurence;
                int currentSuitOccurence;

                facesOccurences.TryGetValue(c.Face, out currentFaceOccurence);
                suitsOccurences.TryGetValue(c.Suit, out currentSuitOccurence);

                facesOccurences[c.Face] = currentFaceOccurence + 1;
                suitsOccurences[c.Suit] = currentSuitOccurence + 1;

            }

            Cards.Sort();
        }

        //public CardSet GetHand()
        //{

        //    // find all straights
        //    // find all flush
        //    // find all pairs
        //    // find all three of a kind
        //    // find all four of a kind

        //    // is there any flush ?
        //        // yes -> is there any straight ?
        //            // yes -> does a flush and a straight match together ?
        //                // yes-> do they end with an ace ?
        //                        // yes -> return royalflush
        //                        // no -> return straight flush
            
        //    // is there any four of a kind ?
        //        // yes -> return four of a kind + 5th card

        //    // is there any three of a kind ?
        //        // yes -> is there any pair ?
        //            // yes -> return fullhouse made of highest of both

        //    // is there any flush ?
        //        // yes -> return flush

        //    // is there any straight ?
        //        // yes -> return straight

        //    // is there any three of a kind ?
        //        // yes -> return three of a kind + 2 highest of the cards left

        //    // is there any pair ?
        //        // yes -> is there another ?
        //            // yes -> return two pairs + highest of the cards that are left
        //            // no -> return pair + 3 highest of the cards left

        //    // return 5 highest cards

        //}

        private static Card[] FindNHighest(ICollection<Card> cards, int n)
        {
            return cards.ToList().OrderByDescending(c => c).ToList().GetRange(0, n).ToArray();
        }

        private List<Card[]> FindTuplesOfSize(int n)
        {

            return Cards
                    .Where(c => facesOccurences[c.Face] == n)
                    .GroupBy(c => c.Face)
                    .Select(grp => grp.ToArray())
                    .ToList();

        }

        private CardSet[] FindAllPairs()
        {
            List<Card[]> pairs = FindTuplesOfSize(2);

            CardSet[] ret = new CardSet[pairs.Count];

            for (int i = 0; i < pairs.Count; i++)
                ret[i] = CardSet.BuildPair(pairs[i]);

            return ret;
        }

        private CardSet[] FindAllThreeOfAKind()
        {
            List<Card[]> triples = FindTuplesOfSize(3);

            CardSet[] ret = new CardSet[triples.Count];

            for (int i = 0; i < triples.Count; i++)
                ret[i] = CardSet.BuildThreeOfAKind(triples[i]);

            return ret;
        }

        private CardSet FindFourOfAKind()
        {
            List<Card[]> quadruples = FindTuplesOfSize(3);

            if (quadruples.Count == 0)
                return null;

            return CardSet.BuildFourOfAKind(quadruples[0]);

        }


        private HashSet<Face> GetFlush()
        {
            bool isFlush = true;
            Suit s = Cards[0].Suit;
            for(int i = 1; i < Cards.Count; i++)
            {
                if (Cards[i].Suit != s)
                    return null;
            }
            return CardColelctionToFaceSet(Cards);
        }

        private HashSet<Face> GetStraight()
        {

            if (IsStraight(Cards))
                return CardColelctionToFaceSet(Cards);

            int lastCardIndex = Cards.Count - 1;
            if (Cards[lastCardIndex].Face == Face.Ace)
            {
                Card aceCard = Cards[lastCardIndex];
                List<Card> cardsWithAceFirst = new List<Card>();
                cardsWithAceFirst.Add(new Card(Face.One, aceCard.Suit));
                cardsWithAceFirst.AddRange(Cards.GetRange(0, 4));

                if (IsStraight(cardsWithAceFirst))
                    return CardColelctionToFaceSet(cardsWithAceFirst);
               
            }

            return null;

        }

        private static bool IsStraight(List<Card> cards)
        {

            for (int i = 0; i < cards.Count - 1; i++)
            {
                if (i == 0 && cards[0].Face == Face.Ace) // if first card is an ace
                {
                    if (cards[1].Face != Face.Two)
                        return false;

                    continue;
                }

                else if (cards[i + 1].Face != cards[i].Face + 1)
                    return false;
            }
            return true;

        }

        private static HashSet<Face> CardColelctionToFaceSet(ICollection<Card> cards)
        {
            return new HashSet<Face> (cards.Select(c => c.Face));
        }


    }
}
