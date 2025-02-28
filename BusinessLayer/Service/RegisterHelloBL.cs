using System;
using ModelLayer.DTO;
using RepositoryLayer.Service;

namespace BusinessLayer.Service
{
    public class RegisterHelloBL
    {
        private readonly RegisterHelloRL _registerHelloRL;

        public RegisterHelloBL(RegisterHelloRL registerHelloRL)
        {
            _registerHelloRL = registerHelloRL;
        }

        public string Registration(LoginDTO newUser)
        {
            bool isRegistered = _registerHelloRL.RegisterUser(newUser);

            return isRegistered ? "User registered successfully" : "Username already exists!";
        }

        public bool LoginUser(LoginDTO loginDTO)
        {
            LoginDTO registeredUser = _registerHelloRL.GetUserByUsername(loginDTO.Username);

            if (registeredUser != null && registeredUser.Credentials.Password == loginDTO.Credentials.Password)
            {
                return true; // Successful login
            }

            return false; // Invalid credentials
        }
    }
}
