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
            INotifier notifier = new DiscordNotifier(discordWebhookUrl);

            var allJobs = new List<Job>();

            // Scrape from all configured job pages
            foreach (var pageUrl in jobPages)
            {
                IJobScraper scraper = new WebScraper(pageUrl);
                var jobs = await scraper.ScrapeJobsAsync();
                allJobs.AddRange(jobs);
            }

            // Load previously notified jobs
            var notifiedJobs = await repo.LoadNotifiedJobsAsync();

            // Filter new jobs that match keywords and haven’t been notified before
            var newJobs = allJobs
                .Where(job => keywords.Any(k =>
                    job.Title.Contains(k, StringComparison.OrdinalIgnoreCase)))
                .Where(job => !notifiedJobs.Contains(job))
                .ToList();

            // Notify new jobs
            foreach (var job in newJobs)
            {
                await notifier.NotifyAsync(job);
            }

            // Add new jobs to the notified set
            notifiedJobs.UnionWith(newJobs);

            // Save updated list
            await repo.SaveNotifiedJobsAsync(notifiedJobs);
        }
    }
}
