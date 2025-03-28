namespace APIDay1.Models
{
    public class User : BaseModel
    {
        public string UserName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int Age { get; set; }

        public int RoleID { get; set; }
        public Role Role { get; set; }
    }
}
