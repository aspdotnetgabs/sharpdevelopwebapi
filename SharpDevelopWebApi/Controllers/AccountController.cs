using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using SharpDevelopWebApi;
using SharpDevelopWebApi.Helpers.JWT;
using SharpDevelopWebApi.Models;

namespace SharpDevelopWebApi.Controllers
{
    public class AccountController : ApiController
	{
    	SDWebApiDbContext _db = new SDWebApiDbContext();
    	
        [HttpPost]
        [Route("TOKEN")]
        public IHttpActionResult GetToken(string email, string password)
        {
            if (UserAccount.Authenticate(email, password))
            {          	
                var data = new { Token = JwtManager.GenerateToken(email) };
                return Ok(data);
            }

            return BadRequest("Login failed");
        }

        [HttpGet]
        [Route("api/account/login")]
        public IHttpActionResult Login(string email, string password)
        {
            var success = UserAccount.Authenticate(email, password);
            if (success)
            {
                HttpContext.Current.Session.Add("currentUser", email);
                return Ok("Login successful");
            }
            else
                return BadRequest("Login failed");
        }

        [HttpGet]
        [Route("api/account/logout")]
        public IHttpActionResult Logout()
        {
            HttpContext.Current.Session.Clear();
            return Ok("Logout successful");
        }

        [HttpPost]
        [Route("api/account/register")]
        public IHttpActionResult RegisterUser(string email, string password, string userType) // You can add more parameter here ex LastName, FirstName etc
        {
        	if(userType.Split(',').Contains(UserAccount.DEFAULT_ADMIN_LOGIN))
        		return BadRequest("Creating an admin account is forbidden.");
        	
            var user = UserAccount.GetUserByEmail(email);
            if(user != null)
                return BadRequest("Account already exists");

            var userId = UserAccount.Create(email, password, userType);
            if (userId != null)
            {
            	if(userType == "doctor")
            	{
            		var doctor = new Doctor();
            		doctor.Email = email;            		
            		_db.Doctors.Add(doctor);
            		_db.SaveChanges();
            	}
            	else if(userType == "patient")
            	{
            		var p = new Patient();
            		p.Email = email;
            		_db.Patients.Add(p);
            		_db.SaveChanges();
            	}
                
            	return Ok(new { UserId = userId, Message = "Account successfully created" });
            }
            else
                return BadRequest("Account registration failed");
        }
        
        [ApiAuthorize]
        [HttpPost]
        [Route("api/account/registerbyadmin")]
        public IHttpActionResult RegisterWithRole(string email, string password, string comma_separated_roles)
        {
        	var userRoles = UserAccount.GetUserRoles(User.Identity.Name);
        	var isAdmin = userRoles.Contains(UserAccount.DEFAULT_ADMIN_LOGIN);
        	if(!isAdmin)
        		return BadRequest("Access forbidden. For administrator only.");
        	
            var user = UserAccount.GetUserByEmail(email);
            if(user != null)
                return BadRequest("Account already exists");

            var userId = UserAccount.Create(email, password, comma_separated_roles);
            if (userId != null)
                return Ok(new { UserId = userId, Message = "Account successfully created" });
            else
                return BadRequest("Account registration failed");
        }        

        [HttpPost]
        [Route("api/account/changepassword")]
        public IHttpActionResult ChangePassword(string email, string newPassword, string currentPassword)
        {
            var currentUser = !string.IsNullOrEmpty(User.Identity.Name) ? User.Identity.Name : (string)HttpContext.Current.Session["currentUser"];
            var forceChangeIfAdmin = false;

            try 
            {
             	forceChangeIfAdmin = Array.IndexOf(UserAccount.GetUserRoles(currentUser), "admin") > -1;           	
            } 
            catch {}

            var success = UserAccount.ChangePassword(email, currentPassword, newPassword, forceChangeIfAdmin);
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