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


            var jobCards = doc.DocumentNode.SelectNodes("//div[@data-testid='base-job-card-container']");

            if (jobCards == null)
                return jobs;

            foreach (var card in jobCards)
            {
                var titleNode = card.SelectSingleNode(".//a[contains(@class, 'BaseJobCard_jobTitle__ehsas')]");

                var locationNode = card.SelectSingleNode(".//div[@data-testid='job-location-tag']//span");

                if (titleNode == null || locationNode == null)
                    continue;

                var relativeUrl = titleNode.GetAttributeValue("href", "").Trim();

                Uri uri = new Uri(_url);
                string baseUrl = $"{uri.Scheme}://{uri.Host}";

                var absoluteUrl = new Uri(baseUrl + relativeUrl).AbsoluteUri;

                var job = new Job
                {
                    Title = HtmlEntity.DeEntitize(titleNode.InnerText.Trim()),
                    Description = $"Location: {locationNode.InnerText.Trim()}",
                    Url = absoluteUrl
                };

                jobs.Add(job);
            }

            return jobs;
        }
    }
}
