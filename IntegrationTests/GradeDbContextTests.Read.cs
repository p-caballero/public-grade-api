namespace IntegrationTests
{
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;
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
            var allGrades = _dbContext.Grades
                .Include(x => x.Courses)
                .ToList();

            // Assert
            allGrades.Should().HaveCount(4);
        }

        /// <summary>
        /// Obtener todos los estudiantes con sus cursos
        /// </summary>
        [Fact]
        public void Get_all_students_with_courses()
        {
            // Act
            var actual = _dbContext.Students
                .Include(x => x.StudentCourses)
                .ThenInclude(x => x.Course)
                .ToList();

            // Assert
            actual.Should().NotBeEmpty();
        }

        [Fact]
        public void Get_all_courses_with_students()
        {
            var allCourses = _dbContext.StudentCourses
                .Include(x => x.Course)
                .Select(x => x.Course)
                .Distinct()
                .OrderBy(x => x.Name)
                .ToList();
        }

        /// <summary>
        /// Obtener todos los nombres de los estudiantes
        /// </summary>
        [Fact]
        public void Get_all_student_names()
        {
            var allNames = _dbContext.Students
                .Select(x => x.Name)
                .ToList();
        }

        [Fact]
        public void Get_all_student_with_name_diego()
        {
            var allNames = _dbContext.Students
                .Where(x => x.Name.Contains("Diego"))
                .Select(x => x.Name)
                .ToList();
        }

        private class EstudianteNombreYPeso
        {
            public string Nombre { get; set; }

            public int Peso { get; set; }
        }

        [Fact]
        public void Get_all_student_weight_greater_90()
        {
            var result = _dbContext.Students
                .Where(x => x.Weight != null && x.Weight > 90)
                .Select(x => new { Nombre = x.Name, Peso = x.Weight }) // Proyección
                .OrderByDescending(x => x.Nombre)
                .First();

            var entDominio = new EstudianteNombreYPeso
            {
                Nombre = result.Nombre,
                Peso = (int)result.Peso.Value
            };
        }

        [Fact]
        public async Task Get_all_student_weight_greater_90_async()
        {
            var result = await _dbContext.Students
                .Where(x => x.Weight != null && x.Weight > 90)
                .Select(x => new { Nombre = x.Name, Peso = x.Weight }) // Proyección
                .OrderByDescending(x => x.Nombre)
                .FirstAsync();

            var entDominio = new EstudianteNombreYPeso
            {
                Nombre = result.Nombre,
                Peso = (int)result.Peso.Value
            };
        }


        /// <summary>
        /// Todos los créditos de un estudiante
        /// </summary>
        [Fact]
        public void Get_all_credits_per_student()
        {
            var actual = _dbContext.Students
                .Include(x => x.StudentCourses).ThenInclude(x => x.Course)
                .Select(x => new { x.Name, TotalCredits = x.StudentCourses.Sum(sc => sc.Course.Credits) })
                .ToList();

            // Assert
            actual.Should().NotBeEmpty();

            actual[0].Should().BeEquivalentTo(new { Name = "Estrella Montaño Garcia", TotalCredits = 60 });

            actual.First()
                .Should().BeEquivalentTo(new { Name = "Estrella Montaño Garcia", TotalCredits = 60 });
        }

        [Fact]
        public void obtener_todos_los_estudiantes_que_empiecen_por_n_con_like()
        {
            var result = _dbContext.Students
                .Where(x => EF.Functions.Like(x.Name, "N%"))
                .ToList();

            result.Should().HaveCount(1);
            result[0].Name.Should().StartWith("Narciso");
        }

        [Fact]
        public void obtener_todos_los_estudiantes_que_empiecen_por_n()
        {
            var result = _dbContext.Students
                .Where(x => x.Name.StartsWith("N"))
                .ToList();

            result.Should().HaveCount(1);
            result[0].Name.Should().StartWith("N");
        }
    }
}
