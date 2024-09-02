namespace BakeryOps.API.Models.DTOs
{
    public class NewUserDTO : UserDTO
    {
       
        public required string Password { get; set; }
    }
}
