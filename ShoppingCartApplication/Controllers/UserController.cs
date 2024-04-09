using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartApplication.CustomExceptions;
using ShoppingCartApplication.Data;
using ShoppingCartApplication.Models;
using ShoppingCartApplication.Models.DTO;
using ShoppingCartApplication.Repositories;
using ShoppingCartApplication.Service;
using ShoppingCartApplication.ServiceContract;
using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace ShoppingCartApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDTO userDetails)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await userService.RegisterUser(userDetails);
                    return Ok($"{userDetails.Name} has been Registered");
                }
                else return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginDTO userLoginDetails)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (await userService.AuthenticateUser(userLoginDetails))
                        return Ok("User is loggedIn.");
                    else return Unauthorized("Incorrect email or password.");
                }
                else
                    return BadRequest("Something went wrong. Please try again!!");
            }
            catch (ItemNotFoundException ex)
            {
                return StatusCode((int)HttpStatusCode.NotFound, ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode((int)HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (BadHttpRequestException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("deleteuser")]
        public async Task<IActionResult> DeleteUser(UserLoginDTO userLoginDetail)
        {
            try
            {
                await userService.DeleteUser(userLoginDetail);
                return Ok("UserDeleted");
            }
            catch (ItemNotFoundException ex)
            {
                return StatusCode((int)HttpStatusCode.NotFound, ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode((int)HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (BadHttpRequestException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}
