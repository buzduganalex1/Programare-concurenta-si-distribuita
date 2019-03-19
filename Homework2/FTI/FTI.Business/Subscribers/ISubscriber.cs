using System.Threading.Tasks;

namespace FTI.Business.Subscribers
{
    public interface ISubscriber
    {
        Task Subscribe();
    }
}