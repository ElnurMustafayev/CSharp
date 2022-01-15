using System.Net;
using System.Net.Sockets;
using System.Text;
using Core.Base;

namespace Server;

public class ServerTCP : IServer
{
    public TcpListener Listener { get; private set; }

    public ServerTCP(IPEndPoint endPoint)
    {
        // create Socket
        this.Listener = new TcpListener(endPoint);
    }

    public async Task ListenAsync()
    {
        while(true) {
            // open listener (wait for new client connection)
            var tcpClient = await this.Listener.AcceptTcpClientAsync();

            System.Console.WriteLine("New User Connected...");
            // the general difference
            using NetworkStream stream = tcpClient.GetStream();

            await Task.Run(async () => {
                while(true) {
                    try {
                        byte[] buffer = new byte[65000];
                        var size = await stream.ReadAsync(buffer);
                        var message = Encoding.ASCII.GetString(buffer, 0, size);
                        System.Console.WriteLine($"Message: '{message}'");
                    }
                    catch(Exception) {
                        System.Console.WriteLine("User disconnected...");
                        break;
                    }
                }
            });
        }
    }

    public void Open()
    {
        this.Listener.Start(10);
    }
}