using GameStoreAPi.Modals.User;
using GameStoreAPi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameStoreAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthenticationController> _logger;
        internal class ReturnLoginObject
        {
            public string Token { get; set; }
            public int StatusCode { get; set; }
        }
        public AuthenticationController(IAuthService authService, ILogger<AuthenticationController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModal modal)
        {
            try
            {
                //for some reason modalstate.isvalid is not working for me.
                if (String.IsNullOrEmpty(modal.UserName) || String.IsNullOrEmpty(modal.Password))
                {
                    return BadRequest("Please fill in usernam and password");
                }

                //the service will return 0 for problem 1 for success.
                var (status, message) = await _authService.Login(modal);
                if (status == 0)
                {
                    return BadRequest(message);
                }
                var ReturnObj = new ReturnLoginObject();
                ReturnObj.Token = message;
                ReturnObj.StatusCode = StatusCodes.Status200OK;
                //if the message is success you are getting a jwt token.
                return Ok(ReturnObj);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> register(Users modal)
        {
            try
            {
                //validation because the stupid modalstate isnt found.
                if (String.IsNullOrEmpty(modal.UserName))
                {
                    return BadRequest("Please fill in user name.");
                }
                if (String.IsNullOrEmpty(modal.Email))
                {
                    return BadRequest("Please fill in your email address.");
                }
                if (String.IsNullOrEmpty(modal.password))
                {
                    return BadRequest("Please fill in your passowrd.");
                }
                if (String.IsNullOrEmpty(modal.firstname))
                {
                    return BadRequest("Please fill in your first name.");
                }
                if (String.IsNullOrEmpty(modal.lastname))
                {
                    return BadRequest("Please fill in your last name.");
                }
                if (String.IsNullOrEmpty(modal.PhoneNumber))
                {
                    return BadRequest("Please fill in your phone number");
                }

                //finally create the user
                var (status, message) = await _authService.Registration(modal, UserRoles.Admin);
                if (status == 0)
                {
                    return BadRequest(message);
                }
                return CreatedAtAction(nameof(register), modal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
