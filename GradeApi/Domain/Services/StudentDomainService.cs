namespace GradeApi.Domain.Services
{
    using GradeApi.Persistence.Entitites;
    using GradeApi.Persistence.Repositories;
    using System.Collections.Generic;
    using System.Linq;

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
            // Comprobar la existencia usando el repositorio según [Name]
            return _studentRepository.GetAll().Any(x => x.Name == name);
        }

        public bool Exist(int id)
        {
            // Comprobar la existencia usando el repositorio según [ID]
            return _studentRepository.GetAll().Any(x => x.Id == id);
        }

        public Student Create(Student student)
        {
            // Pensad qué pasa si alguien inserta los mismo en este punto
            return _studentRepository.Insert(student);
        }

        public Student Update(Student student)
        {
            // Pensad qué pasa si alguien elimina el estudiante en este punto
            return _studentRepository.Update(student);
        }
    }
}
