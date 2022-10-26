using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration iconfiguration;
        public UserRL(FundooContext fundooContext ,IConfiguration iconfiguration)
        {
            this.fundooContext = fundooContext;
            this.iconfiguration = iconfiguration;
        }

        public UserEntity Registration(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = userRegistrationModel.FirstName;
                userEntity.LastName = userRegistrationModel.LastName;
                userEntity.Email = userRegistrationModel.Email;
                //userEntity.Password = userRegistrationModel.Password;
                userEntity.Password = EncodePasswordToBase64(userRegistrationModel.Password);
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

        public string Login(UserLogin userLogin)
        {
            try
            {

                //var LoginResult = fundooContext.UserTable.FirstOrDefault(x => (x.Email == userLogin.Email && x.Password == userLogin.Password));
                var LoginResult = fundooContext.UserTable.Where(user => user.Email == userLogin.Email && user.Password == EncodePasswordToBase64(userLogin.Password)).FirstOrDefault();

                if (LoginResult != null)
                {
                    var token = GenerateSecurityToken(LoginResult.Email, LoginResult.UserId);
                    return token;
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


        public string GenerateSecurityToken(string email, long userID)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(iconfiguration[("Jwt:Key")]);
            //string role = "ranjan@gmail.com";

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    //new Claim(ClaimTypes.Role, role),
                    new Claim("userID",userID.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }

        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
        //this function Convert to Decord your Password
        public string DecodeFrom64(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }

        public string ForgetPassword(string Email)
        {
            try
            {
                var emailCheck = fundooContext.UserTable.Where(x => x.Email == Email).FirstOrDefault();

                if (emailCheck != null)
                {
                    var Token = GenerateSecurityToken(emailCheck.Email, emailCheck.UserId);
                    MSMQmodel mSMQModel = new MSMQmodel();
                    mSMQModel.sendData2Queue(Token);
                    return Token.ToString();
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
        public bool ResetLink(string email, string password, string confirmPassword)
        {
            try
            {
                if (password.Equals(confirmPassword))
                {
                    var emailCheck = fundooContext.UserTable.Where(r => r.Email == email).FirstOrDefault();
                    emailCheck.Password = EncodePasswordToBase64(confirmPassword);

                    fundooContext.SaveChanges();
                    return true;
                }
                
                    return false;
                
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
