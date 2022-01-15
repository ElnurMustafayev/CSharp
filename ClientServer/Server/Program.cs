using System.Net;
using Core.Base;

namespace Server;

public class Program
{
    public static async Task Main() {
        // add server info
        IPEndPoint endPoint = new IPEndPoint(
            address: IPAddress.Parse("127.0.0.1"),
            port: 8080);
            
        // create server
        IServer server = new ServerTCP(endPoint);
        
        // open listener
        server.Open();
        
        // listen
        await server.ListenAsync();
    }
}