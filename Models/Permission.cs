using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BakeryOps.API.Models
{
    [PrimaryKey(nameof(Name),nameof(UserId))]
    public class Permission (string name)
    {
     
        public string Name { get; set; } = name;
        public Guid UserId { get; set; }
    }
}
