using System;

namespace TexasHoldem
{
    /// <summary>
    ///    IValuable are Texas Holdem elements that can be compared between each other
    /// </summary>
    public interface IValuable : IComparable<IValuable>
    {
        String Label { get; }
    }
}
