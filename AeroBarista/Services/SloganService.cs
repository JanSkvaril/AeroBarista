using AeroBarista.Attributes;
using AeroBarista.Services.Interfaces;
using System.Text.Json;

namespace AeroBarista.Services
{
    [ExportTransientAs(nameof(ISloganService))]
    public class SloganService : ISloganService
    {
        private IList<string>? slogans;

        public SloganService()
        {
            slogans = new List<string>();
        }

        private async Task GetDataAsync()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("slogans.json");
            using var reader = new StreamReader(stream);
            var contents = reader.ReadToEnd();
            slogans = JsonSerializer.Deserialize<List<string>>(contents);
        }

        public async Task<string> GetRandomSlogan()
        {
            await GetDataAsync();
            
            if (slogans == null)
            {
                return string.Empty;
            }
            
            Random random = new Random();
            int index = random.Next(0, slogans.Count);
            return slogans[index];
        }
    }
}
