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
            // 4
            Console.WriteLine("\n--- ZADANIE 4: Wyświetlenie wszystkich studentów (ID, Imię, Nazwisko) ---");
            WyswietlWszystkichStudentow(connection);

            // 5
            Console.WriteLine("\n--- ZADANIE 5 ---");
            WyswietlStudentaPoId(connection, 1);

            // 6 
            Console.WriteLine("\n--- ZADANIE 6 ---");
            var studenci = PobierzStudentowZOcenami(connection);
            WyswietlStudentowZOcenami(studenci);

            // 7
            Console.WriteLine("\n--- ZADANIE 7 ---");
            DodajStudenta(connection, new Student
            {
                Imie = "Adam",
                Nazwisko = "Nowak"
            });
            Console.WriteLine("Dodano studenta.");

            // 8
            Console.WriteLine("\n--- ZADANIE 8 ---");
            DodajOcene(connection, new Ocena
            {
                StudentId = 1,
                Przedmiot = "matematyka",
                Wartosc = 4.5
            });
            Console.WriteLine("Dodano ocenę.");

            // 9
            Console.WriteLine("\n--- ZADANIE 9 ---");
            UsunOcenyZGeografii(connection);
            Console.WriteLine("Usunięto oceny z geografii.");

            // 10 
            Console.WriteLine("\n--- ZADANIE 10 ---");
            AktualizujOcene(connection, 1, 5.0);
            Console.WriteLine("Zaktualizowano ocenę.");
        }
        catch (Exception exc)
        {
            Console.WriteLine("Wystąpił błąd: " + exc);
        }
    }

    
    // 4. 
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
    // 5.  
    public static void WyswietlStudentaPoId(SqlConnection connection, int studentId)
    {
        string sql = "SELECT Imie, Nazwisko FROM Student WHERE StudentId = @id";

        using SqlCommand command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", studentId);

        using SqlDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
            Console.WriteLine($"Student: {reader.GetString(0)} {reader.GetString(1)}");
        }
        else
        {
            Console.WriteLine("Nie znaleziono studenta.");
        }
    }
    // 6.
    public static List<Student> PobierzStudentowZOcenami(SqlConnection connection)
    {
        string sql = @"
        SELECT s.StudentId, s.Imie, s.Nazwisko,
               o.OcenaId, o.Wartosc, o.Przedmiot
        FROM Student s
        LEFT JOIN Ocena o ON s.StudentId = o.StudentId
        ORDER BY s.StudentId";

        using SqlCommand cmd = new SqlCommand(sql, connection);
        using SqlDataReader reader = cmd.ExecuteReader();

        List<Student> studenci = new();

        while (reader.Read())
        {
            int id = reader.GetInt32(0);

            Student student = studenci.FirstOrDefault(s => s.StudentId == id);
            if (student == null)
            {
                student = new Student
                {
                    StudentId = id,
                    Imie = reader.GetString(1),
                    Nazwisko = reader.GetString(2)
                };
                studenci.Add(student);
            }

            if (!reader.IsDBNull(3))
            {
                student.Oceny.Add(new Ocena
                {
                    OcenaId = reader.GetInt32(3),
                    Wartosc = reader.GetDouble(4),
                    Przedmiot = reader.GetString(5),
                    StudentId = id
                });
            }
        }
        return studenci;
    }
    // wypisywanie studentów z ocenami
    public static void WyswietlStudentowZOcenami(List<Student> studenci)
    {
        foreach (var s in studenci)
        {
            Console.WriteLine($"\n{s.StudentId}: {s.Imie} {s.Nazwisko}");
            foreach (var o in s.Oceny)
                Console.WriteLine($"  {o.Przedmiot} – {o.Wartosc}");
        }
    }

    // 7.
    public static void DodajStudenta(SqlConnection connection, Student student)
    {
        string sql = "INSERT INTO Student(Imie, Nazwisko) VALUES (@i, @n)";

        using SqlCommand cmd = new SqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("@i", student.Imie);
        cmd.Parameters.AddWithValue("@n", student.Nazwisko);

        cmd.ExecuteNonQuery();
    }
    // 8.
    public static bool PoprawnaOcena(double ocena)
    {
        return ocena >= 2 && ocena <= 5 &&
               (ocena * 10) % 5 == 0 &&
               ocena != 2.5;
    }

    public static void DodajOcene(SqlConnection connection, Ocena ocena)
    {
        if (!PoprawnaOcena(ocena.Wartosc))
        {
            Console.WriteLine("Niepoprawna ocena!");
            return;
        }

        string sql = @"INSERT INTO Ocena(Wartosc, Przedmiot, StudentId)
                   VALUES (@w, @p, @s)";

        using SqlCommand cmd = new SqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("@w", ocena.Wartosc);
        cmd.Parameters.AddWithValue("@p", ocena.Przedmiot);
        cmd.Parameters.AddWithValue("@s", ocena.StudentId);

        cmd.ExecuteNonQuery();
    }

    // 9.
    public static void UsunOcenyZGeografii(SqlConnection connection)
    {
        string sql = "DELETE FROM Ocena WHERE Przedmiot = 'geografia'";
        using SqlCommand cmd = new SqlCommand(sql, connection);
        cmd.ExecuteNonQuery();
    }

    // 10.
    public static void AktualizujOcene(SqlConnection connection, int ocenaId, double nowaWartosc)
    {
        if (!PoprawnaOcena(nowaWartosc))
        {
            Console.WriteLine("Niepoprawna wartość oceny!");
            return;
        }

        string sql = "UPDATE Ocena SET Wartosc = @w WHERE OcenaId = @id";

        using SqlCommand cmd = new SqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("@w", nowaWartosc);
        cmd.Parameters.AddWithValue("@id", ocenaId);

        cmd.ExecuteNonQuery();
    }
}








