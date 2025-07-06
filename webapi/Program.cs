using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using webapi;
using webapi.Models.Context;

public class Program
{
    public static void Main(string[] args)
    {
        // Настройка логирования через Serilog
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .WriteTo.File("logs/app-.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        try
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseSerilog();

            // Загрузка конфигурации из config.json
            var solutionDir = Path.Combine(Directory.GetCurrentDirectory(), "..");
            var configPath = Path.Combine(solutionDir, "config.json");

            if (File.Exists(configPath))
            {
                builder.Configuration
                    .SetBasePath(solutionDir)
                    .AddJsonFile("config.json", optional: false, reloadOnChange: true);
            }

            // Настройка CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp", policy =>
                {
                    policy.WithOrigins("http://localhost:5173")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });

            // JWT аутентификация
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var jwtSection = builder.Configuration.GetSection("Jwt");
                    var secret = Encoding.ASCII.GetBytes(jwtSection["SecretKey"] ?? "<YourSecretKey>");

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSection["Issuer"],
                        ValidAudience = jwtSection["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(secret)
                    };
                });

            builder.Services.AddAuthorization();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMemoryCache();

            // Подключение к базе
            var connStr = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicContext>(opt => opt.UseNpgsql(connStr));

            // Регистрация базовых сервисов (пример)
            builder.Services.AddScoped<JwtService>();
            builder.Services.AddScoped<IUserServ, UserServ>();

            var app = builder.Build();

            // Middleware
            app.UseMiddleware<ExLogging>();
            app.UseCors("AllowReactApp");
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Ошибка при запуске приложения");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}