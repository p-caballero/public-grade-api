namespace IntegrationTests
{
    using GradeApi.Infrastructure;
    using GradeApi.Persistence;
    using Microsoft.EntityFrameworkCore;
    using System;

    public class LocalhostDatabaseFactory
    {
        /// <summary>
        /// Cadena de conexión contra localdb
        /// </summary>
        const string ConnectionStringLocalDb = "Data Source=(localdb)\\MSSQLLocalDB;Database=Grade;Integrated Security=true;";

        /// <summary>
        /// Cadena de conexión contra localhost
        /// </summary>
        private const string ConnectionStringSqlServer = "Data Source=.;Database=Grade;Integrated Security=true;";

        private readonly DbContextOptions<GradeDbContext> _options;

        public LocalhostDatabaseFactory()
        {
            string connectionString = Environment.GetEnvironmentVariable("GRADEAPI_LOCALDB_MODE") == bool.TrueString ? 
                ConnectionStringLocalDb : ConnectionStringSqlServer;

            _options = new DbContextOptionsBuilder<GradeDbContext>()
               .UseSqlServer(connectionString)
               .Options;
        }

        public GradeDbContext Create()
        {
            var dbContext = new GradeDbContext(_options);
            dbContext.Database.EnsureDeleted();

            if (dbContext.Database.EnsureCreated())
            {
                SeedDatabase.Seed(dbContext);
            }

            return dbContext;
        }
    }
}
