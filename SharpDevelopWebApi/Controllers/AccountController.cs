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
        public IHttpActionResult GetToken(string username, string password)
        {
            if (UserAccount.Authenticate(username, password))
            {     
            	var user = UserAccount.GetUserByUserName(username);
                var data = new 
                { 
                	userId = user.Id,
                	userName = user.UserName,
                	userRoles = user.Roles.Split(','),
                	token = JwtManager.GenerateToken(username)
                };
                return Ok(data);
            }

            return BadRequest("Login failed");
        }

        [HttpGet]
        [Route("api/account/login")]
        public IHttpActionResult Login(string username, string password)
        {
            var success = UserAccount.Authenticate(username, password);
            if (success)
            {
                HttpContext.Current.Session.Add("currentUser", username);
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
        public IHttpActionResult RegisterUser(string username, string password, string role = "") // You can add more parameter here ex LastName, FirstName etc
        {
        	if(role.Split(',').Contains(UserAccount.DEFAULT_ADMIN_LOGIN))
        		return BadRequest("Creating an admin account is forbidden.");
        	
            var user = UserAccount.GetUserByUserName(username);
            if(user != null)
                return BadRequest("Account already exists");

            var userId = UserAccount.Create(username, password, role);
            if (userId != null)
            {
            	// Link User Account to Entities e.g. Student, Employee, Customer
            	if(role == "doctor")
            	{
            		var doctor = new Doctor();
            		doctor.UserId = userId.Value;            		
            		_db.Doctors.Add(doctor);
            		_db.SaveChanges();
            	}
            	else if(role == "patient")
            	{
            		var p = new Patient();
            		p.UserId = userId.Value;
            		_db.Patients.Add(p);
            		_db.SaveChanges();
            	}
            	// Feel free to remove the ABOVE code if not needed.
                
            	return Ok(new { UserId = userId, Message = "Account successfully created" });
            }
            else
                return BadRequest("Account registration failed");
        }
        
        [ApiAuthorize]
        [HttpPost]
        [Route("api/account/registerbyadmin")]
        public IHttpActionResult RegisterWithRole(string username, string password, string comma_separated_roles = "")
        {
        	var userRoles = UserAccount.GetUserRoles(User.Identity.Name);
        	var isAdmin = userRoles.Contains(UserAccount.DEFAULT_ADMIN_LOGIN);
        	if(!isAdmin)
        		return BadRequest("Access forbidden. For administrator only.");
        	
            var user = UserAccount.GetUserByUserName(username);
            if(user != null)
            	return BadRequest("Account already registered!");

            var userId = UserAccount.Create(username, password, comma_separated_roles);
            if (userId != null)
                return Ok(new { UserId = userId, Message = "Account successfully created" });
            else
                return BadRequest("Account registration failed");
        }        

        [HttpPost]
        [Route("api/account/changepassword")]
        public IHttpActionResult ChangePassword(string username, string newPassword, string currentPassword = "")
        {
            var currentUser = !string.IsNullOrEmpty(User.Identity.Name) ? User.Identity.Name : (string)HttpContext.Current.Session["currentUser"];
            var forceChangeIfAdmin = false;

            try 
            {
             	forceChangeIfAdmin = Array.IndexOf(UserAccount.GetUserRoles(currentUser), "admin") > -1;           	
            } 
            catch {}

            var success = UserAccount.ChangePassword(username, currentPassword, newPassword, forceChangeIfAdmin);
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