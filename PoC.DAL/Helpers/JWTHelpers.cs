using Microsoft.IdentityModel.Tokens;
using PoC.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PoC.DAL.Helpers
{
    public static class JwtHelpers
    {
        //---Claims y Tokens de Usuarios del sistema con licencia--------------
        public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, Guid Id)
        {
            IEnumerable<Claim> claims = new Claim[]
            {
                
                new Claim(ClaimTypes.Email, userAccounts.EMail),
                new Claim(ClaimTypes.NameIdentifier,Id.ToString()),
                new Claim(ClaimTypes.Expiration, DateTime.Now.AddHours(1).ToString("MMM ddd dd yyyy HH:mm:ss tt"))
            };
            return claims;
        }

        public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, out Guid Id)
        {
            Id = Guid.NewGuid();
            return GetClaims(userAccounts, Id);
        }

        public static UserTokens GetToken(UserTokens model, JwtSettings jwtSettings)
        {
            try
            {
                var usertoken = new UserTokens();
                if (model == null)
                    throw new ArgumentException(nameof(model));

                var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);
                Guid Id = Guid.Empty;
                DateTime expireTime = DateTime.Now.AddHours(16);
                usertoken.Validity = expireTime.TimeOfDay;
                var jwtToken = new JwtSecurityToken(issuer: jwtSettings.ValidIssuer, audience: jwtSettings.ValidAudience, claims: GetClaims(model, out Id), notBefore: new DateTimeOffset(DateTime.Now).DateTime, expires: new DateTimeOffset(expireTime).DateTime, signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256));
                usertoken.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
                usertoken.EMail = model.EMail;
                usertoken.GuidId = Id;
                return usertoken;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
