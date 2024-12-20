﻿using System.Data;
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
            Console.WriteLine("Syntax: Console.exe <path-to-file>");
            Environment.Exit(1);
        }

        try
        {
            var path = GetFullPath(args);
            var json = ReadFile(path);
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
        }
    }

    private static string GetFullPath(string[] args)
    {
        string path;
        if (Path.IsPathRooted(args[0]))
        {
            path = args[0];
        }
        else
        {
            path = Path.Join(Environment.CurrentDirectory, args[0]);
        }

        return path;
    }

    private static List<CDR> DeserializeJson(string json)
    {
        List<CDR> data = JsonConvert.DeserializeObject<List<CDR>>(json);
        if (data is null)
        {
            Console.WriteLine("JSON data is null");
            Environment.Exit(1);
        }
        return data;
    }

    private static string ReadFile(string path)
    {
        if (File.Exists(path) == false)
        {
            Console.WriteLine($"The file does not exist: {path}");
            Environment.Exit(1);
        }
        string json = File.ReadAllText(path);
        return json;
    }
}