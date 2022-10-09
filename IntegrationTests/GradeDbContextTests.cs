namespace IntegrationTests
{
    using FluentAssertions;
    using GradeApi.Persistence;
    using Microsoft.EntityFrameworkCore;
    using System;
    using Xunit;

    public class GradeDbContextTests : IClassFixture<LocalhostDatabaseFixture>
    {
        private readonly GradeDbContext _dbContext;

        public GradeDbContextTests(LocalhostDatabaseFixture databaseFixture)
        {
            _dbContext = databaseFixture.DbContext;
        }

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

            Console.WriteLine();
        }

        /// <summary>
        /// Obtener todos los nombres de los estudiantes
        /// </summary>
        [Fact]
        public void Get_all_student_names()
        {
            // TODO: Obtener todos los nombres de los estudiantes
        }

        /// <summary>
        /// Todos los cr�ditos de un estudiante
        /// </summary>
        [Fact]
        public void Get_all_credits_per_student()
        {
            // TODO: Todos los cr�ditos de un estudiante
        }

        /// <summary>
        /// A�adir un estudiante
        /// </summary>
        [Fact]
        public void Add_Student()
        {
            // TODO: A�adir un estudiante
        }

        /// <summary>
        /// Eliminar al estudiante con nombre "Jesus Guerra Jimenez"
        /// </summary>
        [Fact]
        public void Delete_student()
        {
            // TODO: A�adir un estudiante
        }
    }
}