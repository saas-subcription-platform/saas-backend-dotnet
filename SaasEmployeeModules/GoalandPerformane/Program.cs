using GoalandPerformance.Data;
using Microsoft.EntityFrameworkCore;
using GoalandPerformance.Repositories.Interfaces;
using GoalandPerformance.Repositories.Implementations;
using GoalandPerformance.Services.Interfaces;
using GoalandPerformance.Services.Implementations;

namespace GoalandPerformance
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql( builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IGoalRepository, GoalRepository>();
            builder.Services.AddScoped<IGoalService, GoalService>();


            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
