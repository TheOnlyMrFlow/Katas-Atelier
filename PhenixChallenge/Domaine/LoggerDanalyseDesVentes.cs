using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Domaine
{
    public class LoggerDanalyseDesVentes : ILoggerDanalyseDesVentes
    {

        private enum Localité
        {
            Globale,
            Magasin
        }

        private enum Periode
        {
            Jour = 1,
            Semaine = 7,
            Mois = 30
        }

        private enum Critere
        {
            ChiffreAffaire,
            Ventes
        }

        private string cheminDossierResultats;

        public LoggerDanalyseDesVentes(string cheminDossierResultats)
        {
            this.cheminDossierResultats = cheminDossierResultats;
        }
        public void loggerTop100CA(string magasin, DateTime date, IEnumerable<uint> produitsParOrdreDecroissant, int periode)
        {
            var cheminFichier = Path.Join(cheminDossierResultats, ConstruireNomDuFichier(Localité.Magasin, (Periode)periode, Critere.ChiffreAffaire, date, magasin));

            File.Delete(cheminFichier);
            using (StreamWriter swriter = File.AppendText(cheminFichier))
            {
                foreach (var produit in produitsParOrdreDecroissant)
                    swriter.WriteLine(produit);
            }
        }

        public void LoggerTop100CAGlobal(DateTime date, IEnumerable<uint> produitsParOrdreDecroissant, int periode)
        {
            var cheminFichier = Path.Join(cheminDossierResultats, ConstruireNomDuFichier(Localité.Globale, (Periode)periode, Critere.ChiffreAffaire, date, null));

            File.Delete(cheminFichier);
            using (StreamWriter swriter = File.AppendText(cheminFichier))
            {
                foreach (var produit in produitsParOrdreDecroissant)
                    swriter.WriteLine(produit);
            }
        }

        public void loggerTop100Ventes(string magasin, DateTime date, IEnumerable<uint> produitsParOrdreDecroissant, int periode)
        {

            var cheminFichier = Path.Join(cheminDossierResultats, ConstruireNomDuFichier(Localité.Magasin, (Periode) periode, Critere.Ventes, date, magasin));

            File.Delete(cheminFichier);
            using (StreamWriter swriter = File.AppendText(cheminFichier)) {
                foreach(var produit in produitsParOrdreDecroissant)
                    swriter.WriteLine(produit);
            }


        }

        public void LoggerTop100VentesGlobal(DateTime date, IEnumerable<uint> produitsParOrdreDecroissant, int periode)
        {
            var cheminFichier = Path.Join(cheminDossierResultats, ConstruireNomDuFichier(Localité.Globale, (Periode)periode, Critere.Ventes, date, null));

            File.Delete(cheminFichier);
            using (StreamWriter swriter = File.AppendText(cheminFichier)) {
                foreach (var produit in produitsParOrdreDecroissant)
                    swriter.WriteLine(produit);
            }
        }

        private string ConstruireNomDuFichier(Localité localité, Periode periode, Critere critere, DateTime date, string magasin)
        {
            return new StringBuilder()
            .Append(critere == Critere.ChiffreAffaire ? "top_100_ca_" : "top_100_ventes_")
            .Append(localité == Localité.Globale ? "GLOBAL" : magasin)
            .Append("_")
            .Append(date.ToString("yyyyMMdd"))
            .Append(periode == Periode.Jour ? "" : "-J" + (int) periode)
            .Append(".data")
            .ToString();
        }
    }
}
