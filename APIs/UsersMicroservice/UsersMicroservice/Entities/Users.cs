using Microsoft.Extensions.Configuration.UserSecrets;

namespace UsersMicroservice.Entities
{
    public class Users
    {
        public int UserID {get; set;}
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public int RoleID { get; set; }
    }
}
