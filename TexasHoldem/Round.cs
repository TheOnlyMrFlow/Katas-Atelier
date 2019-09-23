﻿using System;
using System.Linq;
using System.Collections.Generic;

namespace TexasHoldem
{
    public class Round
    {

        public List<Player> players { get; private set; } = new List<Player>();

        public Round() { }

        public Round AddPlayer(Player p)
        {
            players.Add(p);
            return this;
        }

        public Round AddPlayer(String cards)
        {
            players.Add(new Player(cards.Split(' ')));
            return this;
        }
        public List<Player> GetRanking()
        {
            return players.OrderByDescending(p => p).ToList();
        }

        public string ShowResult()
        {
            string output = "";

            var playersInOrder = GetRanking();

            var winningHand = playersInOrder[0].Hand;

            foreach (Player p in playersInOrder)
            {
                HashSet<Card> unused;
                Hand h = p.Hand;

                if (p.Cards.Count < 7)
                {
                    foreach(Card c in p.Cards)
                        output += c + " ";

                    output += "\n";
                    continue;
                }

                foreach (Card c in h.AllCards)
                    output += c + " ";

                foreach (Card c in p.UnusedCards)
                    output += c + " ";

                output += h.Children.Max().Label;

                if (p.Hand.CompareTo(winningHand) == 0)
                    output += " (winner)";

                output += "\n";
            }

            return output;

        }

    }
}
