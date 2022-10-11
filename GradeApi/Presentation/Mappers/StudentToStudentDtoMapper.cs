namespace GradeApi.Presentation.Mappers
{
    using GradeApi.Persistence.Entitites;
    using GradeApi.Presentation.Dtos;

    public class StudentToStudentDtoMapper
    {
        public StudentDto ConvertToStudentDto(Student student)
        {
            if (student == null)
            {
                return null;
            }

            var studentDto = new StudentDto()
            {
                Code = $"S{student.Id}",
                Name = student.Name,
                DateOfBirth = student.DateOfBirth,
            };

            if (student.Address != null)
            {
                studentDto.Address = new StudentAddressDto()
                {
                    Address1 = student.Address.Address1,
                    Address2 = student.Address.Address2,
                    City = student.Address.City,
                    ZipCode = student.Address.ZipCode,
                    State = student.Address.State,
                    Country = student.Address.Country
                };
            }

            return studentDto;
        }

        public Student ConvertToStudent(StudentDto studentDto)
        {
            if (studentDto == null)
            {
                return null;
            }

            int id;
            if (studentDto.Code != null && studentDto.Code.Length > 1)
            {
                id = int.Parse(studentDto.Code[1..]);
            } else
            {
                id = 0;
            }

            var student = new Student()
            {
                Id = id,
                Name = studentDto.Name,
                DateOfBirth = studentDto.DateOfBirth,
            };

            return student;
        }
    }
}
