namespace IntegrationTests
{
    using GradeApi.Persistence;
    using System;

    public abstract class AbstractRepositoryTests : IDisposable
    {
        protected GradeDbContext DbContext { get; private set; }

        protected AbstractRepositoryTests()
        {
            DbContext = new LocalhostDatabaseFactory().Create();
        }

        public void Dispose()
        {
            DbContext?.Dispose();
            DbContext = null;
        }
    }
}
