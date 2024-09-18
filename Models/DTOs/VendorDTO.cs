namespace BakeryOps.API.Models.DTOs
{
    public class VendorDTO
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Description { get; set; }

    }
}
