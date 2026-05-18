using Application.DTOs;
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace TicketApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly JwtService _jwtService;

        public AuthController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            JwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }

        // -------------------------
        // REGISTER                       // PREGUNTAR DONDE METEMOS LA LOGICA DEL LOGIN , O SI POR SER LOGIS ESTAN BIEN QUE ESTE UN POCO CARGADOR LOS CONTROLLER
        // -------------------------
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new User
            {
                UserName = dto.UserName,
                Email = dto.Email,
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            // Asignar rol User
            await _userManager.AddToRoleAsync(user, "User");

            return Ok(new
            {
                message = "Usuario creado correctamente"
            });
        }

        // -------------------------
        // LOGIN  
        // -------------------------
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
                return Unauthorized("Usuario no existe");

            var result = await _signInManager.CheckPasswordSignInAsync(
                user,
                dto.Password,
                false
            );

            if (!result.Succeeded)
                return Unauthorized("Credenciales inválidas");

            var token = await _jwtService.GenerateToken(user);

            return Ok(new
            {
                token,
                user = new
                {
                    user.Id,
                    user.UserName,
                    user.Email
                }
            });
        }
    }
}
