using System.Collections.Generic;
using System.Threading.Tasks;
using Facade.Models;

namespace Facade.Clients
{
    public interface ILoggingSvcClient
    {
        Task<IEnumerable<string>> ListLogs();

        Task Log(Message msg);
    }
}
