using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace MJ.Application.Services;

public class Helper
{

    private readonly IHttpContextAccessor _httpContextAccessor;
    public Helper(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public static string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes("SuperSecretKey@2024#JWTAuth!$%^&*()");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                    new Claim("email", user.Email),
                    // new Claim("role", user.Role?.ToString()??String.Empty),
                    new Claim("userId", user.Id.ToString()??String.Empty),
                    new Claim("exp", new DateTimeOffset(DateTime.Now.AddMinutes(120)).ToUnixTimeSeconds().ToString())
                }),
            Expires = DateTime.Now.AddMinutes(120),
            Issuer = "MJ",
            Audience = "MJ",
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public static bool ValidateJwtToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes("SuperSecretKey@2024#JWTAuth!$%^&*()");
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = "MJ",
                ValidateAudience = true,
                ValidAudience = "MJ",
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Token validation failed: {ex.Message}");
            return false;
        }
    }

    public static string GetEmailIdFromToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        var emailClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "email");
        if (emailClaim == null)
        {
            return null;
        }
        return emailClaim.Value;
    }
}
