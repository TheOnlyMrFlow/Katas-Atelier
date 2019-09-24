using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TexasHoldem
{

    /// <summary>
    /// Represents a player's set of cards
    /// </summary>
    public class Player :  IComparable<Player>
    {

        public ISet<Card> Cards { get; private set; } = new HashSet<Card>();

        public Hand Hand { get; private set; }
        public ISet<Card> UnusedCards { get; private set; }


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

                ComputeHand();
                ComputeUnusedCards();

            }

        }

        private void ComputeHand()
        {

            if (Cards.Count < 7)
            {
                Hand = Hand.FoldedHand();
                return;
            }

            Straight[] straights = FindAllStraights();
            Flush[] flushes = FindAllFlushes();
            Pair[] pairs = FindAllPairs();
            Triple[] triples = FindAllThreeOfAKind();
            Quadruple quadruple = FindFourOfAKind();


            if (flushes.Length > 0)
            {
                foreach (Flush f in flushes)
                {

                    try
                    {
                        // Will try to make a royal flush throw error if it is not a proper one
                        RoyalFlush rf = new RoyalFlush(f.AllCards);
                        Hand =  new Hand(new[] { rf });
                        return;

                    }
                    catch (CardCombinationValidationException) { }

                    try
                    {
                        // Will try to make a straight flush throw error if it is not a proper one
                        StraightFlush sf = new StraightFlush(f.AllCards);
                        Hand = new Hand(new[] { sf });
                        return;

                    }
                    catch (CardCombinationValidationException) { }

                }
            }

            List<IValuable> combs = new List<IValuable>();

            if (quadruple != null)
                combs.Add(quadruple);

            else if (triples.Length > 0 && pairs.Length > 0)
            {
                Hand =  new Hand(new[] { new FullHouse(triples.Max(), pairs.Max()) });
                return;
            }

            else if (flushes.Length > 0)
            {
                Hand = new Hand(new[] { flushes.Max() });
                return;
            }

            else if (straights.Length > 0)
            {
                Hand =  new Hand(new[] { straights.Max() });
                return;
            }

            else if (triples.Length > 0)
                combs.Add(triples.Max());

            else if (pairs.Length > 1)
            {
                Pair[] twoHighestPairs = pairs.OrderByDescending(p => p).Take(2).ToArray();
                combs.Add(new TwoPairs(pairs[0], pairs[1]));
            }

            else if (pairs.Length == 1)
                combs.Add(pairs[0]);


            HashSet<Card> cardsUsed = new HashSet<Card>();

            foreach (ICardCollection comb in combs)
                cardsUsed.UnionWith(comb.AllCards);

            int missingCardAmount = 5 - cardsUsed.Count;

            combs.AddRange(Cards.Except(cardsUsed).OrderByDescending(c => c).Take(missingCardAmount));

            Hand = new Hand(combs);
        }


        private void ComputeUnusedCards()
        {
            UnusedCards = new HashSet<Card>(Cards.Except(Hand.AllCards));   
        }

        public int CompareTo(Player other)
        {
            return Hand.CompareTo(other.Hand);
        }

        private List<HashSet<Card>> FindTuplesOfSize(int n)
        {
            return Cards
                    .Where(c => facesOccurences[c.Face] == n)
                    .GroupBy(c => c.Face)
                    .Select(grp => new HashSet<Card>(grp))
                    .ToList();
        }

        private Pair[] FindAllPairs()
        {
            return FindTuplesOfSize(2).Select(set => new Pair(set)).ToArray();
        }

        private Triple[] FindAllThreeOfAKind()
        {
            return FindTuplesOfSize(3).Select(set => new Triple(set)).ToArray();
        }

        private Quadruple FindFourOfAKind()
        {
            List<HashSet<Card>> quadruples = FindTuplesOfSize(4);

            if (quadruples.Count == 0)
                return null;

            return new Quadruple(quadruples[0]); 
        }

        private Flush[] FindAllFlushes()
        {
            var maxSuit = suitsOccurences.Aggregate((x, y) => x.Value > y.Value ? x : y).Key; ;

            return suitsOccurences[maxSuit] < 5
                ? new Flush[0]
                : Cards
                    .Where(c => c.Suit == maxSuit)
                    .DifferentCombinations(5)
                    .Select(comb => new Flush(new HashSet<Card>(comb)))
                    .ToArray();
        }        

        private Straight[] FindAllStraights()
        {
            HashSet<Card> temp = new HashSet<Card>(Cards);
            var aces = temp.Where(c => c.Face == Face.Ace).ToArray();
            foreach (Card ace in aces)
                temp.Add(new Card(Face.One, ace.Suit));

            Card[] ordered = temp
                    .OrderByDescending(c => c)
                    .ToArray();

            // A potential sequence of cards of any size
            Stack<Card> potentialStraight = new Stack<Card>();

            foreach (Card c in ordered)
                if (potentialStraight.Count == 0)
                    potentialStraight.Push(c);
                else
                    if (potentialStraight.Peek().Face == c.Face + 1)
                        potentialStraight.Push(c);
                    else
                        potentialStraight.Clear();

            if (potentialStraight.Count < 5)
                return new Straight[0];

            List<Straight> straights = new List<Straight>();

            // if the potential straight has more than 5 cards we cut it into sequences of 5
            // The reason we dont simply pick the highest one is because a smaller straight may be a straight flush
            // but it is no concern of this method to find out.
            while (potentialStraight.Count > 5)
            {
                straights.Add(new Straight(new HashSet<Card>(potentialStraight.Take(5))));
                potentialStraight.Pop();
            }

            return straights.ToArray();
        }

    }
}
