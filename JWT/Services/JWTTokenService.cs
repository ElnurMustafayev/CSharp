using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Program.Models;
using Program.Repositories;

namespace Program.Services;

public class JWTTokenService {
    private readonly string SecretKey;
    private readonly UserLocalRepository userLocalRepository;

    public JWTTokenService(string secretKey)
    {
        this.userLocalRepository = new UserLocalRepository();
        this.SecretKey = secretKey ?? Guid.NewGuid().ToString();
    }

    // Method for token GENERATION
    public string GenerateJWTToken(Guid? userid) {
        if(userid == null || userid == default)
            throw new ArgumentException($"{nameof(userid)} is reqired!", nameof(userid));

        var foundUser = this.userLocalRepository.GetAllUsers().FirstOrDefault(u => u.Id == userid);
        if(foundUser == null || foundUser == default)
            throw new ArgumentException($"User not found by '{userid}'!", nameof(userid));

        // generate token that is valid for 30 minutes
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = System.Text.Encoding.ASCII.GetBytes(this.SecretKey);

        // create token descriptor
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            // dates
            IssuedAt = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddMinutes(30),
            // data
            Subject = new ClaimsIdentity(new[] { 
                    new Claim(nameof(foundUser.Id), foundUser.Id.ToString()),
                    new Claim(nameof(foundUser.Role), foundUser.Role.RoleName),
                }),
            // properties
            SigningCredentials = new SigningCredentials(
                key: new SymmetricSecurityKey(key), 
                algorithm: SecurityAlgorithms.HmacSha256Signature),
        };

        // generate token
        var token = tokenHandler.CreateToken(tokenDescriptor);

        // get JWT as string
        return tokenHandler.WriteToken(token);
    }

    // Method for token VALIDATION
    public AccessToken ValidateJWTToken(string token) {
        if(string.IsNullOrWhiteSpace(token))
            throw new ArgumentException($"{nameof(token)} is reqired!", nameof(token));

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = System.Text.Encoding.ASCII.GetBytes(this.SecretKey);

        tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            // key
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            // properties
            ValidateIssuer = false,
            ValidateAudience = false,
            // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
            ClockSkew = TimeSpan.Zero
        }, out SecurityToken validatedToken);

        var jwtToken = (JwtSecurityToken)validatedToken;
        
        return new AccessToken(jwtToken.Claims);
    }
}