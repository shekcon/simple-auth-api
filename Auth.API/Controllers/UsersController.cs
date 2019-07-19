using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Auth.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Auth.API.Services;
using Auth.API.Resources;
using Auth.API.Models;
using Auth.API.Validation;
using Auth.API.Security;
using Auth.API.Responses;

namespace Auth.API.Controllers
{
    [Consumes( "application/json" )]
    [Produces("application/json")]
    [ValidateModel]
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private IMapper _mapper;

        public UsersController(
            IUserService userService,
            IMapper mapper
        )
        {
            _userService = userService;
            _mapper = mapper;
        }

        ///<summary>
        /// Adds a new user
        ///</summary>
        /// <returns>A newly created user </returns>
        /// <response code="200">Returns username of user</response>
        /// <response code="400">Missing required or invaild field</response>
        /// <response code="409">Username is already taken </response>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateUser([FromBody]RegisterResource register)
        {
            var user = _mapper.Map<User>(register);

            try
            {
                _userService.Create(user, register.password);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        ///<summary>
        /// Getall user only access by admin
        ///</summary>
        /// <returns>A array of user </returns>
        /// <response code="200">Returns array</response>
        /// <response code="401">Token is invalid or missed</response>
        /// <response code="403">Permission denied</response>
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            var userDtos = _mapper.Map<IList<UserResponse>>(users);
            return Ok(userDtos);
        }

        ///<summary>
        /// Get information of user
        ///</summary>
        /// <returns>A information of user</returns>
        /// <response code="200">Returns object user</response>
        /// <response code="401">Token is invalid or missed</response>
        /// <response code="403">Permission denied</response>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if(!AuthorizeUser.isMatchID(User, id) && !AuthorizeUser.isMatchRole(User, "admin")){
                return Forbid();
            }
            var user = _userService.GetById(id);
            var userDto = _mapper.Map<UserResponse>(user);
            return Ok(userDto);
        }

        ///<summary>
        /// Update information of user
        ///</summary>
        /// <returns>A updated successfully message</returns>
        /// <response code="200">Returns message</response>
        /// <response code="401">Token is invalid or missed</response>
        /// <response code="403">Permission denied</response>
        [HttpPatch("{id}")]
        public IActionResult Update(int id, [FromBody]UserResource userDto)
        {
            if(!AuthorizeUser.isMatchID(User, id)){
                return Forbid();
            }
            try
            {
                _userService.Update(id, userDto);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        ///<summary>
        /// Delete a user
        ///</summary>
        /// <returns>A deleted successfully message</returns>
        /// <response code="200">Returns message</response>
        /// <response code="401">Token is invalid or missed</response>
        /// <response code="403">Permission denied</response>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if(!AuthorizeUser.isMatchID(User, id)){
                return Forbid();
            }
            _userService.Delete(id);
            return Ok();
        }
    }
}