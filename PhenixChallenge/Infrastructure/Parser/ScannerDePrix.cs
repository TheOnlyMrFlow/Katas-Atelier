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

        private DateTime date;

        private Dictionary<string, Dictionary<uint, double>> cataloguesPrixParMagasin = new Dictionary<string, Dictionary<uint, double>>();

        public ScannerDePrix(string dossierDesReferences)
        {
            this.dossierDesReferences = dossierDesReferences;
        }

        public void FixerLaDate(DateTime date)
        {
            var dateIso = date.ToString("yyyyMMdd");

            foreach (var fichierReference in Directory.EnumerateFiles(dossierDesReferences, "*" + dateIso + ".data"))
            {
                var nomFichier = Path.GetFileName(fichierReference);
                var uuidMagasin = new string(nomFichier.SkipWhile(c => c != '-').Skip(1).TakeWhile(c => c != '_').ToArray());

                var cataloguePrix = new Dictionary<uint, double>();
                cataloguesPrixParMagasin[uuidMagasin] = cataloguePrix;

                foreach (var ligne in File.ReadAllLines(fichierReference))
                {
                    var donnéesDeLaLigne = ligne.Split("|");
                    var idProduit = uint.Parse(donnéesDeLaLigne[0]);
                    var prix = double.Parse(donnéesDeLaLigne[1]);
                    cataloguePrix.Add(idProduit, prix);
                }

            }
        }

        public double ObtenirLePrixDe(uint idProduit, string uidMagasin)
        {
            //var nomDuFichier = FabriquerNomDuFichierReference(idProduit, uidMagasin, date);
            //var cheminFichier = Path.Join(dossierDesReferences, nomDuFichier);
            //string entry = File.ReadLines(cheminFichier).Skip((int) idProduit).Take(1).First();
            //var price = entry.Split("|")[1];
            //return Double.Parse(price);

            if (date == null)
                throw new Exception("Il faut d'abord fixer la date");

            return cataloguesPrixParMagasin[uidMagasin][idProduit];

        }

        private string FabriquerNomDuFichierReference(uint idProduit, string uidMagasin, DateTime date)
        {
            return
               new StringBuilder("reference_prod-")
               .Append(uidMagasin)
               .Append("_")
               .Append(date.ToString("yyyyMMdd"))
               .Append(".data")
               .ToString();
        }

    }
}
