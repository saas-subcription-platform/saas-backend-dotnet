using Microsoft.EntityFrameworkCore;
using Timesheets.Data;
using Timesheets.Mapping;
using Timesheets.Repositories.Implementations;
using Timesheets.Repositories.Interfaces;
using Timesheets.Services.Interfaces;

namespace Timesheets
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Database Configuration
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection")));

            // cors
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("ReactPolicy", policy =>
                {
                    policy.WithOrigins("http://localhost:5173")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            // Registering repository
            builder.Services.AddScoped<ITimesheetRepository, TimesheetRepository>();

            // Registering service
            builder.Services.AddScoped<ITimesheetService, TimesheetService>();

            // Add services to the container
            builder.Services.AddControllers();

            // Swagger Configuration
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Automapper 
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("ReactPolicy");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}