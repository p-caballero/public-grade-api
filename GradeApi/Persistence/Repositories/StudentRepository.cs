namespace GradeApi.Persistence.Repositories
{
    using GradeApi.Persistence.Entitites;
    using System.Linq;

    public class StudentRepository : AbstractRepository<Student>, IStudentRepository
    {
        public StudentRepository(GradeDbContext context)
            : base(context)
        {
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
    }
}
