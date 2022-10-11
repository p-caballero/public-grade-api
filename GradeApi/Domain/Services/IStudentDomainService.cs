namespace GradeApi.Domain.Services
{
    using GradeApi.Persistence.Entitites;
    using System.Collections.Generic;

    public interface IStudentDomainService
    {
        bool Delete(int id);
        Student Get(int id);
        IEnumerable<Student> GetAll();
    }
}