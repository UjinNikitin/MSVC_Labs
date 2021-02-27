using System.Collections.Generic;
using System.Threading.Tasks;
using LoggingSvc.Models;

namespace LoggingSvc.Clients
{
    public interface IStorageProviderClient
    {
        Task<IEnumerable<string>> Get();

        Task Insert(Message msg);
    }
}
