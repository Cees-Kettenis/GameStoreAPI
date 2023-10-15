using System.ComponentModel.DataAnnotations;

namespace GameStoreAPi.Modals.User
{
    public class LoginModal
    {
        [Required(ErrorMessage = "UserId is required")]
        public string UserName { get;set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
