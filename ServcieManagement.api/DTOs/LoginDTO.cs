namespace ServiceManagement.WebAPI.DTOs
{
    public class LoginDTO
    {
        public string? Username { get; set; }
        public required string Password { get; set; }
    }

    public class UserDetailsDTO
    {
        public string id { get; set; }
        public string? Username { get; set;}
        public string? Email { get; set;}
        public string?  Role { get; set;}
    }
}
