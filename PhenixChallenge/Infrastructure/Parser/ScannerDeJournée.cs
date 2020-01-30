using Domaine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Parser
{
    public class ScannerDeJourneé : IRepertoireDeTransactions
    {

        private string dossierDesTransactions;

        public ScannerDeJourneé(string dossierDesTransactions)
        {
            this.dossierDesTransactions = dossierDesTransactions;            
        }

        public IEnumerable<Transaction> ObtenirToutesLesTransactionsALaDateDu(DateTime date)
        {
            var cheminFichier = Path.Join(dossierDesTransactions, FabriquerNomFichier(date));
            return File.ReadAllLines(cheminFichier)
                .Select(ligne => ligne.Split("|"))
                .Select(data => new Transaction
                {
                    Magasin = new Guid(data[2]),
                    Produit = uint.Parse(data[3]),
                    Quantité = ushort.Parse(data[4])
                });
        }

        private string FabriquerNomFichier(DateTime date)
        {
            return new StringBuilder("transactions_")
                .Append(date.ToString("yyyyMMdd"))
                .Append(".data")
                .ToString();
        }

       
    }
}
