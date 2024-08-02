using Microsoft.AspNetCore.Identity;

namespace Orders.Models
{
    public class User: IdentityUser
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string HashedPassword { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<IdentityRole> Roles { get; set; }= new List<IdentityRole>();

    }
}
