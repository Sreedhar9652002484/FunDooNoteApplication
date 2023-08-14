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
        public UserEntity Login(UserLoginModel model);

    }
}
