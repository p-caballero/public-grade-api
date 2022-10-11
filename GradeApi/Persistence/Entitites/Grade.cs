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
            Courses = new HashSet<Course>();
        }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
