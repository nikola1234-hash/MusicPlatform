using CsvHelper;
using System.Globalization;

namespace MusicPlatform.Services.Csv
{
    public class CsvService<T> : ICsvService<T> where T : class
    {
        private IEnumerable<T> records;
        public CsvService<T> ReadCSV(string path)
        {
            using (var reader = new StreamReader(path))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    records = csv.GetRecords<T>().ToList();
                    return this;
                }
            }


        }

        public CsvService<T> ReadCSV(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                records = csv.GetRecords<T>().ToList();
                return this;
            }

        }

        public IEnumerable<T> GetRecords()
        {
            return records;
        }
    }
}
