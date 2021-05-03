using System.Collections.Generic;
using System.Threading.Tasks;
using MessagesSvc.Models;

namespace MessagesSvc.Clients
{
    public interface IStorageProviderClient
    {
        Task<IEnumerable<string>> Get();

        Task Insert(Message msg);
    }
}
