using System.Data;
using Npgsql;
using Microsoft.Extensions.Configuration;

namespace AbankBackend.Data
{
    public class DbContextDapper
    {
        private readonly IConfiguration _config;
        public DbContextDapper(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(_config.GetConnectionString("DefaultConnection"));
        }
    }
}
