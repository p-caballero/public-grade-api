namespace GradeApi
{
    using GradeApi.Infrastructure;
    using GradeApi.Persistence;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Diagnostics;

    public class Program
    {
        /// <summary>
        /// Cadena de conexión contra localdb
        /// </summary>
        //const string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Database=Grade;Integrated Security=true;";

        /// <summary>
        /// Cadena de conexión contra localhost
        /// </summary>
        private const string ConnectionString = "Data Source=.;Database=Grade;Integrated Security=true;";

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<GradeDbContext>(options => options.UseSqlServer(ConnectionString));

            builder.Services.AddDependencies();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger()
                   .UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            EnsureCreatedDatabase(app.Services);

            app.Run();
        }

        [Conditional("DEBUG")]
        private static void EnsureCreatedDatabase(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            using var dbContext = scope.ServiceProvider.GetService<GradeDbContext>();

            dbContext.Database.EnsureDeleted();

            bool created = dbContext.Database.EnsureCreated();
            
            if (created)
            {
                SeedDatabase.Seed(dbContext);
            }
        }
    }
}