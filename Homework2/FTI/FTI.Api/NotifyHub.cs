using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace FTI.Api
{
    public class NotifyHub : Hub<ITypedHubClient>
    {
    }

    public interface ITypedHubClient
    {
        Task BroadcastMessage(string type, string payload);
    }
}