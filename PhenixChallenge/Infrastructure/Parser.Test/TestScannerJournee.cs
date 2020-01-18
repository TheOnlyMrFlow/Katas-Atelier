using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace Parser.Test
{
    public class TestScannerJournee
    {
        ScannerDeJourneé scannerJournee = new ScannerDeJourneé(@"C:\Users\comte\Documents\Kata Atelier\PhenixGenerator.0.1\out\transactions");

   
        [Fact]
        public void le_nombre_de_transactions_est_bon()
        {
            scannerJournee.ObtenirToutesLesTransactionsALaDateDu(new DateTime(2019, 1, 4)).Should().HaveCount(150000);
        }

        [Fact]
        public void le_produit_de_la_transactions_est_bon()
        {
            scannerJournee
                .ObtenirToutesLesTransactionsALaDateDu(new DateTime(2019, 1, 4))
                .ElementAt(10)
                .idProduit
                .Should().Be(11174);
        }

        [Fact]
        public void le_magasin_de_la_transactions_est_bon()
        {
            scannerJournee
                .ObtenirToutesLesTransactionsALaDateDu(new DateTime(2019, 1, 4))
                .ElementAt(10)
                .uuidMagasin
                .Should().Be("f8f52ab1-4ed6-068a-dbdd-a4ef4fb3aada");
        }

        [Fact]
        public void la_quantite_de_la_transactions_est_bonne()
        {
            scannerJournee
                .ObtenirToutesLesTransactionsALaDateDu(new DateTime(2019, 1, 4))
                .ElementAt(10)
                .quantité
                .Should().Be(1);
        }

    }
}
