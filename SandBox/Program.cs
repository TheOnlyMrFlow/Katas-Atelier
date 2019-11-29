using System;

namespace SandBox
{
    class Program
    {
        static void Main(string[] args)
        {
            Base b = new Base();
            Derived d = new Derived();
            Base d2 = new Derived();

            b.CompareTo(d);
            b.CompareTo(d2);
            d2.CompareTo(d);
            d.CompareTo(d2);

        }
    }

    class Base : IComparable<Base>
    {
        public int CompareTo(Base other)
        {
            //if (typeof(Derived).IsAssignableFrom(other.GetType()) && typeof(Derived).IsAssignableFrom(this.GetType()))
            //    return ((Derived)this).CompareTo((Derived)other);

                Console.WriteLine("Base");
            return 1;
        }
    }

    class Derived : Base, IComparable<Base>, IComparable<Derived>
    {

        public int CompareTo(Base other)
        {
            Console.WriteLine("Derived - based ");
            return 1;
        }

        public int CompareTo(Derived other)
        {
            Console.WriteLine("Derived");
            return 1;
        }
    }
}
