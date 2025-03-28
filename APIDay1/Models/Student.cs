namespace APIDay1.Models
{
    public class Student : BaseModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string? Image { get; set; }
        public DateTime DateOfBirth { get; set; }

        public int DepartmentID { get; set; }
        public Department? Department { get; set; }
    }
}
