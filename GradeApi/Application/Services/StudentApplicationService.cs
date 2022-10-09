﻿namespace GradeApi.Application.Services
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

        public Student Get(int id)
        {
            return _studentDomainService.Get(id);
        }

        public IEnumerable<Student> GetAll()
        {
            return _studentDomainService.GetAll();
        }
    }
}
