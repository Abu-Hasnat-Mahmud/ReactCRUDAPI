using ReactCRUDAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ReactCRUDAPI.Services
{
    public interface IUserService
    {
        public string Authenticate(string UserName, string Password);
        public User GetUser(int EmpId);
    }

    public class UserService : IUserService
    {
        private static List<User> users = new List<User>()
        {
            new User(){Id=1, UserName="Admin",Password="12345"}
        };

       
        public string Authenticate(string UserName, string Password)
        {
            var user = users.Where(w => w.UserName == UserName && w.Password.ToUpper() == Password.ToUpper()).FirstOrDefault();

            if (user == null) return null;

            return generateJwtToken(user);           
        }


        public User GetUser(int userId)
        {
            return users.Where(w => w.Id == userId).FirstOrDefault();
        }

        private string generateJwtToken(User user)
        {           
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("THIS IS THE SECRET FOR CREATE JWT TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("Id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
