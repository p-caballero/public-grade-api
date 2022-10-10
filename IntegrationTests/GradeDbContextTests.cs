namespace IntegrationTests
{
    using GradeApi.Persistence;
    using System;
    using Xunit;

    [Collection("Sequential")]
    public partial class GradeDbContextTests : IDisposable
    {
        private GradeDbContext _dbContext;

        public GradeDbContextTests()
        {
            _dbContext = new LocalhostDatabaseFactory().Create();
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
            _dbContext = null;
        }
    }
}