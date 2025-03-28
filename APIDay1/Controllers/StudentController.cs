using APIDay1.Filters;
using APIDay1.Models;
using APIDay1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIDay1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService) 
        {
            _studentService = studentService;            
        }

        [HttpGet]
        [CustomAuthorize("Student")]
        public IActionResult GetAll()
        {
            return Ok(_studentService.GetAll());
        }

        [HttpGet]
        [Route("{id:int}")]
        [CustomAuthorize("Student")]
        public IActionResult GetByID(int id)
        {
            var student = _studentService.GetByID(id);
            if (student is null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpGet]
        [Route("{name:alpha}")]
        [CustomAuthorize("Student")]
        public IActionResult GetByName(string name)
        {
            var student = _studentService.GetByName(name);
            if (student is null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost]
        [CustomAuthorize("Admin")]
        public IActionResult AddStudent(Student student)
        {
            var id = _studentService.Add(student);
            return CreatedAtAction(nameof(GetByID),new { id = id}, student);
        }

        [HttpPut]
        [CustomAuthorize("Admin")]
        [CustomAuthorize("Student")]
        public IActionResult UpdateStudent(Student student)
        {
            _studentService.Update(student);
            return Ok(student);
        }

        [HttpDelete]
        [CustomAuthorize("Admin")]
        public IActionResult Delete(int id)
        {
            var isExist = _studentService.Delete(id);
            if (!isExist)
            {
                return NotFound();
            }
            
            return Ok();
        }

        [HttpPatch]
        public IActionResult UpdateName(int id, string name)
        {
            _studentService.UpdateName(id, name);
            return Ok();
        }

    }
}
