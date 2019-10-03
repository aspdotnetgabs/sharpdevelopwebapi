using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using SharpDevelopWebApi.Models;

namespace SharpDevelopWebApi.Controllers
{
	/// <summary>
	/// Description of LeaveController.
	/// </summary>
	public class LeaveController : ApiController
	{
		SDWebApiDbContext _db = new SDWebApiDbContext();
		
		[HttpGet]
		public IHttpActionResult GetAllLeaves(int? employeeId = null, int? statusId = null)
		{
			List<Leave> leaves;
			if(employeeId == null)
				leaves = _db.Leaves.ToList();
			else
				leaves = _db.Leaves.Where(x => x.EmployeeId == employeeId).ToList();
			
			if(statusId != null)
				leaves = leaves.Where(x=>x.StatusId == statusId).ToList();
												
			return Ok(leaves);		
		}
		
		[HttpGet]
		public IHttpActionResult GetLeaves(int Id)
		{
			var leaves = _db.Leaves.Find(Id);
			if(leaves != null)
				return Ok(leaves);
			else
				return BadRequest("Leave not Found!");											
		}		
		
		[HttpPost]
		public IHttpActionResult CreateLeaves(Leave leave)
		{
			_db.Leaves.Add(leave);
			_db.SaveChanges();
			return Ok(leave);
		}
		
		[HttpDelete]
		public IHttpActionResult DeleteLeave(int Id)
		{
			var leaves = _db.Leaves.Find(Id);
			if(leaves != null)
			{
				_db.Leaves.Remove(leaves);
				_db.SaveChanges();
				return Ok("Leave Deleted Successfully!");
			}
			else
				return BadRequest("Leave not Found!");
		}
		
		[HttpPut]
		public IHttpActionResult UpdateLeave(Leave updatedLeave)
		{
			var leaves = _db.Leaves.Find(updatedLeave.Id);
			if(leaves != null)
			{
				// leaves.Employee = updatedLeave.Employee; // No need to assign value to NotMapped properties
				leaves.EmployeeId = updatedLeave.EmployeeId;
				leaves.FilingDate = updatedLeave.FilingDate;
				leaves.DateFrom = updatedLeave.DateFrom;
				leaves.DateTo = updatedLeave.DateTo;
				leaves.ReasonId = updatedLeave.ReasonId;
				leaves.SpecifiedReason = updatedLeave.SpecifiedReason;
				leaves.StatusId = updatedLeave.StatusId;
				
				
				_db.Entry(leaves).State = System.Data.Entity.EntityState.Modified;
				_db.SaveChanges();
				return Ok(leaves);
			}
			else
				return BadRequest("Leave not Found!");
		}
	}
}