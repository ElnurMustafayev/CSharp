using System.Linq;
using Program.Controllers;
using Program.Enums;
using Program.Middleware;
using Program.Models;
using Program.Repositories;
using Program.Roles;
using Program.Services;

namespace Program;

public class App {
    public static void Main() {
        // Get User
        var userRepository = new UserLocalRepository();
        var users = userRepository.GetAllUsers();
        var admin = users.First(u => u.Role.Id == (int)USER_ROLES.Moderator);

        #region CheckJWT

            // Create service
            string sekcretKey = Guid.NewGuid().ToString();
            var tokenService = new JWTTokenService(sekcretKey);

            // Encode
            string token = tokenService.GenerateJWTToken(admin?.Id);

            // Decode
            AccessToken accessToken = tokenService.ValidateJWTToken(token);

            // Show
            System.Console.WriteLine(accessToken);

        #endregion

        #region CheckRole

            UserRoleMiddleware.Invoke(admin, typeof(AdminController), () => new AdminController().SayHello());

        #endregion
    }
}