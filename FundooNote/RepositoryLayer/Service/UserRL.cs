using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        private readonly FundooContext fundooContext;

        public UserRL(FundooContext fundooContext )
        {
            this.fundooContext = fundooContext;
        }

        public UserEntity Registration(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = userRegistrationModel.FirstName;
                userEntity.LastName = userRegistrationModel.LastName;
                userEntity.Email = userRegistrationModel.Email;
                userEntity.Password = userRegistrationModel.Password;
                userEntity.CreatedAt= DateTime.Now;

                fundooContext.UserTable.Add(userEntity);
                int result = fundooContext.SaveChanges();

                if (result > 0)
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

        public UserEntity Login(UserLogin userLogin)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.Email = userLogin.Email;
                userEntity.Password = userLogin.Password;

                var result = fundooContext.UserTable.FirstOrDefault(x => (x.Email == userLogin.Email && x.Password == userLogin.Password));

                if (result !=null)
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
