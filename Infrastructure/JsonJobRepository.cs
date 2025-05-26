using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Core.Interfaces;
using Core.Models;

namespace Infrastructure
{
    public class JsonJobRepository : IJobRepository
    {
        private readonly string _filePath;

        public JsonJobRepository(string filePath)
        {
            _filePath = filePath;
        }

        public async Task<HashSet<Job>> LoadNotifiedJobsAsync()
        {
            if (!File.Exists(_filePath))
            {
                var empty = new HashSet<Job>();
                await SaveNotifiedJobsAsync(empty);
                return empty;
            }

            var json = await File.ReadAllTextAsync(_filePath);
            return JsonSerializer.Deserialize<HashSet<Job>>(json) ?? new HashSet<Job>();
        }

        public async Task SaveNotifiedJobsAsync(HashSet<Job> jobs)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            var json = JsonSerializer.Serialize(jobs, options);
            await File.WriteAllTextAsync(_filePath, json, Encoding.UTF8);
        }
    }
}
