using Helper.Options;
using RabbitMQ.Client;

namespace Helper.MQ;

public abstract class SenderBase
{
    public readonly string hostname;
    public readonly string password;
    public readonly string queueName;
    public readonly string username;
    public IConnection connection;

    public SenderBase(RabbitMqConfiguration configuration)
    {
        this.hostname = configuration.Hostname;
        this.queueName = configuration.QueueName ?? string.Empty;
        
        this.password = configuration.Password;
        this.username = configuration.UserName;

        CreateConnection();
    }

    public virtual void CreateConnection()
    {
        try
        {
            var factory = new ConnectionFactory
            {
                HostName = this.hostname,
                UserName = this.username,
                Password = this.password
            };

            this.connection = factory.CreateConnection();

            this.connection.ConnectionShutdown += OnShutdown;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not create connection: {ex.Message}");
        }
    }

    private void OnShutdown(object? sender, ShutdownEventArgs e) {
        this.connection?.Dispose();
    }

    public virtual bool ConnectionExists()
    {
        if (this.connection != null)
            return true;

        CreateConnection();

        return this.connection != null;
    }
}