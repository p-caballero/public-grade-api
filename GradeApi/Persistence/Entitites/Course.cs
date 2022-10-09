namespace GradeApi.Persistence.Entitites
{
    using System.Collections.Generic;

    public enum DurationType
    {
        Anual = 0,
        C1 = 1,
        C2 = 2
    }

    public class Course
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public int Credits { get; set; }

        public DurationType Duration { get; set; }

        public bool Mandatory { get; set; }

        public int GradeId { get; set; }

        public virtual Grade Grade { get; set; }

        public Course()
        {
            StudentCourses = new HashSet<StudentCourse>();
        }

        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
