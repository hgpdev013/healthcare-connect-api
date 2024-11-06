using apihealthcareconnect.Infraestrutura;
using apihealthcareconnect.Interfaces;
using apihealthcareconnect.Repositories;
using apihealthcareconnect.ResponseMappings;
using Microsoft.EntityFrameworkCore;

namespace apihealthcareconnect
{
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
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
                options.UseMySql(
                    builder.Configuration.GetConnectionString("HealthcareConnect"),
                    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("HealthcareConnect"))
                )
            );

            builder.Services.AddControllers();
            builder.Services.AddScoped<IUserTypeRepository, UserTypeRepository>();
            builder.Services.AddScoped<IUserTypePermissionsRepository, UserTypePermissionsRepository>();
            builder.Services.AddScoped<ISpecialtyTypeRepository, SpecialtyTypeRepository>();
            builder.Services.AddScoped<IUsersRepository, UsersRepository>();
            builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
            builder.Services.AddScoped<IPacientRepository, PacientRepository>();
            builder.Services.AddScoped<IAllergiesRepository, AllergiesRepository>();
            builder.Services.AddScoped<IAppointmentsRepository, AppointmentsRepository>();
            builder.Services.AddScoped<IAppointmentsReturnRepository, AppointmentsReturnRepository>();
            builder.Services.AddScoped<AppointmentResponseMapping>();


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowAllOrigins");

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}