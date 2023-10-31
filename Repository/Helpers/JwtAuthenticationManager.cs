using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Repository.Helpers;
using Repository.Models;

namespace API.Helpers
{
    public class JwtAuthenticationManager
    {
        private readonly ECDsa key;

        public JwtAuthenticationManager(ECDsa key)
        {
            this.key = key;
        }

        //JWTs con ES256, lo cual significa que se requerira una llave ECDSA
        //(Elliptic Curve Digital Signature Algorithm)
        //que use la curva NIST’s P-256 (tambien conocida como secp256r1)
        public string generarJWT(ClaimsIdentity identity)
        {
            string codigoUsuario = "";

            if (identity != null)
            {
                codigoUsuario = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1";
            } else
            {
                return "";
            }

            if ((codigoUsuario == "")) return "";

            return crearToken(codigoUsuario);
        }

        public string generarJWT(string codigoUsuario)
        {
            if ((codigoUsuario == "") || (codigoUsuario == null)) return "";

            return crearToken(codigoUsuario);
        }

        private string crearToken(string codigoUsuario)
        {
            var now = DateTime.UtcNow;
            var handler = new JsonWebTokenHandler();

            var claims = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, codigoUsuario)
                    });

            string token = handler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = claims,
                Audience = "CUPOS",
                NotBefore = now,
                Expires = DateTime.Now.AddHours(1),
                //Expires = DateTime.Now.AddMinutes(15),
                IssuedAt = now,
                SigningCredentials = new SigningCredentials(new ECDsaSecurityKey(key), "ES256")
            });

            return token;
        }
    }
}
