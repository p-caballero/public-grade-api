namespace GradeApi.Persistence.Repositories
{
    using GradeApi.Persistence.Entitites;
    using Microsoft.EntityFrameworkCore;
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
            throw new System.NotImplementedException();
        }

        Student IStudentRepository.Update(Student student)
        {
            throw new System.NotImplementedException();
        }
    }
}
