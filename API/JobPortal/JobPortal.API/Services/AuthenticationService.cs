
using JobPortal.API.Models;
using JobPortal.API.Models.DTOs;
using JobPortal.API.Models.Enums;
using JobPortal.API.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JobPortal.API.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public AuthenticationService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        public async Task<Token?> Authenticate(UserLoginDTO? user)
        {
            if (user == null || user.Email == null || user.Password == null
                || user.Email == "" || user.Password == "")
            {
                throw new Exception("You must provide an email and password");
            }

            var userInDb = await _unitOfWork.Users.GetUserByEmail(user.Email);
            if (userInDb == null)
            {
                throw new Exception("User doesn't exist");
            }

            string salt = userInDb.PasswordSalt;
            string hashedPassword;
            if (salt != null)
            {
                hashedPassword = HashPassword(user.Password, salt);
            }
            else return null;

            userInDb = await _unitOfWork.Users.GetUserByEmailAndHashedPassword(user.Email, hashedPassword);

            if (userInDb == null)
            {
                throw new Exception("Incorrect password");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, userInDb.Email),
                    new Claim(ClaimTypes.Role, userInDb.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"]
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            //var refreshToken = GenerateRefreshToken();
            //SetRefreshToken(refreshToken);
            return new Token { TokenString = tokenHandler.WriteToken(token) };
        }
        /*
        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddMinutes(20),
                Created = DateTime.Now
            };
            return refreshToken;
        }
        public User user;
        private void SetRefreshToken(RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };

            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            user.RefreshToken = newRefreshToken.Token;
            user.Created = newRefreshToken.Created;
            user.Expires = newRefreshToken.Expires;
        }
        */

        public async Task<User> Register(UserRegisterDTO user)
        {
            if (user == null || user.Email == "" || user.Password == ""
                || user.Email == null || user.Password == null)
            {
                throw new Exception("Must enter an email and password");
            }

            if (user.Role.ToLower() != "admin" && user.Role.ToLower() != "user")
            {
                throw new Exception("Role must be admin or user");
            }

            var userInDb = await _unitOfWork.Users.GetUserByEmail(user.Email);

            if (userInDb != null)
            {
                throw new Exception("User with this email already exists");
            }

            int employee = user.EmployeeId;
            string email = user.Email;
            string salt = GenerateSalt();
            string hashedPassword = HashPassword(user.Password, salt);
            Role role = (Role)Enum.Parse(typeof(Role), user.Role.ToLower());
            

            User newUser = new(employee, email, hashedPassword, salt, role);
            return newUser;
        }

        public string HashPassword(string password, string salt)
        {
            byte[] saltByte = Convert.FromBase64String(salt);

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: saltByte,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 32));

            return hashed;
        }
        public string GenerateSalt()
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return Convert.ToBase64String(salt);
        }
        
        public object GetById(int userId)
        {
            var user = _unitOfWork.Users.GetById(userId);
            if (user == null)
            {
                return null;
            }
            return user;
        }
        
    }
}

