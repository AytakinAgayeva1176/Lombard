using System.ComponentModel.DataAnnotations;

namespace Lombard.Domain.ViewModels
{
    public class LoginVm
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
