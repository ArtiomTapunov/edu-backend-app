using System;

namespace ECA.DomainObjects
{
    public class User
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime? LastLoggedIn { get; set; }
        public DateTime DateCreated { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
    }
}
