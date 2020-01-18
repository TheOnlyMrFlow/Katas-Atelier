using System;
using System.IO;

namespace DataGenerator
{
    class Program
    {

        static int nombreProduits = 120;

        static Random random = new Random();

        static int TirerProduitAuHasard()
        {
            return random.Next(0, nombreProduits);
        }

        static double GenererUnPrix()
        {
            return .1 + 100 * random.NextDouble();
        }

        static int GenererUneQuantite()
        {
            return random.Next(1, 10);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            RepertoireMagasins repertoireMagasins = new RepertoireMagasins(10);

            var outputDir = @"C:\Users\comte\Documents\Kata Atelier\PhenixGenerator.0.1\generated";
            Directory.CreateDirectory(outputDir);

            DateTime d = new DateTime();

            for (int j = 0; j < 30; j++, d.AddDays(1))
            {
                var magasin = repertoireMagasins.TirerUnMagasinAuHasard();
                var produit = TirerProduitAuHasard();
                var prix = GenererUnPrix();
                var dateISO = String.Format("{0, G}", d);

            }

        }


    }
}
