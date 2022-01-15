using System.Net;
using System.Net.Sockets;
using Core.Base;

namespace Client;

public class ClientTcp : IClient
{
    public TcpClient Client { get; private set; }
    public readonly IPEndPoint EndPoint;

    public ClientTcp(IPEndPoint endPoint)
    {
        this.EndPoint = new IPEndPoint(
            address: IPAddress.Parse("127.0.0.1"),
            port: 8080);

        this.Client = new TcpClient();
    }
    public async Task ConnectAsync()
    {
        await this.Client.ConnectAsync(this.EndPoint);
    }

    public async Task SendMessageAsync()
    {
        await Task.Run(async () => {
            while(true) {
                StreamWriter writer = new StreamWriter(this.Client.GetStream());

                System.Console.Write("Message: ");
                string message = Console.ReadLine() ?? string.Empty;
                await writer.WriteLineAsync(message);
                await writer.FlushAsync();
            }
        });
    }
}