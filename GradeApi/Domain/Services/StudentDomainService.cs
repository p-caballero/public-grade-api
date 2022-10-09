namespace GradeApi.Domain.Services
{
    using GradeApi.Persistence.Entitites;
    using GradeApi.Persistence.Repositories;
    using System.Collections.Generic;

    public class StudentDomainService : IStudentDomainService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentDomainService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public IEnumerable<Student> GetAll()
        {
            return _studentRepository.GetAll();
        }

        public Student Get(int id)
        {
            return null;
        }
    }
}
