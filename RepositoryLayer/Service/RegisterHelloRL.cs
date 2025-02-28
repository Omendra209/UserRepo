using System.Collections.Generic;
using System.Linq;
using ModelLayer.DTO;

namespace RepositoryLayer.Service
{
    public class RegisterHelloRL
    {
        // Use a static Dictionary for persistent storage
        private static readonly Dictionary<string, UserCredentials> _users = new();

        public string GetHello(string name)
        {
            return name + " Hello from Repository Layer ";
        }

        public bool RegisterUser(LoginDTO newUser)
        {
            if (_users.ContainsKey(newUser.Username))
            {
                return false; // Username already exists
            }

            _users[newUser.Username] = newUser.Credentials; // Store username and password securely
            return true;
        }

        public LoginDTO GetUserByUsername(string username)
        {
            if (_users.TryGetValue(username, out UserCredentials credentials))
            {
                return new LoginDTO
                {
                    Username = username,
                    Credentials = credentials
                };
            }
            return null; // User not found
        }
    }
}
