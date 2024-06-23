using G4.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace G4.WebApplication.Authentication {
    public static class JWTAuthentication {

        private static string Issuer = "";
        private static string Audience = "";
        private static string SecretKey = "";

        public static string GenerateToken(User user) {

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim("Identifier", user.Identifier.ToString())
                    
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = Issuer,
                Audience = Audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;

        }

        public static void AddJWT(this WebApplicationBuilder builder) {

            var authentication = builder.Configuration.GetSection("Authentication");
            if (authentication == null)
                throw new Exception("JWT authentication cannot be configured, 'Authentication' section not found in AppSettings.");

            var jwt = authentication.GetSection("JWT");
            if (jwt == null)
                throw new Exception("JWT authentication cannot be configured, 'Authentication' section not found in AppSettings.");

            var issuer = jwt.GetValue<string>("Issuer");
            var audience = jwt.GetValue<string>("Audience");
            var secretKey = jwt.GetValue<string>("SecretKey");

            if (string.IsNullOrEmpty(issuer))
                throw new Exception("JWT authentication cannot be configured, 'Authentication.JWT.Issuer' value not found in AppSettings.");

            if (string.IsNullOrEmpty(audience))
                throw new Exception("JWT authentication cannot be configured, 'Authentication.JWT.Audience' value not found in AppSettings.");

            if (string.IsNullOrEmpty(secretKey))
                throw new Exception("JWT authentication cannot be configured, 'Authentication.JWT.SecretKey' value not found in AppSettings.");

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });

            Issuer = issuer;
            Audience = audience;
            SecretKey = secretKey;

        }


    }
}
