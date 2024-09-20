namespace BakeryOps.API.Models;

public class Vendor
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Description { get; set; }

    public ICollection<Material> Materials { get; set; } = new List<Material>();

    public bool IsDeleted { get; set; } = false;
    public bool IsActive { get; set; } = true;
}