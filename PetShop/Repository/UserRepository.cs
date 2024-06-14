using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PetShop.Data;
using PetShop.Interfaces;
using PetShop.Models;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PetShop.Repository
{
    public class UserRepository: IUserRepository
    {
        private readonly IConfiguration _configuration;
        private readonly PetShopContext _context;
        public UserRepository(IConfiguration configuration, PetShopContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        public User Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(a => a.Email == email);
            if (user == null || !VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }
        public User CreateUser(User user, string password)
        {
            HashPassword(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _context.Users.Add(user);
            _context.SaveChanges();

            return user;

        }

        public User GetUserById(int id)
        {
            return _context.Users.SingleOrDefault(x => x.UserId == id);
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public void UpdateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void DeleteUserById(int id)
        {
            User user = _context.Users.FirstOrDefault(x => x.UserId == id);
            if(user != null)
            {
                _context.Users.Remove(user);
            }
        }

        public void SetRefreshToken(User user, RefreshToken refreshToken)
        {
            user.RefreshToken = refreshToken.Token;

            _context.SaveChanges();
        }

        public string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.Role, "User")
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow
            };
            return refreshToken;
        }

        public void OrderPet(int userId, int petId)
        {
            var user = GetUserById(userId);

            if(user == null) { throw new ArgumentException("User not found"); }

            Order order = new Order
            {
                UserId = userId,
                PetId = petId,
                OrderDate = DateTime.Now
            };

            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        private void HashPassword(string pass, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(pass));
            }
        }
        private bool VerifyPassword(string Pass, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Pass));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] == storedHash[i]) return true;
                }
            }
            return false;
        }
    }
}
