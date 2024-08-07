using System.ComponentModel.DataAnnotations;

namespace BakeryOps.API.Security
{
    public class Permission (string name)
    {
        [Key]
        public string Name { get; set; } = name;
    }
}
