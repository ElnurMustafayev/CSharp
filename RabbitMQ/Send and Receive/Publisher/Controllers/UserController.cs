using Microsoft.AspNetCore.Mvc;
using Publisher.Models;
using Publisher.Services;

namespace Publisher.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserSender userSender;
        public UserController(IUserSender userSender)
        {
            this.userSender = userSender;
        }

        [HttpGet]
        public ActionResult<User> SendUser()
        {
            User newUser = new User("John");

            userSender.SendUser(newUser);

            return newUser;
        }
    }
}