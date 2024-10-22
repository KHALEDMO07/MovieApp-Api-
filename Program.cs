using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Project_1.Models;
using Project_1.Services;
namespace Project_1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(
                options => 
                options.UseSqlServer(ConnectionString)
                );

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddCors();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1" , 
                    Title = "MovieApi", 
                    Description = "My first api"
                });
            });
            builder.Services.AddScoped<IGenresService, GenresService>();
            builder.Services.AddScoped<IMovieService, MovieService>();
            builder.Services.AddAutoMapper(typeof(Program));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
