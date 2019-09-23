using System;
using System.Collections.Generic;
using System.Linq;
using TexasHoldem.Utils;

namespace TexasHoldem
{
    public class Hand : CardComplexCombination, IComparable<Hand>
    {

        public override string Label { get => _children.Max().Label; }
        public bool Folded {get; private set;}

        private List<IValuable> _children;

        public static Hand FoldedHand()
        {
            Hand h = new Hand();
            h.Folded = true;
            return h;
        }

        private Hand() : base()
        {
            _children = new List<IValuable>();
        }


        public Hand(ICollection<IValuable> children) : base()
        {
            _children = new List<IValuable>(children);

            if (AllCards.Count != 5)
                throw new CardCombinationValidationException("A hand must be made of 5 cards");

            Folded = false;
        }

        public override ICollection<IValuable> Children { get => new List<IValuable>(_children); }

        public override ISet<Card> AllCards
        {
            get
            {
                ISet<Card> result = new HashSet<Card>();
                foreach (IValuable v in _children)
                {
                    var asCard = v as Card;
                    if (asCard != null)
                        result.Add(asCard);
                    else
                        result.UnionWith(((ICardCollection)v).AllCards);
                }
                return result;
            }
        }

        

        public override int CompareTo(IValuable other)
        {
            var asHand = other as Hand;

            if (asHand != null)
                return CompareTo(asHand);

            return _children.Max().CompareTo(other);
        }

        public int CompareTo(Hand other)
        {
            if (other.Folded && this.Folded)
                return 0;

            if (other.Folded)
                return 1;

            if (this.Folded)
                return -1;

            PriorityQueue<IValuable> selfToVisit = new PriorityQueue<IValuable>();
            PriorityQueue<IValuable> otherToVisit = new PriorityQueue<IValuable>();

            foreach (IValuable v in _children.OrderByDescending(val => val))
                selfToVisit.Enqueue(v);

            foreach (IValuable v in other._children.OrderByDescending(val => val))
                otherToVisit.Enqueue(v);

            while (selfToVisit.Count() > 0 && otherToVisit.Count() > 0)
            {
                int comparison = selfToVisit.Dequeue().CompareTo(otherToVisit.Dequeue());
                if (comparison != 0)
                    return comparison;
            }


            return 0;
        }

    }

}
