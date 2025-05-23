using System.Text.Json;
using Core.Interfaces;
using Core.Models;

namespace Infrastructure
{
    public class JsonJobRepository : IJobRepository
    {
        private readonly string _filePath = "/*jobs*/.json";

        public async Task<List<Job>> LoadNotifiedJobsAsync()
        {
            if (!File.Exists(_filePath)) return new List<Job>();
            var json = await File.ReadAllTextAsync(_filePath);
            return JsonSerializer.Deserialize<List<Job>>(json) ?? new();
        }

        public async Task SaveNotifiedJobsAsync(List<Job> jobs)
        {
            var json = JsonSerializer.Serialize(jobs);
            await File.WriteAllTextAsync(_filePath, json);
        }
    }
}
