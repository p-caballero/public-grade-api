namespace IntegrationTests
{
    using FluentAssertions;
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
            // TODO: Ejecutar SQL desde el script

            // Assert
            _dbContext.Grades.Where(x => x.Section == "Grados")
                .Should().BeEmpty();

            _dbContext.Grades.Where(x => x.Section == "Degrees")
                .Should().HaveCount(4);
        }
    }
}
