using CommonLayer.Model;
using CommonLayer.Models;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    public interface IUserRepo
    {
        public UserEntity UserReg(UserRegModel model);
        public string Login(UserLoginModel model);
        public string ForgetPassword(ForgetPassWordModel model);
        public bool ResetPassword(string email, string NewPassword, string ConfirmPassword);



    }
}
