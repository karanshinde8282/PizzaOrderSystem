using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using PizzaOrderSystemWebAPI.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PizzaServiceWebAPI.DB;
using PizzaOrderSystemWebAPI.Model;

namespace PizzaOrderSystemWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        public DBClass _DBInstance;

        public LoginController(IConfiguration config)
        {
            try
            {
                _config = config;
                _DBInstance = DBClass.GetInstance();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(LogType.Exception, ex);
            }
        }

        [HttpGet]
        public IActionResult TestApi()
        {
            return Ok("Test Done..!");
        }

        [HttpPost]
        public IActionResult Login([FromBody] UserAuth userParam)
        {
            try
            {
                if (userParam == null || string.IsNullOrEmpty(userParam.Username) || string.IsNullOrEmpty(userParam.Password))
                {
                    return Unauthorized("Your are not authorized user to access this site: IDENTITY NOT FOUND");
                }
                IActionResult response = Unauthorized();
                UserDetailsModel userDetails = AuthenticateUser(userParam);
                if (userDetails != null)
                {
                    var tokenString = GenerateJSONWebToken(userDetails);
                    response = Ok(new { token = tokenString });
                }
                return response;
            }
            catch (UserDefinedException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(LogType.Exception, ex);
                return BadRequest(ex);
            }
        }

        private UserDetailsModel AuthenticateUser(UserAuth userParam)
        {

            UserDetailsModel UserDetailsData = null;
            try
            {
                UserDetailsData = _DBInstance.UserDetailsModelData.Find(x =>
                x.UserName == userParam.Username &&
                x.Password == userParam.Password);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(LogType.Exception, ex);
                throw new UserDefinedException(ex.Message);
            }
            return UserDetailsData;
        }

        [NonAction]
        private string GenerateJSONWebToken(UserDetailsModel userInfo)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

                var claims = new[] {
                                 new Claim(JwtRegisteredClaimNames.Sid, userInfo.User_Id.ToString()),
                                 new Claim(JwtRegisteredClaimNames.Email, userInfo.Email_Id),
                                 new Claim("User_Id", userInfo.User_Id.ToString()),
                                 new Claim("First_Name", userInfo.First_Name),
                                 new Claim("Last_Name", userInfo.Last_Name),
                                 new Claim("Username", userInfo.UserName),
                                 new Claim("Email_Id", userInfo.Email_Id),
                                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                                 };
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    SigningCredentials = credentials,
                    Expires = DateTime.Now.AddHours(12),
                    Issuer = _config["Jwt:Issuer"],
                    Audience = _config["Jwt:Issuer"]
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(LogType.Exception, ex);
                throw new UserDefinedException(ex.Message);
            }
        }
    }
}
