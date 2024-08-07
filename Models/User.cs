using System.ComponentModel.DataAnnotations;
using BakeryOps.API.Security;
using Microsoft.AspNetCore.Identity;

namespace BakeryOps.API.Models
{
    public class User
    {
        
        public Guid Id { get; set; }
        [MaxLength(32, ErrorMessage = "Username must be 32 characters or less")]
        
        public string UserName { get; set; }
        public string HashedPassword { get; set; }
        [MaxLength(32, ErrorMessage = "First name must be 32 characters or less")]
        public string? FirstName { get; set; }
        [MaxLength(32, ErrorMessage = "Last name must be 32 characters or less")]
        public string? LastName { get; set; }

        public ICollection<Permission> Permissions { get; set; } = new List<Permission>();

    }
}
