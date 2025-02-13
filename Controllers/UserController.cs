using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Dapper;
using AbankBackend.Data;
using AbankBackend.Models;

namespace AbankBackend.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UsersController : ControllerBase
    {
        private readonly DbContextDapper _dbContext;

        public UsersController(DbContextDapper dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            string sql = "SELECT * FROM public.tblusuarios";
            using var connection = _dbContext.CreateConnection();
            var users = await connection.QueryAsync<User>(sql);
            return Ok(users);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetUser(int id)
        {
            string sql = "SELECT * FROM public.tblusuarios WHERE id = @Id";
            using var connection = _dbContext.CreateConnection();
            var user = await connection.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            string sql = "INSERT INTO public.tblusuarios (nombres, apellidos, fechanacimiento, direccion, telefono, email, password, fechacreacion) " +
                         "VALUES (@Nombres, @Apellidos, @FechaNacimiento, @Direccion, @Telefono, @Email, @Password, CURRENT_DATE)";
            using var connection = _dbContext.CreateConnection();
            var result = await connection.ExecuteAsync(sql, user);
            return result > 0 ? StatusCode(201) : BadRequest();
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            string sql = "UPDATE public.tblusuarios SET nombres = @Nombres, apellidos = @Apellidos, fechanacimiento = @FechaNacimiento, " +
                         "direccion = @Direccion, telefono = @Telefono, email = @Email, password = @Password, fechamodificacion = CURRENT_DATE " +
                         "WHERE id = @Id";
            using var connection = _dbContext.CreateConnection();
            var result = await connection.ExecuteAsync(sql, new { user.Nombres, user.Apellidos, user.FechaNacimiento, user.Direccion, user.Telefono, user.Email, user.Password, Id = id });
            return result > 0 ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {
            string sql = "DELETE FROM public.tblusuarios WHERE id = @Id";
            using var connection = _dbContext.CreateConnection();
            var result = await connection.ExecuteAsync(sql, new { Id = id });
            return result > 0 ? Ok() : NotFound();
        }
    }
}
