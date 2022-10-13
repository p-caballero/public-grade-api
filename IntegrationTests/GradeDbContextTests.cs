namespace IntegrationTests
{
    using GradeApi.Persistence;
    using System;
    using Xunit;
    using Xunit.Abstractions;

    [Collection("Sequential")]
    public partial class GradeDbContextTests : IDisposable
    {
        /// <summary>
        /// Contexto de la base de datos
        /// </summary>
        private GradeDbContext _dbContext;

        /// <summary>
        /// Consola del test
        /// </summary>
        private readonly ITestOutputHelper _output;

        public GradeDbContextTests(ITestOutputHelper output)
        {
            _dbContext = new LocalhostDatabaseFactory().Create();
            _output = output;
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
            _dbContext = null;
        }
    }
}