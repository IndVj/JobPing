using Core.Models;

namespace Core.Interfaces
{
    public interface IJobRepository
    {
        Task<List<Job>> LoadNotifiedJobsAsync();
        Task SaveNotifiedJobsAsync(List<Job> jobs);
    }
}
