using Twitter.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class JWTService : IJWTService
{
    private const double EXPIRY_DURATION_MINUTES = 30;

    public string GenerateToken(string key, string issuer, string audience, User user)
    {
        var claims = new[] {
            new Claim(ClaimTypes.Name, user.Handle), // original = username and ID
            new Claim(ClaimTypes.Email, user.Email),            
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new JwtSecurityToken(
            issuer, 
            audience, 
            claims,
            expires: DateTime.Now.AddMinutes(EXPIRY_DURATION_MINUTES), signingCredentials: credentials);
        
        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }

    public string GetJWTTokenClaim(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
            var claimValue = securityToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            return claimValue;
         }
        catch (Exception)
        {
            return null;
        }
    }

    public bool IsTokenValid(string key, string issuer, string audience,string token)
    {
        var mySecret = Encoding.UTF8.GetBytes(key);
        var mySecurityKey = new SymmetricSecurityKey(mySecret);
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            tokenHandler.ValidateToken(token,
            new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = mySecurityKey,
            }, 
            // passed by reference
            out SecurityToken validatedToken);
        }
        catch
        {
            return false;
        }
        return true;
    }


}
