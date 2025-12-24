using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;

class Program
{
    static void Main()
    {
        Zadanie2();
        Zadanie3();
        Zadanie4();

        ZapisJson();
        OdczytJson();

        ZapisXml();
        OdczytXml();

        OdczytCsv();
        SrednieCsv();
        FiltrowanieCsv();
    }

   // 2.
    static void Zadanie2()
    {
        List<string> wpisy = new List<string>();

        Console.WriteLine("Ile linii tekstu chcesz wprowadzić?");
        int ile = int.Parse(Console.ReadLine());

        for (int i = 0; i < ile; i++)
        {
            Console.Write($"Podaj tekst {i + 1}: ");
            string linia = Console.ReadLine();
            wpisy.Add(linia);
        }

        string sciezka = "dane_uzytkownika.txt";

        File.WriteAllLines(sciezka, wpisy);

        Console.WriteLine($"\nDane zapisano do pliku: {sciezka}");
    }

    // 3.
    static void Zadanie3()
    {
        string sciezka = "dane_uzytkownika.txt";

        if (!File.Exists(sciezka))
        {
            Console.WriteLine("Plik nie istnieje. Najpierw wykonaj zadanie 2.");
            return;
        }

        string[] linie = File.ReadAllLines(sciezka);

        Console.WriteLine("\nZawartość pliku:");

        foreach (string linia in linie)
        {
            Console.WriteLine(linia);
        }
    }

    // 4.
    static void Zadanie4()
    {
        Console.Write("Ile linii dopisać? ");
        int ile = int.Parse(Console.ReadLine());

        using StreamWriter sw = new("dane_uzytkownika.txt", true);
        for (int i = 0; i < ile; i++)
            sw.WriteLine(Console.ReadLine());
    }
    // 5–7.
    static void ZapisJson()
    {
        List<Student> studenci = new()
        {
            new Student { Imie="Jan", Nazwisko="Kowalski", Oceny=new(){3,4,5}},
            new Student { Imie="Anna", Nazwisko="Nowak", Oceny=new(){5,5,4}}
        };

        string json = JsonSerializer.Serialize(studenci);
        File.WriteAllText("studenci.json", json);
    }

    static void OdczytJson()
    {
        var json = File.ReadAllText("studenci.json");
        var lista = JsonSerializer.Deserialize<List<Student>>(json);

        foreach (var s in lista)
            Console.WriteLine($"{s.Imie} {s.Nazwisko}: {string.Join(", ", s.Oceny)}");
    }

    // 8–9.
    static void ZapisXml()
    {
        List<Student> studenci = new()
        {
            new Student { Imie="Piotr", Nazwisko="Zieliński", Oceny=new(){4,4,5}}
        };

        XmlSerializer xs = new(typeof(List<Student>));
        using FileStream fs = new("studenci.xml", FileMode.Create);
        xs.Serialize(fs, studenci);
    }

    static void OdczytXml()
    {
        XmlSerializer xs = new(typeof(List<Student>));
        using FileStream fs = new("studenci.xml", FileMode.Open);
        var lista = (List<Student>)xs.Deserialize(fs);

        foreach (var s in lista)
            Console.WriteLine($"{s.Imie} {s.Nazwisko}: {string.Join(", ", s.Oceny)}");
    }

    // 10.
    static void OdczytCsv()
    {
        var linie = File.ReadAllLines("iris.csv");
        foreach (var linia in linie)
            Console.WriteLine(linia);
    }

    // 11.
    static void SrednieCsv()
    {
        var dane = File.ReadAllLines("iris.csv")
            .Skip(1)
            .Select(l => l.Split(','))
            .ToList();

        for (int i = 0; i < 4; i++)
        {
            double avg = dane.Average(d =>
                double.Parse(d[i], CultureInfo.InvariantCulture));
            Console.WriteLine($"Średnia kolumny {i + 1}: {avg}");
        }
    }
    // 12.
    static void FiltrowanieCsv()
    {
        var linie = File.ReadAllLines("iris.csv");
        List<string> wynik = new() { "sepal_length,sepal_width,class" };

        foreach (var l in linie.Skip(1))
        {
            var c = l.Split(',');
            double sepalLength =
                double.Parse(c[0], CultureInfo.InvariantCulture);

            if (sepalLength < 5)
                wynik.Add($"{c[0]},{c[1]},{c[4]}");
        }

        File.WriteAllLines("iris_filtered.csv", wynik);
    }
}
// 5. 
public class Student
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public List<int> Oceny { get; set; }

        public Student()
        {
            Oceny = new List<int>();
        }
    }