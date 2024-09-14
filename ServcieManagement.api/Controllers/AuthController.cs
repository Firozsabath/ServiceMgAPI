using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using ServiceManagement.Domain.Entities;
using ServiceManagement.Domain.Interfaces;
using ServiceManagement.EFCore;
using ServiceManagement.WebAPI.DTOs;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ServiceManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ApplicationDBContext db;
        private readonly ITechniciansRepository _techniciansop;

        public AuthController(UserManager<ApplicationUser> userManager, IConfiguration configuration, 
            RoleManager<ApplicationRole> roleManager, ApplicationDBContext db, ITechniciansRepository techniciansRepository)
        {
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
            this.db = db;
            _techniciansop = techniciansRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
           
            var users = _userManager.Users.Select(c => new UserDetailsDTO()
            {
                id = c.Id,
                Username = c.UserName,
                Email = c.Email,
                Role = string.Join(",", _userManager.GetRolesAsync(c).Result)
            }).ToList();

            return Ok(users);
        }

        [HttpGet("UserRoles")]

        public async Task<IActionResult> GetUserRoles()
        {
            return Ok(_roleManager.Roles.ToList());
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] ResgisterDTO model)
        {           
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                var role = new ApplicationRole ();
                role.Name = "Admin";
                await _roleManager.CreateAsync(role);
            }
            if (!await _roleManager.RoleExistsAsync("Technician"))
            {
                var role = new ApplicationRole();
                role.Name = "Technician";
                await _roleManager.CreateAsync(role);
            }

            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email               
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var rs = await _userManager.AddToRoleAsync(user, model.Role);
                //var technician = new Technicians { Name = model.Username, }
                return Ok();
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var token = await GenerateJsonToken(user);
                //var token = string.Empty;

                return Ok(new { Token = token });
            }

            return Unauthorized();
        }

        [HttpPut]
        public async Task<IActionResult> Update(ResgisterDTO data)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(data.Email);

                var rle = await _userManager.GetRolesAsync(user);
                var oldRole = rle.FirstOrDefault();


                if (oldRole != data.Role)
                {
                    if (!String.IsNullOrEmpty(oldRole))
                    {
                        await _userManager.RemoveFromRoleAsync(user, oldRole);
                    }                   
                   await _userManager.AddToRoleAsync(user, data.Role);
                }               
                user.Email = data.Email;
                user.UserName = data.Username;
                var s = await _userManager.UpdateAsync(user);
                this.db.Entry(user).State = EntityState.Modified;

                return Ok(true);
            }
            catch (Exception ex)
            {
                return Ok(false);
                //throw;
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id) {

            var user = await _userManager.FindByIdAsync(id);
            if(user == null)
            {
                return BadRequest("User doesn't exist");
            }

            var re = await _userManager.DeleteAsync(user);
            if (re.Succeeded)
            {
                return Ok(true);
            }
            else
            {
                return Ok(re.Errors);
            }
        }


        private async Task<string> GenerateJsonToken(ApplicationUser user)
        {
            var technician = _techniciansop.Get(x => x.Email == user.Email);
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, technician.ID.ToString()),
                new Claim(ClaimTypes.NameIdentifier,user.Id)                
            };
            var Roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(Roles.Select(r => new Claim(ClaimsIdentity.DefaultRoleClaimType, r)));

            var token = new JwtSecurityToken(_configuration["JWT:ValidIssuer"]
                , _configuration["JWT:ValidAudience"]
                , claims
                , null
                , expires: DateTime.Now.AddHours(8)
                , signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
