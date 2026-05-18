using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Identity
{
    public class AuthService
    {
        public readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }


        // REGISTER

        public async Task<AuthResponse> RegisterAsync(
            string email,
            string password,
            string usuario)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);

            if (existingUser != null)
            {
                return new AuthResponse
                {
                    Success = false,
                    Message = "El email ya está registrado"
                };
            }

            var user = new User
            {
                UserName = usuario,
                Email = email,
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                return new AuthResponse
                {
                    Success = false,
                    Message = string.Join(", ", result.Errors.Select(e => e.Description))
                };
            }

            var token = GenerateToken(user);

            return new AuthResponse
            {
                Success = true,
                Message = "Usuario registrado correctamente",
                Token = token,
                Email = user.Email!,
                Username = user.UserName!
            };
        }
        // LOGIN
        public async Task<AuthResponse> LoginAsync(
            string email,
            string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return new AuthResponse
                {
                    Success = false,
                    Message = "Usuario no encontrado"
                };
            }

            var validPassword = await _userManager.CheckPasswordAsync(user, password);

            if (!validPassword)
            {
                return new AuthResponse
                {
                    Success = false,
                    Message = "Contraseña incorrecta"
                };
            }

            var token = GenerateToken(user);

            return new AuthResponse
            {
                Success = true,
                Message = "Login exitoso",
                Token = token,
                Email = user.Email!,
                Username = user.UserName!
            };
        }


        // JWT TOKEN
        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email),

        };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
