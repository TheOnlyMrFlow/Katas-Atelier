using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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


            var logger = new LoggerDanalyseDesVentes(@"C:\Users\comte\Documents\Kata Atelier\PhenixGenerator.0.1\resultats");
            var debut = new DateTime(2019, 1, 1);

            var watch = Stopwatch.StartNew();


            var analyseQuotidienne = new AnalysePériodique(repertoireDeTransactions, repertoireDePrix, debut, debut);

            Console.WriteLine("L'analyse quotidienne a duré " + MillisToPrettyTime(watch.ElapsedMilliseconds));

            var analyseHebdomadaire = new AnalysePériodique(repertoireDeTransactions, repertoireDePrix, debut, debut.AddDays(6));

            Console.WriteLine("L'analyse hebdo a duré " + MillisToPrettyTime(watch.ElapsedMilliseconds));

            var analyseMensuel = new AnalysePériodique(repertoireDeTransactions, repertoireDePrix, debut, debut.AddDays(29));

            Console.WriteLine("L'analyse mensuel a duré " + MillisToPrettyTime(watch.ElapsedMilliseconds));


            watch.Stop();

        }

        private static string MillisToPrettyTime(long millis)
        {
            int secondes = (int)(millis / 1000);
            int minutes = secondes / 60;
            secondes = secondes % 60;

            return minutes + "min " + secondes + "sec";
        }
    }
}
