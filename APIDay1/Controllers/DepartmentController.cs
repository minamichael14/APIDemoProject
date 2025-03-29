using System.Security.Claims;
using APIDay1.Data;
using APIDay1.DTO;
using APIDay1.Filters;
using APIDay1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIDay1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomResponseHeader]
    public class DepartmentController : ControllerBase
    {

        private readonly AppDbContext _context;
        public DepartmentController(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        [HttpGet]
        [Authorize(Roles = "Student,Admin")]

        public IActionResult GetAll()
        {
            // to get the student Id from Claim:
            var claims = HttpContext.User;
            var UserID = int.Parse(claims.FindFirst(ClaimTypes.NameIdentifier).Value);

            List<DepartmentDTO> Depts = new List<DepartmentDTO>();
            
            // if the role is admin : return all departments.
            if(claims.FindFirst(ClaimTypes.Role).Value == "Admin")
            {
                Depts = _context.Departments.ToDTO()
                .ToList();
            }
            // if role is student: Return his departments.
            else if (claims.FindFirst(ClaimTypes.Role).Value == "Student")
            {
                Depts = _context.Departments
                        .Where(x => x.Students.Select(d => d.ID).Contains(UserID))
                        .ToDTO().ToList();
            }
            return Ok(Depts);
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize(Roles = "Student,Admin")]
        public IActionResult GetByID(int id)
        {
            var isExist = _context.Departments.Any(x => x.ID == id);
            if (!isExist)
            {
                return NotFound();
            }
            var Dept = _context.Departments.Where(x=>x.ID==id).ToDTO();
            return Ok(Dept);
        }

        [HttpGet]
        [Route("{name:alpha}")]
        [Authorize(Roles = "Student,Admin")]
        public IActionResult GetByName(string name)
        {
            var isExist = _context.Departments.Any(x => x.Name == name);
            if (!isExist)
            {
                return NotFound();
            }
            var Depts = _context.Departments.Where(x => x.Name == name).ToDTO();
            return Ok(Depts);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddDepartment(Department department)
        {
            _context.Departments.Add(department);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetByID), new { id = department.ID }, department);
        }

        [HttpPost("addV2")]
        [LocationValidate]
        //[Authorize(Roles = "Admin")]
        public IActionResult AddDepartmentV2(Department department)
        {
            _context.Departments.Add(department);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetByID), new { id = department.ID }, department);
        }

        [HttpPut]
        [Authorize(Roles = "Student,Admin")]
        public IActionResult UpdateDepartment(Department department)
        {
            _context.Departments.Update(department);
            _context.SaveChanges();
            return Ok(department);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var isExist = _context.Departments.Any(x => x.ID == id);
            if (!isExist)
            {
                return NotFound();
            }
            var dept = _context.Departments.FirstOrDefault(x=>x.ID ==id);

            _context.Departments.Remove(dept);
            _context.SaveChanges();
            return Ok();
        }

        

    }
}
