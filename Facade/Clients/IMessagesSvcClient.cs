using System.Threading.Tasks;
using Facade.Models;

namespace Facade.Clients
{
    public interface IMessagesSvcClient
    {
        Task<string> GetMessages();

        Task PostMessage(Message msg);
    }
}
