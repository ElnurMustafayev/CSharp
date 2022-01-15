using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client;

public class ClientSocket
{
    public async Task ConnectToServer() {
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

        // connect to server
        await socket.ConnectAsync(endPoint);

        while(true) {
            // write message for server
            System.Console.Write("Message: ");
            string message = Console.ReadLine() ?? string.Empty;
            byte[] messageBinary = Encoding.ASCII.GetBytes(message);

            // sned message to server
            ArraySegment<byte> segment = new ArraySegment<byte>(messageBinary);
            int bytesSend = await socket.SendAsync(segment, SocketFlags.None);
        }
    }
}