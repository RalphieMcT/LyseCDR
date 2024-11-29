using System.Runtime.InteropServices.JavaScript;

namespace CallDataRecord.Models;

public struct TotalDuration(string number, int seconds)
{
    public String Number => number;
    public int Seconds => seconds;
}