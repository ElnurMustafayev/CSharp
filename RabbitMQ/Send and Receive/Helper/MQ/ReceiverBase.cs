using Helper.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Helper.MQ
{
    public class ReceiverBase
    {
        protected IModel channel;
        protected IConnection connection;
        protected readonly string hostname;
        protected readonly string queueName;
        protected readonly string username;
        protected readonly string password;

        public ReceiverBase(RabbitMqConfiguration rabbitMqOptions)
        {
            this.hostname = rabbitMqOptions.Hostname;
            this.queueName = rabbitMqOptions.QueueName;
            this.username = rabbitMqOptions.UserName;
            this.password = rabbitMqOptions.Password;

            InitListener();
        }

        private void InitListener()
        {
            var factory = new ConnectionFactory
            {
                HostName = this.hostname,
                UserName = this.username,
                Password = this.password
            };

            this.connection = factory.CreateConnection();
            // this.connection.ConnectionShutdown += RabbitMQConnectionShutdown;
            this.channel = connection.CreateModel();
            this.channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }
    }
}