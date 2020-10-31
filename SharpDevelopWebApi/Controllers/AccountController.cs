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
            	var userRoles = user.Roles.Split(',');
                var data = new 
                { 
                	userId = user.Id,
                	userName = user.UserName,
                	userRoles = userRoles,
                	token = JWTAuth.TokenManager.createToken(username, userRoles)
                };
                return Ok(data);
            }

            return BadRequest("Login failed");
        }

        [HttpPost]
        [Route("api/account/register")]
        public IHttpActionResult RegisterUser(string username, string password, string role = "") // You can add more parameter here ex LastName, FirstName etc
        {

        	foreach (var r in role.Split(',').Where(r => !string.IsNullOrWhiteSpace(r)))
        	{
        		if(r.Trim().ToLower() == UserAccount.DEFAULT_ADMIN_LOGIN.ToLower())
        			return BadRequest("Creating an admin account is forbidden.");
        	}
        	
            var user = UserAccount.GetUserByUserName(username);
            if(user != null)
                return BadRequest("Account already exists");

            var userId = UserAccount.Create(username, password, role);
            if (userId != null)
            {
            	// Link User Account to Entities e.g. Student, Employee, Customer
//            	if(role == "doctor")
//            	{
//            		var doctor = new Doctor();
//            		doctor.UserId = userId.Value;            		
//            		_db.Doctors.Add(doctor);
//            		_db.SaveChanges();
//            	}
//            	else if(role == "patient")
//            	{
//            		var p = new Patient();
//            		p.UserId = userId.Value;
//            		_db.Patients.Add(p);
//            		_db.SaveChanges();
//            	}
            	// Feel free to remove the ABOVE code if not needed.
                
            	return Ok(new { UserId = userId, Message = "Account successfully created" });
            }
            else
                return BadRequest("Account registration failed");
        }
        
        [Authorize(Roles="admin")]
        [HttpPost]
        [Route("api/account/registerbyadmin")]
        public IHttpActionResult RegisterWithRole(string username, string password, string comma_separated_roles = "")
        {
        	// var userRoles = UserAccount.GetUserRoles(User.Identity.Name);
        	// var isAdmin = userRoles.Contains(UserAccount.DEFAULT_ADMIN_LOGIN);
        	// if(!isAdmin)
        		// return BadRequest("Access forbidden. For administrator only.");
        	
            var user = UserAccount.GetUserByUserName(username);
            if(user != null)
            	return BadRequest("Account already registered!");

            var userId = UserAccount.Create(username, password, comma_separated_roles);
            if (userId != null)
                return Ok(new { UserId = userId, Message = "Account successfully created" });
            else
                return BadRequest("Account registration failed");
        }        

        [HttpPut]
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
        
        [Authorize]
        [HttpGet]
        [Route("api/account/me")]
        public IHttpActionResult GetUserProfile()
        {
        	return Ok(UserAccount.GetCurrentUser());
        }
        
    }



}