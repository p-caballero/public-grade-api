namespace GradeApi.Persistence.Entitites
{
    using System;
    using System.Collections.Generic;

    public partial class Student
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public decimal? Height { get; set; }

        public decimal? Weight { get; set; }

        public int? AddressId { get; set; }

        public virtual StudentAddress Address { get; set; }

        public virtual ICollection<StudentCourse> StudentCourses { get; set; }

        public Student()
        {
            StudentCourses = new HashSet<StudentCourse>();
        }
    }
}
