using GameStoreAPi.Modals.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameStoreAPi.Data
{
    public class AppDBContext : IdentityDbContext<Users>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }
    }
}