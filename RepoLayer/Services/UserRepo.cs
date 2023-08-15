using CommonLayer.Model;
using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepoLayer.Services
{
    public class UserRepo: IUserRepo
    {
        private readonly FunDoContext funDoContext;
        private readonly IConfiguration configuration;

        public UserRepo(FunDoContext funDoContext, IConfiguration configuration)
        {
            this.funDoContext = funDoContext;
            this.configuration = configuration;
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
        ///Generate Token JWTMethod
        public string GenerateJwtToken(string Email, long UserId)
        {

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, Email),
                 new Claim("UserId", UserId.ToString()),
                // Add any other claims you want to include in the token
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(configuration["JwtSettings:Issuer"], configuration["JwtSettings:Audience"], claims, DateTime.Now, DateTime.Now.AddHours(1), creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string Login(UserLoginModel model)
        {
            try
            {
                var user=funDoContext.User.FirstOrDefault(u=>u.Email == model.Email && u.Password==model.Password);
                if(user != null)
                {

                    var token=GenerateJwtToken(user.Email, user.UserId);
                    return token;
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
