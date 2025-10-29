using System;

// 1. Klasa Zwierze
public class Zwierze
{
    protected string nazwa;

    public Zwierze(string nazwa)
    {
        this.nazwa = nazwa;
    }

    public virtual void daj_glos()
    {
        Console.WriteLine("...");
    }
}

// 2. Klasa Pies
public class Pies : Zwierze
{
    public Pies(string nazwa) : base(nazwa) { }

    public override void daj_glos()
    {
        Console.WriteLine($"{nazwa} robi woof woof!");
    }
}

// 3. Klasa Kot
public class Kot : Zwierze
{
    public Kot(string nazwa) : base(nazwa) { }

    public override void daj_glos()
    {
        Console.WriteLine($"{nazwa} robi miau miau!");
    }
}

// 4. Klasa Waz
public class Waz : Zwierze
{
    public Waz(string nazwa) : base(nazwa) { }

    public override void daj_glos()
    {
        Console.WriteLine($"{nazwa} robi ssssssss!");
    }
}

// 6. Metoda globalna powiedz_cos()
public static class Pomocnik
{
    public static void powiedz_cos(Zwierze z)
    {
        z.daj_glos();
        Console.WriteLine($"Typ obiektu: {z.GetType().Name}");
    }
}

// 8. Klasa abstrakcyjna Pracownik
public abstract class Pracownik
{
    public abstract void Pracuj();
}

// 9. Klasa Piekarz
public class Piekarz : Pracownik
{
    public override void Pracuj()
    {
        Console.WriteLine("Trwa pieczenie...");
    }
}

// 12. Klasa A
public class A
{
    public A()
    {
        Console.WriteLine("To jest konstruktor A");
    }
}

// 13. Klasa B
public class B : A
{
    public B() : base()
    {
        Console.WriteLine("To jest konstruktor B");
    }
}

// 14. Klasa C
public class C : B
{
    public C() : base()
    {
        Console.WriteLine("To jest konstruktor C");
    }
}

// 7, 10, 11, 15. Program główny
public class Program
{
    public static void Main()
    {
        Console.WriteLine("=== ZWIERZĘTA ===");
        Zwierze z1 = new Zwierze("Zwierzę");
        Pies p1 = new Pies("Reksio");
        Kot k1 = new Kot("Mruczek");
        Waz w1 = new Waz("Python");

        Pomocnik.powiedz_cos(z1);
        Pomocnik.powiedz_cos(p1);
        Pomocnik.powiedz_cos(k1);
        Pomocnik.powiedz_cos(w1);

        Console.WriteLine("\n=== PRACOWNICY ===");
        Piekarz piekarz = new Piekarz();
        piekarz.Pracuj();

        // Pracownik p = new Pracownik(); // ❌ Błąd kompilacji: nie można utworzyć obiektu klasy abstrakcyjnej

        Console.WriteLine("\n=== KONSTRUKTORY ===");
        A a = new A();
        B b = new B();
        C c = new C();
    }
}