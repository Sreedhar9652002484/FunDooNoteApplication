using CommonLayer.Model;
using CommonLayer.Models;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBusiness
    {
        public UserEntity UserReg(UserRegModel model);
        public string Login(UserLoginModel model);
        public string ForgetPassword(string email, string NewPassword, string ConfirmPassword);

    }
}
