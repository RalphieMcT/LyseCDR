using CallDataRecord.Models;
using FluentAssertions;
using Newtonsoft.Json;

namespace CallDataRecord;

public class LinqAnalyzerProvidedDataTests
{
    private LinqAnalyzer _analyzer;
    
    [SetUp]
    public void Setup()
    {
        var data = JsonConvert.DeserializeObject<List<CDR>>(@"
          [
            { ""Caller"": ""12345678"", ""Receiver"": ""09876543"", ""StartTime"": ""2024-11-27T10:00:00Z"", ""Duration"": 120 },
            { ""Caller"": ""12345678"", ""Receiver"": ""11223344"", ""StartTime"": ""2024-11-27T10:05:00Z"", ""Duration"": 60 },
            { ""Caller"": ""09876543"", ""Receiver"": ""12345678"", ""StartTime"": ""2024-11-27T10:10:00Z"", ""Duration"": 180 },
            { ""Caller"": ""11223344"", ""Receiver"": ""12345678"", ""StartTime"": ""2024-11-27T10:20:00Z"", ""Duration"": 30 },
            { ""Caller"": ""12345678"", ""Receiver"": ""44556677"", ""StartTime"": ""2024-11-27T10:30:00Z"", ""Duration"": 90 }
          ]
        ");
        _analyzer = new LinqAnalyzer(data);
    }

    [Test]
    public void MostActiveCallers()
    {
        _analyzer.ActiveCallers().Should().Equal(new List<KeyValuePair<string, int>>
        {
            new("12345678", 3),
            new("09876543", 1),
            new("11223344", 1),
        });
    }

    [Test]
    public void TotalDurationForTopCaller()
    {
        TotalDuration actual = _analyzer.MostActiveCallersTotalDuration();
        actual.Should().Be(new TotalDuration("12345678", 210));
    }

    [Test]
    public void UniqueNumbers_Should_Be_4()
    {
        _analyzer.UniquePhoneNumbers().Should().Be(4);
    }
}