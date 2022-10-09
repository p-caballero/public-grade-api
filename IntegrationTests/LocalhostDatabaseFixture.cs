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
        //const string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Database=Grade;Integrated Security=true;";

        /// <summary>
        /// Cadena de conexión contra localhost
        /// </summary>
        private const string ConnectionString = "Data Source=.;Database=Grade;Integrated Security=true;";

        public GradeDbContext DbContext { get; }

        public LocalhostDatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<GradeDbContext>()
               .UseSqlServer(ConnectionString)
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
