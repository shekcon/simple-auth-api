using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using Auth.API.Helpers;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Auth.API.Services;
using Auth.API.Resources;
using Auth.API.Models;
using System.Linq;

namespace Auth.API.Controllers
{
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
            var userDtos = _mapper.Map<IList<UserResource>>(users);
            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var userIdentity = (ClaimsIdentity)User.Identity;
            var claims = userIdentity.Claims;
            var roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();
            if(id != Int32.Parse(User.Identity.Name) &&  roles[0].Value == "admin"){
                return Forbid();
            }
            var user = _userService.GetById(id);
            var userDto = _mapper.Map<UserResource>(user);
            return Ok(userDto);
        }

        [HttpPatch("{id}")]
        public IActionResult Update(int id, [FromBody]UserResource userDto)
        {
            if(id != Int32.Parse(User.Identity.Name)){
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
            if(id != Int32.Parse(User.Identity.Name)){
                return Forbid();
            }
            _userService.Delete(id);
            return Ok();
        }
    }
}