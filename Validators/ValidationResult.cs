namespace Orders.API.Validators
{
    public class ValidationResult
    {
        public bool IsValid { get; set; } = false;
        public List<string> Errors { get; set; } = new List<string>();
    }
}
