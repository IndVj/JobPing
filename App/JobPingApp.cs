using Core.Interfaces;
using Core.Models;
using Infrastructure;

namespace App
{
    public static class JobPingApp
    {
        public static async Task RunAsync(string[] jobPages, string discordWebhookUrl, string[] keywords)
        {
            IJobRepository repo = new JsonJobRepository();
            DiscordNotifier discordNotifier = new DiscordNotifier(discordWebhookUrl);
            INotifier notifier = discordNotifier;
            var allJobs = new List<Job>();

            foreach (var pageUrl in jobPages)
            {
                IJobScraper scraper = new WebScraper(pageUrl);
                var jobs = await scraper.ScrapeJobsAsync();
                allJobs.AddRange(jobs);
            }

            var notifiedJobs = await repo.LoadNotifiedJobsAsync();

            var newJobs = allJobs
                .Where(job => keywords.Any(k => job.Title.Contains(k, StringComparison.OrdinalIgnoreCase)))
                .Where(job => !notifiedJobs.Any(nj => nj.Url == job.Url))
                .ToList();

            foreach (var job in newJobs)
            {
                await notifier.NotifyAsync(job);
            }

            notifiedJobs.AddRange(newJobs);
            
            await repo.SaveNotifiedJobsAsync(notifiedJobs);
        }
    }
}
