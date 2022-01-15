using System.Net;

namespace Core.Base;

public interface IServer
{
    void Open(IPEndPoint endPoint);
    void Listen();
}