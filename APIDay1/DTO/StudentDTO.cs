using APIDay1.Models;

namespace APIDay1.DTO
{
    public class StudentDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string DeptName { get; set; }
        public ICollection<String> Skills { get; set; } = new List<String>();
    }

    public static class StudentDTOExtension
    {
        public static StudentDTO ToDTO(this Student student)
        {
            return new StudentDTO
            {
                ID = student.ID,
                Name = student.Name,
                Address = student.Address,
                DeptName = student.Department.Name,
                Skills = new List<string> { "Drawing" }
            };
        }

        public static IQueryable<StudentDTO> ToDTO(this IQueryable<Student> students)
        {
            return students.Select(student=> new StudentDTO
            {
                ID = student.ID,
                Name = student.Name,
                Address = student.Address,
                DeptName = student.Department.Name,
                Skills = new List<string> { "Drawing" }
            });
        }


    }
}
