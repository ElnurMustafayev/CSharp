using System.Text;
using Consumer.Models;
using Helper.MQ;
using Helper.Options;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Consumer.Services
{
    public class UserReceiver : ReceiverBase, IHostedService
    {
        private IModel channel;
        private IConnection connection;
        private readonly string hostname;
        private readonly string queueName;
        private readonly string username;
        private readonly string password;
        private readonly IUserService userService;

        public UserReceiver(IOptions<RabbitMqConfiguration> rabbitMqOptions, IUserService userService) : base(rabbitMqOptions.Value)
        {
            this.hostname = rabbitMqOptions.Value.Hostname;
            this.queueName = rabbitMqOptions.Value.QueueName;
            
            this.username = rabbitMqOptions.Value.UserName;
            this.password = rabbitMqOptions.Value.Password;

            this.userService = userService;
            InitializeRabbitMqListener();
        }

        private void InitializeRabbitMqListener()
        {
            var factory = new ConnectionFactory
            {
                HostName = hostname,
                UserName = username,
                Password = password
            };

            connection = factory.CreateConnection();
            connection.ConnectionShutdown += RabbitMQConnectionShutdown;
            channel = connection.CreateModel();
            channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        private void OnConsumerCancelled(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerRegistered(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerShutdown(object sender, ShutdownEventArgs e)
        {
        }

        private void RabbitMQConnectionShutdown(object sender, ShutdownEventArgs e)
        {
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());

                User newUser = System.Text.Json.JsonSerializer.Deserialize<User>(content);
                userService.AddUser(newUser);

                channel.BasicAck(ea.DeliveryTag, false);
            };
            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerCancelled;

            channel.BasicConsume(queueName, false, consumer);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.channel.Close();
            this.connection.Close();
            
            return Task.CompletedTask;
        }
    }
}