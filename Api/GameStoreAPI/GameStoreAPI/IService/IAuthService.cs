using GameStoreAPi.Modals.User;

namespace GameStoreAPi.Services
{
    public interface IAuthService
    {
        Task<(int, string)> Registration(Users modal, string role);
        Task<(int, string)> Login(LoginModal modal);
        Task<(int, string)> ResetPassword(String Id, String NewPassWord);
    }
}
