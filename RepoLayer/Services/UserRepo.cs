using CommonLayer.Model;
using CommonLayer.Models;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepoLayer.Services
{
    public class UserRepo: IUserRepo
    {
        private readonly FunDoContext funDoContext;

        public UserRepo(FunDoContext funDoContext)
        {
            this.funDoContext = funDoContext;
        }
        public UserEntity UserReg(UserRegModel model)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = model.FirstName;
                userEntity.LastName = model.LastName;
                userEntity.DateOfBirth = model.DateOfBirth;
                userEntity.Email = model.Email;
                userEntity.Password = model.Password;

                funDoContext.User.Add(userEntity);
                funDoContext.SaveChanges();
                if (userEntity != null)
                {
                    return userEntity;
                }
                else
                {
                    return null;
                }
               
            }
            
            catch (Exception)
            {

                throw;
            }
        }
        public UserEntity Login(UserLoginModel model)
        {
            try
            {
                var user=funDoContext.User.FirstOrDefault(u=>u.Email == model.Email && u.Password==model.Password);
                if(user != null)
                {

                    return user;
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
