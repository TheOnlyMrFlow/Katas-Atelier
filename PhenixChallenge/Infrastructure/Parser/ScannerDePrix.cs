using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Domaine;


namespace Parser
{
    public class ScannerDePrix : IRepertoireReferences
    {
        private string dossierDesReferences;
        public ScannerDePrix(string dossierDesReferences)
        {
            this.dossierDesReferences = dossierDesReferences;
        }

        public CatalogueQuotidien ObtenirLeCatalogueDuJour(DateTime date)
        {
            var catalogue = new CatalogueQuotidien();

            var dateIso = date.ToString("yyyyMMdd");


            foreach (var fichierReference in Directory.EnumerateFiles(dossierDesReferences, "*" + dateIso + ".data"))
            {
                var nomFichier = Path.GetFileName(fichierReference);
                var magasin = 
                    new Guid(
                        new string(
                            nomFichier.SkipWhile(c => c != '-').Skip(1).TakeWhile(c => c != '_').ToArray()
                            )
                        );

                foreach (var ligne in File.ReadAllLines(fichierReference))
                {
                    var donnéesDeLaLigne = ligne.Split("|");
                    var idProduit = uint.Parse(donnéesDeLaLigne[0]);
                    var prix = double.Parse(donnéesDeLaLigne[1]);
                    catalogue.DefinirLePrixDe(idProduit, prix, magasin);
                }

            }

            return catalogue;
        }

    }
}
