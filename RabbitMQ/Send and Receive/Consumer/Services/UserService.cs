using Consumer.Models;

namespace Consumer.Services
{
    public class UserService : IUserService
    {
        private static List<User> Users = new List<User>();

        public void AddUser(User newUser)
        {
            Users.Add(newUser);
        }

        public List<User> GetAllUsers()
        {
            return Users;
        }
    }
}