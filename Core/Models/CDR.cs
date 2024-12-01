namespace CallDataRecord.Models;

public record CDR
{
    public string Caller { get; }
    public string Receiver { get; }
    public DateTime StartTime { get; }
    public int Duration { get; }

    public CDR(string caller, string receiver, DateTime startTime, int duration)
    {
       Caller = caller;
       Receiver = receiver;
       StartTime = startTime;
       Duration = duration;
    }
}