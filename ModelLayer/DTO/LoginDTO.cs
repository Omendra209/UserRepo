using System;

namespace ModelLayer.DTO
{
    public class LoginDTO
    {
        public string Username { get; set; }
        public UserCredentials Credentials { get; set; }

        public override string ToString()
        {
            return Username + ":" + Credentials.Password;
        }
    }

    public class UserCredentials
    {
        public string Password { get; set; }
    }
}
