using System.Collections.Generic;

namespace TexasHoldem
{
    /// <summary>
    /// Defines an object that contains Card elements
    /// </summary>
    interface ICardCollection
    {
        ISet<Card> AllCards { get; }
    }

}
