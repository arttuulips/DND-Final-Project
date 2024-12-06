namespace Domain.DTOs
{
    public class UserCreationDto
    {
        public string UserName { get; set; }
        public string Password { get; set;}
        public string Email { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public int SecurityLevel { get; set; }

        public UserCreationDto(string userName, string password, string email, string name, string role, int securityLevel)
        {
            UserName = userName;
            Password = password;
            Email = email;
            Name = name;
            Role = role;
            SecurityLevel = securityLevel;
        }
    }
}