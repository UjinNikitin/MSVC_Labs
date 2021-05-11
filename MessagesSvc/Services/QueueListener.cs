using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using MessagesSvc.Clients;
using MessagesSvc.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MessagesSvc.Services
{
    public class QueueListener : BackgroundService
    {

        public QueueListener(ILogger<QueueListener> logger, IStorageProviderClient storageProviderClient)
        {
            _logger = logger;
            _storageProviderClient = storageProviderClient;
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "Group_1",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false,
            };
            _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken) =>
            Task.Factory.StartNew(() => StartConsumerLoopAsync(stoppingToken), stoppingToken);

        public override void Dispose()
        {
            _consumer.Close();
            _consumer.Dispose();

            base.Dispose();
        }

        private async Task StartConsumerLoopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("QueueListener Service is starting");
            _consumer.Subscribe(Topic);

            while (stoppingToken.IsCancellationRequested is false)
            {
                try
                {
                    var consumeResult = _consumer.Consume(stoppingToken);

                    _logger.LogInformation(consumeResult.Message.Value);
                    var msg = DeserializeMessage(consumeResult.Message.Value);

                    await _storageProviderClient.Insert(msg);

                    _consumer.Commit(consumeResult);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }

            _logger.LogInformation("QueueListener Service is stopping");
        }

        private Message DeserializeMessage(string messageString) =>
            JsonConvert.DeserializeObject<Message>(messageString);

        private readonly ILogger<QueueListener> _logger;
        private readonly IStorageProviderClient _storageProviderClient;
        private IConsumer<Ignore, string> _consumer;
        private const string Topic = "Lab_6_niki";
    }
}
