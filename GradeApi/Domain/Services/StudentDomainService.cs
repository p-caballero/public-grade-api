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
            return _studentRepository.GetById(id);
        }

        public bool Delete(int id)
        {
            return _studentRepository.Delete(id);
        }

        public bool Exist(string name)
        {
            throw new System.NotImplementedException();
            // Comprobar la existencia usando el repositorio según [Name]
        }

        public bool Exist(int id)
        {
            throw new System.NotImplementedException();
            // Comprobar la existencia usando el repositorio según [ID]
        }

        public Student Create(Student student)
        {
            throw new System.NotImplementedException();
            // Insertar un estudiante usando el repositorio
        }

        public Student Update(Student student)
        {
            throw new System.NotImplementedException();
            // Actualizar un estudiante utilizando el repositorio
        }
    }
}
