using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoC.Models;
using PoC.DAL.Helpers;
namespace PoC.DAL.Services
{
    public class UsuarioSistemaService
    {
        public JwtSettings _jwtSettings;

        public UsuarioSistemaService()
        {
            _jwtSettings = new JwtSettings
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = "64A63153-11C1-4919-9133-EFAF99A9B456",
                ValidateIssuer = false,
                ValidIssuer = "https://localhost",
                ValidateAudience = true,
                ValidAudience = "https://localhost",
                RequireExpirationTime = true,
                ValidateLifetime = true
            };
        }

        public UsuarioSistema? Login(string email, string password)
        {
            UsuarioSistema usuarioSistema = new UsuarioSistema();
            if(email.Contains("@issatec.com") || email.Contains("@unisabana.edu.co"))
            {
                usuarioSistema.email = email;
                var token = new UserTokens();
                token = JwtHelpers.GetToken(new UserTokens()
                {
                    EMail = email,
                    GuidId = Guid.NewGuid(),
                }, _jwtSettings);

                usuarioSistema.token = token.Token;
                return usuarioSistema;
            }
            return null;
        }
    }
}
