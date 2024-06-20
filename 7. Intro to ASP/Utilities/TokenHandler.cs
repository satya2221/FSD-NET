using System.Collections;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace _7._Intro_to_ASP;

public interface ITokenHandler 
{
    string Access(IEnumerable<Claim> claims);
}


public class TokenHandler : ITokenHandler
{
    private readonly string _key;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly int _expiration;
    public TokenHandler(string key, string issuer, string audience, int expiration) 
    {
        _key = key;
        _issuer = issuer;
        _audience = audience;
        _expiration = expiration;
    }
    public string Access(IEnumerable<Claim> claims)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        var signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var tokenOptions = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(_expiration),
            signingCredentials: signInCredentials
        );

        var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        return token;
    }
}
