namespace Program.Base;
public abstract class UserRoleBase
{
    public int Id { get; set; }
    public string Name { get; set; }
    public UserRoleBase(int id, string name)
    {
        this.Id = id;
        this.Name = name;
    }
}