using System.Net;
using System.Net.Sockets;
using System.Text;
using Core.Base;

namespace Server;

public class ServerSocket : IServer
{
    public Socket Socket { get; private set; }
    public readonly IPEndPoint EndPoint;

    public ServerSocket(IPEndPoint endPoint) {
        // initialize Socket
        this.Socket = new Socket(
            addressFamily: AddressFamily.InterNetwork,
            socketType: SocketType.Stream,
            protocolType: ProtocolType.Tcp
        );

        this.EndPoint = endPoint;
    }

    public async Task ListenAsync()
    {
        while(true) {
            // open listener (wait for new client connection)
            Socket clientSocket = await this.Socket.AcceptAsync();

            // new connection (run async)
            await Task.Run(async () => {
                System.Console.WriteLine("New user connected...");

                while(true) {
                    try {
                        // get received message from client
                        byte[] buffer = new byte[65000];
                        ArraySegment<byte> segment = new ArraySegment<byte>(buffer);
                        int size = await clientSocket.ReceiveAsync(segment, SocketFlags.None);

                        // convert message to string and show
                        var message = Encoding.ASCII.GetString(buffer, 0, size);
                        System.Console.WriteLine($"Message: '{message}'");
                    }
                    catch(SocketException) {
                        System.Console.WriteLine("User disconnected...");
                        break;
                    }
                }
            });
        }
    }

    public void Open()
    {
        // create port
        this.Socket.Bind(this.EndPoint);

        // add limit
        this.Socket.Listen(10);
    }
}