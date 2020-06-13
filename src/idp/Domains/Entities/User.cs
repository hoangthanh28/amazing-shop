using System;

namespace IdentityServer.Domain.Entity
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsMustChangePassword { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}