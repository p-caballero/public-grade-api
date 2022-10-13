namespace GradeApi.Persistence.Repositories
{
    using GradeApi.Persistence.Entities;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;

    public class StudentRepository : AbstractRepository<Student>, IStudentRepository
    {
        public StudentRepository(GradeDbContext context)
            : base(context)
        {
        }

        public bool Delete(int id)
        {
            var student = DbContext.Students
                .Include(x => x.StudentCourses)
                .SingleOrDefault(x => x.Id == id);

            if (student == null)
            {
                return false;
            }

            DbContext.StudentCourses.RemoveRange(student.StudentCourses);
            DbContext.Students.Remove(student);

            int noChanges = DbContext.SaveChanges();

            return noChanges > 0;
        }

        public IQueryable<Student> GetAll()
        {
            const int pageSize = 10;
            return GetAll(trackChanges: false).Take(pageSize);
        }

        public Student GetById(int id)
        {
            return DbContext.Students.Find(id);
        }

        public Student Insert(Student student)
        {
            var entryStudent = DbContext.Entry(student);

            if (entryStudent.State != EntityState.Detached)
            {
                throw new InvalidOperationException("Entity should be detached!");
            }

            DbContext.Students.Add(student);
            DbContext.SaveChanges();
            return student;
        }

        /// <summary>
        /// Otra forma más sencilla (tal vez).
        /// </summary>
        public Student Insert2(Student student)
        {
            Add(student);
            DbContext.SaveChanges();
            return student;
        }
    }
}
