using AbankBackend.Data;
using Npgsql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configuración de JWT
var key = Encoding.ASCII.GetBytes("Nuqf5pKL2RzIrj6t5dn23ZZjW3XHc4jS");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddSingleton<DbContextDapper>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();


//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();

//builder.Services.AddSingleton<DbContextDapper>();

//var app = builder.Build();


//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//using var conn = new NpgsqlConnection(connectionString);
//try
//{
//    conn.Open();
//    Console.WriteLine("Conexión exitosa a PostgreSQL");
//}
//catch (Exception ex)
//{
//    Console.WriteLine($"Error de conexión: {ex.Message}");
//}

//// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();
