using APIDay1.Data;
using APIDay1.DTO;
using APIDay1.Filters;
using APIDay1.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIDay1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {

        private readonly AppDbContext _context;
        public DepartmentController(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var Depts = _context.Departments.ToDTO()
                .ToList();

            return Ok(Depts);
        }

        [HttpGet]
        [Route("{id:int}")]
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
        public IActionResult AddDepartment(Department department)
        {
            _context.Departments.Add(department);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetByID), new { id = department.ID }, department);
        }

        [HttpPost("addV2")]
        [LocationValidate("l")]
        public IActionResult AddDepartmentV2(Department department)
        {
            _context.Departments.Add(department);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetByID), new { id = department.ID }, department);
        }

        [HttpPut]
        public IActionResult UpdateDepartment(Department department)
        {
            _context.Departments.Update(department);
            _context.SaveChanges();
            return Ok(department);
        }

        [HttpDelete]
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
