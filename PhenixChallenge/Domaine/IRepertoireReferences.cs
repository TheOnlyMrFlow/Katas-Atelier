using System;
using System.Collections.Generic;
using System.Text;

namespace Domaine
{
    public interface IRepertoireReferences
    {
        CatalogueQuotidien ObtenirLeCatalogueDuJour(DateTime date);
    }
}
