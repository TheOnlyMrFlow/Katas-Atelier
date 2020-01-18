using System;
using System.Collections.Generic;
using System.Text;

namespace Domaine
{
    public interface IRepertoireDeTransactions
    {
        IEnumerable<Transaction> ObtenirToutesLesTransactionsALaDateDu(DateTime date);
    }
}
