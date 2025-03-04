using System.Collections.Generic;
using System.Linq;
using ModelLayer.DTO;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Context;
using NLog;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Service
{
    public class RegisterUserRL
    {
        private readonly UserRegistrationAppContext _context;
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public RegisterUserRL(UserRegistrationAppContext context)
        {
            _context = context;
        }
        public async Task<bool> RegisterUser(UserEntity user)
        {
            try
            {
                if(await _context.Users.AnyAsync(u=> u.Email == user.Email))
                {
                    Log.Warn("Registration Failed: Email {0} already exists for user with Name: {1}",user.Email,user.FirstName);
                    return false;
                }
                _context.Users.Add(user);
                _context.SaveChanges();
                Log.Info("User with Email: {0} registered successfully",user.Email);
                return true;

                //var existingUser = _context.Users.FirstOrDefault<UserEntity>(e=> e.Email == user.Email);
                    
                
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in Registration user");
                throw;
            }
        }
        public async Task<UserEntity> GetUserByEmail(string email)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in GetUserByName");
                throw;
            }
        }
    }
}
