using Microsoft.Extensions.Configuration;
using ParkingGarageSystem.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ParkingGarageSystem.Interfaces;
using ParkingGarageSystem.Services;
using AutoMapper;
using ParkingGarageSystem.ViewModels;
using ParkingGarageSystem.Models;

namespace ParkingGarageSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IUserManagements, UserManagementsService>();
            builder.Services.AddScoped<IReservation, ReservationService>();
            builder.Services.AddDbContext<ParkingSystemDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RegisterViewModel, User>();
            });
            var mapper = config.CreateMapper();
            builder.Services.AddSingleton(mapper);
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}