using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bikeshop.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration configuration;

        // Won't be used outside of this controller, so it can be
        // scoped to this namespace. Hence, the definition of the class
        // inside the class.
        public class AuthenticationRequestBody
        {
            public string? UserName { get; set; }
            public string? Password { get; set; }
        }

        private class BikeshopAdmin
        {
            public Guid UserId { get; set; }
            public string UserName { get; set; }

            public BikeshopAdmin (Guid userId, string userName)
            {
                UserId = userId;
                UserName = userName;
            }
        }

        public AuthenticationController(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Get an authorisation token.
        /// </summary>
        /// <param name="authenticationRequestBody">The login data to be verified.</param>
        /// <response code="200">On successful login data validation a Json Web Token is returned.</response>
        /// <response code="401">Is returned when the login data is missing or can't be validated.</response>
        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Authenticate(AuthenticationRequestBody authenticationRequestBody)
        {
            // Step 1: Validate username and password
            var user = ValidateUserCredentials(
                authenticationRequestBody.UserName,
                authenticationRequestBody.Password);

            if (user == null)
                return Unauthorized();

            // Step 2: Create a token
            var securityKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(configuration["Authentication:SecretForKey"]));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", user.UserId.ToString()));

            var jwtSecurityToken = new JwtSecurityToken(
                configuration["Authentication:Issuer"],
                configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(6),
                signingCredentials
                );

            var tokenToReturn = new JwtSecurityTokenHandler()
                .WriteToken(jwtSecurityToken);

            return Ok(tokenToReturn);
        }

        private BikeshopAdmin? ValidateUserCredentials(string? userName, string? password)
        {
            if (userName == null || password == null) 
                return null;

            if (userName == "Admin" && password == "Password")
            {
                return new BikeshopAdmin(
                    Guid.NewGuid(),
                    "Admin"
                    );
            }

            return null;
        }
    }
}
