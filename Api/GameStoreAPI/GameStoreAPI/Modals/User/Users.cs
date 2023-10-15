using Microsoft.AspNetCore.Identity;

namespace GameStoreAPi.Modals.User
{
    public class Users : IdentityUser
    {
        public string firstname { get; set; } = string.Empty;
        public string lastname { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
    }
}
