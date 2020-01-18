using System;
using System.Collections.Generic;
using System.Text;

namespace DataGenerator
{
    class RepertoireMagasins
    {

        private Random random = new Random();

        private List<string> magasins = new List<string>();
        public RepertoireMagasins(int nombreDeMagasins)
        {
            for (int i = 0; i < nombreDeMagasins; i++)
            {
                magasins.Add(System.Guid.NewGuid().ToString());
            }
        }

        public string TirerUnMagasinAuHasard()
        {
            return magasins[random.Next(magasins.Count)];
        }

    }
}
