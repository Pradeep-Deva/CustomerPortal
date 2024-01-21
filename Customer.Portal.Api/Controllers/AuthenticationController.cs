using AutoMapper;
using Customer.Portal.Api.ViewModels;
using Customer.Portal.Application.Contracts;
using Customer.Portal.DataAccess.Read.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Customer.Portal.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        #region PRIVATE VARIABLES
        private string SecretKey;
        #endregion

        #region CONSTRUCTOR
        public AuthenticationController(IConfiguration configuration)
        {
            SecretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }
        #endregion

        #region PUBLIC METHODS
        [HttpPost]
        [Route("GetAuthenticationToken")]  
        public IActionResult GetRecentOrders(String UserId)
        {
            try
            {
                //Generate JWT Token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(SecretKey);
                var toketDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, UserId.ToString()),
                        new Claim(ClaimTypes.Role, "Admin"),
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

                };

                var token = tokenHandler.CreateToken(toketDescriptor);

                return Ok(tokenHandler.WriteToken(token)); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }

        }
        #endregion
    }
}
