namespace IntegrationTests
{
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public partial class GradeDbContextTests
    {
        /// <summary>
        /// Obtener todos Grades
        /// </summary>
        [Fact]
        public void Get_all_grades()
        {
            // Act
            var allGrades = _dbContext.Grades.Include(x => x.Courses);

            // Assert
            allGrades.Should().HaveCount(4);
        }

        /// <summary>
        /// Obtener todos los estudiantes con sus cursos
        /// </summary>
        [Fact]
        public void Get_all_students_with_courses()
        {
            // TODO: Obtener todos los estudiantes con sus cursos
            false.Should().BeTrue("Pendiente");
        }

        /// <summary>
        /// Obtener todos los nombres de los estudiantes
        /// </summary>
        [Fact]
        public void Get_all_student_names()
        {
            // TODO: Obtener todos los nombres de los estudiantes
            false.Should().BeTrue("Pendiente");
        }

        /// <summary>
        /// Todos los créditos de un estudiante
        /// </summary>
        [Fact]
        public void Get_all_credits_per_student()
        {
            // TODO: Todos los créditos de un estudiante
            false.Should().BeTrue("Pendiente");
        }
    }
}
