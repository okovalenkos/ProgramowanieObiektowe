using System;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("To jest cwiczenie 1");

        Zwierze z1 = new Zwierze();
        Zwierze z2 = new Zwierze("Mruczek", "Kot", 4);
        Zwierze z3 = new Zwierze(z2);

        z1.daj_glos();
        z2.daj_glos();
        z3.daj_glos();

        Console.WriteLine($"Liczba zwierząt: {Zwierze.LiczbaZwierzat()}");
    }
}

public class Zwierze
{
    private string nazwa;
    private string gatunek;
    private int liczbaNog;

    private static int liczbaZwierzat = 0;

 
    public string GetNazwa() { return nazwa; }

   
    public string GetGatunek() { return gatunek; }

   
    public int GetLiczbaNog() { return liczbaNog; }

    
    public void SetNazwa(string nowaNazwa)
    {
        nazwa = nowaNazwa;
    }

  
    public Zwierze()
    {
        nazwa = "Rex";
        gatunek = "Pies";
        liczbaNog = 4;
        liczbaZwierzat++;
    }

    
    public Zwierze(string nazwa, string gatunek, int liczbaNog)
    {
        this.nazwa = nazwa;
        this.gatunek = gatunek;
        this.liczbaNog = liczbaNog;
        liczbaZwierzat++;
    }

  
    public Zwierze(Zwierze inne)
    {
        nazwa = inne.nazwa;
        gatunek = inne.gatunek;
        liczbaNog = inne.liczbaNog;
        liczbaZwierzat++;
    }

   
    public void daj_glos()
    {
        switch (gatunek.ToLower())
        {
            case "pies":
                Console.WriteLine("Hau!");
                break;
            case "kot":
                Console.WriteLine("Miau!");
                break;
            case "krowa":
                Console.WriteLine("Muuu!");
                break;
            default:
                Console.WriteLine("Nieznany dźwięk...");
                break;
        }
    }

  
    public static int LiczbaZwierzat()
    {
        return liczbaZwierzat;
    }
}