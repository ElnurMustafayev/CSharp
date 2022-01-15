using System.Net;

namespace Core.Base
{
    public interface IClient
    {
        Task ConnectAsync();
        Task SendMessageAsync();
    }
}