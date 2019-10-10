using System;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using SharpDevelopWebApi.Models;

namespace SharpDevelopWebApi.Controllers
{
	/// <summary>
	/// Description of EmployeeController.
	/// </summary>
	public class EmployeeController : ApiController
	{
		SDWebApiDbContext _db = new SDWebApiDbContext();
		
		[HttpGet]
		public IHttpActionResult GetAllEmployees(int? deparmentId = null, string keyword = "")
		{			
			var employees = _db.Employees.ToList();
			
			if(deparmentId != null) // Filter by Department
				employees = employees.Where(x => x.DepartmentId == deparmentId.Value).ToList();
			
//			if(!string.IsNullOrEmpty(keyword))
//				employees = employees.Where(x => x.FirstName.Contains(keyword) ||  x.LastName.Contains(keyword)).ToList();
						
			foreach(var e in employees)
			{
				e.Department = _db.Departments.Find(e.DepartmentId.Value) ?? new Department();		
			}
			
		    return Ok(employees);
		}
		
		[HttpGet]
		public IHttpActionResult GetEmployee(int Id)
		{		
			var employee = _db.Employees.Find(Id);
			
			if(employee !=null)
			{
				employee.Department = _db.Departments.Find(employee.DepartmentId.Value) ?? new Department();
                
				return Ok(employee);
			}				
			else
				return BadRequest("Employee not found");
		}
		
		[HttpGet]
		[Route("api/employee/getbyuserid/{userId}")]
		public IHttpActionResult GetEmployeeByUserId(int userId)
		{		
			var employee = _db.Employees.Where(x=>x.UserId == userId).FirstOrDefault();
			
			if(employee !=null)
			{
				employee.Department = _db.Departments.Find(employee.DepartmentId.Value) ?? new Department();               
				return Ok(employee);
			}				
			else
				return BadRequest("Employee not found");
		}		
		
		[HttpPost]
		public IHttpActionResult CreateEmployee()
		{
			return Ok("To create Employee, use api/account/Register instead. Role should be 'employee'");
		}		
		
		[HttpPut]
		public IHttpActionResult UpdateEmployee(Employee updatedEm)
		{
			var em = _db.Employees.Find(updatedEm.Id);
			if(em != null)
			{
				em.FirstName = updatedEm.FirstName;
				em.LastName = updatedEm.LastName;
				em.Position = updatedEm.Position;			
				em.DepartmentId = updatedEm.DepartmentId.Value;
				em.Phone = updatedEm.Phone;
				
				_db.Entry(em).State = System.Data.Entity.EntityState.Modified;						
				_db.SaveChanges();
				
				em.Department = _db.Departments.Find(updatedEm.DepartmentId) ?? new Department();
				return Ok(em);
			}
			else
				return BadRequest("Employee not Found!");
		}
	
		[HttpDelete]
		public IHttpActionResult DeleteEmployee(int Id)
		{
			var employees = _db.Employees.Find(Id);
			if(employees != null)
			{
				_db.Employees.Remove(employees);
				_db.SaveChanges();
				return Ok("Employee Deleted Successfully!");
			}
			else
				return BadRequest("Employee not Found!");
		}
		
	}
}