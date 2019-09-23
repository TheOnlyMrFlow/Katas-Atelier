using System;
using System.Collections.Generic;
using System.Text;

namespace TexasHoldem
{
    class CardCombinationValidationException : Exception
    {
        public CardCombinationValidationException()
        {
        }

        public CardCombinationValidationException(string message)
            : base(message)
        {
        }

    }
}
