namespace WebApiForGauge.Models
{
    public class CheckPasswordRequestDTO
    {
        public string PhoneNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
