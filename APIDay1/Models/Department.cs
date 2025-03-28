using System.ComponentModel.DataAnnotations;
using APIDay1.Validators;

namespace APIDay1.Models
{
    public class Department : BaseModel
    {
        [UniqueName]
        public string Name { get; set; }

        //[RegularExpression(@"^(EG|USA)$", ErrorMessage = "This location vlaue must be 'EG' or 'USA'")]
        public string Location { get; set; }
        public string MgrName { get; set; }

        public ICollection<Student>? Students { get; set; } = new List<Student>();
    }
}
