namespace Program.Base;

public abstract class RoleBase
{
    public readonly string RoleName;

    public RoleBase(string roleName)
    {
        this.RoleName = roleName;
    }
}