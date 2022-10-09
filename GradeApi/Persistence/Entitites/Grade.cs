namespace GradeApi.Persistence.Entitites
{
    using System.Collections.Generic;

    public class Grade
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Section { get; set; }

        public Grade()
        {
            Students = new HashSet<Student>();
            Courses = new HashSet<Course>();
        }

        public virtual ICollection<Student> Students { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
