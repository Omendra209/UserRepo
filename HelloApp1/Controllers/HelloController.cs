using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Service;
using ModelLayer.DTO;

namespace HelloApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloAppController : ControllerBase
    {
        private readonly RegisterHelloBL _registerHelloBL;

        public HelloAppController(RegisterHelloBL registerHelloBL)
        {
            _registerHelloBL = registerHelloBL;
        }

        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] LoginDTO newUser)
        {
            var response = new ResponseModel<string>();

            try
            {
                string result = _registerHelloBL.Registration(newUser);
                response.Success = true;
                response.Message = result;

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Registration failed";
                response.Data = ex.Message;

                return BadRequest(response);
            }
        }

        [HttpPost("login")]
        public IActionResult LoginUser([FromBody] LoginDTO loginDTO)
        {
            var response = new ResponseModel<string>();

            try
            {
                bool isAuthenticated = _registerHelloBL.LoginUser(loginDTO);

                if (isAuthenticated)
                {
                    response.Success = true;
                    response.Message = "Login Successful";
                    response.Data = loginDTO.Username;
                    return Ok(response);
                }

                response.Success = false;
                response.Message = "Invalid Credentials";
                return Unauthorized(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Login failed";
                response.Data = ex.Message;

                return BadRequest(response);
            }
        }
    }
}
