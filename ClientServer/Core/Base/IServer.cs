using System.Net;

namespace Core.Base;

public interface IServer
{
    void Open();
    Task ListenAsync();
}