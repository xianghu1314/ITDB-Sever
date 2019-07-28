using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ITDB.Models;
using System.Linq;
using ITDB.Tool;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ITDB.Models.Custom;

namespace ITDB.Controllers
{
    [Route("api/[controller]")]
    public class UserController : BaseController
    {

        public UserController(DBContext context):base(context){
        }
        
        [HttpGet("userinfo")]
        [Authorize]
        public IActionResult GetUserInfo()
        {
            var UserID=CurrentUserID;
            var item = _context.Users.FirstOrDefault(t => t.ID == UserID);
            return new ObjectResult(FormatResult.Success(item));
        }
        [HttpGet("token")]
        public async Task<IActionResult> Token(UserLoginRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await _context.Users.FirstOrDefaultAsync(t => t.UserPhone == model.UserPhone && t.UserPwd == model.UserPwd.ToMD5());

            if (user == null)
            {
                return NotFound();
            }

            var token = GetJwtSecurityToken(user);

            return new ObjectResult(
                FormatResult.Success(
                    new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    }));
        }

        ///注册
        [HttpPost]
        public IActionResult Sign([FromBody] User item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            if (string.IsNullOrEmpty(item.UserPhone))
            {
                return new ObjectResult(FormatResult.Failure("手机号不能为空"));
            }
            if (string.IsNullOrEmpty(item.UserPwd))
            {
                return new ObjectResult(FormatResult.Failure("密码不能为空"));
            }
            if (_context.Users.FirstOrDefault(t => t.UserPhone == item.UserPhone) != null)
            {
                return new ObjectResult(FormatResult.Failure("手机号已被占用"));
            }
            item.UserBalance = 0;
            item.Status = true;
            item.UserLogo = "https://s1.mi.com/m/images/m/default.png";
            item.UserName = item.UserPhone.Substring(0,2)+"****"+ item.UserPhone.Substring(7,10);
            string pwd = item.UserPwd;
            item.UserPwd = pwd.ToMD5();

            _context.Users.Add(item);
            _context.SaveChanges();

            return new ObjectResult(item);
        }




        private JwtSecurityToken GetJwtSecurityToken(User user)
        {
            string UnixEpochDate = Math.Round((DateTime.Now.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds).ToString();
            //声明
            var userClaims = new Claim[]
            {
             new Claim(JwtRegisteredClaimNames.Sub,user.ID.ToString()),
             new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
             new Claim(JwtRegisteredClaimNames.Iat,UnixEpochDate,ClaimValueTypes.Integer64),
             //new Claim(ClaimTypes.Role,role),
             new Claim(ClaimTypes.Name,user.ID.ToString())
            };
            return new JwtSecurityToken(
                issuer: "issuer",
                audience: "audience",
                claims: userClaims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("kj7ufh574rkj7ufh574rkj7ufh574rkj7ufh574rkj7ufh574rkj7ufh574rkj7ufh574rkj7ufh574r")), SecurityAlgorithms.HmacSha256)
            );
        }

    }

}