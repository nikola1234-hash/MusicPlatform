namespace MusicPlatform.Services.Csv
{
    public interface ICsvService<T> where T : class
    {
        IEnumerable<T> GetRecords();
        CsvService<T> ReadCSV(Stream stream);
        CsvService<T> ReadCSV(string path);
    }
}