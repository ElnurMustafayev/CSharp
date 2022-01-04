using Program.Base;

namespace Program.Models;

public class User
{
    public readonly Guid Id;
    public string Name { get; set; }
    public RoleBase Role { get; set; }

    public User(string name, RoleBase role)
    {
        this.Id = Guid.NewGuid();
        this.Name = name;
        this.Role = role;
    }
}