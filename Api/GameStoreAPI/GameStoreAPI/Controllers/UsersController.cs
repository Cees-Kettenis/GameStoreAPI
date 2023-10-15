using GameStoreAPi.Data;
using GameStoreAPi.Modals.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GameStoreAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly AppDBContext _context;
        public UsersController(AppDBContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }
    }
}
