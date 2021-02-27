using System.Threading.Tasks;

namespace Facade.Clients
{
    public interface IMessagesSvcClient
    {
        Task<string> GetMessages();
    }
}
