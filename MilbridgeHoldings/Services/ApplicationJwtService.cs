namespace ModelLibrary.Services
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using ModelLibrary.UserManagementAPI.Data;
    using Models.Data;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    public class ApplicationJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
     
        public ApplicationJwtService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public string JwtTokenBuilder(ApplicationUser user, IList<string> roles)
        {
            List<Claim> claims = new();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new Claim(ClaimTypes.Authentication, Guid.NewGuid().ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes("afrosoftcrisismodifiersecretkeyandivwerwwerwmkmkmkmkmkmkmkmkmkmkmkmkmkmkmkmkmkmkmkmkmkmkmkmkmkmkm")), SecurityAlgorithms.HmacSha256Signature),

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string JwtTokenBuilderMember(ModelLibrary.UserManagementAPI.Data.Member member)
        {


            List<Claim> claims2 = new();
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, member.Id.ToString()),

                }),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes("afrosoftcrisismodifiersecretkeyandivwerwwerwmkmkmkmkmkmkmkmkmkmkmkmkmkmkmkmkmkmkmkmkmkmkmkmkmkmkm")),
                SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }



    }
}
