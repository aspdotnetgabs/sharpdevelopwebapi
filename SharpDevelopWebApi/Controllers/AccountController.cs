using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Route("api/account/register")]
        public IHttpActionResult RegisterUser(RegisterViewModel newUser) // You can add more parameter here ex LastName, FirstName etc
        {
//        	if(!IsValidEmail(newUser.UserName)) // Requires the username to be email format.
//        	   return BadRequest("Username must be in a valid email format.");
        	
        	if(newUser.Role.Split(',').Contains(UserAccount.DEFAULT_ADMIN_LOGIN))
        		return BadRequest("Creating an admin account is forbidden.");
        	
            var user = UserAccount.GetUserByUserName(newUser.UserName);
            if(user != null)
                return BadRequest("Account already exists");

            var userId = UserAccount.Create(newUser.UserName, newUser.Password, newUser.Role);
            if (userId != null)
            {
            	// Link User Account to Entities e.g. Student, Employee, Customer
            	if(newUser.Role == "employee")
            	{
            		var emp = new Employee();
            		emp.UserId = userId.Value;
            		emp.FirstName = newUser.FirstName;
            		emp.LastName = newUser.LastName;
            		emp.DepartmentId = newUser.DepartmentId;
            		emp.Position = newUser.Position;
            		_db.Employees.Add(emp);
            		_db.SaveChanges();
            	}
            	else  if(newUser.Role == "other role")
            	{
            		// logic for adding other role
            	}
            	// Feel free to remove the ABOVE code if not needed.
                
            	return Ok(new { UserId = userId, Message = "Account successfully created" });
            }
            else
                return BadRequest("Account registration failed");
        }    	
    	
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
                return BadRequest("Account already exists");

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
        
		private bool IsValidEmail(string email)
		{
		    try {
		        var addr = new System.Net.Mail.MailAddress(email);
		        return addr.Address == email;
		    }
		    catch {
		        return false;
		    }
		}        
    }



}