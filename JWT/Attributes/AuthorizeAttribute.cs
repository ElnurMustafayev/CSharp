using System.Collections.Generic;
using Program.Base;
using Program.Enums;
using Program.Services;

namespace Program.Attributes
{
    public class AuthorizeAttribute : Attribute
    {
        public List<UserRoleBase> Roles { get; set; } = new List<UserRoleBase>();
        public AuthorizeAttribute(params USER_ROLES[] roles)
		{
            var currentRoles = RoleService.Roles.Where(ur => roles.Any(r => (int)r == ur.Id));

            if(currentRoles is null)
                throw new Exception("Roles not found in local!");

            Roles.AddRange(currentRoles);
		}
    }
}