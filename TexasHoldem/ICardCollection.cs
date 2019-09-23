using System.Collections.Generic;

namespace TexasHoldem
{
    interface ICardCollection
    {
        ISet<Card> AllCards { get; }
    }

}
