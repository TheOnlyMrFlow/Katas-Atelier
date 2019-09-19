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
            foreach (String cardString in cardsAsStrings)
            {
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

        public List<Hand> GetHands()
        {

            List<Hand> hands = new List<Hand>();

            Card[] highCards = FindHighestCards();


            hands.Add(new Hand(new HashSet<Card>(highCards), Hand.HandLabel.HighCard));

            List<Card[]> pairs = FindMultiples(2);
            List<Card[]> threeOfAKinds = FindMultiples(3);
            List<Card[]> fourOfAKinds = FindMultiples(4);


            if (pairs.Count == 1 && threeOfAKinds.Count == 1)
                hands.Add(new Hand(new HashSet<Card>(Cards), Hand.HandLabel.FullHouse));

            else if (fourOfAKinds.Count == 1)
                hands.Add(new Hand(new HashSet<Card>(fourOfAKinds[0]), Hand.HandLabel.FourOfAkind));


            else if (pairs.Count > 0 || threeOfAKinds.Count > 0)
            {
                foreach (Card[] pair in pairs)
                    hands.Add(new Hand(new HashSet<Card>(pair), Hand.HandLabel.Pair));

                foreach (Card[] threeOfAKind in threeOfAKinds)
                    hands.Add(new Hand(new HashSet<Card>(threeOfAKind), Hand.HandLabel.ThreeOfAkind));
            }

            else
            {
                ISet<Card> flush = GetFlush();
                ISet<Card> straight = GetStraight();

                if (flush != null && straight != null)
                {
                    if (straight.Max().Face == Face.Ace)
                        hands.Add(new Hand(straight, Hand.HandLabel.RoyalFlush));

                    else
                        hands.Add(new Hand(straight, Hand.HandLabel.StraightFlush));
                }

                else if (straight != null)
                    hands.Add(new Hand(straight, Hand.HandLabel.Straight));

                else if (flush != null)
                    hands.Add(new Hand(flush, Hand.HandLabel.Flush));


            }

            return hands;

        }

        private Card[] FindHighestCards()
        {
            Card max = Cards.Max();
            return Cards.Where(c => c.CompareTo(max) == 0).ToArray();
        }

        private List<Card[]> FindMultiples(int n)
        {
            return Cards
               .Where(c => facesOccurences[c.Face] == n)
               .GroupBy(c => c.Face)
               .Select(grp => grp.ToArray())
               .ToList();
        }

        private HashSet<Card> GetFlush()
        {
            bool isFlush = true;
            Suit s = Cards[0].Suit;
            for(int i = 1; i < Cards.Count; i++)
            {
                if (Cards[i].Suit != s)
                    return null;
            }
            return new HashSet<Card>(Cards);
        }

        private HashSet<Card> GetStraight()
        {
            
            if (IsStraight(Cards))
                return new HashSet<Card>(Cards);

            int lastCardIndex = Cards.Count - 1;
            if (Cards[lastCardIndex].Face == Face.Ace)
            {
                Card aceCard = Cards[lastCardIndex];
                List<Card> cardsWithAceFirst = new List<Card>();
                cardsWithAceFirst.Add(new Card(Face.One, aceCard.Suit));
                cardsWithAceFirst.AddRange(Cards.GetRange(0, 4));

                if (IsStraight(cardsWithAceFirst))
                    return new HashSet<Card>(cardsWithAceFirst);
               
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

    }
}
