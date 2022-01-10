using System.Reflection;
using Program.Base;
using Program.Roles;

namespace Program.Services
{
    public static class RoleService
    {
        public static readonly List<UserRoleBase> Roles = new List<UserRoleBase>();

        static RoleService() {
            var rolesAssembly = Assembly.GetAssembly(typeof(AdminRole));

            if(rolesAssembly is null)
                throw new Exception("Couldn't get assembly of roles!");
            
            var roleTypes = from t in rolesAssembly.GetTypes()
                            where t.BaseType == typeof(UserRoleBase) && !t.IsAbstract
                            select t;

            if(roleTypes is null)
                throw new Exception("Roles have not been created!");

            var roles = roleTypes.Select(rt => (Activator.CreateInstance(rt) as UserRoleBase));
            Roles.AddRange(roles);
        }
    }
}