using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Confluent.Kafka;
using Facade.Models;
using Newtonsoft.Json;

namespace Facade.Clients.Implementation
{
    public class MessagesSvcClient : IMessagesSvcClient
    {
        public MessagesSvcClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _random = new Random();
            _remoteServiceBaseUrls = new List<string>
            {
                "http://localhost:9081/",
                "http://localhost:9082/",
                "http://localhost:9083/"
            };
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            };
            _producer = new ProducerBuilder<Null, string>(config).Build();
        }


        public Task<string> GetMessages()
        {
            var uri = $"{GetServiceBaseUrl()}api/Message";
            return _httpClient.GetStringAsync(uri);
        }

        public Task PostMessage(Message msg)
        {
            _producer.ProduceAsync(Topic, new Message<Null, string>
            {
                Value = JsonConvert.SerializeObject(msg)
            });

            _producer.Flush(TimeSpan.FromSeconds(10));

            return Task.CompletedTask;
        }


        private string GetServiceBaseUrl() =>
            _remoteServiceBaseUrls[_random.Next(_remoteServiceBaseUrls.Count)];


        private readonly HttpClient _httpClient;
        private readonly List<string> _remoteServiceBaseUrls;
        private readonly Random _random;
        private IProducer<Null, string> _producer;
        private const string Topic = "Lab_6_niki";
    }
}
