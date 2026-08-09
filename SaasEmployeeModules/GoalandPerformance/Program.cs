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
                        .WithOrigins("http://localhost:5173","http://16.192.104.21")
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


            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                db.Database.Migrate();
            }
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }



            app.UseCors("AllowReactApp");



           // app.UseHttpsRedirection();


            app.UseAuthorization();


            app.MapControllers();


            app.Run();

        }
    }
}