using GameStoreAPi.Modals.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GameStoreAPi.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<Users> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        public AuthService(UserManager<Users> usersManager, RoleManager<IdentityRole> roleManger, IConfiguration configuration) { 
            this.userManager = usersManager;
            this.roleManager = roleManger;
            this._configuration = configuration;
        }
        public async Task<(int, string)> Registration(Users modal, string role)
        {
            var DoesUserExists = await userManager.FindByNameAsync(modal.UserName);
            if(DoesUserExists != null)
            {
                return (0, "User already exists");
            }
            
            Users NewUser = new Users();
            NewUser.UserName = modal.UserName;
            NewUser.firstname = modal.firstname;
            NewUser.lastname = modal.lastname;
            NewUser.Email = modal.Email;
            NewUser.PhoneNumber = modal.PhoneNumber;

            var CreatedUser = await userManager.CreateAsync(NewUser, modal.password);
            if (!CreatedUser.Succeeded)
            {
                return (0, "Unable to create user, please check the details and try again!");
            }

            if(!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }

            if(!await roleManager.RoleExistsAsync(role))
            {
                await userManager.AddToRoleAsync(NewUser, role);
            }

            return (1, "User created successfully!");
        }

        public async Task<(int, string)> Login(LoginModal modal)
        {
            var User = await userManager.FindByNameAsync(modal.UserName);
            if (User == null)
            {
                return (0, "Invalid username or password");
            }
            if(!await userManager.CheckPasswordAsync(User, modal.Password))
            {
                return (0, "Invalid username or password");
            }
            var userRoles = await userManager.GetRolesAsync(User);

            var ClaimList = new List<Claim>
            {
                new Claim(ClaimTypes.Name, User.firstname),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach(var role in userRoles)
            {
                ClaimList.Add(new Claim(ClaimTypes.Role, role));
            }

            string Token = GenerateJwTToken(ClaimList);
            return (1, Token);

        }

        private string GenerateJwTToken(IEnumerable<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTKey:Secret"]));
            var TokenDesciptor = new SecurityTokenDescriptor();

            TokenDesciptor.Issuer = _configuration["JWTKey:ValidIsuer"];
            TokenDesciptor.Audience = _configuration["JWTKey:ValidAudience"];
            TokenDesciptor.Expires = DateTime.UtcNow.AddHours(int.Parse(_configuration["JWTKey:TokenExpiryTimeInHours"]));
            TokenDesciptor.SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256);
            TokenDesciptor.Subject = new ClaimsIdentity(claims);

            var tokenHandeler = new JwtSecurityTokenHandler();
            var token = tokenHandeler.CreateToken(TokenDesciptor);

            return tokenHandeler.WriteToken(token);
        }
    }
}
