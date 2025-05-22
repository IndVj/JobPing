using Core.Models;

namespace Core.Interfaces
{
    public interface INotifier
    {
        Task NotifyAsync(Job job);
    }
}
