using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ShopsNearByy.Models;
using ShopsNearByy.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace ShopsNearByy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Authentication : ControllerBase
    {
        public static UserModel user = new UserModel();

        private readonly IConfiguration _configuration;
        private readonly IUserServices _userservices;

        public Authentication(IConfiguration configuration , IUserServices userservices)
        {
            _configuration = configuration;
            _userservices = userservices;
        }


        //get the user

        [HttpGet("{id}")]
        public ActionResult<UserModel> Get(int id)
        {
            var userExist = _userservices.Get(id);
            return new EmptyResult();
        }

        //Registration

        [HttpPost("register")]
        public async Task<ActionResult<UserModel>> Register(UserRegist request, UserModel user)
        {
            

            if (user == null || user.Email == request.Email)

            {
                return BadRequest("Check Your Email PLease!");
            }

            else
            {
                CreatePassHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

                _userservices.Create(user);

                return CreatedAtAction(nameof(Get), new { id = user.Id }, user);


                user.Email = request.Email;
                user.Username = request.Username;
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                return Ok(user);
            }    

            
        }

        //Login

        [HttpPost("Login")]

        public async Task<ActionResult<string>> Login(UserRegist request)
        {
            if(user.Username != request.Username)
            {
                return BadRequest("User Not Found.");
            }

            if (!VerifyPassHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong Password.");
            }
            


            string token = CreateToken(user);
            return Ok(token);
        }
        //Jwt creation

        private string CreateToken(UserModel user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(

                claims : claims,
                expires : DateTime.Now.AddDays(1),
                signingCredentials : creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        //Password Hashing

        private void CreatePassHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
        }

        //Password Hashing Verification

        private bool VerifyPassHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
