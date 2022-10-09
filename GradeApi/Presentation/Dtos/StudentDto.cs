namespace GradeApi.Presentation.Dtos
{
    using System;

    public class StudentDto
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public StudentAddressDto Address { get; set; }
    }

    public class StudentAddressDto
    {
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public int ZipCode { get; set; }

        public string State { get; set; }

        public string Country { get; set; }
    }
}
