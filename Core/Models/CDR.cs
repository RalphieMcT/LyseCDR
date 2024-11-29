namespace CallDataRecord.Models;

public record CDR
{
    public string Caller { get; set; }
    public string Receiver { get; set; }
    public DateTime StartTime { get; set; }
    public int Duration { get; set; }

    public CDR(string caller, string receiver, DateTime startTime, int duration)
    {
       Caller = caller;
       Receiver = receiver;
       StartTime = startTime;
       Duration = duration;
    }
}