namespace GradeApi.Presentation.Controllers
{
    using System;
    using GradeApi.Application.Services;
    using GradeApi.Presentation.Dtos;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using AutoMapper;
    using System.Collections.Generic;

    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        const string studentPrefix = "S";

        private readonly IStudentApplicationService _studentApplicationService;
        private readonly IMapper _mapper;

        public StudentController(IMapper mapper, IStudentApplicationService studentApplicationService)
        {
            _studentApplicationService = studentApplicationService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("v1/[controller]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentDto[]))]
        public IActionResult Get()
        {
            var allDomain = _studentApplicationService.GetAll();
            var allDto = _mapper.Map<IEnumerable<Domain.Entities.Student>, StudentDto[]> (allDomain);
            return Ok(allDto);
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

            var studentDomain = _studentApplicationService.Get(id);

            if (studentDomain == null)
            {
                return NotFound();
            }

            var studentDto = _mapper.Map<Domain.Entities.Student, StudentDto>(studentDomain);

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
            var studentDomain = _mapper.Map<StudentDto, Domain.Entities.Student>(studentDto);

            (bool conflict, Domain.Entities.Student newStudentDomain) = _studentApplicationService.Create(studentDomain);

            if (conflict)
            {
                return Conflict();
            }

            var newStudentDto = _mapper.Map<Domain.Entities.Student, StudentDto>(newStudentDomain);

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

            var studentDomain = _mapper.Map<StudentDto, Domain.Entities.Student>(studentDto);

            (bool notFound, _) = _studentApplicationService.Update(studentDomain);

            if (notFound)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
