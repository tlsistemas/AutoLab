using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace AutoLab.Utils
{
    public static class Security
    {
        public static string secretTokenJWT = "fedaf7d8863b48e197b9287d492b708e";
        public static String NewKey()
        {
            return Guid.NewGuid().ToString("n");
        }
        public static String NewCode()
        {
            var random = new Random();
            var code = "";
            while (code.Length < 4)
                code += random.Next(0, 9).ToString();

            return code;
        }
        public static String NewCodeInSix()
        {
            var random = new Random();
            var code = "";
            while (code.Length < 6)
                code += random.Next(0, 9).ToString();

            return code;
        }
        public static String NewTokenGeo(String ip)
        {
            try
            {
                var array = ip.Split('.');

                var token = array[0] + (array[0].Length * 9).ToString() +
                    array[2] + (array[2].Length * 5).ToString() +
                    array[1] + (array[1].Length * 7).ToString() +
                    array[3] + (array[3].Length * 3).ToString() +
                    (ip.Length * 3).ToString();

                byte[] bytes = new byte[token.Length];
                return Convert.ToBase64String(Encoding.UTF8.GetBytes(token));
            }
            catch (Exception)
            {
                return "";
            }
        }
        public static Boolean ValidTokenGeo(String token, String ip)
        {
            if (String.IsNullOrEmpty(token) || String.IsNullOrEmpty(ip)) return false;
            return token.Equals(NewTokenGeo(ip));
        }
        public static String NewCodeEmail()
        {
            try
            {
                byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
                byte[] key = Guid.NewGuid().ToByteArray();
                string token = Regex.Replace(Convert.ToBase64String(time.Concat(key).ToArray()).EncryptSHA256(), "[^0-9a-zA-Z]+", "").Substring(0, 6);
                return token;
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public static string GenerateTokenJWT(string value, string type, DateTime expires)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretTokenJWT);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, value),
                    new Claim(ClaimTypes.Role, type)
                }),
                Expires = expires, // DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static string GetNameTokenJWT(string token)
        {
            string secret = secretTokenJWT;
            var key = Encoding.ASCII.GetBytes(secret);
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            var claims = handler.ValidateToken(token, validations, out var tokenSecure);
            return claims.Identity.Name;
        }
    }
}
