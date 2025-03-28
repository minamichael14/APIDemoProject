using APIDay1.DTO;
using APIDay1.Models;
using APIDay1.Repository;
using Microsoft.EntityFrameworkCore;

namespace APIDay1.Services
{
    public class StudentService : IStudentService
    {
        private readonly IRepository<Student> _studentRepo;
        public StudentService(IRepository<Student> studentRepo)
        {
            _studentRepo = studentRepo;
        }

        public int Add(Student student)
        {
            _studentRepo.Add(student);
            return student.ID;
        }

        public bool Delete(int id)
        {
            var isExist = _studentRepo.IsExist(id);
            if (!isExist)
            {
                return false;
            }
            _studentRepo.Delete(new Student { ID = id});
            return true;
        }

        public ICollection<StudentDTO> GetAll()
        {
            return _studentRepo.GetAll().ToDTO().ToList();
        }

        public StudentDTO GetByID(int id)
        {
            var isExist = _studentRepo.IsExist(id);
            if (!isExist)
            {
                return null;
            }
            return _studentRepo.GetByID(id).ToDTO();
        }

        public ICollection<StudentDTO> GetByName(string name)
        {
            var isExist = _studentRepo.GetAll(x=>x.Name==name).Any();
            if (!isExist)
            {
                return null;
            }
            return _studentRepo.GetAll(x => x.Name == name).ToDTO().ToList();

        }

        public void Update(Student student)
        {
            _studentRepo.SaveInclude(student, nameof(student.Name), nameof(student.Age),
                nameof(student.Address), nameof(student.Image), nameof(student.DateOfBirth));
        }

        public void UpdateName(int id, string name)
        {
            _studentRepo.SaveInclude(new Student { ID = id}, nameof(Student.Name));
        }
}
}
