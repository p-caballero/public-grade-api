namespace GradeApi.Presentation.Controllers
{
    using System;
    using GradeApi.Application.Services;
    using GradeApi.Presentation.Dtos;
    using GradeApi.Presentation.Mappers;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using GradeApi.Persistence.Entitites;

    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        const string studentPrefix = "S";

        private readonly IStudentApplicationService _studentApplicationService;
        private readonly StudentToStudentDtoMapper _mapper;

        public StudentController(IStudentApplicationService studentApplicationService, StudentToStudentDtoMapper mapper)
        {
            _studentApplicationService = studentApplicationService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("v1/[controller]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentDto[]))]
        public IActionResult Get()
        {
            var all = _studentApplicationService.GetAll()
                .Select(student => _mapper.ConvertToStudentDto(student))
                .ToArray();

            return Ok(all);
        }

        [HttpGet]
        [Route("v1/[controller]/{code}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentDto))]
        public IActionResult Get(string code)
        {
            const string studentPrefix = "S";

            if (!code.StartsWith(studentPrefix) || !int.TryParse(code.AsSpan(1), out int id))
            {
                return BadRequest();
            }

            var student = _studentApplicationService.Get(id);

            if (student == null)
            {
                return NotFound();
            }

            var studentDto = _mapper.ConvertToStudentDto(student);

            return Ok(studentDto);
        }

        [HttpDelete]
        [Route("v1/[controller]/{code}")]
        public IActionResult Delete(string code)
        {
            if (!code.StartsWith(studentPrefix) || !int.TryParse(code.AsSpan(1), out int id))
            {
                return BadRequest();
            }

            bool deleted = _studentApplicationService.Delete(id);

            if (deleted)
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpPost]
        [Route("v1/[controller]")]
        public IActionResult Create(StudentDto studentDto)
        {
            var newStudent = _mapper.ConvertToStudent(studentDto);

            (bool conflict, newStudent) = _studentApplicationService.Create(newStudent);

            if (conflict)
            {
                return Conflict();
            }

            var newStudentDto = _mapper.ConvertToStudentDto(newStudent);

            return Created("", newStudentDto);
        }

        [HttpPut]
        [Route("v1/[controller]/{code}")]
        public IActionResult Update(string code, StudentDto studentDto)
        {
            if (!code.StartsWith(studentPrefix) || !int.TryParse(code.AsSpan(1), out int id) ||
                code != studentDto.Code)
            {
                return BadRequest();
            }

            var newStudent = _mapper.ConvertToStudent(studentDto);

            (bool notFound, _) = _studentApplicationService.Update(newStudent);

            if (notFound)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
