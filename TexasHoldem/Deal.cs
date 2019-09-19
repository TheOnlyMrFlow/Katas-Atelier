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
        }

        public List<Hand> GetHands()
        {

            List<Hand> hands = new List<Hand>();

            Hand highCardHand = new Hand(
                new HashSet<Card> (FindHighestCards()), Hand.HandLabel.HighCard
            );

            Console.WriteLine(highCardHand);

            hands.Add(highCardHand);

            List<Card[]> pairs = FindMultiples(2);
            List<Card[]> threeOfAKinds = FindMultiples(3);
            List<Card[]> fourOfAKinds = FindMultiples(4);


            if (pairs.Count == 1 && threeOfAKinds.Count == 1)
                hands.Add(new Hand(new HashSet<Card>(Cards), Hand.HandLabel.FullHouse));

            else if (fourOfAKinds.Count == 1)
                hands.Add(new Hand(new HashSet<Card>(fourOfAKinds[0]), Hand.HandLabel.FourOfAkind));


            else
            {
                foreach (Card[] pair in pairs)
                    hands.Add(new Hand(new HashSet<Card>(pair), Hand.HandLabel.Pair));

                foreach (Card[] threeOfAKind in threeOfAKinds)
                    hands.Add(new Hand(new HashSet<Card>(threeOfAKind), Hand.HandLabel.ThreeOfAkind));
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

    }
}
