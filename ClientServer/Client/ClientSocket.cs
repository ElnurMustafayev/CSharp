using System.Net;
using System.Net.Sockets;
using System.Text;
using Core.Base;

namespace Client;

public class ClientSocket : IClient
{
    public Socket Socket { get; private set; }
    public readonly IPEndPoint EndPoint;

    public ClientSocket(IPEndPoint endPoint) {
        this.Socket = new Socket(
            addressFamily: AddressFamily.InterNetwork,
            socketType: SocketType.Stream,
            protocolType: ProtocolType.Tcp
        );

        this.EndPoint = endPoint;
    }
    public async Task ConnectAsync() {
        // connect to server
        await this.Socket.ConnectAsync(this.EndPoint);
    }

    public async Task SendMessageAsync() {
        while(true) {
            // write message for server
            System.Console.Write("Message: ");
            string message = Console.ReadLine() ?? string.Empty;
            byte[] messageBinary = Encoding.ASCII.GetBytes(message);

            // sned message to server
            ArraySegment<byte> segment = new ArraySegment<byte>(messageBinary);
            int bytesSend = await this.Socket.SendAsync(segment, SocketFlags.None);
        }
    }
}