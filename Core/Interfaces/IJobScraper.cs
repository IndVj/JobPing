using Core.Models;

namespace Core.Interfaces
{
    public interface IJobScraper
    {
        Task<List<Job>> ScrapeJobsAsync();
    }
}
