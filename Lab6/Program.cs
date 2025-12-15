using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;

public class Student
{
    public int StudentId { get; set; }
    public string Imie { get; set; } = "";
    public string Nazwisko { get; set; } = "";
    public List<Ocena> Oceny { get; set; } = new();
}
public class Ocena
{
    public int OcenaId { get; set; }
    public double Wartosc { get; set; }
    public string Przedmiot { get; set; } = "";
    public int StudentId { get; set; }
}
public class Program
{

    public static void Main()
    {
        string connectionString =
        "Data Source=10.200.2.28;" + //"(LocalDb)\\MSSQLLocalDB;" - dla lokalnej bazy
        "Initial Catalog=studenci_71452;" +
       "Integrated Security=True;" +
        "Encrypt=True;" +
        "TrustServerCertificate=True";
        try
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            Console.WriteLine("Połączono z bazą.");

            Console.WriteLine("\n--- ZADANIE 4: Wyświetlenie wszystkich studentów (ID, Imię, Nazwisko) ---");
            WyswietlWszystkichStudentow(connection);

        }
        catch (Exception exc)
        {
            Console.WriteLine("Wystąpił błąd: " + exc);
        }
    }

    
    // 4. Przygotuj funkcję, która wyświetli w kolejnych wierszach wynik zapytania
    // zwracającego informacje z tabeli Student (identyfikator, imię, nazwisko)
    public static void WyswietlWszystkichStudentow(SqlConnection connection)
    {
        string sql = "SELECT StudentId, Imie, Nazwisko FROM Student";
        using SqlCommand command = new SqlCommand(sql, connection);

        using SqlDataReader reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            Console.WriteLine("Brak studentów w bazie.");
            return;
        }

        while (reader.Read())
        {
            int id = reader.GetInt32("studentid");
            string imie = reader.GetString("imie");
            string nazwisko = reader.GetString("nazwisko");
            Console.WriteLine($"ID: {id}, Imię: {imie}, Nazwisko: {nazwisko}");
        }
    }

}








