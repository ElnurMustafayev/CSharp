using Program.Attributes;
using Program.Models;

namespace Program.Middleware
{
    public class UserRoleMiddleware
    {
        public static void Invoke (User currentUser, Type controllerType, Action method) {
            // AuthorizeAttribute
            var attr = controllerType.GetCustomAttributes(false).FirstOrDefault() as AuthorizeAttribute;

            if(attr == null || attr.Roles.Exists(ur => ur.Id == currentUser.Role.Id))
                method.Invoke();
        }
    }
}