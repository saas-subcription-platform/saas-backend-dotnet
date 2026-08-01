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


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp",
                    policy =>
                    {
                        policy
                        .WithOrigins("http://localhost:5173")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });



            builder.Services.AddControllers();



            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                ));



            builder.Services.AddScoped<IGoalRepository, GoalRepository>();
            builder.Services.AddScoped<IGoalService, GoalService>();


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();



            var app = builder.Build();



            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }



            app.UseCors("AllowReactApp");



            app.UseHttpsRedirection();


            app.UseAuthorization();


            app.MapControllers();


            app.Run();

        }
    }
}