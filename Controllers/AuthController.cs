using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AbankBackend.Data;
using Dapper;
using AbankBackend.Models;

namespace AbankBackend.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly DbContextDapper _dbContext;
        private readonly IConfiguration _config;

        public AuthController(DbContextDapper dbContext, IConfiguration config)
        {
            _dbContext = dbContext;
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            string sql = "SELECT * FROM public.tblusuarios WHERE telefono = @Telefono AND password = @Password";
            using var connection = _dbContext.CreateConnection();
            var user = connection.QueryFirstOrDefault<User>(sql, new { request.Telefono, request.Password });

            if (user == null)
                return Unauthorized(new { message = "Credenciales incorrectas" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Name, user.Email)
        }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(token);

            var response = new
            {
                user = new
                {
                    id = user.Id,
                    nombres = user.Nombres,
                    apellidos = user.Apellidos,
                    session_active = true,
                    fecha_nacimiento = user.FechaNacimiento.ToString("yyyy-MM-dd"),
                    email = user.Email,
                    telefono = user.Telefono,
                    password = user.Password,
                    address = user.Direccion
                },
                access_token = accessToken,
                token_type = "bearer"
            };

            return Ok(response);
        }

    }
}

public class LoginRequest
{
    public string Telefono { get; set; }
    public string Password { get; set; }
}

