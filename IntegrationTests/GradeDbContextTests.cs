namespace IntegrationTests
{
    using GradeApi.Persistence;
    using Xunit;

    public partial class GradeDbContextTests : IClassFixture<LocalhostDatabaseFixture>
    {
        private readonly GradeDbContext _dbContext;

        public GradeDbContextTests(LocalhostDatabaseFixture databaseFixture)
        {
            _dbContext = databaseFixture.DbContext;
        }
    }
}