namespace IntegrationTests
{
	using FluentAssertions;
	using GradeApi.Persistence.Extensions;
	using Microsoft.EntityFrameworkCore;
	using System.Data;
	using System.Data.Common;
	using System.Linq;
	using Xunit;

	public partial class GradeDbContextTests
	{
		/// <summary>
		/// Crear un procedimiento almacenado y ejecutarlo
		/// </summary>
		[Fact]
		public void Execute_storage_procedure()
		{
			using DbCommand command = _dbContext.Database.GetDbConnection().CreateCommand();

            if (command.Connection.State != ConnectionState.Open)
            {
                command.Connection.Open();
            }

            command.CommandText = "EXECUTE [dbo].[sp_get_students_by_course_name] @CourseName = 'Sistemas Operativos'";
			command.CommandType = CommandType.Text;

            var students = command.LoadMany(reader => new { Id = reader.GetInt32(0), Name = reader.GetString(1) }).ToList();

			students.Should().HaveCount(26);
        }
	}
}
