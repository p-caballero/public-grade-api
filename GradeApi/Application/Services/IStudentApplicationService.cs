namespace GradeApi.Application.Services
{
    using GradeApi.Persistence.Entitites;
    using System.Collections.Generic;

    public interface IStudentApplicationService
    {
        IEnumerable<Student> GetAll();

        Student Get(int id);
    }
}