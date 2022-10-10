namespace IntegrationTests
{
    using System;
    using Xunit;

    public partial class GradeDbContextTests
    {
        /// <summary>
        /// Eliminar al estudiante con nombre "Jesus Guerra Jimenez"
        /// </summary>
        [Fact]
        public void Delete_student()
        {
            int noChanges;

            using var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                var student = _dbContext.Students.Find(43);

                _dbContext.StudentCourses.RemoveRange(student.StudentCourses); // Elimina todos los elementos de la asociación StudentCourses del estuddiante 43

                noChanges = _dbContext.SaveChanges();

                _dbContext.Students.Remove(student);

                noChanges = _dbContext.SaveChanges();

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }


        [Fact]
        public void Delete_student_error()
        {
            try
            {
                DeleteStudent(43);
            } catch(InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void DeleteStudent(int id)
        {
            using var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                var student = _dbContext.Students.Find(id);

                _dbContext.Students.Remove(student);

                _dbContext.SaveChanges();

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
