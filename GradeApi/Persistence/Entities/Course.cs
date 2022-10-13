namespace GradeApi.Persistence.Entities
{
    using System;
    using System.Collections.Generic;

    public partial class Course
    {
        public Course()
        {
            Students = new HashSet<Student>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Credits { get; set; }
        public int Duration { get; set; }
        public bool Mandatory { get; set; }
        public int GradeId { get; set; }

        public virtual Grade Grade { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
