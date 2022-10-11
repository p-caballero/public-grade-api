namespace GradeApi.Presentation.Controllers
{
    using System;
    using GradeApi.Application.Services;
    using GradeApi.Presentation.Dtos;
    using GradeApi.Presentation.Mappers;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

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
    }
}
