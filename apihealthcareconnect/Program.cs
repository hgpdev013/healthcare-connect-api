using apihealthcareconnect.Interfaces;
using apihealthcareconnect.Repositories;
using apihealthcareconnect.ViewModel;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace apihealthcareconnect
{
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
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
            builder.Services.AddControllers();
            builder.Services.AddTransient<IUserTypeRepository, UserTypeRepository>();
            builder.Services.AddTransient<IUsersRepository, UsersRepository>();
            builder.Services.AddTransient<IDoctorsRepository, DoctorsRepository>();
            builder.Services.AddTransient<ISpecialtyTypeRepository, SpecialtyTypeRepository>();

            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

            //builder.Services.AddTransient<>
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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