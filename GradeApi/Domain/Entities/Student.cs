namespace GradeApi.Domain.Entities
{
    using System;

    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public StudentAddress Address { get; set; }
    }
}
