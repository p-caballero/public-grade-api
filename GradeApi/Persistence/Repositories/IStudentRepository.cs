namespace GradeApi.Persistence.Repositories
{
    using GradeApi.Persistence.Entitites;
    using System.Linq;

    public interface IStudentRepository
    {
        IQueryable<Student> GetAll();
    }
}