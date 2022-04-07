using System.Text;
using Helper.MQ;
using Helper.Options;
using Microsoft.Extensions.Options;
using Publisher.Models;
using RabbitMQ.Client;

namespace Publisher.Services;

public class UserSender : SenderBase, IUserSender
{
    public UserSender(IOptions<RabbitMqConfiguration> rabbitMqOptions) : base(rabbitMqOptions.Value)
    {
    }

    public void SendUser(User user)
    {
        if (ConnectionExists())
        {
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName,
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

                var json = System.Text.Json.JsonSerializer.Serialize<User>(user);
                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: "",
                                    routingKey: queueName,
                                    basicProperties: null,
                                    body: body);
            }
        }
    }
}