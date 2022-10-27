using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace FundooNote.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
   
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        
        [HttpPost("Register")]
        public IActionResult RegisterUser(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                var result = userBL.Registration(userRegistrationModel);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Registration Successful", response = result }) ;
                }
                else
                {
                    return BadRequest(new { success = false, message = "Registration Unsuccessful" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }



        [HttpPost("Login")]
        public IActionResult LoginUser(UserLogin userLogin)
        {
            try
            {
                var result = userBL.Login(userLogin);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Login Successful",Token=result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Login Unsuccessful" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("ForgetPassword")]
        public IActionResult ForgetPassword(string Email)
        {
            try
            {
                var result = userBL.ForgetPassword(Email);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Email Sent Successfully" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Reset Email Not Sent" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }



        [Authorize]
        [HttpPut("ResetLink")]
        public IActionResult ResetLink(string password, string confirmPassword)
        {
            try
            {
                var Email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                //var email = User.Claims.First(e => e.Type == "Email").Value;
                //var result = userBL.ResetLink(email, password, confirmPassword);

                if (userBL.ResetLink(Email, password, confirmPassword))
                {
                    return Ok(new { success = true, message = "Password Reset Successfully" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Password Reset Unsuccessful" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }

}

