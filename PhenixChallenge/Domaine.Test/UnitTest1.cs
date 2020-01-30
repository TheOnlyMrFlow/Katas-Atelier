using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Domaine.Test
{
    public class AnalysePériodiqueTest
    {
        readonly AnalysePériodique analyseJournalière;
        DateTime dateDuJour = new DateTime(2017, 05, 14);
        Guid magasin1;
        public AnalysePériodiqueTest()
        {
            magasin1 = new Guid("2a4b6b81-5aa2-4ad8-8ba9-ae1a006e7d71");
            var magasin2 = new Guid("bdc2a431-797d-4b07-9567-67c565a67b84");
            var magasin3 = new Guid("72a2876c-bc8b-4f35-8882-8d661fac2606");
            var magasin4 = new Guid("29366c83-eae9-42d3-a8af-f15339830dc5");
            var transactions = new List<Transaction>
            {
                new Transaction { Magasin = magasin1, Produit = 531, Quantité = 5 },
                new Transaction { Magasin = magasin2, Produit = 55, Quantité = 3 },
                new Transaction { Magasin = magasin3, Produit = 39, Quantité = 8 },
                new Transaction { Magasin = magasin4, Produit = 10, Quantité = 6 },
                new Transaction { Magasin = magasin1, Produit = 773, Quantité = 2 },
                new Transaction { Magasin = magasin4, Produit = 531, Quantité = 4 },
            };


            var repertoireTransactions = new Mock<IRepertoireDeTransactions>();
            repertoireTransactions
                .Setup(repertoire => repertoire.ObtenirToutesLesTransactionsALaDateDu(dateDuJour))
                .Returns(transactions);

            var repertoirePrix = new Mock<IRepertoireReferences>();

            MockLitReferentielProduitDuJour(repertoirePrix,
                new[] {
                    (magasin1, new[] { 5.59, 1.50, 0.95, 11.50, 37.60 }),
                    (magasin2, new[] { 5.57, 2.00, 1.10, 10.80, 39.00 }),
                    (magasin3, new[] { 5.54, 1.50, 1.00, 12.00, 37.90 }),
                    (magasin4, new[] { 5.55, 2.10, 1.10, 11.50, 37.60 })
                });

            analyseJournalière = new AnalysePériodique(repertoireTransactions.Object, repertoirePrix.Object, dateDuJour, dateDuJour);
        }

        [Fact]
        public void Obtenir100MeilleursVentesEnGeneral()
        {
            // Arrange
            // Act
            var meilleursVentes = analyseJournalière.Top100VentesGlobal;
            // Assert
            meilleursVentes.Should().HaveCount(5);
            meilleursVentes.ElementAt(0).Produit.Should().Be(531);
            meilleursVentes.ElementAt(0).QuantitéVendue.Should().Be(9);
            meilleursVentes.ElementAt(1).Produit.Should().Be(39);
            meilleursVentes.ElementAt(1).QuantitéVendue.Should().Be(8);
            meilleursVentes.ElementAt(2).Produit.Should().Be(10);
            meilleursVentes.ElementAt(2).QuantitéVendue.Should().Be(6);
            meilleursVentes.ElementAt(3).Produit.Should().Be(55);
            meilleursVentes.ElementAt(3).QuantitéVendue.Should().Be(3);
            meilleursVentes.ElementAt(4).Produit.Should().Be(773);
            meilleursVentes.ElementAt(4).QuantitéVendue.Should().Be(2);
        }

        [Fact]
        public void Obtenir100PlusGrosChiffreDAffaireEnGeneral()
        {
            // Arrange
            // Act
            var plusGrosChiffreDAffaire = analyseJournalière.Top100CAGlobal;
            // Assert
            plusGrosChiffreDAffaire.Should().HaveCount(5);
            plusGrosChiffreDAffaire.ElementAt(0).Produit.Should().Be(773);
            plusGrosChiffreDAffaire.ElementAt(0).ChiffreDAffaire.Should().Be(75.20M);
            plusGrosChiffreDAffaire.ElementAt(1).Produit.Should().Be(10);
            plusGrosChiffreDAffaire.ElementAt(1).ChiffreDAffaire.Should().Be(69M);
            plusGrosChiffreDAffaire.ElementAt(2).Produit.Should().Be(531);
            plusGrosChiffreDAffaire.ElementAt(2).ChiffreDAffaire.Should().Be(50.15M);
            plusGrosChiffreDAffaire.ElementAt(3).Produit.Should().Be(39);
            plusGrosChiffreDAffaire.ElementAt(3).ChiffreDAffaire.Should().Be(8M);
            plusGrosChiffreDAffaire.ElementAt(4).Produit.Should().Be(55);
            plusGrosChiffreDAffaire.ElementAt(4).ChiffreDAffaire.Should().Be(6M);
        }

        [Fact]
        public void Obtenir100MeilleursVenteParMagasin()
        {
            // Arrange
            // Act
            var meilleursVentesParMagasin = analyseJournalière.Top100VentesParMagasin(magasin1);
            // Assert
            meilleursVentesParMagasin.Should().HaveCount(2);
            meilleursVentesParMagasin.ElementAt(0).Produit.Should().Be(531);
            meilleursVentesParMagasin.ElementAt(0).QuantitéVendue.Should().Be(5);
            meilleursVentesParMagasin.ElementAt(1).Produit.Should().Be(773);
            meilleursVentesParMagasin.ElementAt(1).QuantitéVendue.Should().Be(2);
        }


        [Fact]
        public void Obtenir100PlusGrosChiffreDAffaireParMagasin()
        {
            // Arrange

            // Act
            var plusGrosChiffreDAffaire = analyseJournalière.Top100CAParMagasin(magasin1);
            // Assert
            plusGrosChiffreDAffaire.Should().HaveCount(2);
            plusGrosChiffreDAffaire.ElementAt(0).Produit.Should().Be(773);
            plusGrosChiffreDAffaire.ElementAt(0).ChiffreDAffaire.Should().Be(75.20M);
            plusGrosChiffreDAffaire.ElementAt(1).Produit.Should().Be(531);
            plusGrosChiffreDAffaire.ElementAt(1).ChiffreDAffaire.Should().Be(27.95M);
        }

        private void MockLitReferentielProduitDuJour(Mock<IRepertoireReferences> repertoireReferencesMock,
            (Guid magasin, double[] prix)[] referentielMagasin)
        {
            var catalogueDuJour = new CatalogueQuotidien();
            foreach (var referentiel in referentielMagasin)
            {
                catalogueDuJour.DefinirLePrixDe(531, referentiel.prix[0], referentiel.magasin);
                catalogueDuJour.DefinirLePrixDe(55, referentiel.prix[1], referentiel.magasin);
                catalogueDuJour.DefinirLePrixDe(39, referentiel.prix[2], referentiel.magasin);
                catalogueDuJour.DefinirLePrixDe(10, referentiel.prix[3], referentiel.magasin);
                catalogueDuJour.DefinirLePrixDe(773, referentiel.prix[4], referentiel.magasin);
            }

            repertoireReferencesMock
                .Setup(x => x.ObtenirLeCatalogueDuJour(dateDuJour))
                .Returns(catalogueDuJour);
        }

    }
}
