using System.Net;
using Core.Base;

namespace Client;

public class Program
{
    public static async Task Main() {
        // add server info
        IPEndPoint endPoint = new IPEndPoint(
            address: IPAddress.Parse("127.0.0.1"),
            port: 8080);
            
        // create listener
        IClient client = new ClientTcp(endPoint);

        // create connect with server
        await client.ConnectAsync();

        // send message to server
        await client.SendMessageAsync();
    }
}