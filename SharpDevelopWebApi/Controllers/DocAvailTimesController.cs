using System;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using SharpDevelopWebApi.Models;

namespace SharpDevelopWebApi.Controllers
{
	/// <summary>
	/// Description of DocAvailTimesController.
	/// </summary>
	public class DocAvailTimesController : ApiController
	{
		SDWebApiDbContext _db = new SDWebApiDbContext();
			
		[HttpGet]
		[Route("api/docavailtimes/bydoctoruserid/{id}")]
		public IHttpActionResult GetAllByDoctorUserId(int id){
			var doc = _db.DocAvailTimes.Where(x => x.DoctorUserId == id).ToList();
			
			foreach (var ap in doc) 
			{
				ap.Doctor = _db.Doctors.Where(x=>x.UserId == ap.DoctorUserId).First() ?? new Doctor();				
			}
			return Ok(doc);
		}
				
		
		[HttpGet]
		[Route("api/docavailtimes/{id}")]
		public IHttpActionResult getIDavailTimes(int id){
			var avil = _db.DocAvailTimes.Find(id);
			if(avil != null)
				return Ok(avil);
			else
				return BadRequest("Not Found");
		}
		
		[HttpPost]
		public IHttpActionResult CreateDocAvailTimes(DoctorAvailableTimes doc){
			_db.DocAvailTimes.Add(doc);
				_db.SaveChanges();
				return Ok("Success");
		}
			
		[HttpPut]
		public IHttpActionResult UpdateDocAvailTimes(DoctorAvailableTimes doc){
			var doctor = _db.DocAvailTimes.Find(doc.Id);
			if(doctor != null){
				doctor.Date = doc.Date;
				doctor.StartTime = doc.StartTime;
				doctor.EndTime = doc.EndTime;
				doctor.Doctor = doc.Doctor;
				_db.Entry(doctor).State = System.Data.Entity.EntityState.Modified;
				_db.SaveChanges();
				return Ok("Successfully");
			}
			else 
				return BadRequest("Unsuccessfull");
		}
		
		[HttpDelete]
		public IHttpActionResult DeleteDocAvailTimes(int id){
			var d = _db.DocAvailTimes.Find(id);
			if(d != null)
			{
				_db.DocAvailTimes.Remove(d);
				_db.SaveChanges();
				return Ok("Delete Successfully");
			}
			else
				return BadRequest("Delete Unsuccessfully");
			       
		}
	}
}
