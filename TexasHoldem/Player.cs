using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TexasHoldem
{
    public class Player : IComparable<Player>
    {

        public ISet<Card> Cards { get; private set; } = new HashSet<Card>();

        private Dictionary<Face, int> facesOccurences = new Dictionary<Face, int>();
        private Dictionary<Suit, int> suitsOccurences = new Dictionary<Suit, int>();

        public Player(string[] cardsAsStrings)
        {
            int n = 0;
            foreach (String cardString in cardsAsStrings)
            {
                n++;

                if (n > 7)
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

        }

        public Hand GetHand()
        {

            if (Cards.Count < 7)
                return Hand.FoldedHand();

            CardSet[] straights = FindAllStraights();
            CardSet[] flushes = FindAllFlushes();
            CardSet[] pairs = FindAllPairs();
            CardSet[] triples = FindAllThreeOfAKind();
            CardSet quadruple = FindFourOfAKind();
            

            if (flushes.Length > 0)
            {
                foreach (CardSet f in flushes)
                {
                    Card[] cards = f.SubSets as Card[];
                    if (IsStraight(cards))
                    {
                        if (cards.Select(c => c.Face).Contains(Face.Ace))
                            return new Hand(new[] { CardSet.BuildRoyalFlush(cards) });
                        else
                            return new Hand(new[] { CardSet.BuildStraightFlush(cards) });
                    }
                }
            }

            List<CardSet> sets = new List<CardSet>();

            if (quadruple != null)
                sets.Add(quadruple);

            if (triples.Length > 0 && pairs.Length > 0)
                return new Hand(new[] { CardSet.BuildFullHouse(triples.Max(), pairs.Max()) });


            if (flushes.Length > 0)
                return new Hand(new[] { flushes.Max() });

            if (straights.Length > 0)
                return new Hand(new[] { straights.Max() });

            if (triples.Length > 0)
                sets.Add(triples.Max());

            if (pairs.Length > 1)
            {
                CardSet[] twoHighestPairs = pairs.OrderByDescending(p => p).Take(2).ToArray();
                sets.Add(CardSet.BuildTwoPairs(pairs));
            }

            else if (pairs.Length == 1)
                sets.Add(pairs[0]);


            HashSet<Card> usedCards = new HashSet<Card>();

            foreach (CardSet set in sets)
                usedCards.UnionWith(set.GetAllCards());

            int missingCardAmount = 5 - usedCards.Count;

            sets.AddRange(Cards.Except(usedCards).OrderByDescending(c => c).Take(missingCardAmount));

            return new Hand(sets);
        }

        public Hand GetHand(out HashSet<Card> unused)
        {
            Hand h = GetHand();

            unused = new HashSet<Card>(Cards.Except(h.AllCards));

            return h;
        }

        public int CompareTo(Player other)
        {
            return GetHand().CompareTo(other.GetHand());
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
            List<Card[]> quadruples = FindTuplesOfSize(4);

            if (quadruples.Count == 0)
                return null;

            return CardSet.BuildFourOfAKind(quadruples[0]);

        }


        private CardSet[] FindAllFlushes()
        {
            // suitsOccurences.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
            var maxSuit = suitsOccurences.Aggregate((x, y) => x.Value > y.Value ? x : y).Key; ;
            if (suitsOccurences[maxSuit] < 5)
                return new CardSet[0];

            List<CardSet> flushes = new List<CardSet>();

            var combinations = Cards
                                .Where(c => c.Suit == maxSuit)
                                .DifferentCombinations(5);

            foreach(IEnumerable<Card> comb in combinations)
            {
                flushes.Add(CardSet.BuildFlush(comb.ToArray()));
            }


            return flushes.ToArray();
        }        


        private CardSet[] FindAllStraights()
        {
            HashSet<Card> temp = new HashSet<Card>(Cards);
            var aces = temp.Where(c => c.Face == Face.Ace).ToArray();
            foreach (Card ace in aces)
                temp.Add(new Card(Face.One, ace.Suit));

            Card[] ordered = temp
                    .OrderByDescending(c => c)
                    .ToArray();

            Stack<Card> potentialStraight = new Stack<Card>();

            foreach (Card c in ordered)
            {
                if (potentialStraight.Count == 0)
                    potentialStraight.Push(c);

                else
                {
                    if (potentialStraight.Peek().Face == c.Face + 1)
                        potentialStraight.Push(c);
                    else
                        potentialStraight.Clear();
                }

            }

            if (potentialStraight.Count < 5)
            {
                return new CardSet[0];
            }


            List<CardSet> straights = new List<CardSet>();

            while (potentialStraight.Count > 5)
            {
                straights.AddRange(potentialStraight.Take(5));
                potentialStraight.Pop();
            }

            return straights.ToArray();
        }

        private static bool IsStraight(Card[] sequence)
        {
            Card[] orderedSeq = sequence.OrderBy(c => c.Face).ToArray();

            int len = orderedSeq.Length;
            Face prevFace = orderedSeq[0].Face;
            for (int i = 1; i < len; i++)
            {
                if (orderedSeq[i].Face != prevFace + 1)
                    return false;

                prevFace = orderedSeq[i].Face;
            }

            return true;
        }

    }
}


// thank you stack overflow
public static class Util
{
    public static IEnumerable<IEnumerable<T>> DifferentCombinations<T>(this IEnumerable<T> elements, int k)
    {
        return k == 0 ? new[] { new T[0] } :
          elements.SelectMany((e, i) =>
            elements.Skip(i + 1).DifferentCombinations(k - 1).Select(c => (new[] { e }).Concat(c)));
    }
}