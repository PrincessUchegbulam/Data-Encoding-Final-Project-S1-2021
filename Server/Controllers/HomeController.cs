using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Server.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        // Here we are making tokens by creating a new user its like cookie authentication
        public IActionResult Authenticate()
        {
            //constructing a user
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "some_id"),//sub is the id of the user 
                new Claim("granny", "cookie")//custom claims
            };
            //get secret strings from constant class
            var secretBytes = Encoding.UTF8.GetBytes(Constants.Secret);
            //only one key is used and needs some array of bits which is secret bytes
            var key = new SymmetricSecurityKey(secretBytes);
            //using SHA256 algorithm to hashing the key and for creating the signiture
            var algorithm = SecurityAlgorithms.HmacSha256; 

            //making the signing credentials..
            var signingCredentials = new SigningCredentials(key, algorithm);

            var token = new JwtSecurityToken( // here we supply the claims 
                //we have made a constants class and has taken value form them as it should be private
                Constants.Issuer,
                Constants.Audiance,
                claims,
                notBefore: DateTime.Now, //token start date
                expires: DateTime.Now.AddHours(1), //token expire date
                signingCredentials);

            var tokenJson = new JwtSecurityTokenHandler().WriteToken(token); //printing the token

            return Ok(new { access_token = tokenJson });
        }

        // 

        public IActionResult Decode(string part)
        {
            var bytes = Convert.FromBase64String(part);
            return Ok(Encoding.UTF8.GetString(bytes));
        }
    }
}
