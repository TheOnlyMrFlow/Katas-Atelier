using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaine
{
    public class AnalysePériodique
    {
        IRepertoireDeTransactions repertoireDeTransactions;
        IRepertoireReferences repertoireDeReferences;
        private DateTime début;
        private DateTime fin;

        
        private ConcurrentDictionary<Guid, StatistiqueMagasin> magasinVersStatistiques = new ConcurrentDictionary<Guid, StatistiqueMagasin>();
        private readonly object _lockMagasinVersStatistiques = new object();

        public AnalysePériodique(IRepertoireDeTransactions repertoireDeTransactions, IRepertoireReferences repertoireDeReferences, DateTime debutCompris, DateTime finComprise)
        {
            this.repertoireDeTransactions = repertoireDeTransactions;
            this.repertoireDeReferences = repertoireDeReferences;
            this.début = debutCompris;
            this.fin = finComprise;
            LancerLAnalyse();
        }



        private void LancerLAnalyse()
        {

        ConcurrentDictionary<uint, StatistiqueProduit> produitVersStatistiques = new ConcurrentDictionary<uint, StatistiqueProduit>();
        object _lockProduitVersStatistiques = new object();

        var duréeEnJours = (fin - début).Days + 1;
            var jours =
                Enumerable.Range(0, duréeEnJours)
                .Select(offset => début.AddDays(offset));

            //foreach (var jour in jours)
            Parallel.ForEach(jours, jour =>
            {
                var transactionsDuJours = repertoireDeTransactions.ObtenirToutesLesTransactionsALaDateDu(jour);
                var catalogue = repertoireDeReferences.ObtenirLeCatalogueDuJour(jour);

                Parallel.ForEach(transactionsDuJours, transaction =>
                {
                    var idProduit = transaction.Produit;
                    var quantite = transaction.Quantité;
                    var uuidMagasin = transaction.Magasin;

                    var prixProduit = catalogue.ObtenirLePrixDe(transaction.Produit, transaction.Magasin);
                    var recette = (decimal)(quantite * prixProduit);


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
            });

            Top100VentesGlobal = 
                produitVersStatistiques
                .Values
                .OrderByDescending(stat => stat.QuantitéVendue)
                .Take(100);

            Top100CAGlobal = produitVersStatistiques
                .Values
                .OrderByDescending(stat => stat.ChiffreDAffaire)
                .Take(100);

        }

        public IEnumerable<StatistiqueProduit> Top100VentesGlobal { get; private set; }
        public IEnumerable<StatistiqueProduit> Top100CAGlobal { get; private set; }

        public IEnumerable<StatistiqueProduit> Top100VentesParMagasin(Guid magasin)
        {
            return magasinVersStatistiques[magasin].Top100Ventes();
        }

        public IEnumerable<StatistiqueProduit> Top100CAParMagasin(Guid magasin)
        {
            return magasinVersStatistiques[magasin].Top100CA();
        }

    }
}
