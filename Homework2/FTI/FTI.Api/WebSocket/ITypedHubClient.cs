using System.Threading.Tasks;

namespace FTI.Api
{
    public interface ITypedHubClient
    {
        Task BroadcastMessage(string type, string payload, string id);
    }
}