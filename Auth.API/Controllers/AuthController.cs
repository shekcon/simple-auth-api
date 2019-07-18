using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Auth.API.Services;
using Auth.API.Resources;
using Auth.API.Validation;

namespace Auth.API.Controllers
{
    ///<summary>
    /// Operations available to users to login, signup and validate user
    ///</summary>
    [Consumes( "application/json" )]
    [Produces("application/json")]
    [ValidateModel]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        private IMapper _mapper;

        ///<summary>
        /// Setup services
        ///</summary>
        public AuthController(
            IAuthService authService,
            IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }
        ///<summary>
        /// Authenticate user and return jwt
        ///</summary>
        /// <returns>A newly created token </returns>
        /// <response code="200">Returns the newly created token</response>
        /// <response code="400">Username or password is incorrect</response>
        [AllowAnonymous]
        [HttpPost("Login")]
        
        public IActionResult LogIn([FromBody]LoginResource paramBody)
        {
            var user = _authService.Authenticate(paramBody.username, paramBody.password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(new
            {
                Message = "Authenticate successfully!",
                Username = user.username,
                Token = _authService.CreateToken(user)
            });

        }

        ///<summary>
        /// Adds a new user
        ///</summary>
        /// <returns>A newly created user </returns>
        /// <response code="200">Returns username of user</response>
        /// <response code="400">Missing required or invaild field</response>
        /// <response code="409">Username is already taken </response>
        [AllowAnonymous]
        [Route("Register")]
        [HttpPost]
        public RedirectToActionResult Register([FromBody]RegisterResource paramBody)
        {
            return RedirectToActionPreserveMethod("CreateUser", "Users");
        }
        ///<summary>
        /// Gets user by id
        ///</summary>
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult AuthorizeSuccess()
        {
            return Ok(new
            {
                message = "Validate token successfully!",
            });
        }

        ///<summary>
        /// Authorizes user by token
        ///</summary>
        /// <returns>A authorized successfully message</returns>
        /// <response code="200">Returns username of user</response>
        /// <response code="401">Token is invalid or missed</response>
        /// <response code="403">Permission denied</response>
        [Authorize]
        [HttpGet]
        [Route("user")]
        public IActionResult ValidateUser()
        {
            return AuthorizeSuccess();
        }

        ///<summary>
        /// Authorizes admin by token
        ///</summary>
        /// <returns>A authorized successfully message</returns>
        /// <response code="200">Returns username of admin</response>
        /// <response code="401">Token is invalid or missed</response>
        /// <response code="403">Permission denied</response>
        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("admin")]
        public IActionResult ValidateAdmin()
        {
            return AuthorizeSuccess();
        }

    }
}
