using System;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using SharpDevelopWebApi.Models;

namespace SharpDevelopWebApi.Controllers
{
	public class StudentController : ApiController
	{
		SDWebApiDbContext _db = new SDWebApiDbContext();
		
		[HttpGet]
		public IHttpActionResult GetAll()
		{		
			var students = _db.Students.ToList();
			return Ok(students);
		}
		
		[HttpPost]
		public IHttpActionResult Create(Student stud)
		{
			_db.Students.Add(stud);
			_db.SaveChanges();
			return Ok(stud);
		}
		
		[HttpPut]
		public IHttpActionResult Update(Student updatedStud)
		{
			var stud = _db.Students.Find(updatedStud.Id);
			if(stud != null)
			{
				stud.LastName = updatedStud.LastName;
				stud.FirstName = updatedStud.FirstName;
				stud.Course = updatedStud.Course;
				_db.Entry(stud).State = System.Data.Entity.EntityState.Modified;
				_db.SaveChanges();
				return Ok(stud);
			}
			else
				return BadRequest("Student not found");
		}
		
		[HttpDelete]
		public IHttpActionResult Delete(int Id)			
		{
			var student = _db.Students.Find(Id);
			if(student != null)
			{
				_db.Students.Remove(student);
				_db.SaveChanges();
				return Ok("Delete successfully");
			}
			else
				return BadRequest("Student not found");
		}
		
	}
}