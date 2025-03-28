using APIDay1.Models;

namespace APIDay1.DTO
{
    public class DepartmentDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string MgrName { get; set; }
        public ICollection<string> StudentsName { get; set; } = new List<string>();
        public int Count { get; set; }
        public string message { get; set; }
    }

    public static class DepartmentDTOExtension
    {
        public static DepartmentDTO ToDTO(this Department department)
        {
            return new DepartmentDTO
            {
                ID = department.ID,
                Name = department.Name,
                MgrName = department.MgrName,
                Count = department.Students.Count,
                StudentsName = department.Students.Select(x => x.Name).ToList(),
                message = department.Students.Count > 2 ? "Overload" : "Ok"
            };
        }


        public static IQueryable<DepartmentDTO> ToDTO(this IQueryable<Department> departments)
        {
            return departments.Select(department => new DepartmentDTO
            {
                ID = department.ID,
                Name = department.Name,
                MgrName = department.MgrName,
                Count = department.Students.Count,
                StudentsName = department.Students.Select(x => x.Name).ToList(),
                message = department.Students.Count > 2 ? "Oveload" : "Ok"
            });
        }
    }
}
