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
            scannerPrix.FixerLaDate(new DateTime(2019, 1, 4));
            scannerPrix.ObtenirLePrixDe(226, "3a92d362-ce23-2b76-e30d-7a44fa00c30d").Should().Be(681.0);
        }

    }
}
