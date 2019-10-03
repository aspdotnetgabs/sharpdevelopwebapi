using System;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using SharpDevelopWebApi.Models;

namespace SharpDevelopWebApi.Controllers
{
	/// <summary>
	/// Description of StatusController.
	/// </summary>
	public class StatusController : ApiController
	{
		SDWebApiDbContext _db = new SDWebApiDbContext();
		
		[HttpGet]
		public IHttpActionResult GetAllStatus()
		{
			
			var s = _db.Status.ToList();
			return Ok(s);
		
		}
		
		[HttpPost]
		public IHttpActionResult CreateStatus(Status s)
		{
			_db.Status.Add(s);
			_db.SaveChanges();
			return Ok(s);
		}
		
		[HttpDelete]
		public IHttpActionResult DeleteStatus(int Id)
		{
			var s = _db.Status.Find(Id);
			if(s != null)
			{
				_db.Status.Remove(s);
				_db.SaveChanges();
				return Ok("Status Deleted Successfully!");
			}
			else
				return BadRequest("Status not Found!");
		}
		
		[HttpPut]
		public IHttpActionResult UpdateStatus(Status updatedS)
		{
			var s = _db.Status.Find(updatedS.Id);
			if(s != null)
			{
				s.Name = updatedS.Name;
		
				_db.Entry(s).State = System.Data.Entity.EntityState.Modified;
				_db.SaveChanges();
				return Ok(s);
			}
			else
				return BadRequest("Status not Found!");
		}
	}
}