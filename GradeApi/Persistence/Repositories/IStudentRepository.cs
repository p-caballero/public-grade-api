namespace GradeApi.Persistence.Repositories
{
    using GradeApi.Persistence.Entities;
    using System.Linq;

    public interface IStudentRepository
    {
        bool Delete(int id);

        IQueryable<Student> GetAll();

        Student GetById(int id);

        Student Insert(Student student);

        int SaveChanges();
    }
}