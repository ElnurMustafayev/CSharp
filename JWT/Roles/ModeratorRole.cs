using Program.Base;
using Program.Enums;

namespace Program.Roles
{
    public sealed class ModeratorRole : UserRoleBase
    {
        public ModeratorRole() : base((int)USER_ROLES.Moderator, USER_ROLES.Moderator.ToString())
        {
        }
    }
}