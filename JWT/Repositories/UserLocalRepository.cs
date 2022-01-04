using Program.Models;

namespace Program.Repositories;

public class UserLocalRepository
{
    private static IEnumerable<User> Users;

    static UserLocalRepository()
    {
        Users = new List<User>(new [] {
            new User("Elnur", new AdminRole()),
            new User("John", new ModeratorRole()),
            new User("Ann", new ModeratorRole()),
        });
    }

    public IEnumerable<User> GetAllUsers() => UserLocalRepository.Users;
}