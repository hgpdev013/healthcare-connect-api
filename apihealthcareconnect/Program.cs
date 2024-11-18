using apihealthcareconnect.Infraestrutura;
using apihealthcareconnect.Interfaces;
using apihealthcareconnect.Repositories;
using apihealthcareconnect.ResponseMappings;
using apihealthcareconnect.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace apihealthcareconnect
{
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration
                .AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            builder.Services.AddDbContext<ConnectionContext>(options =>
            {
                string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING_PRODUCTION");

                if (string.IsNullOrEmpty(connectionString))
                {
                    connectionString = builder.Configuration.GetConnectionString("HealthcareConnect");
                }

                options.UseMySql(
                    connectionString,
                    ServerVersion.AutoDetect(connectionString)
                );
            }
            );

            builder.Services.AddScoped<IUserTypeRepository, UserTypeRepository>();
            builder.Services.AddScoped<IUserTypePermissionsRepository, UserTypePermissionsRepository>();
            builder.Services.AddScoped<ISpecialtyTypeRepository, SpecialtyTypeRepository>();
            builder.Services.AddScoped<IUsersRepository, UsersRepository>();
            builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
            builder.Services.AddScoped<IPacientRepository, PacientRepository>();
            builder.Services.AddScoped<IAllergiesRepository, AllergiesRepository>();
            builder.Services.AddScoped<IAppointmentsRepository, AppointmentsRepository>();
            builder.Services.AddScoped<IAppointmentsReturnRepository, AppointmentsReturnRepository>();
            builder.Services.AddScoped<IExamsRepository, ExamsRepository>();
            builder.Services.AddScoped<IPrescriptionsRepository, PrescriptionsRepository>();
            builder.Services.AddScoped<AppointmentResponseMapping>();
            builder.Services.AddScoped<UserResponseMapping>();
            builder.Services.AddScoped<ExamResponseMapping>();
            builder.Services.AddScoped<PrescriptionResponseMapping>();
            builder.Services.AddScoped<TokenService>();
            builder.Services.AddScoped<EmailService>();

            var jwtSettings = builder.Configuration.GetSection("JwtSettings");

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
                };
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Insira o token JWT com o prefixo 'Bearer' em seu cabeçalho",
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });


            var app = builder.Build();

            //if (app.Environment.IsDevelopment()) {
            app.UseSwagger();
            app.UseSwaggerUI();
            //}

            app.UseHttpsRedirection();

            app.UseCors("AllowAllOrigins");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}