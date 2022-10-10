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
        /// Cadena de conexi�n contra localdb
        /// </summary>
        const string ConnectionStringLocalDb = "Data Source=(localdb)\\MSSQLLocalDB;Database=Grade;Integrated Security=true;";

        /// <summary>
        /// Cadena de conexi�n contra localhost
        /// </summary>
        private const string ConnectionStringSqlServer = "Data Source=.;Database=Grade;Integrated Security=true;";

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            string connectionString = Environment.GetEnvironmentVariable("GRADEAPI_LOCALDB_MODE") == bool.TrueString ?
                ConnectionStringLocalDb : ConnectionStringSqlServer;

            builder.Services.AddDbContext<GradeDbContext>(options => options.UseSqlServer(connectionString));

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