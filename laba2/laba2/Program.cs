using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Text.Json;

    public class Zapis
{
    [Column("kod")]
    public int id { get; set; }

    public Klient klient { get; set; }

    public int? tovar { get; set; }

    public int? sotrudnik { get; set; }

    public int? usluga { get; set; }

    public int? data { get; set; }

    public int? adressalona { get; set; }
   

    public Zapis(int id, Klient klient, int tovar, int sotrudnik, int usluga, int data, int adressalona)
    {
        this.id = id;
        this.klient = klient;
        this.tovar = tovar;
        this.sotrudnik = sotrudnik;
        this.data = data;
        this.adressalona = adressalona;
    }
}
    public class Klient
{
    [Key]
    public int kod { get; set; }

    public string familiya { get; set; }

    public string imya { get; set; }

    public string otchestvo { get; set; }

    public int? telefon { get; set; }

    public string adres { get; set; }


    public Klient(int kod, string familiya, string imya, string otchestvo, int telefon, string adres)
    {
        this.kod = kod;
        this.familiya = familiya;
        this.imya = imya;
        this.otchestvo = otchestvo;
        this.telefon = telefon;
        this.adres = adres;
    }
}



public class JsonHandler<T> where T : class
{
    private string fileName;
    JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };


    public JsonHandler() { }

    public JsonHandler(string fileName)
    {
        this.fileName = fileName;
    }


    public void SetFileName(string fileName)
    {
        this.fileName = fileName;
    }

    public void Write(List<T> list)
    {
        string jsonString = JsonSerializer.Serialize(list, options);

        if (new FileInfo(fileName).Length == 0)
        {
            File.WriteAllText(fileName, jsonString);
        }
        else
        {
            Console.WriteLine("Specified path file is not empty");
        }
    }

    public void Delete()
    {
        File.WriteAllText(fileName, string.Empty);
    }

    public void Rewrite(List<T> list)
    {
        string jsonString = JsonSerializer.Serialize(list, options);

        File.WriteAllText(fileName, jsonString);
    }

    public void Read(ref List<T> list)
    {
        if (File.Exists(fileName))
        {
            if (new FileInfo(fileName).Length != 0)
            {
                string jsonString = File.ReadAllText(fileName);
                list = JsonSerializer.Deserialize<List<T>>(jsonString);
            }
            else
            {
                Console.WriteLine("Specified path file is empty");
            }
        }
    }

    public void OutputJsonContents()
    {
        string jsonString = File.ReadAllText(fileName);

        Console.WriteLine(jsonString);
    }

    public void OutputSerializedList(List<T> list)
    {
        Console.WriteLine(JsonSerializer.Serialize(list, options));
    }
}



class Program
{
    static void Main(string[] args)
    {
        List<Zapis> partsList = new List<Zapis>();

        JsonHandler<Zapis> partsHandler = new JsonHandler<Zapis>("partsFile.json");

        partsList.Add(new Zapis(1, new Klient(1, " Petrov","Ivan","Petrovich", 89898989,"Novoselev" ),1 ,1 ,1 ,  21 ,1 ));
        partsList.Add(new Zapis(2, new Klient(2, " Zlobin", "Zhora", "Petrovich", 75377525, "Noviy"), 1, 1, 1, 23, 1));
        partsList.Add(new Zapis(2, new Klient(3, " Komarov", "Igor", "Petrovich", 998939298, "Solmnaya"), 1, 1, 1, 26, 1));
        partsList.Add(new Zapis(2, new Klient(4, " Cherkin", "Sergey", "Petrovich", 999594050, "Hazhnaya"), 1, 1, 1, 30, 1));
        partsHandler.Rewrite(partsList);
        partsHandler.OutputJsonContents();
    }
}
