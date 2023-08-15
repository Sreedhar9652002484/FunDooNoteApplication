using BusinessLayer.Interface;
using CommonLayer.Model;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoLayer.Interface;
using System.Security.Claims;

namespace FunDooNoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserBusiness _userBusiness;

        public UserController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }
        [Route("UserRegistration")]

        [HttpPost]
        public IActionResult UserReg(UserRegModel model)
        {
            var result=_userBusiness.UserReg(model);
            if(result != null)
            {
                return this.Ok(new{ Success = true, Message="User Registration Succesfull",Data=result });
            }
            else
            {
                return this.BadRequest(new { success = false, Message = "User Registration UnSuccessful", Data = result });

            }
        }
        [Route("Login")]

        [HttpPost]
        public IActionResult UserLogin(UserLoginModel model)
        {
            var result = _userBusiness.Login(model);
            if (result != null)
            {
                return this.Ok(new { Success = true, Message = "User Login Succesfull", data=result});
            }
            else
            {
                return this.BadRequest(new { success = false, Message = "Invalid Credentials" });

            }
        }
        [Route("ForgetPassword")]

        [HttpPost]
        public IActionResult forgetPass(ForgetPassWordModel model)
        {
            var result = _userBusiness.ForgetPassword(model);
            if (result != null)
            {
                return this.Ok(new { Success = true, Message = "Token sent Succesfully" });
            }
            else
            {
                return this.NotFound(new { success = false, Message = "Invalid Email" });

            }

        }

        [Authorize]
        [Route("ResetPassword")]

        [HttpPut]
        public IActionResult ResetPassword(string NewPassword, string ConfirmPassword)
        {
            var email=User.FindFirst(ClaimTypes.Email).Value;
            if (email != null)
            {
                var result = _userBusiness.ResetPassword(email, NewPassword, ConfirmPassword);
                if (result == true)
                {
                    return this.Ok(new { Success = true, Message = "Password Reset Succesfull" });
                }
                else
                {
                    return this.NotFound(new { success = false, Message = "Invalid Credentials" });

                }
            }
            return null;

        }
    }
}
