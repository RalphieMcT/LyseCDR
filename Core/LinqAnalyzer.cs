using CallDataRecord.Models;

namespace CallDataRecord;

public class LinqAnalyzer
{
    private readonly List<CDR> _data;

    public LinqAnalyzer(List<CDR> data)
    {
        _data = data;
    }

    public int UniquePhoneNumbers()
    {
        IEnumerable<string> allNumbers = _data.Select(x => x.Caller).Union(_data.Select(x => x.Receiver));
        int uniqueCount = allNumbers.Distinct().Count();
        return uniqueCount;
    }

    public Dictionary<string, int> ActiveCallers()
    {
        IEnumerable<IGrouping<string, CDR>> byCaller = _data.GroupBy(x => x.Caller);
        Dictionary<string, int> callers = byCaller.ToDictionary(group => group.Key, group => group.Count());
        return callers.OrderByDescending(x=>x.Value).ToDictionary();
    }

    public TotalDuration MostActiveCallersTotalDuration()
    {
        string number = ActiveCallers().FirstOrDefault().Key;
        int seconds = _data.Where(x => x.Receiver == number).Sum(x => x.Duration);
        return new TotalDuration(number, seconds);
    }
}
