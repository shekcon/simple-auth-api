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
        private readonly AppSettings _appSettings;

        public UsersController(
            IUserService userService,
            IMapper mapper
        )
        {
            _userService = userService;
            _mapper = mapper;
        }


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

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            var userDtos = _mapper.Map<IList<UserResponse>>(users);
            return Ok(userDtos);
        }

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