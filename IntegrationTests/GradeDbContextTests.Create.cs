namespace IntegrationTests
{
    using FluentAssertions;
    using GradeApi.Persistence.Entitites;
    using Xunit;

    public partial class GradeDbContextTests
    {
        [Fact]
        public void Add_Grade()
        {
            var miGrado = new Grade();
            miGrado.Name = "Nombre del grado";
            miGrado.Section = "Sección de ejemplo";

            _dbContext.Grades.Add(miGrado);

            int noCambios = _dbContext.SaveChanges();

            noCambios.Should().Be(1);
            miGrado.Id.Should().NotBe(0);
        }

        /// <summary>
        /// Añadir un estudiante
        /// </summary>
        [Fact]
        public void Add_Student()
        {
            var student = new Student()
            {
                Name = "Felipe de Borbón y Grecia",
                Address = new StudentAddress
                {
                    Address1 = "Palacio de la Zarzuela",
                    Address2 = "Carretera del Pardo S/N",
                    ZipCode = 28071,
                    City = "El Pardo",
                    State = "Madrid",
                    Country = "España"
                }
            };

            _dbContext.Students.Add(student);

            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Añadir dirección de un studiante seleccionado por su id
        /// </summary>
        [Fact]
        public void Add_StudentAddress_to_one_student_selectedby_id()
        {
            // TODO: Añadir dirección de un studiante seleccionado por su id
            false.Should().BeTrue("Pendiente");
        }
    }
}
