using System;

namespace TexasHoldem.App
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Resultats pour l'input de l'enonce du kata");
            Console.WriteLine("*******************************************************");
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
            Console.WriteLine("*******************************************************");

            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Resultats pour une plus grande partie");
            Console.WriteLine("*******************************************************");
            Console.WriteLine(
            new Round()
                .AddPlayer("Kc 9s Ks Kd 6d 3c 9d")
                .AddPlayer("9c Ah Ks Kd 9d 3c 6d")
                .AddPlayer("Ts Kc Ks Kd Th 3c 6d")
                .AddPlayer("Ac Qc Ks Kd 9d 3c")
                .AddPlayer("9h 5s")
                .AddPlayer("4d 2d Ks Kd 9d 3c 6d")
                .AddPlayer("7s Ts Ks Kd 9d")
                .AddPlayer("Qc Qh Qs Qd 9d 9h 7s")
                .AddPlayer("Kc Kh Ks Kd 6d 3c 9d")
                .ShowResult()
                );
            Console.WriteLine("*******************************************************");
        }
    }
}
