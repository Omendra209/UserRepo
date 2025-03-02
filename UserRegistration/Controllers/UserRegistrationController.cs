using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Service;
using ModelLayer.DTO;
using NLog;
using RepositoryLayer.Entity;

namespace UserRegistration.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserRegistrationAppController : ControllerBase
    {
        private readonly RegisterUserBL _registerHelloBL;
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public UserRegistrationAppController(RegisterUserBL registerHelloBL)
        {
            _registerHelloBL = registerHelloBL;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserEntity user)
        {
            try
            {
                var result = await _registerHelloBL.Registration(user);
                return Ok(new ResponseModel<string> { Success = true,Message = result});
            }
            catch (Exception ex)
            {
                Log.Error(ex,"Error in Register");
                return StatusCode(500,new ResponseModel<string> { Success = false,Message="Internal Server Error"});
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserEntity user)
        {
            try
            {
                bool isAuthenticated = await _registerHelloBL.LoginUser(user.Email,user.Password);
                if (isAuthenticated)
                {
                    return Ok(new ResponseModel<string> { Success=true,Message=user.Name+" Login Successfully.",Data=user.Name});
                }
                return Unauthorized(new ResponseModel<string> { Success=false,Message="Invalid Credentials"});
            }
            catch (Exception ex)
            {
                Log.Error(ex,"Error in LoginUser");
                return StatusCode(500, new ResponseModel<string> { Success = false, Message = "Internal Server Error" });
            }
        }
    }
}
