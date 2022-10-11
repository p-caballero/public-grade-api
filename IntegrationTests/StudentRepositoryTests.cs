namespace IntegrationTests
{
    using FluentAssertions;
    using GradeApi.Persistence.Entitites;
    using GradeApi.Persistence.Repositories;
    using Xunit;

    public class StudentRepositoryTests : AbstractRepositoryTests
    {
        private readonly StudentRepository _repository;

        public StudentRepositoryTests()
        {
            _repository = new StudentRepository(DbContext);
        }

        [Fact]
        public void GetById_ExistingStudent_ReturnStudent()
        {
            // Arrange
            // int id = DbContext.Students.Select(x => x.Id).First();
            const int id = 42;

            // Act
            Student actual = _repository.GetById(id);

            // Assert
            actual.Should().NotBeNull();
            Assert.NotNull(actual);

            actual.Id.Should().Be(id);
            Assert.Equal(id, actual.Id);
        }

        public void Delete_ExistingStudent_DeleteStudentAndReturnsTrue()
        { 
        }

        public void Delete_NonExistingStudent_ReturnsFalse()
        {
        }
    }
}
