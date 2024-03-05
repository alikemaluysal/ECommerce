namespace WebMVC.Client.Models.ViewModels
{
    public class LoginViewModel
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? AuthenticatorCode { get; set; }
    }
}