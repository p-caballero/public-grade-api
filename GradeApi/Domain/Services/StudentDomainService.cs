namespace GradeApi.Domain.Services
{
    using AutoMapper;
    using GradeApi.Domain.Entities;
    using GradeApi.Persistence.Repositories;
    using System.Collections.Generic;
    using System.Linq;
    using StorageStudent = GradeApi.Persistence.Entities.Student;

    public class StudentDomainService : IStudentDomainService
    {
        private readonly IMapper _mapper;
        private readonly IStudentRepository _studentRepository;

        public StudentDomainService(IMapper mapper,
            IStudentRepository studentRepository)
        {
            _mapper = mapper;
            _studentRepository = studentRepository;
        }

        public IEnumerable<Student> GetAll()
        {
            var allStorage = _studentRepository.GetAll();
            var allDomain = _mapper.Map<IQueryable<StorageStudent>, IEnumerable<Student>>(allStorage);
            return allDomain;
        }

        public Student Get(int id)
        {
            var studentStorage = _studentRepository.GetById(id);
            var studentDomain = _mapper.Map<StorageStudent, Student>(studentStorage);
            return studentDomain;
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
            var studentStorage = _mapper.Map<Student, StorageStudent>(student);
            // Pensad qué pasa si alguien inserta los mismo en este punto
            studentStorage = _studentRepository.Insert(studentStorage);
            var studentDomain = _mapper.Map<StorageStudent, Student>(studentStorage);
            return studentDomain;
        }

        public Student Update(Student student)
        {
            var oldStudent = _studentRepository.GetById(student.Id);

            // Automapper pisa los datos que se han cambiado
            var studentStorage = _mapper.Map<Student, StorageStudent>(student, oldStudent);
            
            // Pensad qué pasa si alguien elimina el estudiante en este punto
            _studentRepository.SaveChanges();

            //studentStorage = _studentRepository.Update(studentStorage);
            var studentDomain = _mapper.Map<StorageStudent, Student>(studentStorage);
            return studentDomain;
        }
    }
}
