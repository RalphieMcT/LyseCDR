using System.Text;
using CallStatsTool;
using Newtonsoft.Json;

public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            throw new ArgumentException("Filnavn mangler");
        }
            
        string filename = args[0];
        string json = ReadJsonFile(filename);
        List<CDRData> data = JsonConvert.DeserializeObject<List<CDRData>>(json);
        var callers = ActiveCallers(data);
        TotalDurationToNumber(data, callers.FirstOrDefault().Key);
        UniquePhoneNumbers(data);
    }

    private static void UniquePhoneNumbers(List<CDRData> data)
    {
        IEnumerable<string> allNumbers = data.Select(x => x.Caller).Union(data.Select(x => x.Receiver));
        Console.WriteLine($"Total Unique Phone Numbers: {allNumbers.Distinct().Count()}");
    }

    private static Dictionary<string, int> ActiveCallers(List<CDRData> data)
    {
        IEnumerable<IGrouping<string, CDRData>> byCaller = data.GroupBy(x => x.Caller);
        Dictionary<string, int> callers = byCaller.ToDictionary(group => group.Key, group => group.Count());
        Console.WriteLine("Top 3 Most Active Callers:");
        var mostActiveCallers = callers.OrderByDescending(x => x.Value).Take(3);
        foreach (var caller in mostActiveCallers)
        {
            Console.WriteLine($"{caller.Key}: {caller.Value} calls");
        }

        return callers;
    }

    private static void TotalDurationToNumber(List<CDRData> data, string number)
    {
        int sum = data.Where(x=>x.Caller==number).Sum(x=>x.Duration);
        Console.WriteLine($"Total Duration of Calls to {number}: {sum} seconds");
    }

    private static string ReadJsonFile(string filename)
    {
        string path = Path.Join(Environment.CurrentDirectory, filename);
        string json = File.ReadAllText(path);
        return json;
    }
}
