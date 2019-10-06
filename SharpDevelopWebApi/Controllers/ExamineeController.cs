using System;
using System.Web.Http;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using SharpDevelopWebApi.Models;

namespace SharpDevelopWebApi.Controllers
{
	public class ExamineeController : ApiController
	{
		SDWebApiDbContext _db = new SDWebApiDbContext();
		
		[HttpGet]
        public IHttpActionResult GetAll(string keyword = "")
        {
            keyword = keyword.Trim();
            var examinee = new List<Examinee>();
            if(!string.IsNullOrEmpty(keyword))
            {
                examinee = _db.Examinees
                    .Where(x => x.LastName.Contains(keyword) || x.FirstName.Contains(keyword))
                    .ToList();
            }
            else
            	examinee = _db.Examinees.ToList();

            return Ok(examinee);
        }
        
        [HttpGet]
        [Route("api/examinee/getexamineebyuserid/{UserId}")]
		public IHttpActionResult GetExamineeByUserId(int UserId)			
		{
			var examinee = _db.Examinees.Where(x => x.UserId == UserId).FirstOrDefault();
			if(examinee != null)
			{
				return Ok(examinee);
			}
			else
				return BadRequest("Examinee not found");
		}        
		
		[HttpPost]
		public IHttpActionResult Create(Examinee examinee)
		{
			_db.Examinees.Add(examinee);
			_db.SaveChanges();
			return Ok(examinee);
		}
		
		[HttpPut]
		public IHttpActionResult Update(Examinee updatedExaminee)
		{
			var examinee = _db.Examinees.Find(updatedExaminee.Id);
			if(examinee != null)
			{
				examinee.LastName = updatedExaminee.LastName;
				examinee.FirstName = updatedExaminee.FirstName;
				examinee.Gender = updatedExaminee.Gender;
				examinee.Email = updatedExaminee.Email;
				
				_db.Entry(examinee).State = System.Data.Entity.EntityState.Modified;
				_db.SaveChanges();
				return Ok(examinee);
			}
			else
				return BadRequest("Examinee not found");
		}
		
		[HttpDelete]
		public IHttpActionResult Delete(int Id)			
		{
			var examinee = _db.Examinees.Find(Id);
			if(examinee != null)
			{
				_db.Examinees.Remove(examinee);
				_db.SaveChanges();
				return Ok("Delete successfully");
			}
			else
				return BadRequest("Student not found");
		}
		
	}
}