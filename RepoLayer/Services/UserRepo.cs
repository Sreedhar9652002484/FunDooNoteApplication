using CommonLayer.Model;
using RepoLayer.Context;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using RepoLayer.Interface;


namespace RepoLayer.Services
{
    public class UserRepo:IUserRepo
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
    }
}
