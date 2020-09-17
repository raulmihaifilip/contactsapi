using ContactsApi.Core.Entities;
using ContactsApi.Core.Interfaces;
using ContactsApi.Core.Models;
using ContactsApi.Core.ViewModels;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ContactsApi.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly AuthOptions _authOptions;
        public AuthService(IAuthRepository authRepository, IOptions<AuthOptions> authOptionsAccessor)
        {
            _authRepository = authRepository;
            _authOptions = authOptionsAccessor.Value;
        }

        public async Task RegisterAsync(RegisterViewModel registerViewModel)
        {
            var userExists = await _authRepository.UserExistsAsync(registerViewModel.Username);

            if (userExists)
            {
                throw new ValidationException("This user already exists.");
            }

            var (passwordHash, passwordSalt) = ComputePassword(registerViewModel.Password);
            var user = new User()
            {
                Username = registerViewModel.Username,
                PasswordSalt = passwordSalt,
                PasswordHash = passwordHash
            };

            await _authRepository.AddUserAsync(user);
        }

        public async Task<string> LoginAsync(LoginViewModel loginViewModel)
        {
            var user = await _authRepository.GetUserAsync(loginViewModel.Username);

            if (user == null)
            {
                throw new UnauthorizedAccessException("You have entered an invalid username.");
            }

            if (!ValidatePassword(loginViewModel.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new UnauthorizedAccessException("You have entered an invalid password.");
            }

            var authClaims = new[]
            {
                  new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                  new Claim(ClaimTypes.Name, user.Username)
            };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.SecureKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                issuer: _authOptions.Issuer,
                audience: _authOptions.Audience,
                expires: DateTime.Now.AddHours(_authOptions.ExpiresInMinutes),
                claims: authClaims,
                signingCredentials: signingCredentials
                );

            var tokenHandler = new JwtSecurityTokenHandler();
            var serializedToken = tokenHandler.WriteToken(token);

            return serializedToken;
        }

        private (byte[] passwordHash, byte[] passwordSalt) ComputePassword(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                return (hmac.ComputeHash(Encoding.UTF8.GetBytes(password)), hmac.Key);
            }
        }

        private bool ValidatePassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (var index = 0; index < computedHash.Length; index++)
                {
                    if (computedHash[index] != passwordHash[index])
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }
}
