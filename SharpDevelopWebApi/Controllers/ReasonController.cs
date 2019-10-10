using System;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using SharpDevelopWebApi.Models;


namespace SharpDevelopWebApi.Controllers
{
	/// <summary>
	/// Description of ReasonController.
	/// </summary>
	public class ReasonController : ApiController
	{
		SDWebApiDbContext _db = new SDWebApiDbContext();
		
		[HttpGet]
		public IHttpActionResult GetAllReasons()
		{			
			var reason = _db.Reasons.ToList();
			return Ok(reason);		
		}
		
		[HttpPost]
		public IHttpActionResult CreateReasons(Reason r)
		{
			_db.Reasons.Add(r);
			_db.SaveChanges();
			return Ok(r);
		}
		
		[HttpDelete]
		public IHttpActionResult DeleteReason(int Id)
		{
			var r = _db.Reasons.Find(Id);
			if(r != null)
			{
				_db.Reasons.Remove(r);
				_db.SaveChanges();
				return Ok("Reason Deleted Successfully!");
			}
			else
				return BadRequest("Reason not Found!");
		}
		
		[HttpPut]
		public IHttpActionResult UpdateReason(Reason updatedR)
		{
			var r = _db.Reasons.Find(updatedR.Id);
			if(r != null)
			{
				r.ReasonText = updatedR.ReasonText;
				r.Description = updatedR.Description;
		
				_db.Entry(r).State = System.Data.Entity.EntityState.Modified;
				_db.SaveChanges();
				return Ok(r);
			}
			else
				return BadRequest("Reason not Found!");
		}
	}
}