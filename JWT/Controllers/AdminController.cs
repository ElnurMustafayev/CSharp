using Program.Attributes;
using Program.Enums;

namespace Program.Controllers
{
    [Authorize(USER_ROLES.Admin)]
    public class AdminController
    {
        public void SayHello() {
            System.Console.WriteLine("Hello from Admin!");
        }
    }
}