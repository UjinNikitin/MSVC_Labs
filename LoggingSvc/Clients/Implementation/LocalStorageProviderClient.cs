using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoggingSvc.Models;

namespace LoggingSvc.Clients.Implementation
{
    public class LocalStorageProviderClient : IStorageProviderClient
    {
        public LocalStorageProviderClient()
        {
            _messages = new ConcurrentDictionary<Guid, string>();
        }


        public Task<IEnumerable<string>> Get() =>
            Task.FromResult(_messages.Values.AsEnumerable());

        public Task Insert(Message msg)
        {
            _messages.TryAdd(msg.Id, msg.Value);
            return Task.CompletedTask;
        }


        private readonly ConcurrentDictionary<Guid, string> _messages;
    }
}
