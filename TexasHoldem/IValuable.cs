using System;

namespace TexasHoldem
{
    public interface IValuable : IComparable<IValuable>
    {
        String Label { get; }
    }
}
