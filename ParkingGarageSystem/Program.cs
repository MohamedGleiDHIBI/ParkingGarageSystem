using Microsoft.Extensions.Configuration;
using ParkingGarageSystem.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ParkingGarageSystem.Interfaces;
using ParkingGarageSystem.Services;
using AutoMapper;
using ParkingGarageSystem.Models;
using ParkingGarageSystem.ViewModels.Garage;
using ParkingGarageSystem.ViewModels.Vehicle;
using ParkingGarageSystem.ViewModels.LocationView;
using ParkingGarageSystem.ViewModels.User;
using ParkingGarageSystem.ViewModels.History;

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
            builder.Services.AddScoped<IGarage, GarageService>();
            builder.Services.AddScoped<IHistory, HistoryService>();
            builder.Services.AddScoped<ILocation, LocationService>();
            builder.Services.AddScoped<IVehicle, VehicleService>();
            builder.Services.AddDbContext<ParkingSystemDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RegisterViewModel, User>();
                cfg.CreateMap<AddGarageViewModel,Garage>().ForMember(g=>g.Id,opt=>opt.Ignore());
                cfg.CreateMap<VehicleViewModel, Vehicle>();
                cfg.CreateMap<LocationModelView,Location>();
                cfg.CreateMap<HistoryModelView,History>();
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