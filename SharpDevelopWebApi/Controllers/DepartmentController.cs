using System;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using SharpDevelopWebApi.Models;



namespace SharpDevelopWebApi.Controllers
{
	/// <summary>
	/// Description of DepartmentController.
	/// </summary>
	public class DepartmentController : ApiController
	{
		SDWebApiDbContext _db = new SDWebApiDbContext();
		
		[HttpGet]
		public IHttpActionResult GetAllDepartments()
		{
			
			var department = _db.Departments.ToList();
			return Ok(department);
		
		}
		
		[HttpPost]
		public IHttpActionResult CreateDepartments(Department dept)
		{
			_db.Departments.Add(dept);
			_db.SaveChanges();
			return Ok(dept);
		}
		
		[HttpDelete]
		public IHttpActionResult DeleteDepartment(int Id)
		{
			var departments = _db.Departments.Find(Id);
			if(departments != null)
			{
				_db.Departments.Remove(departments);
				_db.SaveChanges();
				return Ok("Department Deleted Successfully!");
			}
			else
				return BadRequest("Department not Found!");
		}
		
		[HttpPut]
		public IHttpActionResult UpdateDepartment(Department updatedDept)
		{
			var d = _db.Departments.Find(updatedDept.Id);
			if(d != null)
			{
				d.Name = updatedDept.Name;
		
				_db.Entry(d).State = System.Data.Entity.EntityState.Modified;
				_db.SaveChanges();
				return Ok(d);
			}
			else
				return BadRequest("Department not Found!");
		}
	}
}