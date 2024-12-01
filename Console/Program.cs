using System.Data;
using System.Security;
using CallDataRecord.Models;
using Newtonsoft.Json;

namespace CallDataRecord;

public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            throw new ArgumentException("Filnavn mangler");
        }

        try
        {
            string filename = args[0];
            var json = ReadFile(filename);
            List<CDR> data = DeserializeJson(json);
            LinqAnalyzer analyzer = new LinqAnalyzer(data);

            //Print most active Callers
            var callers = analyzer.ActiveCallers();
            var mostActiveCallers = callers.Take(3);
            Console.WriteLine("Top 3 Most Active Callers:");
            foreach (var caller in mostActiveCallers)
            {
                Console.WriteLine($"{caller.Key}: {caller.Value} calls");
            }

            //Print Total duration of the most active caller
            TotalDuration totalDuration = analyzer.MostActiveCallersTotalDuration();
            Console.WriteLine($"Total Duration of Calls to {totalDuration.Number}: {totalDuration.Seconds} seconds");

            //Print total unique phone numbers in dataset
            int uniqueCount = analyzer.UniquePhoneNumbers();
            Console.WriteLine($"Total Unique Phone Numbers: {uniqueCount}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Console.WriteLine("Press any key to exit");
        }
        finally
        {
            Console.ReadKey();
        }
    }

    private static List<CDR> DeserializeJson(string json)
    {
        List<CDR> data = JsonConvert.DeserializeObject<List<CDR>>(json);
        return data;
    }

    private static string ReadFile(string filename)
    {
        string path = Path.Join(Environment.CurrentDirectory, filename);
        string json = File.ReadAllText(path);
        return json;
    }
}