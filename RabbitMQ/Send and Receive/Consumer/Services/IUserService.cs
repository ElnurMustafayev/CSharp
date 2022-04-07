using Consumer.Models;

namespace Consumer.Services
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        void AddUser(User newUser);
    }
}