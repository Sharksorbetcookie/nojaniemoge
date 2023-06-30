using System;

public class Osoba
{
    private string imie;
    private string nazwisko;
    public DateTime? DataUrodzenia { get; set; }
    public DateTime? DataSmierci { get; set; }

    public string Imie
    {
        get { return imie; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Imię nie może być puste.");
            imie = value;
        }
    }

    public string Nazwisko
    {
        get { return nazwisko; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Nazwisko nie może być puste.");
            nazwisko = value;
        }
    }

    public string ImieNazwisko
    {
        get { return $"{imie} {nazwisko}"; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                imie = string.Empty;
                nazwisko = string.Empty;
            }
            else
            {
                string[] parts = value.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length > 0)
                    imie = parts[0];
                else
                    imie = string.Empty;

                if (parts.Length > 1)
                    nazwisko = parts[1];
                else
                    nazwisko = string.Empty;
            }
        }
    }

    public int? Wiek
    {
        get
        {
            if (DataUrodzenia.HasValue)
            {
                DateTime endDate = DataSmierci ?? DateTime.Now;
                TimeSpan roznica = endDate.Subtract(DataUrodzenia.Value);
                int wiek = (int)Math.Floor(roznica.TotalDays / 365.25);
                return wiek;
            }
            return null;
        }
    }

    public Osoba(string imie, string nazwisko)
    {
        Imie = imie;
        Nazwisko = nazwisko;
    }

    public static void Main()
    {
        Console.WriteLine("Podaj imię: ");
        string imie = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(imie))
        {
            Console.WriteLine("Imię nie może być puste.");
            return;
        }

        Console.WriteLine("Podaj nazwisko: ");
        string nazwisko = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(nazwisko))
        {
            Console.WriteLine("Nazwisko nie może być puste.");
            return;
        }

        Osoba osoba = new Osoba(imie, nazwisko);

        Console.WriteLine("Podaj datę urodzenia (RRRR-MM-DD): ");
        string dataUrodzeniaStr = Console.ReadLine();
        DateTime dataUrodzenia;
        if (!DateTime.TryParse(dataUrodzeniaStr, out dataUrodzenia))
        {
            Console.WriteLine("Niepoprawny format daty urodzenia.");
            return;
        }
        osoba.DataUrodzenia = dataUrodzenia;

        Console.WriteLine("Czy osoba zmarła? (T/N): ");
        string czyZmarlaStr = Console.ReadLine();
        if (czyZmarlaStr.ToLower() == "t")
        {
            Console.WriteLine("Podaj datę śmierci (RRRR-MM-DD): ");
            string dataSmierciStr = Console.ReadLine();
            DateTime dataSmierci;
            if (!DateTime.TryParse(dataSmierciStr, out dataSmierci))
            {
                Console.WriteLine("Niepoprawny format daty śmierci.");
                return;
            }
            osoba.DataSmierci = dataSmierci;
        }

        Console.WriteLine($"Imię: {osoba.Imie}");
        Console.WriteLine($"Nazwisko: {osoba.Nazwisko}");
        Console.WriteLine($"Wiek: {osoba.Wiek} lat");
    }
}
