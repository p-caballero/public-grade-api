﻿namespace IntegrationTests
{
    using FluentAssertions;
    using System.Linq;
    using Xunit;

    public partial class GradeDbContextTests
	{
        /// <summary>
        /// Modifica un estudiante con todo el nombre en mayúscula
        /// </summary>
        [Fact]
        public void Update_student_name_uppercase()
        {
            // 1.- Traer estudiante con id = 42
            var estudiante42 = _dbContext.Students.Find(42); // Busca por ID

            /*
            estudiante42 = _dbContext.Students.Single(x => x.Id == 42); // El primero donde el ID 42 y sólo puede haber 1

            estudiante42 = _dbContext.Students.Where(x => x.Id == 42).Single(); // El primero donde el ID 42 y sólo puede haber 1

            estudiante42 = _dbContext.Students.SingleOrDefault(x => x.Id == 42); // El primero donde el ID 42 y sólo puede haber 1 ó 0 (devuelve null)

            estudiante42 = _dbContext.Students.First(x => x.Id == 42); // El primero donde el ID 42 pero puede haber más

            estudiante42 = _dbContext.Students.FirstOrDefault(x => x.Id == 42); // El primero donde el ID 42 pero puede haber más o ninguno

            estudiante42 = _dbContext.Students.ToList().Single(x => x.Id == 42); // Trae TODOS los estudiantes de la BD a memoria y me quedo con el primero
            */

            // 2.- Cambiar el campo nombre a mayúsculas
            estudiante42.Name = estudiante42.Name.ToUpper();

            // 3.- Guardar los cambios
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Todos los estudiantes del grado 1 suben 1 kilo si tienen el peso
        /// </summary>
        [Fact]
        public void Update_student_weight_of_a_grade()
        {
            // TODO: Todos los estudiantes del grado 1 suben 1 kilo si tienen el peso
            false.Should().BeTrue("Pendiente");
        }
    }
}
