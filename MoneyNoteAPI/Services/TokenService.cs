using System;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using MoneyNoteLibrary5.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MoneyNoteAPI.Services
{
    public class TokenService
    {
        public TokenService()
        {

        }

        //public IResult CreateToken(User user)
        //{
        //    if (user.Name == "joydip" && user.Password == "joydip123")
        //    {
        //        // appSetting.json에서 값 가져오는 방법 찾기
        //        var issuer = builder.Configuration["Jwt:Issuer"];
        //        var audience = builder.Configuration["Jwt:Audience"];
        //        var key = Encoding.ASCII.GetBytes
        //        (builder.Configuration["Jwt:Key"]);
        //        var tokenDescriptor = new SecurityTokenDescriptor
        //        {
        //            Subject = new ClaimsIdentity(new[]
        //            {
        //        new Claim("Id", Guid.NewGuid().ToString()),
        //        new Claim(JwtRegisteredClaimNames.Sub, user.Name),
        //        new Claim(JwtRegisteredClaimNames.Email, user.Name),
        //        new Claim(JwtRegisteredClaimNames.Jti,
        //        Guid.NewGuid().ToString())
        //     }),
        //            Expires = DateTime.UtcNow.AddMinutes(5),
        //            Issuer = issuer,
        //            Audience = audience,
        //            SigningCredentials = new SigningCredentials
        //            (new SymmetricSecurityKey(key),
        //            SecurityAlgorithms.HmacSha512Signature)
        //        };
        //        var tokenHandler = new JwtSecurityTokenHandler();
        //        var token = tokenHandler.CreateToken(tokenDescriptor);
        //        var jwtToken = tokenHandler.WriteToken(token);
        //        var stringToken = tokenHandler.WriteToken(token);
        //        return Results.Ok(stringToken);
        //    }
        //    return Results.Unauthorized();
        //}
    }
}

