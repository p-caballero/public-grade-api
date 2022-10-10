namespace IntegrationTests
{
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using Xunit;

    public partial class GradeDbContextTests
    {
        /// <summary>
		/// Ejecutar SQL desde el script
		/// </summary>
		[Fact]
        public void Execute_sql_query()
        {
            // Arrange
            const string SqlQuery = @"
UPDATE
  [dbo].[grades]
SET
  [name] = REPLACE(
    [name],
    'Ingeniería de Computadores',
    'Computer Engineering'
  ),
  [section] = 'Degrees'
WHERE
  [Name] LIKE 'Ingeniería de Computadores%'
  AND [section] = 'Grados'";

            // Act
            int actual = _dbContext.Database.ExecuteSqlRaw(SqlQuery);

            // Assert
            actual.Should().Be(4);

            _dbContext.Grades.Where(x => x.Section == "Grados")
                .Should().BeEmpty();

            _dbContext.Grades.Where(x => x.Section == "Degrees")
                .Should().HaveCount(4);
        }
    }
}
