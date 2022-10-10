namespace IntegrationTests
{
    using GradeApi.Infrastructure;
    using GradeApi.Persistence;
    using Microsoft.EntityFrameworkCore;
    using System;

    public class LocalhostDatabaseFixture : IDisposable
    {
        /// <summary>
        /// Cadena de conexión contra localdb
        /// </summary>
        const string ConnectionStringLocalDb = "Data Source=(localdb)\\MSSQLLocalDB;Database=Grade;Integrated Security=true;";

        /// <summary>
        /// Cadena de conexión contra localhost
        /// </summary>
        private const string ConnectionStringSqlServer = "Data Source=.;Database=Grade;Integrated Security=true;";

        public GradeDbContext DbContext { get; }

        public LocalhostDatabaseFixture()
        {
            string connectionString = Environment.GetEnvironmentVariable("GRADEAPI_LOCALDB_MODE") == bool.TrueString ? 
                ConnectionStringLocalDb : ConnectionStringSqlServer;

            var options = new DbContextOptionsBuilder<GradeDbContext>()
               .UseSqlServer(connectionString)
               .Options;

            DbContext = new GradeDbContext(options);
            DbContext.Database.EnsureDeleted();

            if (DbContext.Database.EnsureCreated())
            {
                SeedDatabase.Seed(DbContext);
            }
        }

        public void Dispose()
        {
            DbContext?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
