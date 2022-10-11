namespace GradeApi.Application.Services
{
    using GradeApi.Persistence.Entitites;
    using System.Collections.Generic;

    public interface IStudentApplicationService
    {
        IEnumerable<Student> GetAll();

        Student Get(int id);

        bool Delete(int id);

        (bool conflict, Student student) Create(Student student);

        (bool notFound, Student student) Update(Student student);
    }
}