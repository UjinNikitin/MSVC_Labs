using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hazelcast;
using Hazelcast.DistributedObjects;
using LoggingSvc.Models;
using Microsoft.Extensions.Logging;

namespace LoggingSvc.Clients.Implementation
{
    public class HazelcastStorageProviderClient : IStorageProviderClient
    {
        public HazelcastStorageProviderClient(ILogger<HazelcastStorageProviderClient> logger)
        {
            _logger = logger;
        }

        public async Task<IEnumerable<string>> Get()
        {
            await SetClient();
            await GetMap();

            try
            {
                return await _map.GetValuesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            finally
            {
                await _map.DisposeAsync();
                await _client.DisposeAsync();
            }
        }

        public async Task Insert(Message msg)
        {
            await SetClient();
            await GetMap();

            try
            {
                await _map.SetAsync(msg.Id, msg.Value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            finally
            {
                await _map.DisposeAsync();
                await _client.DisposeAsync();
            }
        }


        private async Task SetClient()
        {
            _client = await HazelcastClientFactory.StartNewClientAsync(new HazelcastOptions
            {
                Networking =
                {
                    Addresses =
                    {
                        "192.168.0.50:6001",
                        "192.168.0.50:6002",
                        "192.168.0.50:6003"
                    }
                }
            });
        }

        private async Task GetMap()
        {
            _map = await _client.GetMapAsync<Guid, string>("Logging-map");
        }


        private IHazelcastClient _client;
        private IHMap<Guid, string> _map;
        private readonly ILogger<HazelcastStorageProviderClient> _logger;
    }
}
