using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server;

public class ServerSocket
{
    public async Task OpenConnectionAsync() {
        // create Socket
        Socket socket = new Socket(
            addressFamily: AddressFamily.InterNetwork,
            socketType: SocketType.Stream,
            protocolType: ProtocolType.Tcp
        );

        // add server info
        IPEndPoint endPoint = new IPEndPoint(
            address: IPAddress.Parse("127.0.0.1"),
            port: 8080);

        // create port
        socket.Bind(endPoint);

        // add limit
        socket.Listen(10);

        while(true) {
            // open listener (wait for new client connection)
            Socket clientSocket = await socket.AcceptAsync();

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
}