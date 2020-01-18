using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaine
{

    public class AnalyseurDesVentesProduits
    {
        IRepertoireDeTransactions repertoireDeTransactions;
        IRepertoireReferences repertoireDeReferences;

        private ConcurrentDictionary<uint, StatistiqueProduit> produitVersStatistiques = new ConcurrentDictionary<uint, StatistiqueProduit>();
        private readonly object _lockProduitVersStatistiques = new object();
        private ConcurrentDictionary<string, StatistiqueMagasin> magasinVersStatistiques = new ConcurrentDictionary<string, StatistiqueMagasin>();
        private readonly object _lockMagasinVersStatistiques = new object();

        public AnalyseurDesVentesProduits(IRepertoireDeTransactions repertoireDeTransactions, IRepertoireReferences repertoireDeReferences)
        {
            this.repertoireDeTransactions = repertoireDeTransactions;
            this.repertoireDeReferences = repertoireDeReferences;
        }

        public void LancerLAnalyseALaDateDu(DateTime date, ILoggerDanalyseDesVentes logger)
        {
            DateTime dateDeLAnalyse = date;

            var sw = Stopwatch.StartNew();

            for (int jour = 1; jour <= 30; jour++, date.AddDays(-1))
            {
                repertoireDeReferences.FixerLaDate(date);

                Parallel.ForEach(repertoireDeTransactions.ObtenirToutesLesTransactionsALaDateDu(date), transaction =>
                {
                    var idProduit = transaction.idProduit;
                    var quantite = transaction.quantité;
                    var uuidMagasin = transaction.uuidMagasin;

                    var prixProduit = repertoireDeReferences.ObtenirLePrixDe(transaction.idProduit, transaction.uuidMagasin);
                    var recette = quantite * prixProduit;


                    StatistiqueProduit statistiquesDuProduit;
                    lock (_lockProduitVersStatistiques)
                    {
                        if (produitVersStatistiques.ContainsKey(idProduit))
                        {
                            statistiquesDuProduit = produitVersStatistiques[idProduit];
                        }
                        else
                        {
                            statistiquesDuProduit = new StatistiqueProduit(idProduit);
                            produitVersStatistiques[idProduit] = statistiquesDuProduit;
                        }
                    }
                    statistiquesDuProduit.AjouterQuantite(quantite);
                    statistiquesDuProduit.AjouterChiffreDAffaire(recette);


                    StatistiqueMagasin statistiquesDuMagasin;
                    lock (_lockMagasinVersStatistiques)
                    {
                        if (magasinVersStatistiques.ContainsKey(uuidMagasin))
                        {
                            statistiquesDuMagasin = magasinVersStatistiques[uuidMagasin];
                        }
                        else
                        {
                            statistiquesDuMagasin = new StatistiqueMagasin(uuidMagasin);
                            magasinVersStatistiques[uuidMagasin] = statistiquesDuMagasin;
                        }
                    }
                    statistiquesDuMagasin.AjouterQuantiteEtChiffreDaffaireAuProduit(idProduit, quantite, recette);

                });

                if (jour == 1 | jour == 7 | jour == 30)
                {
                    foreach (StatistiqueMagasin statMagasin in magasinVersStatistiques.Values)
                    {
                        logger.loggerTop100Ventes(statMagasin.UidMagasin, dateDeLAnalyse, statMagasin.Top100Ventes(), jour);
                        logger.loggerTop100CA(statMagasin.UidMagasin, dateDeLAnalyse, statMagasin.Top100CA(), jour);
                    }

                    logger.LoggerTop100VentesGlobal(dateDeLAnalyse, Top100VentesGlobal(), jour);
                    logger.LoggerTop100CAGlobal(dateDeLAnalyse, Top100CAGlobal(), jour);
                }

                Console.WriteLine(jour);

            }
        }

        private IEnumerable<uint> Top100VentesGlobal()
        {
            return produitVersStatistiques
                .Values
                .OrderByDescending(stat => stat.QuantiteVendue)
                .Take(100)
                .Select(stat => stat.IdProduit);
        }

        private IEnumerable<uint> Top100CAGlobal()
        {
            return produitVersStatistiques
                .Values
                .OrderByDescending(stat => stat.ChiffreDaffaire)
                .Take(100)
                .Select(stat => stat.IdProduit);
        }
    }
}
