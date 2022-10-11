﻿namespace IntegrationTests
{
    using FluentAssertions;
    using GradeApi.Persistence.Entitites;
    using GradeApi.Persistence.Repositories;
    using System.Linq;
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

        [Fact]
        public void Delete_ExistingStudent_DeleteStudentAndReturnsTrue()
        {
            // Arrange
            // 1.- Un ID que exista
            const int id = 42;

            // Act
            bool actual = _repository.Delete(id);

            // Assert
            actual.Should().BeTrue();
            DbContext.Students.Any(x => x.Id == id).Should().BeFalse();
            DbContext.Students.FirstOrDefault(x => x.Id == id).Should().BeNull(); // Hace lo mismo que el anterior
        }

        [Fact]
        public void Delete_NonExistingStudent_ReturnsFalse()
        {
            // Arrange
            // 1.- Un ID que NO exista
            const int id = 1000;

            // Act
            bool actual = _repository.Delete(id);

            // Assert
            actual.Should().BeFalse();
        }

        [Fact]
        public void Insert_ExistingStudent_ReturnCreatedStudent()
        {
            // Arrange
            Student existingStudent = null;

            // Act
            var actual = _repository.Insert(existingStudent);

            // Assert

            // <El estudiante se ha creado, es diferente al existente y los campos pasado se han guardado>
        }

        [Fact]
        public void Insert_NonExistingStudent_ReturnCreatedStudent()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void Update_ExistingStudent_ReturnUpdatedStudent()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void Update_NonExistingStudent_ReturnNull()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}
