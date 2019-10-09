using System;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using SharpDevelopWebApi.Models;

namespace SharpDevelopWebApi.Controllers
{
	/// <summary>
	/// Description of DoctorController.
	/// </summary>
	public class DoctorController : ApiController
	{
		SDWebApiDbContext _db = new SDWebApiDbContext();
		
		[HttpGet]
		public IHttpActionResult GetAll(){
			var doc = _db.Doctors.ToList();
			if(doc != null)
				return Ok(doc);
			else
				return BadRequest("Patients Not Found");
		}
		
		[HttpGet]
		[Route("api/doctor/{id}")]
		public IHttpActionResult getIDdoctor(int id){
			var doc = _db.Doctors.Find(id);
			if(doc != null)
				return Ok(doc);
			else
				return BadRequest("Not Found");
		}
		
		[HttpGet]
		[Route("api/doctor/byuserid/{id}")]
		public IHttpActionResult getDoctorByUserId(int id){
			var doc = _db.Doctors.Where(x=>x.UserId == id).FirstOrDefault();
			if(doc != null)
				return Ok(doc);
			else
				return BadRequest("Not Found");
		}		
		
		[HttpPost]
		public IHttpActionResult CreateDoctor(Doctor doc){
			_db.Doctors.Add(doc);
			_db.SaveChanges();
			return Ok("Successfully");
		}	
		
		[HttpPut]
		public IHttpActionResult UpdateDoctor(Doctor doc){
			var doctor = _db.Doctors.Find(doc.Id);
			if(doctor != null)
			{
				doctor.FirstName = doc.FirstName;
				doctor.LastName = doc.LastName;
				doctor.Address = doc.Address;
				doctor.Phone = doc.Phone;
				doctor.Specialization = doc.Specialization;
				_db.Entry(doctor).State = System.Data.Entity.EntityState.Modified;
				_db.SaveChanges();
				return Ok(doctor);
			}
			else
				return BadRequest("Doctor not Found");
		}
		
		[HttpDelete]
		[Route("api/doctor/{id}")]
		public IHttpActionResult DeleteDoctor(int id){
			var d = _db.Doctors.Find(id);
			if(d != null)
			{
				_db.Doctors.Remove(d);
				_db.SaveChanges();
				return Ok("Delete Successfully");
			}
			else
				return BadRequest("Delete Unsuccessfully");
			       
		}
	}
}