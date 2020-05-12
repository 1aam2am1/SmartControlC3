using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Api.Queries;
using Api.Responses;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace Server.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> Get(string returnUrl = null)
        {
            string authHeader = HttpContext.Request.Headers["Authorization"];
            if (authHeader != null && authHeader.StartsWith("basic", StringComparison.OrdinalIgnoreCase))
            {
                var token = authHeader.Substring("Basic ".Length).Trim();

                var credentialstring = Encoding.UTF8.GetString(Convert.FromBase64String(token));

                int seperatorIndex = credentialstring.IndexOf(':');

                var username = credentialstring.Substring(0, seperatorIndex);
                var password = credentialstring.Substring(seperatorIndex + 1);
                var query = new AuthQuery { User = username, Password = password };

                var result = await Authenticate(query);
                if (Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);
                else
                    return result;
            }
            else
            {
                HttpContext.Response.StatusCode = 401;
                HttpContext.Response.Headers["WWW-Authenticate"] = "Basic";
                return new EmptyResult();
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]AuthQuery query, string returnUrl = null)
        {
            if(query != null)
            {
                var result = await Authenticate(query);
                if (Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);
                else
                    return result;
            }
            HttpContext.Response.StatusCode = 401;
            HttpContext.Response.Headers["WWW-Authenticate"] = "Basic";
            return new EmptyResult();
        }

        private async Task<ActionResult> Authenticate(AuthQuery query)
        {
            var passwd = new PasswdResponse { User=query.User};

            _logger.LogInformation("User: {0} logged", query.User);

            if (/*new UserManager().IsValid(username, password)*/ true)
            {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.NameIdentifier, query.User),

                    new Claim(ClaimTypes.Name, query.User),

                    // optionally you could add roles if any
                    //new Claim(ClaimTypes.Role, "RoleName"),
                    //new Claim(ClaimTypes.Role, "AnotherRole"),
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                passwd.Authenticated = true;


                return Ok(passwd);
            }
            // invalid username or password
            ModelState.AddModelError("", "invalid username or password");

            passwd.Authenticated = false;
            return new JsonResult(passwd) { StatusCode = 401 };
        }
    }
}
