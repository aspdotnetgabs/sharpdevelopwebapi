using System;
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
		
		// For admin
		[HttpGet]
		[Route("api/leave/GetAll")]
		public IHttpActionResult GetAllLeaves(string status = "pending")
		{							
			var leaves = _db.Leaves.ToList();
			
			if(!string.IsNullOrEmpty(status))
				leaves = _db.Leaves.Where(x => x.Status == status).OrderBy(x => x.FilingDate).ToList();
			
			foreach(var lv in leaves)
			{
				lv.Employee = _db.Employees.Where(x => x.UserId == lv.EmployeeUserId).FirstOrDefault() ?? new Employee();
				lv.Employee.Department = _db.Departments.Find(lv.Employee.DepartmentId.Value) ?? new Department();
				lv.Reason = _db.Reasons.Find(lv.ReasonId) ?? new Reason();
			}
			
			return Ok(leaves);		
		}
		
		// For admin
		[HttpGet]
		[Route("api/leave/GetByStatus/{status}")]
		public IHttpActionResult GetByStatus(string status)
		{							
			var leaves = _db.Leaves.Where(x => x.Status == status).OrderBy(x => x.FilingDate).ToList();	

			foreach(var lv in leaves)
			{
				lv.Employee = _db.Employees.Where(x => x.UserId == lv.EmployeeUserId).FirstOrDefault() ?? new Employee();
				lv.Employee.Department = _db.Departments.Find(lv.Employee.DepartmentId.Value) ?? new Department();
				lv.Reason = _db.Reasons.Find(lv.ReasonId) ?? new Reason();
			}
						
			return Ok(leaves);		
		}		
		
		[HttpGet]
		[Route("api/leave/GetByEmployeeUserId/{userId}")]
		public IHttpActionResult GetByEmpId(int userId)
		{							
			var leaves = _db.Leaves.Where(x => x.EmployeeUserId == userId).OrderBy(x => x.FilingDate).ToList();

			foreach(var lv in leaves)
			{
				lv.Employee = _db.Employees.Where(x => x.UserId == lv.EmployeeUserId).FirstOrDefault() ?? new Employee();
				lv.Employee.Department = _db.Departments.Find(lv.Employee.DepartmentId.Value) ?? new Department();
				lv.Reason = _db.Reasons.Find(lv.ReasonId) ?? new Reason();
			}
			
			return Ok(leaves);		
		}	

		[HttpGet]
		public IHttpActionResult GetLeave(int Id)
		{
			var leave = _db.Leaves.Find(Id);
			if(leave != null)
			{
				leave.Employee = _db.Employees.Where(x => x.UserId == leave.EmployeeUserId).FirstOrDefault() ?? new Employee();
				leave.Employee.Department = _db.Departments.Find(leave.Employee.DepartmentId.Value) ?? new Department();
				leave.Reason = _db.Reasons.Find(leave.ReasonId) ?? new Reason();
				return Ok(leave);
			}
			else
				return BadRequest("Leave not Found!");
		}	
		
		[HttpPost]
		public IHttpActionResult CreateLeaves(Leave leave)
		{
			leave.Status = "pending"; // When applying for leave, should start with status "pending"
			_db.Leaves.Add(leave);
			_db.SaveChanges();
			return Ok(leave.Id);
		}

		// For admin
		[HttpPut]
		[Route("api/leave/setstatus/{leaveId}/{status}")]
		public IHttpActionResult ApprovedLeave(int leaveId, string status)
		{
			var leave = _db.Leaves.Find(leaveId);
			if(leave != null)
			{
				leave.Status = status;
				_db.Entry(leave).State = System.Data.Entity.EntityState.Modified;
				_db.SaveChanges();	
				return Ok("Leave " + leave.Status);
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
				if(leaves.Status == "pending") // Should not be able to update leave when it's already approved or denied
				{
					leaves.FilingDate = updatedLeave.FilingDate;
					leaves.DateFrom = updatedLeave.DateFrom;
					leaves.DateTo = updatedLeave.DateTo;
					leaves.Reason = updatedLeave.Reason;
					leaves.SpecifiedReason = updatedLeave.SpecifiedReason;
					// Employee should not be able to change the leave status and userId			
					
					_db.Entry(leaves).State = System.Data.Entity.EntityState.Modified;
					_db.SaveChanges();				
				}

				return Ok(leaves);
			}
			else
				return BadRequest("Leave not Found!");
		}
		
		// For admin only
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
	}
}