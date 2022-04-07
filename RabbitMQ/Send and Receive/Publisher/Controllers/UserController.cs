using Microsoft.AspNetCore.Mvc;
using Publisher.Models;
using Publisher.Services;

namespace Publisher.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
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
            User newUser = new User() {
                Id = 123,
                CreatedDate = DateTime.Now,
                Username = "John"
            };

            userSender.SendUser(newUser);

            return newUser;
        }
    }
}