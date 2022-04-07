using Publisher.Models;

namespace Publisher.Services;

public interface IUserSender
{
    void SendUser(User user);
}