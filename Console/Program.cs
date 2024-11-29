using CallDataRecord.Models;

namespace CallDataRecord;

public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            throw new ArgumentException("Filnavn mangler");
        }
        
        string filename = args[0];
        var data = DataLoader.ReadCallDataRecordFile(filename);
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
}
