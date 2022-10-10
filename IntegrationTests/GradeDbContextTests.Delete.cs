namespace IntegrationTests
{
    using FluentAssertions;
    using Xunit;

    public partial class GradeDbContextTests
    {
        /// <summary>
        /// Eliminar al estudiante con nombre "Jesus Guerra Jimenez"
        /// </summary>
        [Fact]
        public void Delete_student()
        {
            // TODO: Añadir un estudiante
            false.Should().BeTrue("Pendiente");
        }
    }
}
