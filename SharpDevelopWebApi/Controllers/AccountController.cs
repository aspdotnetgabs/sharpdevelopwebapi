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

            return BadRequest("Login failed");
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
                return Ok(new { code = 1, message = "Login successful" });
            }
            else
                return BadRequest("Login failed");
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/account/logout")]
        public IHttpActionResult Logout()
        {
            HttpContext.Current.Session.Clear();
            return Ok(new { code = 1, message = "Logout successful" });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/account/register")]
        public IHttpActionResult Register(string email, string password)
        {
            var userId = UserAccount.Create(email, password);
            if (userId != null)
                return Ok(new { userId = userId, message = "Account successfully created" });
            else
                return BadRequest("Account registration failed");
        }

        [ApiAuthorize]
        [HttpPost]
        [Route("api/account/changepassword")]
        public IHttpActionResult ChangePassword(string email, string newPassword, string currentPassword = "")
        {
            var currentUser = !string.IsNullOrEmpty(User.Identity.Name) ? User.Identity.Name : (string)HttpContext.Current.Session["currentUser"];
            var isAdmin = Array.IndexOf(UserAccount.GetUserRoles(currentUser), "admin") > -1;

            var success = UserAccount.ChangePassword(email, currentPassword, newPassword, isAdmin);
            if (success)
            {
                HttpContext.Current.Session.Clear();
                return Ok("Password successfully changed");
            }
            else
                return BadRequest("Password change failed");
        }
    }



}