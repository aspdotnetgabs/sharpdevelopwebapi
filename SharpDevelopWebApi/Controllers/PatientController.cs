using System;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using SharpDevelopWebApi.Models;

namespace SharpDevelopWebApi.Controllers
{
	/// <summary>
	/// Description of PatientController.
	/// </summary>
	public class PatientController : ApiController
	{
		SDWebApiDbContext _db = new SDWebApiDbContext();
		
		[HttpGet]
		public IHttpActionResult GetAll(){
			var p = _db.Patients.ToList();
			if(p != null)
				return Ok(p);
			else
				return BadRequest("Patients Not Found");		
		}
		
		[HttpGet]
		[Route("api/patient/{id}")]
		public IHttpActionResult getIDpatient(int id) {
			var pat = _db.Patients.Find(id);
			if(pat != null)
				return Ok(pat);
			else
				return BadRequest("Not Found");
		}
		
		[HttpGet]
		[Route("api/patient/byuserid/{id}")]
		public IHttpActionResult getPatientByUserId(int id) {
			var pat = _db.Patients.Where(x=>x.UserId == id).FirstOrDefault();
			if(pat != null)
				return Ok(pat);
			else
				return BadRequest("Not Found");
		}		
		
		[HttpPost]
		public IHttpActionResult PatientRegister(Patient p){
			_db.Patients.Add(p);
			_db.SaveChanges();
			return Ok(p);
		}
			
		[HttpPut]
		public IHttpActionResult UpdatePatient(Patient p){
			var patient = _db.Patients.Find(p.Id);
			if(patient != null)
			{
				patient.LastName = p.LastName;
				patient.FirstName = p.FirstName;
				patient.Address = p.Address;
				patient.Phone = p.Phone;
				_db.Entry(patient).State = System.Data.Entity.EntityState.Modified;
				_db.SaveChanges();
				return Ok("Patient Successfully");
			}
			else
				return BadRequest("Student not Found");
		}
		
		[HttpDelete]
		public IHttpActionResult DeletePatient(int Id){
			var p = _db.Patients.Find(Id);
			if(p != null)
			{
				_db.Patients.Remove(p);
				_db.SaveChanges();
				return Ok("Delete Successfully");
			}
			else
				return BadRequest("Delete Unsuccessfully");
			       
		}
	}
}