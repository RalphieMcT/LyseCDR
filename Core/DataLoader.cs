using CallDataRecord.Models;
using Newtonsoft.Json;

namespace CallDataRecord;

public static class DataLoader
{
    public static List<CDR> ReadCallDataRecordFile(string filename)
    {
        string path = Path.Join(Environment.CurrentDirectory, filename);
        string json = File.ReadAllText(path);
        List<CDR> data = JsonConvert.DeserializeObject<List<CDR>>(json);
        return data;
    }
}