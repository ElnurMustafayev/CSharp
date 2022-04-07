using Helper.Options;
using RabbitMQ.Client;

namespace Helper.MQ;

public abstract class SenderBase
{
    protected readonly string hostname;
    protected readonly string password;
    protected readonly string queueName;
    protected readonly string username;
    protected IConnection connection;

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
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not create connection: {ex.Message}");
        }
    }

    public virtual bool ConnectionExists()
    {
        if (this.connection != null)
            return true;

        CreateConnection();

        return this.connection != null;
    }
}