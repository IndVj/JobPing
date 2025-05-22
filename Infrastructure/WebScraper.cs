using Core.Interfaces;
using Core.Models;
using HtmlAgilityPack;

namespace Infrastructure
{
    public class WebScraper : IJobScraper
    {
        private readonly string _url;

        public WebScraper(string url)
        {
            _url = url;
        }

        public async Task<List<Job>> ScrapeJobsAsync()
        {
            var jobs = new List<Job>();
            var web = new HtmlWeb();
            var doc = await web.LoadFromWebAsync(_url);
            // Implement specific scraping logic here
            return jobs;
        }
    }
}
