using FluentAssertions;
using System;
using Xunit;

namespace Parser.Test
{
    public class TestScannerPrix
    {
        ScannerDePrix scannerPrix = new ScannerDePrix(@"C:\Users\comte\Documents\Kata Atelier\PhenixGenerator.0.1\out\reference");

   
        [Fact]
        public void le_scanner_trouve_le_bon_prix()
        {
            var catalogue = scannerPrix.ObtenirLeCatalogueDuJour(new DateTime(2019, 1, 4));
            catalogue.ObtenirLePrixDe(226, new Guid("3a92d362-ce23-2b76-e30d-7a44fa00c30d")).Should().Be(681.0);
        }

    }
}
