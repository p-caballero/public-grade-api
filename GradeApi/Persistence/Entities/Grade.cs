namespace GradeApi.Persistence.Entities
{
    using System;
    using System.Collections.Generic;

    public partial class Grade
    {
        public Grade()
        {
            Courses = new HashSet<Course>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Section { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
