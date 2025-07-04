using BTGProject.Core.Models;
using System.Text.Json;

namespace BTGProject.Core
{
    public class AppSettings
    {
        private const string FileName = "clients.json";

        public List<Client> Clients { get; set; } = new();

        public async Task SaveAsync()
        {
            var json = JsonSerializer.Serialize(Clients);
            var filePath = GetFilePath();
            await File.WriteAllTextAsync(filePath, json);
        }

        public async Task LoadAsync()
        {
            var filePath = GetFilePath();
            if (File.Exists(filePath))
            {
                var json = await File.ReadAllTextAsync(filePath);
                var clients = JsonSerializer.Deserialize<List<Client>>(json);
                if (clients != null)
                    Clients = clients;
            }
        }

        private static string GetFilePath()
        {
#if ANDROID || IOS || MACCATALYST
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
#else
            var folder = AppContext.BaseDirectory;
#endif
            return Path.Combine(folder, FileName);
        }
    }
}