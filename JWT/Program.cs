using System.Linq;
using Program.Models;
using Program.Repositories;
using Program.Services;

namespace Program;

public class App {
    public static void Main() {
        // Get User
        var userRepository = new UserLocalRepository();
        var users = userRepository.GetAllUsers();
        var admin = users.FirstOrDefault(u => u.Role is AdminRole);

        // Create service
        string sekcretKey = Guid.NewGuid().ToString();
        var tokenService = new JWTTokenService(sekcretKey);

        // Encode
        string token = tokenService.GenerateJWTToken(admin?.Id);

        // Decode
        AccessToken accessToken = tokenService.ValidateJWTToken(token);

        // Show
        System.Console.WriteLine(accessToken);
    }
}