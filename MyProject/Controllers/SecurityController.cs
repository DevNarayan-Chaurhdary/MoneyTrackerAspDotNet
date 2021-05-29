using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Controllers
{
    public class SecurityController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult Login([FromBody] loginObj)
        //{
        //    ////CustomerDbContex contex = new CustomerDbContex();
        //    ////List<User> user = (from temp in contex.users
        //    ////                   where temp.userName == obj.userName &&
        //    ////                   temp.password == obj.password
        //    ////                   select temp).ToList();
        //    ////if (user.Count > 0)
        //    ////{
        //    ////    Token t = new Token();
        //    ////    String token = GenerateToken(loginObj.userName);
        //    ////    t.token = token;
        //    ////    return Ok(t);
        //    ////}
        //    ////else
        //    ////{
        //    ////    return StatusCode(StatusCodes.Status401Unauthorized, "Credentials is not proper");
        //    ////}

        //}

        private String GenerateToken(String userName)

        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Devkeieidkdkkdkdkfke"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Issuer","Dev"),
                new Claim("Admin","true"),
                new Claim(JwtRegisteredClaimNames.UniqueName,userName)
            };

            var token = new JwtSecurityToken("Dev",
                "Dev",
                claims,
                expires: DateTime.Now.AddSeconds(40),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
