using System;
using System.Collections.Generic;
using System.Text;

namespace Domaine
{
    public interface ILoggerDanalyseDesVentes
    {
        void loggerTop100Ventes(string magasin, DateTime date, IEnumerable<uint> produitsParOrdreDecroissant, int periode);

        void loggerTop100CA(string magasin, DateTime date, IEnumerable<uint> produitsParOrdreDecroissant, int periode);

        void LoggerTop100VentesGlobal(DateTime date, IEnumerable<uint> produitsParOrdreDecroissant, int periode);

        void LoggerTop100CAGlobal(DateTime date, IEnumerable<uint> produitsParOrdreDecroissant, int periode);
    }
}
