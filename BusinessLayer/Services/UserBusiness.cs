using BusinessLayer.Interface;
using CommonLayer.Model;
using CommonLayer.Models;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBusiness:IUserBusiness
    {
        private readonly IUserRepo _userRepo;

        public UserBusiness(IUserRepo userRepo)
        {
            this._userRepo = userRepo;
        }
        public UserEntity UserReg(UserRegModel model)
        {
            try
            {
                return _userRepo.UserReg(model);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string Login(UserLoginModel model)
        {
            try
            {
                return _userRepo.Login(model);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string ForgetPassword(ForgetPassWordModel model)
        {
            try
            {
                return  _userRepo.ForgetPassword(model);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool ResetPassword(string email, string NewPassword, string ConfirmPassword)
        {
            try
            {
                return _userRepo.ResetPassword(email, NewPassword, ConfirmPassword);
            }
            catch (Exception)
            {

                throw;
            }
        }




    }
}
