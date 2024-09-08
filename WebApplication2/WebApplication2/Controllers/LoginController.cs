using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication2.Entity;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly FreshKit _dbcontext;
        private readonly IConfiguration configuration;
        public LoginController(FreshKit freshKit, IConfiguration configuration)
        {
            _dbcontext = freshKit;
            this.configuration = configuration;
        }
        [HttpPost("Registration")]

        public IActionResult Registration([FromBody] User user)
        {
            _dbcontext.Users.Add(user);
            bool Result=  _dbcontext.SaveChanges()>0;

            return Ok(Result);
        }
        [Authorize]
        [HttpPost("DeleteUser")]

        public IActionResult DeleteUser(int UserID) {
           var user= _dbcontext.Users.Find(UserID);
            if(user!=null)
            {
                _dbcontext.Users.Remove(user);
               bool Result= _dbcontext.SaveChanges()>0;
                return Ok(Result);

            }
            return NotFound();
        }
        [Authorize]
        [HttpPost("GetUserDetails")]

        public IActionResult GetUserDetails(int UserID)
        {
            var user = _dbcontext.Users.Find(UserID);
            if (user != null)
            {
              
                return Ok(user);

            }
            return NotFound();
        }
        [Authorize]

        [HttpPost("UpdateUser")]
        public IActionResult UpdateUser([FromBody] User user) {
        
            _dbcontext.Users.Update(user);
            bool Result= _dbcontext.SaveChanges()>0;
            return Ok(Result);
        }
        [Authorize]

        [HttpPost("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            var user = _dbcontext.Users.ToList();
            if (user == null)
            {
                return NoContent();
            }
            return Ok(user);

        }
        [HttpPost("Login")]
        public IActionResult Login(string Email, string Password)
        {
         var user=   _dbcontext.Users.Where(x=>x.Email.Trim().Equals(Email.Trim()) && x.Password.Equals(Password.Trim())).FirstOrDefault();
            if (user == null)
            {
                return NotFound("Wrong Email Or Password");
            }
            return Ok(GetJwtToken(user.UserId, user.Name, user.Email));
            
        }
        [NonAction]
        private string GetJwtToken(int id, string name, string email)
        {
            var issuer = configuration.GetSection("Jwt:Issuer").Value;
            var audience = configuration.GetSection("Jwt:Audience").Value;
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("Jwt:key").Value);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim("Id",id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, name),
                new Claim(JwtRegisteredClaimNames.Email, email),
                //new Claim("Repotation", Repotation.ToString())

               }),// policy claim 
                Expires = DateTime.UtcNow.AddDays(12),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }

    }
}
