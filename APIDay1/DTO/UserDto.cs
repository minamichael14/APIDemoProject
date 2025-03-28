using APIDay1.Models;

namespace APIDay1.DTO
{
    public class UserDto
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int Age { get; set; }
        public int RoleID { get; set; }
    }

    public static class UserDTOExtension
    {
        public static User ToModel(this UserDto userDTO)
        {
            return new User
            {
                Age = userDTO.Age,
                RoleID = userDTO.RoleID,
                UserName = userDTO.UserName,
                email = userDTO.email,
                password = userDTO.password,
                ID = userDTO.ID
            };
        }
    }
}
