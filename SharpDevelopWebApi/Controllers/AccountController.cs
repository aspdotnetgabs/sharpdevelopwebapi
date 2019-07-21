using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using SharpDevelopWebApi;

namespace SharpDevelopWebApi.Controllers
{
	/// <summary>
	/// Description of ValuesController.
	/// </summary>
	public class AccountController : ApiController
	{
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

        [HttpGet]
        [Route("api/account/logout")]
        public IHttpActionResult Logout()
        {
            HttpContext.Current.Session.Clear();
            return Ok(new { code = 1, message = "Account logout" });
        }

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