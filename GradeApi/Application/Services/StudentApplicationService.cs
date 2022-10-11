namespace GradeApi.Application.Services
{
    using GradeApi.Domain.Services;
    using GradeApi.Persistence.Entitites;
    using System.Collections.Generic;

    public class StudentApplicationService : IStudentApplicationService
    {
        private readonly IStudentDomainService _studentDomainService;

        public StudentApplicationService(IStudentDomainService studentDomainService)
        {
            _studentDomainService = studentDomainService;
        }

        public (bool conflict, Student student) Create(Student student)
        {
            if (_studentDomainService.Exist(student.Name))
            {
                return (true, null);
            }

            var newStudent = _studentDomainService.Create(student);

            return (conflict: false, student: newStudent);
        }

        public bool Delete(int id)
        {
            //_logger.LogWarning($"ELIMINANDO ESTUDIANTE S{id}");
            return _studentDomainService.Delete(id);
        }

        public Student Get(int id)
        {
            return _studentDomainService.Get(id);
        }

        public IEnumerable<Student> GetAll()
        {
            return _studentDomainService.GetAll();
        }

        public (bool notFound, Student student) Update(Student student)
        {
            if (!_studentDomainService.Exist(student.Id))
            {
                return (true, null);
            }

            var newStudent = _studentDomainService.Update(student);

            return (notFound: false, student: newStudent);
        }
    }
}
