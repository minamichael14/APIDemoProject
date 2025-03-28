using APIDay1.DTO;
using APIDay1.Models;
namespace APIDay1.Services
{
    public interface IStudentService
    {
        ICollection<StudentDTO> GetAll();
        StudentDTO GetByID(int id);
        ICollection<StudentDTO> GetByName(string name);

        int Add (Student student);
        void Update (Student student);
        bool Delete (int id);

        void UpdateName(int id, string name);



    }
}
