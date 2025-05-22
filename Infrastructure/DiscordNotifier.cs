using System.Net.Http.Json;
using Core.Interfaces;
using Core.Models;

namespace Infrastructure
{
    public class DiscordNotifier : INotifier
    {
        private readonly string _webhookUrl;

        public DiscordNotifier(string webhookUrl)
        {
            _webhookUrl = webhookUrl;
        }

        public async Task NotifyAsync(Job job)
        {
            using var client = new HttpClient();
            var content = new { content = $"**{job.Title}**\n{job.Description}\n{job.Url}" };
            await client.PostAsJsonAsync(_webhookUrl, content);
        }
    }
}
