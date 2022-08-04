using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService
{
    public class MyHostedService : IHostedService
    {
        private readonly ILogger<MyHostedService> logger;
        private readonly IConfiguration configuration;

        public MyHostedService(ILogger<MyHostedService> logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
        }


        public async Task StartAsync(CancellationToken cancellationToken)
        {
            ConnectionFactory connectionFactory = new ConnectionFactory();
            connectionFactory.HostName = configuration["RabbitMQ:Connections:Default:HostName"];
            try
            {
                var connection = connectionFactory.CreateConnection();
                var channel = connection.CreateModel();

                channel.QueueDeclare(queue: "MyQueue001", durable: true, exclusive: false, autoDelete: false);
                channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (sender, e) =>
                {
                    byte[] body = e.Body.ToArray();
                    string message = Encoding.UTF8.GetString(body);
                    this.logger.LogWarning("Received:" + message);
                    channel.BasicAck(e.DeliveryTag, false);
                };

                channel.BasicConsume(queue: "MyQueue001", autoAck: false, consumer: consumer);
            }
            catch (Exception ex)
            {

            }

            await Task.CompletedTask;// 即使该方法执行完毕，consumer仍然可以保持监听消息队列
        }


        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
