using Consumer.DTO;
using Consumer.Models;
using Consumer.Services;
using Microsoft.AspNetCore.Mvc;

namespace Consumer.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public IEnumerable<User> GetAllUsers()
        {
            return this.userService.GetAllUsers();
        }

        [HttpGet]
        public int? GetAllUsersCount()
        {
            return this.userService.GetAllUsers()?.Count;
        }
    }
}