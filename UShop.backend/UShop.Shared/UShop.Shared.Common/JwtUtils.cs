using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace UShop.Shared.Common
{
    public static class JwtUtils
    {
        public static string GenerateJwtToken(string userId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigUtils.Instance.Get("Token:SecretKey")));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: ConfigUtils.Instance.Get("Token:Issuer"),
                audience: ConfigUtils.Instance.Get("Token:Audience"),
                claims: claims,
                expires: DateTime.Now.AddMinutes(ConfigUtils.Instance.Get<int>("Token:Expires")),
                signingCredentials: credentials
            );

            return $"{new JwtSecurityTokenHandler().WriteToken(token)}" ;
        }

        public static string? GetUserId(IHttpContextAccessor httpContextAccessor)
        {
            var token = "";
            var httpContext = httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                var headers = httpContext.Request.Headers;
                var Authorization = headers["Authorization"].FirstOrDefault();
                if (Authorization != null)
                {
                    if (Authorization.Contains("Bearer"))
                    {
                        token = Authorization.Replace("Bearer ", "");
                    }
                    else
                    {
                        return null;
                    }

                }
            }
            if (!string.IsNullOrEmpty(token))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                try
                {
                    var jwtToken = tokenHandler.ReadJwtToken(token);
                    var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);

                    return userIdClaim?.Value;
                }
                catch
                {
                    return null; // Token 解析失败
                }
            }
            return null;
        }
    }
}
