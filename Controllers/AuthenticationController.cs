using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using project_backend.Interfaces;
using project_backend.Models;
using project_backend.Schemas;
using project_backend.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace project_backend.Controllers
{
    [ApiController]
    [Route("api/Auth")]
    public class AuthenticationController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IUser _userService;
        private readonly IEmployee _employeeService;

        public AuthenticationController(IConfiguration configuration, IUser userService, IEmployee employeeService)
        {
            _configuration = configuration;
            _userService = userService;
            _employeeService = employeeService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<ActionResult<AuthResponse>> Token([FromBody] AuthRequest user)
        {
            // Validar datos de entrada
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userFromBD = await _userService.GetByEmail(user.Email);

            if (userFromBD == null || !SecurityUtils.CheckPassword(userFromBD.Password, user.Password))
            {
                return Unauthorized(new
                {
                    message = "Correo electrónico o contraseña incorrecta"
                });
            }

            var token = GenerateJWTToken(userFromBD);

            return Ok(new AuthResponse
            {
                AccessToken = token
            });
        }

        [HttpGet]
        [Authorize]
        [Route("GetCurrentUser")]
        public async Task<ActionResult<EmployeeGet>> GetCurrentUser()
        {
            var identity = User.Identity as ClaimsIdentity;
            var id = int.Parse(identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value);
            var currentUser = (await _employeeService.GetById(id)).Adapt<EmployeeGet>();

            return Ok(currentUser);
        }

        private string GenerateJWTToken(User user)
        {
            // Obtenemos nuestra secretKey en formato de bytes
            var secretKeyBytes = Encoding.UTF8.GetBytes(_configuration["JWTSettings:SecretKey"]);

            var employee = user.Employee.Adapt<EmployeeGet>();

            // Declaramos una lista de claims las cuáles serán información de nuestro token
            var claimsList = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, employee.Id.ToString()),
                new Claim(ClaimTypes.Role, employee.Role.RoleName)
            };

            // Se añade algunas configuraciones al token, como la expiración y los claims
            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration["JWTSettings:Issuer"],
                audience: _configuration["JWTSettings:Audience"],
                claims: claimsList,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(secretKeyBytes),
                    SecurityAlgorithms.HmacSha256)
            );

            // Escribimos el token en formato de string
            var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            return token;
        }
    }
}
