using System;

namespace TexasHoldem.App
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine(
            new Round()
                .AddPlayer("Kc 9s Ks Kd 9d 3c 6d")
                .AddPlayer("9c Ah Ks Kd 9d 3c 6d")
                .AddPlayer("Ac Qc Ks Kd 9d 3c")
                .AddPlayer("9h 5s")
                .AddPlayer("4d 2d Ks Kd 9d 3c 6d")
                .AddPlayer("7s Ts Ks Kd 9d")
                .ShowResult()
                );
        }
    }
}
