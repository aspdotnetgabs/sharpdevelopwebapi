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
		public IHttpActionResult GetAllEmployees()
		{			
			var employee = _db.Employees.ToList();
			return Ok(employee);		
		}
		
		[HttpGet]
		public IHttpActionResult GetEmployee(int Id)
		{			
			var employee = _db.Employees.Find(Id);
			if(employee != null)
				return Ok(employee);
			else
				return BadRequest("Employee not Found!");	
		}		
		
		[HttpPost]
		public IHttpActionResult CreateEmployees(Employee em)
		{
			_db.Employees.Add(em);
			_db.SaveChanges();
			return Ok(em);
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
		
		[HttpPut]
		public IHttpActionResult UpdateEmployee(Employee updatedEm)
		{
			var em = _db.Employees.Find(updatedEm.Id);
			if(em != null)
			{
				em.FirstName = updatedEm.FirstName;
				em.LastName = updatedEm.LastName;
				em.Position = updatedEm.Position;
				//em.Department = updatedEm.Department; // No need to assign value to NotMapped properties
				em.DepartmentId = updatedEm.DepartmentId;
				em.Phone = updatedEm.Phone;
				
				
				_db.Entry(em).State = System.Data.Entity.EntityState.Modified;
				_db.SaveChanges();
				return Ok(em);
			}
			else
				return BadRequest("Employee not Found!");
		}
	}
}