namespace GradeApi.Persistence.Entities
{
    using System;
    using System.Collections.Generic;

    public partial class Student
    {
        public Student()
        {
            Courses = new HashSet<Course>();
        }

        public int Id { get; set; }
        /// <summary>
        /// Nombre y apellidos
        /// </summary>
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public float? Height { get; set; }
        public float? Weight { get; set; }
        public int? AddressId { get; set; }

        public virtual StudentAddress StudentAddress { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
