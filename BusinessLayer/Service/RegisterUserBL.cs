using System;
using ModelLayer.DTO;
using NLog;
using RepositoryLayer.Entity;
using RepositoryLayer.Service;

namespace BusinessLayer.Service
{
    public class RegisterUserBL
    {
        private readonly RegisterUserRL _registerHelloRL;
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public RegisterUserBL(RegisterUserRL registerHelloRL)
        {
            _registerHelloRL = registerHelloRL;
        }

        public async Task<string> Registration(UserEntity user)
        {
            try
            {
                bool isRegistered = await _registerHelloRL.RegisterUser(user);
                return isRegistered ? "User registered successfully":"User already exist with "+user.Email+" Email!";
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in Repository_RegisterUserBL_Registration");
                throw;
            }
        }

        public async Task<bool> LoginUser(string email , string password)
        {
            try
            {
                var user = await _registerHelloRL.GetUserByEmail(email);
                if(user != null && user.Password ==  password)
                {
                    Log.Info("User {0} logged in successfully.", email);
                    return true;
                }
                Log.Warn("Login failed for {0}: Invalid credentials.");
                return false;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in LoginUser");
                throw;
            }
        }
    }
}
