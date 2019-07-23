using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using SharpDevelopWebApi;
using SharpDevelopWebApi.Helpers.JWT;

namespace SharpDevelopWebApi.Controllers
{
	/// <summary>
	/// Description of ValuesController.
	/// </summary>
	public class AccountController : ApiController
	{
        // THis is naive endpoint for demo, it should use Basic authentication to provide token or POST request
        [AllowAnonymous]
        [HttpPost]
        [Route("TOKEN")]
        public IHttpActionResult GetToken(string email, string password)
        {
            if (UserAccount.Authenticate(email, password))
            {
                var data = new { token = JwtManager.GenerateToken(email) };
                return Ok(data);
            }

            return BadRequest("Invalid login");
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/account/login")]
        public IHttpActionResult Login(string email, string password)
        {
            var success = UserAccount.Authenticate(email, password);
            if (success)
            {
                HttpContext.Current.Session.Add("currentUser", email);
                return Ok(new { code = 1, message = "Successful login" });
            }
            else
                return BadRequest("Invalid login");
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/account/logout")]
        public IHttpActionResult Logout()
        {
            HttpContext.Current.Session.Clear();
            return Ok(new { code = 1, message = "Account logout" });
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/account/register")]
        public IHttpActionResult Register(string email, string password)
        {
            var userId = UserAccount.Create(email, password);
            if (userId != null)
                return Ok(new { userId = userId, message = "Account successfully created" });
            else
                return BadRequest("Account registration failed");
        }
    }



}