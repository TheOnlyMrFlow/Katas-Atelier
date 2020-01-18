using System;
using System.Collections.Generic;
using System.Text;

namespace Domaine
{
    public interface IRepertoireReferences
    {
        void FixerLaDate(DateTime date);
        double ObtenirLePrixDe(uint idProduit, string uidMagasin);
    }
}
