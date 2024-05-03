using ApiCofidisMicroCredit.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiCofidisMicroCredit.Repository
{
    public class TokenGeneratorRepository : ITokenGeneratorRepository
    {
        private readonly string _secretKey;
        private readonly string _issuer;

        public TokenGeneratorRepository()
        {
            var _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            _secretKey = _config["JwtSettings:Key"];
            _issuer = _config["JwtSettings:Issuer"];
        }

        public string GenerateToken(string clientId)
        {
            var key = new SymmetricSecurityKey(Convert.FromBase64String(_secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim("clientId", clientId),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
        };

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _issuer,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
