using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TexasHoldem
{
    public class Deal
    {

        public List<Card> cards { get; private set; } = new List<Card>();

        public Deal(string[] cardsAsStrings)
        {
            foreach (String cardString in cardsAsStrings)
                this.cards.Add(new Card(cardString));
        }

        public List<Hand> GetHands()
        {

            return new List<Hand> {
                 new Hand(HighestFace(), Hand.HandLabel.HighCard)
            };
        }

        private Face HighestFace()
        {
            return cards.Max().Face;
        }
    }
}
