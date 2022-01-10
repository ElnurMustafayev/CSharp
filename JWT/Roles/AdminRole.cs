using Program.Base;
using Program.Enums;

namespace Program.Roles
{
    public sealed class AdminRole : UserRoleBase
    {
        public AdminRole() : base((int)USER_ROLES.Admin, USER_ROLES.Admin.ToString())
        {
        }
    }
}