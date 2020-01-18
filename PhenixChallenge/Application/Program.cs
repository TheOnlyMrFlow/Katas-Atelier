using System;
using System.Diagnostics;
using Domaine;
using Parser;

namespace Application
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("here");


            var repertoireDeTransactions = new ScannerDeJourneé(@"C:\Users\comte\Documents\Kata Atelier\PhenixGenerator.0.1\out\transactions");
            var repertoireDePrix = new ScannerDePrix(@"C:\Users\comte\Documents\Kata Atelier\PhenixGenerator.0.1\out\reference");

            var analyseur = new AnalyseurDesVentesProduits(repertoireDeTransactions, repertoireDePrix);

            var logger = new LoggerDanalyseDesVentes(@"C:\Users\comte\Documents\Kata Atelier\PhenixGenerator.0.1\resultats");
            var trenteJanvier2019 = new DateTime(2019, 1, 30);


            var watch = Stopwatch.StartNew();

            analyseur.LancerLAnalyseALaDateDu(trenteJanvier2019, logger);
            
            watch.Stop();

            var millis = watch.ElapsedMilliseconds;
            int secondes = (int) ( millis / 1000);
            int minutes = secondes / 60;
            secondes = secondes % 60;

            Console.WriteLine(minutes + "min " + secondes + "sec");
        }
    }
}
