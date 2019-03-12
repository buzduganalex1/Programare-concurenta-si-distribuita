using System.Threading.Tasks;

namespace FTI.Subscribers.Subscribers
{
    public interface ISubscriber
    {
        Task Subscribe();
    }
}