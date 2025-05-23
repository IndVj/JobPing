using Core.Models;

namespace Core.Interfaces
{
    public interface IJobRepository
    {
        Task<HashSet<Job>> LoadNotifiedJobsAsync();
        Task SaveNotifiedJobsAsync(HashSet<Job> jobs);
    }
}
