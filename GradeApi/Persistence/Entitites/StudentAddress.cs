namespace GradeApi.Persistence.Entitites
{
    public class StudentAddress
    {
        public int Id { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public int ZipCode { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public int StudentId { get; set; }

        public virtual Student Student { get; set; }
    }
}
