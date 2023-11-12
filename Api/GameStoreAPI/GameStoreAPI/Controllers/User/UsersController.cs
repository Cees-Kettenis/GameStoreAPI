using GameStoreAPi.Data;
using GameStoreAPi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameStoreAPi.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : BaseController
    {
        [HttpGet]
        public IActionResult Get()
        {
            AppDBContext dbService = (AppDBContext)HttpContext.RequestServices.GetService(typeof(AppDBContext));
            var users = dbService.Users.ToList();
            return Ok(users);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetUserById(String Id)
        {
            AppDBContext dbService = (AppDBContext)HttpContext.RequestServices.GetService(typeof(AppDBContext));
            var user = dbService.Users.Where(x => x.Id == Id).FirstOrDefault();
            return Ok(user);
        }

        [HttpPut]
        [Route("resetpassword/{id}/{newpassword}")]
        public async Task<IActionResult> UpdatePassword(String Id, String NewPassword)
        {
            var authService = (IAuthService)HttpContext.RequestServices.GetService(typeof(IAuthService));
            var (status, message) = await authService.ResetPassword(Id, NewPassword);
            if(status == 0)
            {
                return BadRequest(message);
            }
            return Ok(message);
        }
    }
}
